using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
        public ActionResult Create(Property property ,List<HttpPostedFileBase> files)
        {
           
            if (ModelState.IsValid)
            {
                string album = "";
          
                var file = Request.Files["file"];
               //up album
                if (files != null)
                {
                    foreach (var imageFile in files)
                    {
                        if (imageFile != null)
                        {
                            var fileName = DateTime.Now.Ticks + "-" + Path.GetFileName(imageFile.FileName);
                            var physicalPath = Path.Combine(Server.MapPath("~/Images"), fileName);

                            // The files are not actually saved in this demo
                            imageFile.SaveAs(physicalPath);
                            album += album.Length > 0 ? ";" + fileName : fileName;
                        }
                    }
                }
                property.Album = album;
                // Avatar
                if (file != null)
                {
                    var avatar = DateTime.Now.Ticks + "-" + Path.GetFileName(file.FileName);
                    var physicPath = Path.Combine(Server.MapPath("~/Images"), avatar);
                    file.SaveAs(physicPath);
                    property.Avatar = avatar;
                }
                property.Installment_Rate = 0.7;
                model.Properties.Add(property);
                model.SaveChanges();
               PopularMessage(true);
            }
            else { PopularMessage(false); }
         
            return RedirectToAction("Index");

        }
        //json district
        public JsonResult GetDistrictByCityId(int id)
        {
            // Disable proxy creation
            model.Configuration.ProxyCreationEnabled = false;
            var listDistrict = model.Districts.Where(x => x.City_ID == id).ToList();
            return Json(listDistrict, JsonRequestBehavior.AllowGet);
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
                //upload ảnh
                var file = Request.Files["file"];
                if (file != null)
                {
                    var avatar = DateTime.Now.Ticks + "-" + Path.GetFileName(file.FileName);
                    var physicPath = Path.Combine(Server.MapPath("~/Images"), avatar);
                    file.SaveAs(physicPath);
                    p.Avatar = avatar;
                }
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
        public ActionResult Details(int id)
        {
//            PopularData();
            var property = model.Properties.FirstOrDefault(x => x.ID == id);
            return View(property);
        }
        //delete Ảnh trong album
        [HttpPost]
        public string deleteImage(string imageName, int id)
        {
            string fullPath = Request.MapPath("~/Images" + imageName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            var property = model.Properties.FirstOrDefault(x => x.ID == id);
            var album = property.Album.Split(';');
            album = album.Where(w => w != imageName).ToArray();
            property.Album = string.Join(";", album);

            model.Entry(property).State = EntityState.Modified;
            model.SaveChanges();
            return property.Album;
        }
        public void PopularData(object propertyTypeSelected = null, object districtSelected = null, object propertyStatusSelected =null, object listCitySelected = null)
        {
            ViewBag.Property_Status_ID = new SelectList(model.Property_Status.ToList(), "ID", "Property_Status_Name", propertyStatusSelected);
            ViewBag.CityList = new SelectList(model.Cities.ToList(), "ID", "City_Name", listCitySelected);
            ViewBag.Property_Type_ID = new SelectList(model.Property_Type.ToList(), "ID", "Property_Type_Name",propertyTypeSelected);
            ViewBag.District_ID = new SelectList(model.Districts.ToList(), "ID", "District_Name", districtSelected);
            ViewBag.Property_Status_ID = new SelectList(model.Property_Status.ToList(), "ID", "Property_Status_Name", propertyStatusSelected);
        }

    }
}