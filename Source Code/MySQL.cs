using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
namespace IniToSQL
{

    class MySQL
    {
        private MySqlConnection con;
        private string connectStr;

        /// <summary>
        /// 
        /// Intializes the MySQL Class.
        /// 
        /// </summary>
        /// <param name="server">The server to connnect to.</param>
        /// <param name="username">The username of the account.</param>
        /// <param name="password">The password for the account.</param>
        /// <param name="dbname">The database name.</param>
        public MySQL(string server, string username, string password, string dbname)
        {
            connectStr = string.Format("SERVER={0}; DATABASE={1}; UID={2}; PASSWORD={3}", server, username, password, dbname);

            con = new MySqlConnection(connectStr);
        }

        public int Connect()
        {
            try
            {
                con.Open();
            }
            catch(MySqlException ex)
            {
                switch(ex.Number)
                {
                    case 0:
                        return 1;
                    case 1:
                        return 2;
                }
            }
            return 0;
        }

        public bool Disconnect()
        {
            try
            {
                con.Close();
                return true;
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
