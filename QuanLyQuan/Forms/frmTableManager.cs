using QuanLyQuan.DAO;
using QuanLyQuan.DTO;
using QuanLyQuan.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuan
{
    public partial class frmTableManager : Form
    {
        public frmTableManager()
        {
            InitializeComponent();
            loadTable();
            LoadCategory();
        }

        #region Evens

        private void Btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lvsBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }



        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdmin f = new frmAdmin();
            f.ShowDialog();
        }


        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
            {
                return;
            }

            Category selected = cb.SelectedItem as Category;

            id = selected.ID;

            LoadListFoodByCategoryID(id);
        }


        private void btnCheckout_Click(object sender, EventArgs e)
        {
            Table table = lvsBill.Tag as Table;
            int idBill = DAOBill.Instance.GetUncheckBillIDbyTableID(table.ID);
            if (idBill == -1)
            {

            }
            else
            {
                if (MessageBox.Show("Bạn có muốn thanh toán bàn " + table.Name, "Thông báo...", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    var x = Convert.ToDouble(lbTotalPrice.Text.Trim());
                    var FinalPrice = x - (x / 100 * (double)nmDiscount.Value);


                    report(table.Name, FinalPrice.ToString("#,##0"));
                    frmReport frmReport = new frmReport();
                    frmReport.webBrowser1.Navigate(Environment.CurrentDirectory + "/Report.html");
                    frmReport.ShowDialog();


                    DAOBill.Instance.CheckOut(idBill, (int)nmDiscount.Value);
                    ShowBill(table.ID);
                    loadTable();
                }
            }
        }


        #endregion



        #region Methos

        private void report(string tableName, string final)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("vi-VN");
            string htm = File.ReadAllText("Reports/temp.html");
            htm = htm.Replace("txtTable", tableName);
            htm = htm.Replace("txtTime", DateTime.Now.ToString(culture));
            htm = htm.Replace("valTotal", lbTotalPrice.Text + "đ");
            htm = htm.Replace("valDiscount", nmDiscount.Value + "%");
            htm = htm.Replace("valFinal", final);
            string rowAll = "";
            foreach (ListViewItem itemRow in this.lvsBill.Items)
            {
                string row = "<tr>";
                for (int i = 0; i < itemRow.SubItems.Count; i++)
                {
                     row += "<td align=\"right\">" + itemRow.SubItems[i].Text +"</td>";
                }
                row += "</tr>\r\n";
                rowAll += row;
            }
            htm = htm.Replace("<@@>", rowAll);
            File.WriteAllText("Report.html", htm);

        }

        private void loadTable()
        {
            flpTable.Controls.Clear();

            List<Table> tables = DAOTable.Instance.loadTableList();
            foreach (Table item in tables)
            {
                Button btn = new Button() { Width = DAOTable.TableWith, Height = DAOTable.TableHeigh };
                btn.Image = Properties.Resources.Food_Dome_icon_64;
                btn.ImageAlign = ContentAlignment.TopCenter;
                btn.Text = item.Name + " - " + item.Status;
                btn.TextAlign = ContentAlignment.BottomCenter;

                btn.Click += Btn_Click;
                btn.Tag = item;

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.FromArgb(244, 252, 192);
                        break;
                    default:
                        btn.BackColor = Color.FromArgb(232, 205, 250);
                        break;
                }

                flpTable.Controls.Add(btn);
            }
        }

        void ShowBill(int id)
        {
            float totalPrice = 0;
            lvsBill.Items.Clear();
            List<DTO.Menu> listBillInfo = DAOMenu.Instance.GetMenuByTable(id);
            foreach (DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.FoodCount.ToString());
                lsvItem.SubItems.Add(item.Price.ToString("#,##0"));
                lsvItem.SubItems.Add(item.TotalPrice.ToString("#,##0"));
                lvsBill.Items.Add(lsvItem);
                totalPrice += item.TotalPrice;

            }
            lbTotalPrice.Text = totalPrice.ToString("#,##0");

        }

        private void LoadCategory()
        {
            List<Category> listCategory = DAOCategory.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";

        }

        private void LoadListFoodByCategoryID(int id)
        {
            List<Food> food = DAOFood.Instance.GetFoodCategoryID(id);
            cbFood.DataSource = food;
            cbFood.DisplayMember = "Name";
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lvsBill.Tag as Table;
            int idBill = DAOBill.Instance.GetUncheckBillIDbyTableID(table.ID);
            int foodID = (cbFood.SelectedItem as Food).ID;
            int count = (int)nmFoodCount.Value;

            if (idBill == -1)
            {
                DAOBill.Instance.InsertBill(table.ID);
                //DAOBillInfo.Instance.InsertBillInfo(DAOBill.Instance.GetMaxIdBill(), foodID, count);
                if(count > 0)
                {
                    idBill = DAOBill.Instance.GetUncheckBillIDbyTableID(table.ID);
                    DAOBillInfo.Instance.InsertBillInfo(idBill, foodID, count);
                }
            }
            else
            {
                DAOBillInfo.Instance.InsertBillInfo(idBill, foodID, count);

            }

            ShowBill(table.ID);
            nmFoodCount.Value = 1;
            loadTable();
        }



        #endregion

        

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {

        }

        private void nmDiscount_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void cbSwitchTable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
