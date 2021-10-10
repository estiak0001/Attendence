using BusinessLogic;

using BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPosApp.Controllers
{
    public class EmployeeController : Controller
    {

        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();
        Crd_HRM_Employee empModel = new Crd_HRM_Employee();
        Crd_HRM_EmployeeOfficialInfo empModelOfficial = new Crd_HRM_EmployeeOfficialInfo();

        ClsCommon common = new ClsCommon();
        string strMaxNO = "";
        public ActionResult Index(string search)
        {
            int page = 1;
            ProductIndexView employeeView = new ProductIndexView();
            employeeView.employeePerPage = 18;
            employeeView.employees = empModel.GetAllEmployee(search);
            employeeView.CurrentPage = page;
            employeeView.Pager = new Pager(employeeView.employees.Count(), page);
            return View(employeeView);
        }
        public ActionResult SearchResult(string search)
        {
            int page = 1;
            ProductIndexView employeeView = new ProductIndexView();
            employeeView.employeePerPage = 18;
            employeeView.employees = empModel.GetAllEmployee(search);
            employeeView.CurrentPage = page;
            employeeView.Pager = new Pager(employeeView.employees.Count(), page);
            //return Pertia(employeeView);
            return PartialView("_EmployeeList", employeeView);
        }
        public ActionResult ShowEmployeInfo()
        {
            string search = null;
            var result = empModel.GetAllEmployee(search);
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Search(string serchtext)
        //{

        //}

        [HttpGet]
        public ActionResult AddOrEditEmployee(string empID)
        {
            //ViewBag.ActiveTab = "custom-tabs-one-messages";
            ViewBag.BloodGroup = new SelectList(db.HRM_Def_BloodGroup, "BloodGroupCode", "BloodGroup");
            ViewBag.Sex = new SelectList(db.HRM_Def_Sex, "SexCode", "Sex");
            ViewBag.Religion = new SelectList(db.HRM_Def_Religion, "ReligionCode", "Religion");
            ViewBag.LoadNationality = new SelectList(db.HRM_Def_Nationality, "nationalitycode", "Nationality");

            //official
            string empIDLastInserted = "";
            if (TempData.ContainsKey("InsertedEmployeeID"))
            {
                empIDLastInserted = TempData["InsertedEmployeeID"].ToString();
                empID = empIDLastInserted;
            }
            
            if (empIDLastInserted != "")
            {
                ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.Where(x=>x.EmployeeID == empIDLastInserted).ToList().Select(u
                     => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
             "EmployeeID", "FirstName");
            }
            else
            {
                if (empID != null)
                {
                    ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.ToList().Select(u
                     => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
             "EmployeeID", "FirstName");
                }
                else
                {
                    ViewBag.LoadEmployee = new SelectList(db.HRM_Employee.Where(c => !db.HRM_EmployeeOfficialInfo.Select(b => b.EmployeeID).Contains(c.EmployeeID)).ToList().Select(u
                             => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
                     "EmployeeID", "FirstName");
                }
            }
            
            ViewBag.LoadCompany = new SelectList(db.Core_Company, "CompanyCode", "CompanyName");
            ViewBag.LoadBranch = new SelectList(db.Core_Branch, "BranchCode", "BranchName");
            ViewBag.LoadDepartment = new SelectList(db.HRM_Def_Department, "DepartmentCode", "DepartmentName");
            ViewBag.LoadDesignation = new SelectList(db.HRM_Def_Designation, "DesignationCode", "DesignationName");
            ViewBag.LoadShift = new SelectList(db.HRM_ATD_Shift, "ShiftCode", "ShiftName");
            ViewBag.LoadEmpType = new SelectList(db.HRM_Def_EmpType, "EmpTypeCode", "EmpTypeName");
            ViewBag.LoadEmpNature = new SelectList(db.HRM_EIS_Def_EmploymentNature, "EmploymentNatureId", "EmploymentNature");

            ViewBag.LoadReportingTo = new SelectList(db.HRM_Employee.ToList().Select(u
                     => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
             "EmployeeID", "FirstName");
            ViewBag.LoadHOD = new SelectList(db.HRM_Employee.ToList().Select(u
                      => new { FirstName = String.Format("{0}{1}{2}{3}", u.FirstName, " (", u.EmployeeID, ")"), EmployeeID = u.EmployeeID }),
             "EmployeeID", "FirstName");
            ViewBag.LoadEmpStatus = new SelectList(db.HRM_Def_EmployeeStatus, "EmployeeStatusCode", "EmployeeStatus");
            if (empID == null)
            {
                common.FindMaxNoAuto(ref strMaxNO, "EmployeeID", "HRM_Employee");
                ViewBag.MaxComID = strMaxNO.ToString();
                Model_GaneranAndOfficialEmployee p = new Model_GaneranAndOfficialEmployee();
                p.EmployeeID = strMaxNO.ToString();
                return View(p);
            }
            else
            {
                string empIDLastInserted3 = "";
                if (TempData.ContainsKey("InsertedEmployeeID"))
                {
                    empIDLastInserted3 = TempData["InsertedEmployeeID"].ToString();
                    var result2 = empModel.GetEmployee2(empID);
                    return View(result2);
                }
                var result = empModel.GetEmployee(empID);
                return View(result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEditEmployee(Model_GaneranAndOfficialEmployee Emp)
        {
            string LoginEmployeeID = Session["EmployeeID"].ToString();
            var Item = db.HRM_Employee.FirstOrDefault(x => x.EmployeeID == Emp.EmployeeID);
            if (Item == null)
            {
                string directory = @"~\EmpImage\";
                HttpPostedFileBase ProfileImageUpload = Emp.Photo; // Request.Files["ProfileImageUpload"];
                string fileName = null;
                if (ProfileImageUpload != null && ProfileImageUpload.ContentLength > 0)
                {
                    string extension = Path.GetExtension(ProfileImageUpload.FileName);
                    fileName = Emp.EmployeeID + extension;
                    string imgPath = Path.Combine(directory, fileName);
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                    ProfileImageUpload.SaveAs(Server.MapPath(Path.Combine(directory, fileName)));
                }
                else
                {
                    fileName = "default.png";
                }
                string NewDateString = string.Empty;
                if (Emp.DateOfBirthOrginal != null)
                {
                    string DateString = Emp.DateOfBirthOrginal.ToString();
                    DateTime date = new DateTime();
                    date = DateTime.ParseExact(DateString, "dd/MM/yyyy", null);
                    NewDateString = date.ToString("yyyy/MM/dd");
                }
                else
                {
                    NewDateString = DBNull.Value.ToString();
                }

                HRM_Employee coreCom = new HRM_Employee()
                {
                    EmployeeID = Emp.EmployeeID,
                    FirstName = Emp.FirstName,
                    LastName = Emp.LastName,
                    FirstNameBangla = Emp.FirstNameBangla,
                    LastNameBangla = Emp.LastNameBangla,
                    FatherName = Emp.FatherName,
                    MotherName = Emp.MotherName,
                    DateOfBirthOrginal = Convert.ToDateTime(NewDateString),
                    BirthCertificateNo = "",
                    PlaceOfBirth = "",
                    SexCode = Emp.SexCode,
                    BloodGroupCode = Emp.BloodGroupCode,
                    NationalityCode = Emp.NationalityCode,
                    NationalIDNO = "",
                    ReligionCode = Emp.ReligionCode,
                    MaritalStatusCode = "",
                    NoOfSon = "",
                    NoOfDaughters = "",
                    CardNo = "",
                    PersonalEmail = Emp.PersonalEmail,
                    Telephone = Emp.Telephone,
                    LUser = LoginEmployeeID,
                    CompanyCode = "001",
                    UserInfoEmployeeID = LoginEmployeeID,
                    PhotoUrl = "~/EmpImage/" + fileName
                };
                TempData["ActiveTab"] = "custom-tabs-one-profile";
                TempData["InsertedEmployeeID"] = Emp.EmployeeID;
                db.HRM_Employee.Add(coreCom);
                db.SaveChanges();
                
                return RedirectToAction("AddOrEditEmployee", "Employee");
            }
            else
            {
                string directory = @"~\EmpImage\";
                HttpPostedFileBase ProfileImageUpload = Emp.Photo; // Request.Files["ProfileImageUpload"];
                string fileName = null;
                
                if (ProfileImageUpload != null && ProfileImageUpload.ContentLength > 0)
                {
                    string extension = Path.GetExtension(ProfileImageUpload.FileName);
                    fileName = Emp.EmployeeID + extension;
                    string imgPath = Path.Combine(directory, fileName);
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                    ProfileImageUpload.SaveAs(Server.MapPath(Path.Combine(directory, fileName)));
                    fileName = "~/EmpImage/" + fileName;
                }
                else
                {
                    fileName = Item.PhotoUrl;
                }
                Item.EmployeeID = Emp.EmployeeID;
                Item.FirstName = Emp.FirstName;
                Item.LastName = Emp.LastName;
                Item.FirstNameBangla = Emp.FirstNameBangla;
                Item.LastNameBangla = Emp.LastNameBangla;
                Item.FatherName = Emp.FatherName;
                Item.MotherName = Emp.MotherName;

                string DateString = Emp.DateOfBirthOrginal;
                DateTime date = new DateTime();
                date = DateTime.ParseExact(DateString, "dd/MM/yyyy", null);
                string NewDateString = date.ToString("yyyy/MM/dd");
                Item.DateOfBirthOrginal = Convert.ToDateTime(NewDateString);

                Item.BirthCertificateNo = "";
                Item.PlaceOfBirth = "";
                Item.SexCode = Emp.SexCode;
                Item.BloodGroupCode = Emp.BloodGroupCode;
                Item.NationalityCode = Emp.NationalityCode;
                Item.NationalIDNO = "";

                Item.ReligionCode = Emp.ReligionCode;
                Item.MaritalStatusCode = "";
                Item.NoOfSon = "";
                Item.NoOfDaughters = "";
                Item.CardNo = "";
                Item.PersonalEmail = Emp.PersonalEmail;
                Item.Telephone = Emp.Telephone;
                Item.UserInfoEmployeeID = LoginEmployeeID;
                Item.PhotoUrl = fileName;
                db.SaveChanges();
                return RedirectToAction("Index", "Employee", Emp);
            }
        }
        [HttpPost]
        public ActionResult AddOrEditEmployeeOfficialInfo(Model_GaneranAndOfficialEmployee Emp)
        {
            string LoginEmployeeID = Session["EmployeeID"].ToString();
            var Item = db.HRM_EmployeeOfficialInfo.FirstOrDefault(x => x.EmployeeID == Emp.EmployeeID);
            if (Item == null)
            {
                string DateString = Emp.JoiningDate.ToString();
                DateTime date = new DateTime();
                date = DateTime.ParseExact(DateString, "dd/MM/yyyy", null);
                string JoiningDate = date.ToString("MM/dd/yyyy");

                HRM_EmployeeOfficialInfo coreCom = new HRM_EmployeeOfficialInfo();
                coreCom.EmployeeID = Emp.EmployeeID;
                coreCom.CompanyCode = Emp.CompanyCode;
                if (Emp.BranchCode != null)
                {
                    coreCom.BranchCode = Emp.BranchCode;
                }
                else
                {
                    coreCom.BranchCode = "";
                }
                if (Emp.DivisionCode != null)
                {
                    coreCom.DivisionCode = Emp.DivisionCode;
                }
                else
                {
                    coreCom.DivisionCode = "";
                }
                coreCom.DepartmentCode = Emp.DepartmentCode;
                coreCom.DesignationCode = Emp.DesignationCode;

                if (Emp.EmpTypeCode != null)
                {
                    coreCom.EmpTypeCode = Emp.EmpTypeCode;
                }
                else
                {
                    coreCom.EmpTypeCode = "";
                }
                if (Emp.GradeCode != null)
                {
                    coreCom.GradeCode = Emp.GradeCode;
                }
                else
                {
                    coreCom.GradeCode = "";
                }
                coreCom.EmploymentNatureId = Emp.EmploymentNatureId;
                if (Emp.GrossSalary != null)
                {
                    coreCom.GrossSalary = Convert.ToDecimal(Emp.GrossSalary);
                }
                else
                {
                    coreCom.GrossSalary = 0;
                }
                coreCom.CurrencyCode = "";
                coreCom.PaymentPeriodID = "";
                coreCom.DisbursementMethodId = "";
                coreCom.ShiftCode = Emp.ShiftCode;
                coreCom.EmployeeStatus = Emp.EmployeeStatus;
                if (Emp.ReportingTo != null)
                {
                    coreCom.ReportingTo = Emp.ReportingTo;
                }
                else
                {
                    coreCom.ReportingTo = "";
                }
                if (Emp.HOD != null)
                {
                    coreCom.HOD = Emp.HOD;
                }
                else
                {
                    coreCom.HOD = "";
                }
                if (Emp.MobileNo != null)
                {
                    coreCom.MobileNo = Emp.MobileNo;
                }
                else
                {
                    coreCom.MobileNo = "";
                }
                if (Emp.Email != null)
                {
                    coreCom.Email = Emp.Email;
                }
                else
                {
                    coreCom.Email = "";
                }
                coreCom.AppointmentLetterNo = "";
                coreCom.JoiningDate = Convert.ToDateTime(JoiningDate);
                if (Emp.JoiningSalary != null)
                {
                    coreCom.JoiningSalary = Convert.ToDecimal(Emp.JoiningSalary);
                }
                else
                {
                    coreCom.JoiningSalary = 0;
                }
                coreCom.ProbationPeriodType = "";
                coreCom.ProbationPeriod = "";
                coreCom.LUser = LoginEmployeeID;
                coreCom.LDate = DateTime.Now;

                coreCom.CompanyCode_Session = "001";
                coreCom.UserInfoEmployeeID = LoginEmployeeID;
                db.HRM_EmployeeOfficialInfo.Add(coreCom);
                db.SaveChanges();
                return RedirectToAction("Index", "Employee");
            }
            else
            {

                string DateString = Emp.JoiningDate.ToString();
                DateTime date = new DateTime();
                date = DateTime.ParseExact(DateString, "dd/MM/yyyy", null);
                string JoiningDate = date.ToString("MM/dd/yyyy");

                Item.EmployeeID = Emp.EmployeeID;
                Item.CompanyCode = Emp.CompanyCode;
                if (Emp.BranchCode != null)
                {
                    Item.BranchCode = Emp.BranchCode;
                }
                else
                {
                    Item.BranchCode = "";
                }
                if (Emp.DivisionCode != null)
                {
                    Item.DivisionCode = Emp.DivisionCode;
                }
                else
                {
                    Item.DivisionCode = "";
                }
                Item.DepartmentCode = Emp.DepartmentCode;
                Item.DesignationCode = Emp.DesignationCode;
                if (Emp.EmpTypeCode != null)
                {
                    Item.EmpTypeCode = Emp.EmpTypeCode;
                }
                else
                {
                    Item.EmpTypeCode = "";
                }
                if (Emp.GradeCode != null)
                {
                    Item.GradeCode = Emp.GradeCode;
                }
                else
                {
                    Item.GradeCode = "";
                }
                Item.EmploymentNatureId = Emp.EmploymentNatureId;
                if (Emp.GrossSalary != null)
                {
                    Item.GrossSalary = Convert.ToDecimal(Emp.GrossSalary);
                }
                else
                {
                    Item.GrossSalary = 0;
                }
                Item.CurrencyCode = "";
                Item.PaymentPeriodID = "";
                Item.DisbursementMethodId = "";
                Item.ShiftCode = Emp.ShiftCode;
                Item.EmployeeStatus = Emp.EmployeeStatus;
                if (Emp.ReportingTo != null)
                {
                    Item.ReportingTo = Emp.ReportingTo;
                }
                else
                {
                    Item.ReportingTo = "";
                }
                if (Emp.HOD != null)
                {
                    Item.HOD = Emp.HOD;
                }
                else
                {
                    Item.HOD = "";
                }
                if (Emp.MobileNo != null)
                {
                    Item.MobileNo = Emp.MobileNo;
                }
                else
                {
                    Item.MobileNo = "";
                }
                if (Emp.Email != null)
                {
                    Item.Email = Emp.Email;
                }
                else
                {
                    Item.Email = "";
                }
                Item.AppointmentLetterNo = "";
                Item.JoiningDate = Convert.ToDateTime(JoiningDate);
                if (Emp.JoiningSalary != null)
                {
                    Item.JoiningSalary = Convert.ToDecimal(Emp.JoiningSalary);
                }
                else
                {
                    Item.JoiningSalary = 0;
                }
                Item.ProbationPeriodType = "";
                Item.ProbationPeriod = "";
                Item.LUser = LoginEmployeeID;
                Item.LDate = DateTime.Now;

                Item.CompanyCode_Session = "001";
                Item.UserInfoEmployeeID = LoginEmployeeID;
                db.SaveChanges();

                return RedirectToAction("Index", "Employee");
            }
        }
        public JsonResult GetEmployeeInfo(string EmployeeID)
        {
            var result = empModel.GetEmployeeInfo(EmployeeID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            empModel.DeleteCompany(id);
            return Json(new { success = true, message = "deleted Successfully" }, JsonRequestBehavior.AllowGet);

        }

    }
}