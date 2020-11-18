using QuanLyQuan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuan.DAO
{
    public class DAOCategory
    {
        private static DAOCategory instance;

        public static DAOCategory Instance
        {
            get { if (instance == null) instance = new DAOCategory(); return DAOCategory.instance; }
            private set => instance = value;
        }

        private DAOCategory() { }

        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();

            string query = "select * from foodCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                list.Add(category);
            }

            return list;
        }

        

    }
}
