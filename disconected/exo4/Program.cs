using System.Data;
using System.Data.Common;
using System.Security.AccessControl;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        string masterConnectionString= "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;";
        string querySelect = "Select * FROM Records;";
        string query = @"
                    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Records]'))
                    BEGIN
                    CREATE TABLE Records (
                    RecordID INT PRIMARY KEY IDENTITY,
                    Titre NVARCHAR(100) UNIQUE, -- Contrainte unique sur Titre
                    Annee INT
                    );
                    INSERT INTO Records (Titre, Annee) VALUES ('Record du plus grand gâteau', 2022);
                    INSERT INTO Records (Titre, Annee) VALUES ('Record de la plus longue barbe', 2021);
                    INSERT INTO Records (Titre, Annee) VALUES ('Record du plus grand nombre de dominos alignés', 2019);
                    END";

        DataSet dataSet = new DataSet();
        using (SqlConnection masterConnection = new SqlConnection(masterConnectionString))
        {
            masterConnection.Open();
            using (SqlCommand cmd = new SqlCommand(query, masterConnection))
            {
                cmd.ExecuteNonQuery();
            };
            SqlDataAdapter dataAdapter = new SqlDataAdapter(querySelect, masterConnection);
            
            dataAdapter.Fill(dataSet, "Records");

            Console.WriteLine("Records à la création de la table:");

            foreach(DataRow row in dataSet.Tables["Records"].Rows)
            {     
                Console.WriteLine($"Titre: {row["Titre"]}");
                Console.WriteLine($"Année: {row["Annee"]}");
                Console.WriteLine("-----------");
            }

            DataTable table = dataSet.Tables["Records"]!;

            table.Rows[0]["Titre"] = "Record du plus grand gâteau aux noisettes";
            Console.WriteLine("Modifications executed");    
        
        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
        dataAdapter.Update(dataSet, "Produits");
        }


    }
}