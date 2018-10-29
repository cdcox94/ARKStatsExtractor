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
            MySqlParameter species = new MySqlParameter("@species", MySqlDbType.VarChar);
            MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar);
            MySqlParameter sex = new MySqlParameter("@sex", MySqlDbType.Enum);
            MySqlParameter stauts = new MySqlParameter("@status", MySqlDbType.Enum);
            MySqlParameter wildhealth = new MySqlParameter("@wildhealth", MySqlDbType.Int32);
            MySqlParameter wildstam = new MySqlParameter("@wildstam", MySqlDbType.Int32);
            MySqlParameter wildoxygen = new MySqlParameter("@wildoxygen", MySqlDbType.Int32);
            MySqlParameter wildfood = new MySqlParameter("@wildfood", MySqlDbType.Int32);
            MySqlParameter wildweight = new MySqlParameter("@wildweight", MySqlDbType.Int32);
            MySqlParameter wilddamage = new MySqlParameter("@wilddamage", MySqlDbType.Int32);
            MySqlParameter wildspeed = new MySqlParameter("@wildspeed", MySqlDbType.Int32);
            MySqlParameter wildtorpor = new MySqlParameter("@wildtorpor", MySqlDbType.Int32);
            MySqlParameter domhealth = new MySqlParameter("@domhealth", MySqlDbType.Int32);
            MySqlParameter domstam = new MySqlParameter("@domstam", MySqlDbType.Int32);
            MySqlParameter domoxygen = new MySqlParameter("@domoxygen", MySqlDbType.Int32);
            MySqlParameter domfood = new MySqlParameter("@domfood", MySqlDbType.Int32);
            MySqlParameter domweight = new MySqlParameter("@domweight", MySqlDbType.Int32);
            MySqlParameter domdamage = new MySqlParameter("@domdamage", MySqlDbType.Int32);
            MySqlParameter domspeed = new MySqlParameter("@domspeed", MySqlDbType.Int32);
            MySqlParameter domtorpor = new MySqlParameter("@domtorpor", MySqlDbType.Int32);
            MySqlParameter tamingEff = new MySqlParameter("@tamingEff", MySqlDbType.Double);
            MySqlParameter imprintingBonus = new MySqlParameter("@imprintingBonus", MySqlDbType.Double);
            MySqlParameter owner = new MySqlParameter("@owner", MySqlDbType.VarChar);
            MySqlParameter imprinterName = new MySqlParameter("@imprinterName", MySqlDbType.VarChar);
            MySqlParameter tribe = new MySqlParameter("@tribe", MySqlDbType.VarChar);
            MySqlParameter server = new MySqlParameter("@server", MySqlDbType.VarChar);
            MySqlParameter note = new MySqlParameter("@note", MySqlDbType.Text);
            MySqlParameter arkid = new MySqlParameter("@arkid", MySqlDbType.Int32);
            MySqlParameter isBred = new MySqlParameter("@isBred", MySqlDbType.Bit);
            MySqlParameter fatherGuid = new MySqlParameter("@fatherGuid", MySqlDbType.Int32);
            MySqlParameter motherGuid = new MySqlParameter("@motherGuid", MySqlDbType.Int32);
            MySqlParameter generation = new MySqlParameter("@generation", MySqlDbType.Int32);
            MySqlParameter color0 = new MySqlParameter("@color0", MySqlDbType.Int32);
            MySqlParameter color1 = new MySqlParameter("@color1", MySqlDbType.Int32);
            MySqlParameter color2 = new MySqlParameter("@color2", MySqlDbType.Int32);
            MySqlParameter color3 = new MySqlParameter("@color3", MySqlDbType.Int32);
            MySqlParameter color4 = new MySqlParameter("@color4", MySqlDbType.Int32);
            MySqlParameter color5 = new MySqlParameter("@color5", MySqlDbType.Int32);

            MySqlParameter growingUntil = new MySqlParameter("@growingUntil", MySqlDbType.DateTime);
            MySqlParameter cooldownUntil = new MySqlParameter("@cooldownUntil", MySqlDbType.DateTime);
            MySqlParameter domesticatedAt = new MySqlParameter("@domesticatedAt", MySqlDbType.DateTime);
            MySqlParameter addedToLibrary = new MySqlParameter("@addedToLibrary", MySqlDbType.DateTime);
            MySqlParameter neutered = new MySqlParameter("@neutered", MySqlDbType.Bit);
            MySqlParameter mutationsMaternal = new MySqlParameter("@mutationsMaternal", MySqlDbType.Int32);
            MySqlParameter mutationsPaternal = new MySqlParameter("@mutationsPaternal", MySqlDbType.Int32);
            MySqlParameter placeholder = new MySqlParameter("@placeholder", MySqlDbType.Bit);
            MySqlParameter tags = new MySqlParameter("@tags", MySqlDbType.Text);



            string cmdString = "INSERT INTO Creatures Values (@guid, @species, @name, @sex, @status, @wildhealth, @wildstam, @wildoxygen, @wildfood, @wildweight, @wilddamage, @wildspeed, @wildtorpor, @domhealth, @domstam, @domoxygen, @domfood, @domweight, @domdamage, @domspeed, @domtorpor, @tamingEff, @imprintingBonus, @owner, @imprinterName, @tribe, @server, @note, @arkid, @isBred, @fatherGuid, @motherGuid, @generation, @color0, @color1, @color2, @color3, @color4, @color5, @growingUntil, @cooldownUntil, @domesticatedAt, @addedToLibrary, @neutered, @mutationsMaternal, @mutationsPaternal, @placeholder, @tags)";
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

