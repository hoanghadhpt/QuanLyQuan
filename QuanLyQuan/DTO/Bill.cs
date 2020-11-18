using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuan.DTO
{
    public class Bill
    {

        

        private int iD;
        private DateTime? dateCheckin;
        private DateTime? dateCheckout;
        private int status;


        public int ID { get => iD; set => iD = value; }
        public DateTime? DateCheckin { get => dateCheckin; set => dateCheckin = value; }
        public int Status { get => status; set => status = value; }
        public DateTime? DateCheckout { get => dateCheckout; set => dateCheckout = value; }


        public Bill(int id, DateTime? dateCheckin, DateTime? dateCheckout, int status)
        {
            this.ID = id;
            this.DateCheckin = dateCheckin;
            this.DateCheckout = dateCheckout;
            this.Status = status;
        }

        public Bill(DataRow row)
        {
            this.iD = (int)row["id"];
            this.DateCheckin = (DateTime?)row["dateCheckin"];

            var DateCheckOutTemp = row["dateCheckout"];
            if (DateCheckOutTemp.ToString() != "")
            {
                this.DateCheckout = (DateTime?)DateCheckOutTemp;
            }

           
            this.Status = (int)row["Status"];
        }
    }
}
