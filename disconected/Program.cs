// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using System.Data;
using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using static DataInitializer;

class Program
{
    static void Main()
    {
        string masterConnectionString= "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;";
        string query =  "SELECT * FROM Book";
        using (SqlConnection masterConnection = new SqlConnection(masterConnectionString))
        {
            InitDatabase(masterConnection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, masterConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "Book");
            foreach (DataRow row in dataSet.Tables["Book"].Rows)
            {
                Console.WriteLine(row[""]);
            }
        }
    }
}
