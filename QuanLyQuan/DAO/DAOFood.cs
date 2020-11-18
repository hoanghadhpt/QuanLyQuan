using QuanLyQuan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuan.DAO
{
    public class DAOFood
    {
        
            private static DAOFood instance;

            public static DAOFood Instance
            {
                get { if (instance == null) instance = new DAOFood(); return DAOFood.instance; }
                private set => instance = value;
            }

            private DAOFood() { }



            public List<Food> GetFoodCategoryID(int id)
            {
                List<Food> list = new List<Food>();

                string query = "select * from Food where idCategory = " + id;
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                foreach (DataRow item in data.Rows)
                {
                    Food food = new Food(item);
                    list.Add(food);
                }

                return list;
            }


        }
}
