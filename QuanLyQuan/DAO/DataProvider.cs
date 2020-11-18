using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Sql;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QuanLyQuan.DAO
{
    public class DataProvider
    {

        private static DataProvider instance;

        public static DataProvider Instance
        {
            get{if (instance == null) instance = new DataProvider();return DataProvider.instance;}
            private set { DataProvider.instance = value; }
        }
        private DataProvider() { }
               
        private string connectionString = "Data Source=.\\SqlExpress;Initial Catalog=QuanLyQuan;Integrated Security=True";

        

        public DataTable ExecuteQuery(string Query, object[] parameters = null)
        {

            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Query, connection);

                if (parameters != null)
                {
                    string[] listPara = Query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameters[i]);
                            i++;
                        }
                    }
                }
                else
                {

                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

        public int ExecuteNonQuery(string Query, object[] parameters = null)
        {

            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Query, connection);

                if (parameters != null)
                {
                    string[] listPara = Query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameters[i]);
                            i++;
                        }
                    }
                                }

                data = command.ExecuteNonQuery();
                
                connection.Close();
            }
            return data;
        }

        public object ExecuteScalar(string Query, object[] parameters = null)
        {

            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Query, connection);

                if (parameters != null)
                {
                    string[] listPara = Query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameters[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }
            return data;
        }

    }
}
