using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;
 
namespace WebApplication1.Models
{
    public class Dal : IDisposable
    {
        private MySqlConnection conn;
        private bool Connect()
        {
            string connStr = "server=localhost;user=root;database=world;port=3306;password=wuqing";
            conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                // Perform database operations
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            Console.WriteLine("Connected.");
            return true;
        }

        private void Close()
        {
            try
            {
                conn.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if(disposed)
            {
                return;
            }

            if(disposing)
            {
                Close();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public Dal()
        {
            Connect();
        }

        ~Dal()
        {
            Dispose(true);
        }

        public List<string> ReadCityData(string cityName)
        {
            var sql = string.Format("select Name, HeadOfState from Country where Continent='{0}'", cityName);
            var cmd = new MySqlCommand(sql, conn);
            
            var reader = cmd.ExecuteReader();
            var ret = new List<string>();
            while (reader.Read())
            {
                ret.Add(string.Format("{0} -- {1}", reader[0], reader[1]));

            }
            reader.Close();
            return ret;
        }

        public void InsertData(string name, string headOfState, string continent)
        {
            var sql = string.Format("Insert into country (Name, HeadOfState, Continent) values ('{0}', '{1}, '{2}')", name, headOfState, continent);
            var cmd = new MySqlCommand(sql, conn);

            cmd.ExecuteNonQuery();
        }

        public int GetRecordsTotalNum()
        {
            var sql = "select count(*) from Country";
            var cmd = new MySqlCommand(sql, conn);
            var num = cmd.ExecuteScalar();
            if(num != null)
            {
                return Convert.ToInt32(num);
            }
            return 0;
        }
    }
}
