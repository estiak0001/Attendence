﻿using PXLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLogic.Repository
{
    
    public class Crud_Core_UserInfo
    {
        PXlibrary Pxlib = new PXlibrary();
        public List<Model_Core_UserInfo> GetAllInfo()
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from mo in context.Core_UserInfo
                              from em in context.HRM_Employee.Where(hrm => hrm.EmployeeID == mo.EmployeeID.ToString()).DefaultIfEmpty()
                              select new
                              {
                                  username = mo.username,
                                  EmployeeID = mo.EmployeeID,
                                  EmployeeName = em.FirstName+" "+em.LastName,
                                  AccessCode = mo.AccessCode,
                                  Role = mo.Role,
                                  

                              }).AsEnumerable().Select(a => new Model_Core_UserInfo()
                              {
                                  username = a.username.ToString(),
                                  EmployeeID = a.EmployeeID,
                                  EmployeeName=a.EmployeeName,
                                  AccessCode = a.AccessCode,
                                  Role = a.Role
                              }).ToList();



                return result;
            }
        }


        public string SaveInfo(Model_Core_UserInfo model, string LoginEmployeeID)
        {

            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            Core_UserInfo coreCom = new Core_UserInfo();
            coreCom.username = model.username;
            coreCom.AccessCode= model.AccessCode;
            coreCom.EmployeeID =model.EmployeeID;
            coreCom.Role =model.Role;

            string Userpassword = "";
            //Pxlib.(ref Userpassword, txtPassword.Text.Trim());
            Pxlib.PXEncode(ref Userpassword, model.password);

            coreCom.password= Userpassword;
            coreCom.EntryDate = DateTime.Now;
            coreCom.LUser =LoginEmployeeID;
            coreCom.LDate = DateTime.Now;            
            context.Core_UserInfo.Add(coreCom);
            context.SaveChanges();
            return coreCom.EmployeeID;
        }
        public Model_Core_UserInfo GetInfo(string EmployeeID)
        {
            
            var db = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from psi in db.Core_UserInfo
                          .Where(psi => psi.EmployeeID ==EmployeeID).DefaultIfEmpty().AsEnumerable()
                          select new
                          {
                              username = psi.username,
                              EmployeeID = psi.EmployeeID,
                              AccessCode = psi.AccessCode,
                              password = psi.password,
                              Role = psi.Role,
                          }).AsEnumerable().Select(a => new Model_Core_UserInfo()
                          {
                              username = a.username,
                              EmployeeID =a.EmployeeID,
                              AccessCode = a.AccessCode,
                              password = a.password,
                              Role = a.Role,
                          }).FirstOrDefault();
            return result;
        }

        public Model_EmployeeBasicInfo GetEmployeeInfo(string EmployeeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from hrm in context.HRM_EmployeeOfficialInfo
                          from gn in context.HRM_Employee.Where(gn=>gn.EmployeeID==hrm.EmployeeID).DefaultIfEmpty()
                          from depart in context.HRM_Def_Department.Where(depart => depart.DepartmentCode == hrm.DepartmentCode)
       .DefaultIfEmpty()
                          from Desig in context.HRM_Def_Designation.Where(Desig => Desig.DesignationCode == hrm.DesignationCode)
     .DefaultIfEmpty()
                          select new
                          {
                              EmployeeID = hrm.EmployeeID,
                              EmployeeName = gn.FirstName+gn.LastName,                         
                              MobileNo = hrm.MobileNo,
                              Email = hrm.Email,
                              DepartmentCode = depart.DepartmentName,
                              DesignationCode = Desig.DesignationName,
                          }).AsEnumerable().Select(a => new Model_EmployeeBasicInfo()
                          {
                              EmployeeID = a.EmployeeID,
                              EmployeeName=a.EmployeeName,
                              DepartmentCode=a.DepartmentCode,
                              DesignationCode=a.DesignationCode
                              }).Where(a => a.EmployeeID == EmployeeID).FirstOrDefault();
                return result;
            
        }


        public bool UpdateInfo(string id, Model_Core_UserInfo model)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.Core_UserInfo.FirstOrDefault(x => x.EmployeeID == model.EmployeeID);
            if (result != null)
            {               
                result.username = model.username;
                result.AccessCode = model.AccessCode;
                result.Role = model.Role;
                string Userpassword = "";                
                Pxlib.PXEncode(ref Userpassword, model.password);
                result.password = Userpassword;
                result.ModifyDate = DateTime.Now;
                
            }
            context.SaveChanges();
            return true;
        }
        public bool DeleteInfo(string EmployeeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.Core_UserInfo.FirstOrDefault(x => x.EmployeeID == EmployeeID);
            if (result != null)
            {
                context.Core_UserInfo.Remove(result);
                context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
