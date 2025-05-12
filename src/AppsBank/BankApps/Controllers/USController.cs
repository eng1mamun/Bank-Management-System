using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankApps.Models;
using System.Web.Security;
namespace BankApps.Controllers
{
   
    public class USController : Controller
    {
        Brank_InfomationEntities db = new Brank_InfomationEntities();
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult LogIn(string UName,string Password)
        {
            var Valid = db.EmployeeInfoes.Where(x => x.UName == UName && x.Password == Password).FirstOrDefault();
            if(Valid!=null)
            {
                FormsAuthentication.SetAuthCookie(UName, false);
                return RedirectToAction("UserPanel", "BA");
            }
            return View();
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "US");
        }
    }
}