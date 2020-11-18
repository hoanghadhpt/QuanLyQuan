using QuanLyQuan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuan.DAO
{
    public class DAOBillInfo
    {
        private static DAOBillInfo instance;

        public static DAOBillInfo Instance
        {
            get
            {
                if (instance == null) instance = new DAOBillInfo(); return DAOBillInfo.instance;
            }

            set => instance = value;
        }

        private DAOBillInfo() { }

        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.BillInfo bi WHERE bi.idBill = "+id);

            foreach (DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                listBillInfo.Add(info);
            }

            return listBillInfo;
        }

        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("exec USP_InsertBillInfo @idBill , @idFood , @idCount", new object[] { idBill, idFood, count });
        }

    }
}
