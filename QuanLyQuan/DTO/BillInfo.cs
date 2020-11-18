using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuan.DTO
{
    public class BillInfo
    {
        private int iD;
        private int billID;
        private int foodID;
        private int count;

        public int ID { get => iD; set => iD = value; }
        public int BillID { get => billID; set => billID = value; }
        public int FoodID { get => foodID; set => foodID = value; }
        public int Count { get => count; set => count = value; }

        public BillInfo(int id, int billID, int foodID, int count)
        {
            this.BillID = billID;
            this.Count = count;
            this.ID = iD;
            this.FoodID = foodID;
        }

        public BillInfo(DataRow row)
        {
            this.BillID = (int)row["idBill"];
            this.Count = (int)row["count"];
            this.ID = (int)row["id"];
            this.FoodID = (int)row["idFood"];
        }

    }
}
