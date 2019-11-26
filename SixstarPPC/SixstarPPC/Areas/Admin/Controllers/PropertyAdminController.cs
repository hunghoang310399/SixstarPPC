using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
        public ActionResult Create(Property property)
        {
           
            if (ModelState.IsValid)
            {
                property.Installment_Rate = 0.7;
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
        [HttpGet]
        public ActionResult Edit(int id)
        {
            PopularData();
            var property = model.Properties.FirstOrDefault(x => x.ID == id);
            return View(property);
        }

        [HttpPost]
        public ActionResult Edit(int id, Property p)
        {
            if (id == p.ID)
            {
                var property = model.Properties.FirstOrDefault(x => x.ID == id);
                property.Property_Name = p.Property_Name;
                property.Property_Type_ID = p.Property_Type_ID;
                property.Description = p.Description;
                property.District_ID = p.District_ID;
                property.Address = p.Address;
                property.Area = p.Area;
                property.Avatar = p.Avatar;
                property.Album = p.Album;
                property.Bath_Room = p.Bath_Room;
                property.Bed_Room = p.Bed_Room;
                property.Price = p.Price;
                property.Installment_Rate = p.Installment_Rate;
                property.Property_Status_ID = p.Property_Status_ID;
                model.SaveChanges();
            }
            else { PopularMessage(false); }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var property = model.Properties.FirstOrDefault(x => x.ID == id);
            return View(property);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteComfirm(int id)
        {
            var property = model.Properties.FirstOrDefault(x => x.ID == id);
            model.Properties.Remove(property);
            model.SaveChanges();
            return RedirectToAction("Index");
        }
        public void PopularData(object propertyTypeSelected = null, object districtSelected = null, object propertyStatusSelected =null)
        {
            ViewBag.Property_Type_ID = new SelectList(model.Property_Type.ToList(), "ID", "Property_Type_Name",propertyTypeSelected);
            ViewBag.District_ID = new SelectList(model.Districts.ToList(), "ID", "District_Name", districtSelected);
            ViewBag.Property_Status_ID = new SelectList(model.Property_Status.ToList(), "ID", "Property_Status_Name", propertyStatusSelected);
        }

    }
}