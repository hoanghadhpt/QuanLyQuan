using QuanLyQuan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuan.DAO
{
    public class DAOTable
    {
        private static DAOTable instance;

        public static DAOTable Instance
        {
            get { if (instance == null) instance = new DAOTable(); return DAOTable.instance; }
            private set => instance = value;
        }

        private DAOTable() { }

        public static int TableWith = 105;
        public static int TableHeigh = 105;


        public List<Table> loadTableList()
        {

            List < Table >  tableList = new List<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList");

            foreach (DataRow row in data.Rows)
            {
                Table table = new Table(row);
                tableList.Add(table);

            }

            return tableList;
        }

    }
}
