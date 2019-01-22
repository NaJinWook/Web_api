using ClassLibrary;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Modules
{
    public class Commons
    {
        public int nNo { get; set; }
        public string nTitle { get; set; }
        public string nContents { get; set; }
        public string delYn { get; set; }
    }

    public class CRUD
    {
        public static ArrayList GetSelect()
        {
            DataBase db = new DataBase();
            string sql = "select * from Notice where delYn = 'N';";
            ArrayList list = new ArrayList();
            MySqlDataReader sdr = db.Reader(sql);
            while (sdr.Read())
            {
                Hashtable ht = new Hashtable();
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    ht.Add(sdr.GetName(i), sdr.GetValue(i));
                }
                list.Add(ht);
            }
            sdr.Close();
            db.ConnectionClose();
            return list;
        }

        public static ArrayList GetInsert(Commons cm)
        {
            DataBase db = new DataBase();
            string sql = string.Format("insert into Notice (nTitle, nContents) values ('{0}','{1}');", cm.nTitle, cm.nContents);
            if (db.NonQuery(sql))
            {
                return GetSelect();
            }
            else
            {
                return new ArrayList();
            }
        }
        
        public static ArrayList GetUpdate(Commons cm)
        {
            DataBase db = new DataBase();
            string sql = string.Format("update Notice set nTitle = '{1}', nContents = '{2}' where nNo = {0};", cm.nNo, cm.nTitle, cm.nContents);
            if (db.NonQuery(sql))
            {
                return GetSelect();
            }
            else
            {
                return new ArrayList();
            }
        }

        public static ArrayList GetDelete(Commons cm)
        {
            DataBase db = new DataBase();
            string sql = string.Format("update Notice set delYn = 'Y' where nNo = {0};", cm.nNo);
            if (db.NonQuery(sql))
            {
                return GetSelect();
            }
            else
            {
                return new ArrayList();
            }
        }
    }
}
