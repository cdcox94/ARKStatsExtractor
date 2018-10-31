using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Linq;


namespace ARKBreedingStats
{
    public class LinqConnect
    {
        private static string connectionString =
            "address=ark-smb.cqytkhcqzqs9.us-east-2.rds.amazonaws.com,3306;" +
            "Initial Catalog=smb;" +
            "User id=root;" +
            "Password=BobrossRuined94!!;";

        public SmartBreederDB dB = new SmartBreederDB(connectionString) { Log = Console.Out };
    }

    public partial class SmartBreederDB : DataContext
    {
        public Table<Creature> Creatures;
        public SmartBreederDB(string connection) : base(connection)
        {

        }
    }
}
