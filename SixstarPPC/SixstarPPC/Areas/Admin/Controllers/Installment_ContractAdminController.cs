using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SixstarPPC.Models;

namespace SixstarPPC.Areas.Admin.Controllers
{
    public class Installment_ContractAdminController : Controller
    {
        ppcdbEntities model = new ppcdbEntities();
        // GET: Admin/Installment_ContractAdmin
        public ActionResult Index()
        {
            var installmentContract = model.Installment_Contract.ToList();
            return View(installmentContract);
        }
        public ActionResult Print(int id)
        {
            var contract = model.Installment_Contract.FirstOrDefault(x => x.ID == id);
            if (contract != null)
            {
                InstallmentContractPrintModel ic = new InstallmentContractPrintModel();
                ic.Installment_Contract_Code = contract.Installment_Contract_Code;
                ic.Customer_Name = contract.Customer_Name;
                ic.Customer_Address = contract.Customer_Address;
                ic.Data_Of_Contract = contract.Date_Of_Contract;
                ic.Deposit = contract.Deposit;
                ic.Price = contract.Price;
                ic.SSN = contract.SSN;
                ic.Mobile = contract.Mobile;
                ic.Installment_Payment_Method = contract.Installment_Payment_Method;
                ic.Payment_Period = contract.Payment_Period;
                ic.Loan_Amount = contract.Loan_Amount;
                ic.Property_Code = contract.Property.Property_Code;
                ic.Address = contract.Property.Address;
                return View(ic);
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
        public ActionResult Create([Bind(Include = "ID, Installment_Contract_Code, Customer_Name, Year_Of_Birth, SSN, Customer_Address, Mobile, Property_ID, Date_Of_Contract, Installment_Payment_Method, Payment_Period, Price, Deposit, Loan_Amount, Taken, Remain, Status")] Installment_Contract F)
        {
            try
            {

                model.Installment_Contract.Add(F);
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
            var iC = model.Installment_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
            return View(iC);
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var iC = model.Installment_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
            PopularData(iC.Property_ID);
            return View(iC);
        }
        public ActionResult Edit(int id, Installment_Contract pp)
        {
            var iC = model.Installment_Contract.ToList();
            try
            {
                var IC = model.Installment_Contract.Select(p => p).Where(p => p.ID == id).FirstOrDefault();
                PopularData(IC.Property_ID);

                IC.Installment_Contract_Code = pp.Installment_Contract_Code;
                IC.Customer_Name = pp.Customer_Name;
                IC.Year_Of_Birth = pp.Year_Of_Birth;
                IC.SSN = pp.SSN;
                IC.Customer_Address = pp.Customer_Address;
                IC.Mobile = pp.Mobile;
                IC.Date_Of_Contract = pp.Date_Of_Contract;
                IC.Installment_Payment_Method = pp.Installment_Payment_Method;
                IC.Payment_Period = pp.Payment_Period;
                IC.Price = pp.Price;
                IC.Deposit = pp.Deposit;
                IC.Loan_Amount = pp.Loan_Amount;
                IC.Taken = pp.Taken;
                IC.Remain = pp.Remain;
                IC.Status = pp.Status;

                model.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View(iC);
            }

        }
    }

}