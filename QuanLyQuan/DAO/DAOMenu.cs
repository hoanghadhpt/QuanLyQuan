using QuanLyQuan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuan.DAO
{
    public class DAOMenu
    {
        private static DAOMenu instance;


        public static DAOMenu Instance
        {
            get { if (instance == null) instance = new DAOMenu(); return instance; }
            private set => instance = value;
        }

        private DAOMenu() { }

        public List<Menu> GetMenuByTable(int id)
        {
            List<Menu> listMenu = new List<Menu>();

            string query = "SELECT f.name, bi.count, f.price, f.price* bi.count AS[TotalPrice] FROM dbo.BillInfo bi, dbo.Bill b, dbo.Food f WHERE bi.idBill = b.id AND bi.idFood = f.id AND b.idTable = "+id + "  AND b.status = 0";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listMenu.Add(menu);
            }

            return listMenu;
        }

    }
}
