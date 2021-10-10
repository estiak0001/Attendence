
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Crd_HRM_Employee
    {
        //public List<Model_HRM_Employee> GetAllEmployee()
        //{
        //    using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
        //    {
        //        var result = (from hrm in context.HRM_Employee
        //                      select new Model_HRM_Employee()
        //                      {
        //                          EmployeeID = hrm.EmployeeID,
        //                          FirstName = hrm.FirstName + hrm.LastName,
        //                          FatherName = hrm.FatherName,
        //                          MotherName = hrm.MotherName,
        //                          Telephone = hrm.Telephone,
        //                          PersonalEmail = hrm.PersonalEmail,
        //                          PhotoUrl = hrm.PhotoUrl
        //                      }).ToList();
        //        return result;
        //    }

        //}

        public List<Model_GaneranAndOfficialEmployee> GetAllEmployee(string search)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from offic in context.HRM_EmployeeOfficialInfo
                              from gen in context.HRM_Employee.Where(gen => gen.EmployeeID == offic.EmployeeID)
           .DefaultIfEmpty()
                              from depart in context.HRM_Def_Department.Where(depart => depart.DepartmentCode == offic.DepartmentCode)
           .DefaultIfEmpty()
                              from Desig in context.HRM_Def_Designation.Where(Desig => Desig.DesignationCode == offic.DesignationCode)
         .DefaultIfEmpty()
                              from shi in context.HRM_ATD_Shift.Where(shi => shi.ShiftCode == offic.ShiftCode)
             .DefaultIfEmpty()
                              select new Model_GaneranAndOfficialEmployee()
                              {
                                  EmployeeID = offic.EmployeeID,
                                  EmployeeName = gen.FirstName + " " + gen.LastName,
                                  DepartmentCode = depart.DepartmentName,
                                  DesignationCode = Desig.DesignationName,
                                  ShiftCode = shi.ShiftName,
                                  PhotoUrl = gen.PhotoUrl
                              }).Where(x => x.EmployeeName.Contains(search) || search == null).ToList();
                return result;
            }
        }

        public Model_GaneranAndOfficialEmployee GetEmployee(string EmployeeID)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from offic in context.HRM_EmployeeOfficialInfo.Where(x => x.EmployeeID == EmployeeID).AsEnumerable()
                              .DefaultIfEmpty()
                              from gen in context.HRM_Employee.Where(gen => gen.EmployeeID == offic.EmployeeID).AsEnumerable()
                              .DefaultIfEmpty()
                              from des in context.HRM_Def_Designation.Where(des => des.DesignationCode == offic.DesignationCode).AsEnumerable()
                              .DefaultIfEmpty()
                              from dep in context.HRM_Def_Department.Where(dep => dep.DepartmentCode == offic.DepartmentCode).AsEnumerable()
                              .DefaultIfEmpty()
                              select new Model_GaneranAndOfficialEmployee()
                              {
                                  EmployeeID = offic.EmployeeID,
                                  FirstName = gen.FirstName,
                                  LastName = gen.LastName,
                                  FatherName = gen.FatherName,
                                  MotherName = gen.MotherName,
                                  //FirstNameBangla = gen.FirstNameBangla,
                                  //LastNameBangla = gen.LastNameBangla,
                                  DateOfBirthOrginal = ((DateTime)gen.DateOfBirthOrginal).ToString("dd/MM/yyyy"),
                                  BirthCertificateNo = gen.BirthCertificateNo,
                                  SexCode = gen.SexCode,
                                  BloodGroupCode = gen.BloodGroupCode,
                                  NationalityCode = gen.NationalityCode,
                                  NationalIDNO = gen.NationalIDNO,
                                  ReligionCode = gen.ReligionCode,
                                  MaritalStatusCode = gen.MaritalStatusCode,
                                  Telephone = gen.Telephone,
                                  PersonalEmail = gen.PersonalEmail,
                                  PhotoUrl = gen.PhotoUrl,
                                  CompanyCode = offic.CompanyCode,
                                  BranchCode = offic.BranchCode,
                                  DepartmentCode = offic.DepartmentCode,
                                  DesignationCode = offic.DesignationCode,
                                  ShiftCode = offic.ShiftCode,
                                  EmpTypeCode = offic.EmpTypeCode,
                                  EmploymentNatureId = offic.EmploymentNatureId,
                                  GrossSalary = offic.GrossSalary,
                                  ReportingTo = offic.ReportingTo,
                                  HOD = offic.HOD,
                                  JoiningDate = ((DateTime)offic.JoiningDate).ToString("dd/MM/yyyy"),
                                  EmployeeStatus = offic.EmployeeStatus,
                                  MobileNo = offic.MobileNo,
                                  Email = offic.Email,
                                  EmployeeFullName = gen.FirstName+" "+gen.LastName,
                                  designationNAme = des.DesignationName,
                                  DepartmentName = dep.DepartmentName,
                              }).FirstOrDefault();
                return result;
            }

        }

        public Model_GaneranAndOfficialEmployee GetEmployee2(string EmployeeID)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from gen in context.HRM_Employee.Where(gen => gen.EmployeeID == EmployeeID).AsEnumerable()
                              .DefaultIfEmpty()
                              select new Model_GaneranAndOfficialEmployee()
                              {
                                  EmployeeID = gen.EmployeeID,
                                  FirstName = gen.FirstName,
                                  LastName = gen.LastName,
                                  FatherName = gen.FatherName,
                                  MotherName = gen.MotherName,
                                  //FirstNameBangla = gen.FirstNameBangla,
                                  //LastNameBangla = gen.LastNameBangla,
                                  DateOfBirthOrginal = ((DateTime)gen.DateOfBirthOrginal).ToString("dd/MM/yyyy"),
                                  BirthCertificateNo = gen.BirthCertificateNo,
                                  SexCode = gen.SexCode,
                                  BloodGroupCode = gen.BloodGroupCode,
                                  NationalityCode = gen.NationalityCode,
                                  NationalIDNO = gen.NationalIDNO,
                                  ReligionCode = gen.ReligionCode,
                                  MaritalStatusCode = gen.MaritalStatusCode,
                                  Telephone = gen.Telephone,
                                  PersonalEmail = gen.PersonalEmail,
                                  PhotoUrl = gen.PhotoUrl,
                                  EmployeeFullName = gen.FirstName + " " + gen.LastName,
                              }).FirstOrDefault();
                return result;
            }

        }
        public Model_EmployeeBasicInfo GetEmployeeInfo(string EmployeeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from gn in context.HRM_Employee.DefaultIfEmpty()
                          select new
                          {
                              EmployeeID = gn.EmployeeID,
                              EmployeeName = gn.FirstName + " " +gn.LastName,
                              MobileNo = gn.Telephone,
                              Email = gn.PersonalEmail
                          }).AsEnumerable().Select(a => new Model_EmployeeBasicInfo()
                          {
                              EmployeeID = a.EmployeeID,
                              EmployeeName = a.EmployeeName
                          }).Where(a => a.EmployeeID == EmployeeID).FirstOrDefault();
            return result;
        }

        public bool DeleteCompany(string id)
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var EmployeeID = context.HRM_Employee.FirstOrDefault(x => x.EmployeeID == id);
                if (EmployeeID != null)
                {
                    context.HRM_Employee.Remove(EmployeeID);
                    context.SaveChanges();
                    return true;
                }
                return false;


            }

        }
    }
}
