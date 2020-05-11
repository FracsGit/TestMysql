using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace TestMysql
{
    class Program
    {
        public static string connectionString = "User ID = root; Password = 123; Host = localhost; Port = 3306;Database = zero;Charset = utf8";
        public static MySqlConnection connection = null;
        public static string table = "chartbl"; //character是mysql保留字，表明需用database.table，如zero.character

        static void Main()
        {         
            Read();
            Count();
            Insert();
            Delete();
            Update();
            DataSet ds = new DataSet();
            Get(ds, "select * from " + table);
            Update(ds, table);
            DataTable dt = new DataTable();
            Get(dt, "select * from " + table);
            Update(dt, table);
            Console.Read();
        }

        static void Open()
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void Close()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        public static void Read()
        {
            Open();
            try
            {
                string para1 = "1";
                string para2 = "'志津香'";
                string sql = "select * from " + table + " where id=1 and name='志津香'";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("para1", para1);
                cmd.Parameters.AddWithValue("para2", para2);

                MySqlDataReader reader = cmd.ExecuteReader();   //执行ExecuteReader()返回一个MySqlDataReader对象
                while (reader.Read())   //初始索引是-1，执行读取下一行数据，返回值是bool
                {
                    //Console.WriteLine(reader[0].ToString() + reader[1].ToString() + reader[2].ToString());
                    //Console.WriteLine(reader.GetInt32(0)+reader.GetString(1)+reader.GetString(2));
                    Console.WriteLine(reader.GetInt32("id") + reader.GetString("name") + reader.GetString("description")
                        + reader.GetString("dialog") + reader.GetString("element") + reader.GetString("race")
                        + reader.GetString("gender") + reader.GetString("rarity") + reader.GetString("nature")
                        + reader.GetString("occupation") + reader.GetString("ability1") + reader.GetString("ability2")
                        + reader.GetString("con") + reader.GetString("str") + reader.GetString("dex")
                        + reader.GetString("itg") + reader.GetString("def") + reader.GetString("res")
                        + reader.GetString("skill0") + reader.GetString("skill1") + reader.GetString("skill2")
                        + reader.GetString("skill3") + reader.GetString("skill4"));
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Close();
        }

        //执行查询，并返回查询结果集中第一行的第一列。所有其他的列和行将被忽略。select语句无记录返回时，ExecuteScalar()返回NULL值
        public static void GetScalar(string cmdString)
        {
            Open();
            try
            {
                //string cmdString = "select name from chartbl";
                MySqlCommand cmd = new MySqlCommand(cmdString, connection);
                Object result = cmd.ExecuteScalar();    //执行查询，并返回查询结果集中第一行的第一列。所有其他的列和行将被忽略。select语句无记录返回时，ExecuteScalar()返回NULL值
                if (result != null)
                {
                    Console.WriteLine(result);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Close();
        }

        //
        public static void Count()
        {
            Open();
            try
            {
                string cmdString = "select count(*) from " + table;
                GetScalar(cmdString);
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Close();
        }

        public static void Insert()
        {
            Open();
            try
            {
                string sql = "insert into " + table + " (name,str,skill9) values('哈泽露','2.4','魔力球')";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                int result = cmd.ExecuteNonQuery();//执行成功返回受影响的数据的行数，返回1可做true判断。执行失败不返回任何数据，报错，下面代码都不执行
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Close();
        }

        public static void Delete()
        {
            Open();
            try
            {
                string sql = "delete from " + table + " where id=5";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                int result = cmd.ExecuteNonQuery();//执行成功返回受影响的数据的行数，返回1可做true判断。执行失败不返回任何数据，报错，下面代码都不执行
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Close();
        }

        public static void Update()
        {
            Open();
            try
            {
                string sql = "update " + table + " set dex=3.1, skill0='暗影球' where id=3";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                int result = cmd.ExecuteNonQuery();//执行成功返回受影响的数据的行数，返回1可做true判断。执行失败不返回任何数据，报错，下面代码都不执行
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Close();
        }

        public static void Get(DataSet ds, string sqlString)
        {
            Open();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sqlString, connection);
                da.Fill(ds);
                //Console.WriteLine(ds.Tables[0].Rows[0].ItemArray[1]);
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Close();
        }

        public static void Get(DataTable dt, string sqlString)
        {
            Open();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sqlString, connection);
                da.Fill(dt);
                //Console.WriteLine(dt.Rows[0].ItemArray[2]);
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Close();
        }

        public static void Update(DataSet ds, string tbl)
        {
            Open();
            string sqlString = "select * from " + tbl;
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sqlString, connection);
                da.Update(ds);
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Close();
        }

        public static void Update(DataTable dt, string tbl)
        {
            Open();
            string sqlString = "select * from " + tbl;
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sqlString, connection);
                da.Update(dt);
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Close();
        }
    }
}