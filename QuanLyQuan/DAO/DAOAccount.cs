using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuan.DAO
{
    public class DAOAccount
    {
        private static DAOAccount instance;

        public static DAOAccount Instance
        {
            get { if (instance == null) instance = new DAOAccount(); return instance; }
            private set { instance = value; }
        }

        private DAOAccount(){}

        public bool Login(string username, string password)
        {

            string query = "EXEC dbo.USP_Login  @username , @password";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password});

            return result.Rows.Count>0 ;
        }

    }
}
