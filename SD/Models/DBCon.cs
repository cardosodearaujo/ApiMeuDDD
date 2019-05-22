using System.Data.SqlClient;
using System.Diagnostics;

namespace SD.Models
{
    public class DBCon
    {

        static private SqlConnection connection = null;
        static private string server = "new.database.windows.net";
        static private string database = "BD_New_MeuDDD";
        static private string port = "1433";
        static private string user = "administrador";
        static private string pass = "M1n3Rv@7";
        static private string connetionString = "Data Source=" + server + "," + port + ";MultipleActiveResultSets=true;Initial Catalog=" + database + ";User ID=" + user + ";Password=" + pass;
        static private SqlCommand command = new SqlCommand("", getCon());
        
        public static SqlConnection getCon()
        {
            connection = new SqlConnection(connetionString );
            connection.Open();
            return connection;
        }

        public static void closeCon()
        {
            connection.Close();
        }
        public static SqlDataReader Read(string query)
        {
            Debug.WriteLine(query);
            command.CommandText = query;
            SqlDataReader reader = command.ExecuteReader();
            return reader;
        }

        public static string Exec(string query)
        {
            Debug.WriteLine(query);
            command.CommandText = query;
            string exception;
            try
            {
                command.ExecuteNonQuery();
                exception = "0";
            }
            catch (SqlException ex)
            {
                exception = ex.Number.ToString();

            }
            return exception;
        }

    }
}