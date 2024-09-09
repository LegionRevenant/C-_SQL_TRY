using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;

namespace Contacts
{
    class Connections
    {
        SqlConnection conn;
        public SqlConnection getConnect()
        {
            conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial " +
                "Catalog=Contacts;Integrated Security=True;");
            return conn;
        }
    } 

}

