using QuanLyQuan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuan.DAO
{
    public class DAOBill
    {
        private static DAOBill instance;

        public static DAOBill Instance
        {
            get
            {
                if (instance == null) instance = new DAOBill();
                return DAOBill.instance;
            }
            private set => instance = value;
        }

        private DAOBill() { }

        /// <summary>
        /// Thành công trả ra Bill ID. Không trả về -1    
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public int GetUncheckBillIDbyTableID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Bill b WHERE b.idTable = "+id+" AND b.STATUS = 0");
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            else
            {
                return -1;
            }
        }

        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("Exec USP_InsertBill @idTable", new object[] { id});
        }

        public int GetMaxIdBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT max(id) FROM dbo.Bill b");
            }
            catch
            {
                return 1;
            }
        }

        public void CheckOut(int id, int discount)
        {
            string query = "UPDATE dbo.Bill SET dateCheckout = GETDATE(), status = 1, discount = "+ discount + " WHERE id = " + id;
            DataProvider.Instance.ExecuteNonQuery(query);
        }
    
    }
}
