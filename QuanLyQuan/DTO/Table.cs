using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuan.DTO
{
    public class Table
    {
        private int iD;
        private string name;
        private string status;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public string Status { get => status; set => status = value; }

        public Table(int id, string name, string status)
        {
            this.iD = ID;
            this.name= Name;
            this.status = Status;
        }

        public Table(DataRow dataRow)
        {
            this.ID = (int)dataRow["id"];
            this.Name = (string)dataRow["name"];
            this.Status = (string)dataRow["status"];
        }

    }
}
