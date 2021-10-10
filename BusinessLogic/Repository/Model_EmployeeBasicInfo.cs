using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
  public  class Model_EmployeeBasicInfo
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentCode { get; set; }
        public string DesignationCode { get; set; }
        public string PhotoUrl { get; set; }
        public string JoiningDate { get; set; }
        public string ReportingTo { get; set; }
        public string HeadOfDep { get; set; }
    }
}
