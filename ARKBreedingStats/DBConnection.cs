using MySql.Data;
using MySql.Data.MySqlClient;

namespace ARKBreedingStats
{
    public class DBConnection
    {
        private DBConnection()
        {
        }

        private string databaseName = string.Empty;
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        private string serverString = string.Empty;
        public string ServerString
        {
            get { return serverString; }
            set { serverString = value; }
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (string.IsNullOrEmpty(databaseName))
                    return false;
                string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", serverString, databaseName, UserName, Password);
                connection = new MySqlConnection(connstring);
                connection.Open();
            }

            return true;
        }

        public void Close()
        {
            connection.Close();
            connection = null;
        }

        public void SendCreature(Creature creature)
        {
            MySqlParameter guid = new MySqlParameter("@guid", MySqlDbType.Int32);
            MySqlParameter species = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter name = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter sex = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter stauts = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter wildhealth = new MySqlParameter("@", MySqlDbType.Double);
            MySqlParameter wildstam = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter wildoxygen = new MySqlParameter("@", MySqlDbType.Double);
            MySqlParameter wildfood = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter wildweight = new MySqlParameter("@", MySqlDbType.Double);
            MySqlParameter wilddamage = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter wildspeed = new MySqlParameter("@", MySqlDbType.Double);
            MySqlParameter wildtorpor = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter domhealth = new MySqlParameter("@", MySqlDbType.Double);
            MySqlParameter domstam = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter domoxygen = new MySqlParameter("@", MySqlDbType.Double);
            MySqlParameter domfood = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter domweight = new MySqlParameter("@", MySqlDbType.Double);
            MySqlParameter domdamage = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter domspeed = new MySqlParameter("@", MySqlDbType.Double);
            MySqlParameter domtorpor = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter tamingEff = new MySqlParameter("@", MySqlDbType.Double);
            MySqlParameter imprintingBonus = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter owner = new MySqlParameter("@", MySqlDbType.Double);
            MySqlParameter imprinterName = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter tribe = new MySqlParameter("@", MySqlDbType.Double);
            MySqlParameter server = new MySqlParameter("@", MySqlDbType.Double);
            MySqlParameter note = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter arkid = new MySqlParameter("@", MySqlDbType.Double);
            MySqlParameter isBred = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter fatherGuid = new MySqlParameter("@", MySqlDbType.Double);
            MySqlParameter motherGuid = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter generation = new MySqlParameter("@", MySqlDbType.Double);
            MySqlParameter color0 = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter color1 = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter color2 = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter color3 = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter color4 = new MySqlParameter("@", MySqlDbType.VarChar);
            MySqlParameter color5 = new MySqlParameter("@", MySqlDbType.VarChar);

            MySqlParameter growingUntil = new MySqlParameter("@growingUntil", MySqlDbType.);
            MySqlParameter cooldownUntil = new MySqlParameter("@cooldownUntil", MySqlDbType.);
            MySqlParameter domesticatedAt = new MySqlParameter("@domesticatedAt", MySqlDbType.);
            MySqlParameter addedToLibrary = new MySqlParameter("@addedToLibrary", MySqlDbType.);
            MySqlParameter neutered = new MySqlParameter("@neutered", MySqlDbType.);
            MySqlParameter mutationsMaternal = new MySqlParameter("@mutationsMaternal", MySqlDbType.);
            MySqlParameter mutationsPaternal = new MySqlParameter("@mutationsPaternal", MySqlDbType.);
            MySqlParameter placeholder = new MySqlParameter("@placeholder", MySqlDbType.);
            MySqlParameter tags = new MySqlParameter("@tags", MySqlDbType.);



            string cmdString = "INSERT INTO Creatures (name, tamingeff) Values (@param1, @param2)";
            

            if (IsConnect())
            {
                MySqlCommand cmd = new MySqlCommand(cmdString, connection);

                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                cmd.ExecuteNonQuery();
            }
        }

        public string GetCreature()
        {
            string retString = "";
            MySqlParameter param1 = new MySqlParameter("@Param1", MySqlDbType.VarChar, 5);
            MySqlParameter param2 = new MySqlParameter("@Param2", MySqlDbType.Double);

            param1.Value = "Bilbo";
            param2.Value = 3.2;

            string cmdString = "SELECT id, tamingeff, name FROM testtable WHERE id >= 10";


            if (IsConnect())
            {
                MySqlDataReader reader = null;
                MySqlCommand cmd = new MySqlCommand(cmdString, connection);

                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        retString += reader[i];
                        retString += ", ";
                    }
                    retString += "\r\n";
                }

                reader.Close();
            }

            return retString;
        }


    }
}

