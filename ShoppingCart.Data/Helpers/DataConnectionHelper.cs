using System.Data.SQLite;
using System.IO;

namespace ShoppingCart.Data.Helpers
{
    public static class DataConnectionHelper
    {
        /// <summary>
        /// Setting DB connection string
        /// </summary>
        /// <returns>connection string</returns>
        public static SQLiteConnection GetDBPath()
        {
            string relativePath = @"ShoppingCart.Data\shoppingcarttask.db";
            var CurrentDirectory = Directory.GetCurrentDirectory();
            string absolutePath = Path.Combine(Path.GetFullPath(Path.Combine(CurrentDirectory, @"../")), relativePath);
            string connectionString = string.Format("Data Source={0};Cache=Shared", absolutePath);
            SQLiteConnection m_dbConnection = new SQLiteConnection(connectionString);
            return m_dbConnection;
        }
    }
}
