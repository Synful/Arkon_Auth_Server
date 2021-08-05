using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server.ViewModel {
    public class Database {
        private MySqlConnection con;
        
        private static Database _instance;
        public static Database instance {
            get {
                if(_instance == null) {
                    _instance = new Database();
                }
                return _instance;
            }
        }

        public Database() {
            con = new MySqlConnection("server=107.180.50.169;database=genisys;uid=synful;pwd=g,kDS4ig=)+S;");
        }

        public bool ValidKey(string lic) {
            con.Open();

            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM users WHERE license = '{lic}'", con);
            MySqlDataReader rdr = cmd.ExecuteReader();

            bool ret = rdr.HasRows;

            rdr.Close();
            con.Close();

            return ret;
        }

        public string GetUsername(string lic) {
            con.Open();

            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM users WHERE license = '{lic}'", con);
            MySqlDataReader rdr = cmd.ExecuteReader();

            rdr.Read();
            string ret = rdr["username"].ToString();

            rdr.Close();
            con.Close();

            return ret;
        }
    }
}
