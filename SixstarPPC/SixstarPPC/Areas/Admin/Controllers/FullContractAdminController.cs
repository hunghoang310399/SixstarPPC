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
            if (contract !=null)
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
    }
}