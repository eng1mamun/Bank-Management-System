using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankApps.Models;
using System.IO;
using Microsoft.Reporting.WebForms;

namespace BankApps.Controllers
{
    [Authorize]
    public class BAController : Controller
    {
        Brank_InfomationEntities db = new Brank_InfomationEntities();
        #region BankEntry
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult BankEntry(int? Id)
        {
            BankInfo getId = db.BankInfoes.Where(x => x.BankId == Id).FirstOrDefault();
            if(getId==null)
            {
                getId = new BankInfo();
            }
            return View(getId);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult BankEntry(BankInfo Bra, HttpPostedFileBase Image)
        {
            var Update = db.BankInfoes.Where(x => x.BankId == Bra.BankId).FirstOrDefault();
            if(Update==null)
            {
                string F = Path.GetFileNameWithoutExtension(Image.FileName);
                string E = Path.GetExtension(Image.FileName);
                string ImageName = F + DateTime.Now.ToString("yymmssff") + E;
                Bra.Image= ImageName;
                Image.SaveAs(Path.Combine(Server.MapPath("~/Appfile"), ImageName));
                db.BankInfoes.Add(Bra);
                db.SaveChanges();
            }
          else
            {
                Update.BankId = Bra.BankId;
                Update.BankName = Bra.BankName;
                Update.BankCode = Bra.BankCode;
                Update.Phone = Bra.Phone;
                Update.Email = Bra.Email;
                Update.Date = Bra.Date;
                Update.Image = Bra.Image;
                db.SaveChanges();
            }
            return View(Bra);
        }
        [Authorize(Roles = "admin")]
        public ActionResult delBankEntry(int? Id)
        {
            BankInfo getId = db.BankInfoes.Where(x => x.BankId == Id).FirstOrDefault();
            if(getId==null)
            {
                getId = new BankInfo();
            }
            string ImageNmae = getId.Image;
            db.BankInfoes.Remove(getId);
            db.SaveChanges();
            if (ImageNmae != null)
            {
                FileInfo fi = new FileInfo(Path.Combine(Server.MapPath("~/Appfile"), ImageNmae));
                fi.Delete();
            }
            return RedirectToAction("BankEntry");
        }
        #endregion BrankEntry

        #region BranchEntry
        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public ActionResult BranchEntry(int? Id)
        {
            BranchInfo getId = db.BranchInfoes.Where(x => x.BranchId == Id).FirstOrDefault();
            if(getId==null)
            {
                getId = new BranchInfo();
            }
            return View(getId);
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public ActionResult BranchEntry(BranchInfo ban)
        {
            var Update = db.BranchInfoes.Where(x => x.BranchId == ban.BranchId).FirstOrDefault();
            if(Update==null)
            {
                db.BranchInfoes.Add(ban);
                db.SaveChanges();
            }
            else
            {
                Update.BranchId = ban.BranchId;
                Update.BranchName = ban.BranchName;
                db.SaveChanges();
            }
          
            return View(ban);
        }
        [Authorize(Roles = "admin, user")]
        public ActionResult delBranchEntry(int? Id)
        {
            BranchInfo getId = db.BranchInfoes.Where(x => x.BranchId == Id).FirstOrDefault();
            if(getId==null)
            {
                getId = new BranchInfo();
            }
            db.BranchInfoes.Remove(getId);
            db.SaveChanges();
            return RedirectToAction("BranchEntry");
        }
        #endregion BranchEntry

        #region DesignationInfo
        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public ActionResult DesignationEntry(int? Id)
        {
            DesignationInfo getId = db.DesignationInfoes.Where(x => x.DesignationId== Id).FirstOrDefault();
            if (getId == null)
            {
                getId = new DesignationInfo();
            }
            return View(getId);
           
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public ActionResult DesignationEntry(DesignationInfo Den)
        {
            var Update = db.DesignationInfoes.Where(x => x.DesignationId == Den.DesignationId).FirstOrDefault();
            if (Update == null)
            {
                db.DesignationInfoes.Add(Den);
                db.SaveChanges();
            }
            else
            {
                Update.DesignationId = Den.DesignationId;
                Update.DesName = Den.DesName;
                db.SaveChanges();
            }
            return View(Den);
        }
        [Authorize(Roles = "admin, user")]
        public ActionResult delDesignationEntry(int? Id)
        {
            DesignationInfo getId = db.DesignationInfoes.Where(x => x.DesignationId == Id).FirstOrDefault();
            if(getId==null)
            {
                getId = new DesignationInfo();
            }
            db.DesignationInfoes.Remove(getId);
            db.SaveChanges();
            return RedirectToAction("DesignationEntry");
        }
        #endregion DesignationInfo

        #region GenderEntry
        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public ActionResult GenderEntry(int? Id)
        {
            GenderInfo getId = db.GenderInfoes.Where(x => x.GenderId == Id).FirstOrDefault();
            if (getId == null)
            {
                getId = new GenderInfo();
            }
            //GenderInfo Ge = new GenderInfo();
            return View(getId);

        }
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public ActionResult GenderEntry(GenderInfo Gen)
        {
            var Update = db.GenderInfoes.Where(x => x.GenderId == Gen.GenderId).FirstOrDefault();
            if (Update == null)
            {
                db.GenderInfoes.Add(Gen);
                db.SaveChanges();
            }
            else
            {
                Update.GenderId = Gen.GenderId;
                Update.GenName = Gen.GenName;
                db.SaveChanges();
            }
            return View(Gen);
        }
        [Authorize(Roles = "admin, user")]
        public ActionResult delGenderEntry(int? Id)

        {

            GenderInfo getId = db.GenderInfoes.Where(x => x.GenderId == Id).FirstOrDefault();
            if (getId == null)
            {
                getId = new GenderInfo();
            }
            db.GenderInfoes.Remove(getId);
            db.SaveChanges();
            return RedirectToAction("GenderEntry");
        }
        #endregion GenderEntry  

        #region EmployeeEntry
        [HttpGet]
        [Authorize(Roles ="admin, user")]
        public ActionResult EmployeeEntry(int? Id)
        {
            EmployeeInfo getId = db.EmployeeInfoes.Where(x => x.EmployeeId == Id).FirstOrDefault();
            if (getId == null)
            {
                getId = new EmployeeInfo();
            }
            return View(getId);
        }
        [HttpPost]
        [Authorize(Roles ="admin, user")]
        public ActionResult EmployeeEntry(EmployeeInfo em,HttpPostedFileBase Image)
        {
            var Update = db.EmployeeInfoes.Where(x => x.EmployeeId == em.EmployeeId).FirstOrDefault();
            if (Update == null)
            {
                string F = Path.GetFileNameWithoutExtension(Image.FileName);
                string E = Path.GetExtension(Image.FileName);
                string ImageName = F + DateTime.Now.ToString("yymmssff") + E;
                em.Image = ImageName;
                Image.SaveAs(Path.Combine(Server.MapPath("~/Appfile"), ImageName));
                db.EmployeeInfoes.Add(em);
                db.SaveChanges();
            }
            else
            {
                Update.EmployeeId = em.EmployeeId;
                Update.EmpName = em.EmpName;
                Update.BankId = em.BankId;
                Update.BranchId = em.BranchId;
                Update.DesignationId = em.DesignationId;
                Update.GenderId = em.GenderId;
                Update.Age = em.Age;
                Update.Salary = em.Salary;
                
                Update.Date = em.Date;
                Update.Image = em.Image;
                Update.Role = em.Role;
                db.SaveChanges();
            }
            return View(em);
        }
        [Authorize(Roles ="admin, user")]
        public ActionResult delEmployeeEntry(int? Id)
        {
            EmployeeInfo getId = db.EmployeeInfoes.Where(x => x.EmployeeId == Id).FirstOrDefault();
            if (getId == null)
            {
                getId = new EmployeeInfo();
            }
            string ImageNmae = getId.Image;
            db.EmployeeInfoes.Remove(getId);
            db.SaveChanges();
            if (ImageNmae != null)
            {
                FileInfo fi = new FileInfo(Path.Combine(Server.MapPath("~/Appfile"), ImageNmae));
                fi.Delete();
            }
            return RedirectToAction("EmployeeEntry");
        }
        #endregion EmployeeEntry

        #region EmployeeSearch
            [HttpGet]
        [Authorize(Roles = "admin, user")]
        public ActionResult EmployeeSearch()
        {
            return View();
        }
        [Authorize(Roles = "admin, user")]
        public JsonResult GetEmployeeAPI(int? BId,int? BrId,int? DId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var EmployeeList = db.EmployeeInfoes.Where(x => x.BankId == BId && x.BranchId == BrId && x.DesignationId == DId).ToList();
            return Json(EmployeeList, JsonRequestBehavior.AllowGet);
        }
        #endregion EmployeeSearch
        [HttpGet]
        public ActionResult UserPanel()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EmployeeReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeReport(int? BranchId,int? DesignationId)
        {
            string BranchName =db.BranchInfoes.Where(x => x.BranchId == BranchId).Select(x => x.BranchName).FirstOrDefault();
            string Designation = db.DesignationInfoes.Where(x => x.DesignationId == DesignationId).Select(x => x.DesName).FirstOrDefault();
            var EMPList = db.EmployeeInfoes.Where(x => x.BranchId == BranchId && x.DesignationId == DesignationId).ToList();
            var E = db.Rpt_EmployeeInfo.Where(x => x.BranchName == BranchName && x.DesignationName == Designation).ToList();
            db.Rpt_EmployeeInfo.RemoveRange(E);
            db.SaveChanges();
            foreach (var item in EMPList)
            {
                Rpt_EmployeeInfo rei = new Rpt_EmployeeInfo();
                rei.EmployeeId = item.EmployeeId;
                rei.EmpName = item.EmpName;
                rei.BankName = db.BankInfoes.Where(x => x.BankId == item.BankId).Select(x => x.BankName).FirstOrDefault();
                rei.BranchName = db.BranchInfoes.Where(x => x.BranchId == item.BranchId).Select(x => x.BranchName).FirstOrDefault();
                rei.DesignationName = db.DesignationInfoes.Where(x => x.DesignationId == item.DesignationId).Select(x => x.DesName).FirstOrDefault();
                rei.GenderName = db.GenderInfoes.Where(x => x.GenderId == item.GenderId).Select(x => x.GenName).FirstOrDefault();
                rei.Age = item.Age;
                rei.Salary = item.Salary.ToString();
                rei.Date = item.Date;
                db.Rpt_EmployeeInfo.Add(rei);
                db.SaveChanges();
            }
            LocalReport lr = new LocalReport();
            string p = Path.Combine(Server.MapPath("~/Report"), "CustomerList.rdlc");
            lr.ReportPath = p;
            var EmployeeList = db.Rpt_EmployeeInfo.Where(x => x.BranchName == BranchName && x.DesignationName == Designation).ToList();
            ReportDataSource rd1 = new ReportDataSource("CUDataSet", EmployeeList);
            lr.DataSources.Add(rd1);
            string mt, enc, f;
            string[] s;
            Warning[] w;

            byte[] b = lr.Render("PDF", null, out mt, out enc, out f, out s, out w);
            string downloadname = "Emplist.pdf";

            return File(b, mt, downloadname);

        }
    }
}
