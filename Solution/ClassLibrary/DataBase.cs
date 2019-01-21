using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary
{
    public class DataBase
    {
        private MySqlConnection conn;
        public DataBase()
        {
            this.conn = GetConnetion();
        }
        public MySqlConnection GetConnetion()
        {
            try
            {
                conn = new MySqlConnection();
                string host = "127.0.0.1";
                string user = "root";
                string password = "1234";
                string db = "test";

                string strConnection = string.Format("server={0}; uid={1}; password={2}; database={3};", host, user, password, db);
                conn.ConnectionString = strConnection;
                //string path = "/public/DBInfo.json";
                //string result = new StreamReader(File.OpenRead(path)).ReadToEnd();

                //JObject jo = JsonConvert.DeserializeObject<JObject>(result);
                //Hashtable map = new Hashtable();
                //foreach (JProperty col in jo.Properties())
                //{
                //    Console.WriteLine("{0} : {1}", col.Name, col.Value);
                //    map.Add(col.Name, col.Value);
                //}
                //string strConnection = string.Format("server={0}; uid={1}; password={2}; database={3};", map["server"], map["user"], map["password"], map["database"]);
                //conn.ConnectionString = strConnection;
                conn.Open();
                return conn;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public bool ConnectionClose()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool NonQuery(string sql)
        {
            try
            {
                if (conn != null)
                {
                    MySqlCommand comm = new MySqlCommand(sql, conn);
                    comm.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public MySqlDataReader Reader(string sql)
        {
            try
            {
                if (conn != null)
                {
                    MySqlCommand comm = new MySqlCommand(sql, conn);
                    return comm.ExecuteReader();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public void ReaderClose(MySqlDataReader reader)
        {
            reader.Close();
        }
    }
}