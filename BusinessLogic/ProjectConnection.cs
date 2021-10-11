using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FJS
{
    public class ProjectConnection
    {

        public static SqlConnection conn = null;
        public void connection_today()
        {
            conn = new SqlConnection("Data Source=VMD51868;Initial Catalog=GCTL_ERP_DB_MVC_06_27_2;User ID=sa;Password=GCTL#123");
            //conn = new SqlConnection("Data Source=DESKTOP-PIK65JK;Initial Catalog=Att_DB;User ID=sa;Password=Walton@2021");
        }

        public ProjectConnection()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
