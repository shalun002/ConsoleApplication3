using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    //[Table("MyTable")]
    class Program
    {
        //[Column("Test")]
        public int MyProperty { get; set; }



        private static SqlConnection con = new SqlConnection();
        static void Main(string[] args)
        {
            con.ConnectionString =
                "Data Source=192.168.110.195;Initial Catalog=MCS;User ID=sa;Password=Ev4865";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from newEqipment";

            Task3();


        }
        static void Task1(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            DataTable tbl = ds.Tables.Add("newEquipment");

            SqlDataAdapter da = 
                new SqlDataAdapter(cmd);

            //инициилировать объект TblMapping
            DataTableMapping TblMap;
            DataColumnMapping ColMap = new DataColumnMapping();

            TblMap = da.TableMappings
                       .Add("Table", "newEquipment");

            ColMap = TblMap.ColumnMappings
                           .Add("intEquipmentID",
                                "EquipmentID");
            //ColMap = TblMap.ColumnMappings
            //             .Add("GarageRoom",
            //                  "intGarageRoom");
            //ColMap = TblMap.ColumnMappings
            //           .Add("SerialNo",
            //                "strSerialNo");

            //
            con.Open();
            
            //da.Fill(ds);
            //деление на страницы
            int start = 0;
            int numRec = 10;
            da.Fill(ds, start, numRec, "newEquipment");

            con.Close();


            foreach (DataTable table in ds.Tables)
            {
                //Console.WriteLine(table.TableName);
                //foreach (DataColumn colmn in table.Columns)
                //{
                //    Console.WriteLine("\t{0}", colmn.ColumnName);
                //}

                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine(row["EquipmentID"]);
                    //for (int i = 0; i < row.ItemArray.Length; i++)
                    //{
                    //    Console.WriteLine("\t\t{0}", row.ItemArray[]);
                    //}
                }
            }
        }

        static void Task2()
        {
            DataTableMapping dtm = new DataTableMapping("Table", "newEquipment");
            SqlDataAdapter da = new SqlDataAdapter("select * from newEquipment", con);
            da.TableMappings.Add(dtm);

            DataColumnMapping dcm = new DataColumnMapping("intEquipmentID", "EquipmentID");
            dtm.ColumnMappings.Add(dcm);


            DataSet ds = new DataSet();
            da.Fill(ds);


            //dtm.ColumnMappings.Add("EquipmentID", "intEquipmentID");
          
            //DataSet ds = new DataSet();
            //da.Fill(ds);

            //Console.WriteLine("DataTable name = {0}", ds.Tables[0].TableName);

            //foreach (DataColumn col in ds.Tables["newEquipment"].Columns)
            //{
            //    Console.WriteLine(col.Ordinal);
            //    Console.WriteLine(col.ColumnName);
            //}
            //foreach (DataRow row in ds.Tables["newEquipment"].Rows)
            //{
            //    Console.WriteLine(
            //        "EquipmentID = {0}",
            //        row["EquipmentID"]);
            //}
        }
    
        static void Task3()
        {
            SqlDataAdapter da = 
                new SqlDataAdapter("select * from TablesModel", con);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = ds.Tables[0];
            //добавить новую строку
            DataRow row = dt.NewRow();
            row["strName"] = "Outback";
            row["intManufacturerID"] = 1;
            row["intSMCSFamilyID"] = 1;
            row["strImage"] = "19.png";
            dt.Rows.Add(row);

            //Создать объект
            SqlCommandBuilder cmBuilder = new SqlCommandBuilder(da);
            da.Update(ds);

            ds.Clear();

            da.Fill(ds);

            foreach (DataRow rowItem in ds.Tables[0].Rows)
            {
                for (int i = 0; i < rowItem.ItemArray.Length; i++)
                {
                    Console.Write("\t{0}", rowItem[i]);
                }
                Console.WriteLine("");
            }
        }
    }
}
