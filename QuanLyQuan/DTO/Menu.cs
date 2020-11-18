using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuan.DTO
{
    public class Menu
    {

        public Menu(string foodName, int foodCount, float price, float totalPrice = 0)
        {
            this.FoodName = foodName;
            this.FoodCount = foodCount;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }

        public Menu(DataRow row)
        {
            this.FoodName = row["name"].ToString();
            this.FoodCount = (int)row["count"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
            this.TotalPrice = (float)Convert.ToDouble(row["totalprice"].ToString()); 
        }

        private float totalPrice;
        private float price;
        private int foodCount;
        private string foodName;

        public string FoodName { get => foodName; set => foodName = value; }
        public int FoodCount { get => foodCount; set => foodCount = value; }
        public float Price { get => price; set => price = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
    }
}
