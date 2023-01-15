using System.Data;
using System.Data.SqlClient;

namespace AdoDataSetApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryDb;Integrated Security=True;Connect Timeout=10;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT * FROM books";
                SqlDataAdapter adapter = new(sqlQuery, connection);
                DataSet dataSet = new();
                adapter.Fill(dataSet);
            }
        }
    }
}