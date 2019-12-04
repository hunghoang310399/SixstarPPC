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
    }
}