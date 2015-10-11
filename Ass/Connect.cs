using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace Ass
{
    class Connect
    {
        public SqlConnection conn;
        public void GetConnect()
        {
            string conectstring = ConfigurationManager.ConnectionStrings["Test"].ConnectionString.ToString();
            conn = new SqlConnection(conectstring);
        }
        public void Open()
        {
            try
            {
                conn.Open();
            }
            catch
            {
                conn.Close();
                conn.Open();
            }
        }
        public void Close()
        {
            conn.Close();
        }
    }
}
