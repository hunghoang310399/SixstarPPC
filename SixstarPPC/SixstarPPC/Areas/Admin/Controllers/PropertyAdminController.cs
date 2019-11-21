using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SixstarPPC.Models;

namespace SixstarPPC.Areas.Admin.Controllers
{
    
    public class PropertyAdminController : Controller
    { 
        ppcdbEntities model = new ppcdbEntities();
        // GET: Admin/PropertyAdmin
        public ActionResult Index()
        {
            var porperty = model.Properties.ToList();
            return View(porperty);
        }
        [HttpGet]
        public ActionResult Create()
        {
            PopularData();
         
           
            return View();
           
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "Property_Name,Property_Type_ID,Description,District_ID,Address,Area,Bed_Room,Bath_Room,Price,Installment_Rate,Avatar,Album,Property_Status_ID")]Property property)
        {
           
            if (ModelState.IsValid)
            {
                model.Properties.Add(property);
                model.SaveChanges();
               PopularMessage(true);
            }
            else { PopularMessage(false); }
         
            return RedirectToAction("Index");

        }

        public void PopularMessage(bool success)
        {
            if (success)
                Session["success"] = "SuccessFull!";
            else
            {
                Session["success"] = "Fail!";
            }

        }
        public ActionResult Edit(int? id)
        {
            var porperty = model.Properties.Find(id);
            PopularData(porperty.Property_Type_ID,porperty.District_ID,porperty.Property_Status_ID);
            return View(porperty);
        }

        public void PopularData(object propertyTypeSelected = null, object districtSelected = null, object propertyStatusSelected =null)
        {
            ViewBag.Property_Type_ID = new SelectList(model.Property_Type.ToList(), "ID", "Property_Type_Name",propertyTypeSelected);
            ViewBag.District_ID = new SelectList(model.Districts.ToList(), "ID", "District_Name", districtSelected);
            ViewBag.Property_Status_ID = new SelectList(model.Property_Status.ToList(), "ID", "Property_Status_Name", propertyStatusSelected);
        }

    }
}