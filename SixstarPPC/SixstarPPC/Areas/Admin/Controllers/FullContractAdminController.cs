using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SixstarPPC.Models;

namespace SixstarPPC.Areas.Admin.Controllers
{
    public class FullContractAdminController : Controller
    {
        ppcdbEntities model = new ppcdbEntities();
        // GET: Admin/FullContractAdmin
        public ActionResult Index()
        {
            var fullcontract = model.Full_Contract.ToList();
            return View(fullcontract);
        }

        public ActionResult Print(int id)
        {
            var contract = model.Full_Contract.FirstOrDefault(x => x.ID == id);
            if (contract != null)
            {
                FullContractPrintModel fc = new FullContractPrintModel();
                fc.Full_Contract_Code = contract.Full_Contract_Code;
                fc.Customer_Name = contract.Customer_Name;
                fc.Customer_Address = contract.Customer_Address;
                fc.Data_Of_Contract = contract.Date_Of_Contract;
                fc.Deposit = contract.Deposit;
                fc.Price = contract.Price;
                fc.SSN = contract.SSN;
                fc.Mobile = contract.Mobile;

                fc.Property_Code = contract.Property.Property_Code;
                fc.Address = contract.Property.Address;
                return View(fc);
            }
            else
            {
                return View();
            }

        }
        public ActionResult Create()
        {
            PopularData();
            return View();
        }
        public void PopularData(object propertyIDSelected = null)
        {
            ViewBag.Property_ID = new SelectList(model.Properties.ToList(), "ID", "Property_Name", propertyIDSelected);

        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,  Customer_Name, Year_Of_Birth, SSN, Customer_Address, Mobile, Property_ID, Date_Of_Contract, Price, Deposit, Remain, Status")] Full_Contract F)
        {
            try
            {

                model.Full_Contract.Add(F);
                model.SaveChanges();

                return RedirectToAction("Index");
            }
            catch { return View(); }
        }
        public void PopularMessage(bool success)
        {
            if (success)
                Session["success"] = "Successfull";
            else
                Session["success"] = "Fail";
        }
        public ActionResult Details(int id)
        {
            var fullC = model.Full_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
            return View(fullC);
        }

       
        [HttpGet]

        public ActionResult Edit(int id)
        {
            var fullC = model.Full_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
            PopularData(fullC.Property_ID);
            return View(fullC);
        }
        public ActionResult Edit(int id, Full_Contract pp)
        {
            var fullC = model.Full_Contract.ToList();
            try
            {
                var FullC = model.Full_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
                PopularData(FullC.Property_ID);
                FullC.ID = pp.ID;
                FullC.Full_Contract_Code = pp.Full_Contract_Code;
                FullC.Customer_Name = pp.Customer_Name;
                FullC.Year_Of_Birth = pp.Year_Of_Birth;
                FullC.SSN = pp.SSN;
                FullC.Customer_Address = pp.Customer_Address;
                FullC.Mobile = pp.Mobile;
                FullC.Date_Of_Contract = pp.Date_Of_Contract;
                FullC.Price = pp.Price;
                FullC.Deposit = pp.Deposit;
                FullC.Remain = pp.Remain;
                FullC.Status = pp.Status;
                model.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View(fullC);
            }

        }
       
    }
}