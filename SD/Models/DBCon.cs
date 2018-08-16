using System.Data.SqlClient;
using System.Diagnostics;

namespace SD.Models
{
    public class DBCon
    {

        static private SqlConnection connection = null;
        static private string server = "noc.outview.com.br";
        static private string database = "SD";
        static private string port = "14333";
        static private string user = "sa";
        static private string pass = "123*abc";
        static private string connetionString = "Data Source=" + server + "," + port + ";MultipleActiveResultSets=true;Initial Catalog=" + database + ";User ID=" + user + ";Password=" + pass;
        static private SqlCommand command = new SqlCommand("", getCon());
        static private SqlDataReader reader;

        
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
            reader = command.ExecuteReader();
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