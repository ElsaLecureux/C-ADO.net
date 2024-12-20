
using System.Data;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        string masterConnectionString= "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;";
        string querySelect = "Select * FROM Etudiants;";
        string query = @"
                    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Etudiants]'))
                    BEGIN
                    CREATE TABLE Etudiants (
                    Id INT PRIMARY KEY IDENTITY,
                    Nom NVARCHAR(50),
                    Age INT,
                    Classe NVARCHAR(50)
                    );
                    INSERT INTO Etudiants (Nom, Age, Classe) VALUES
                    ('Dinel', 18, 'Première');
                    INSERT INTO Etudiants (Nom, Age, Classe) VALUES
                    ('Charles', 17, 'Seconde');
                    INSERT INTO Etudiants (Nom, Age, Classe) VALUES
                    ('Durand', 19, 'Terminale');
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
            
            dataAdapter.Fill(dataSet, "Etudiants");

            Console.WriteLine("Etudiants à la création de la table:");

            foreach(DataRow row in dataSet.Tables["Etudiants"].Rows)
            {
                Console.WriteLine(row["Nom"]);
                Console.WriteLine(row["Age"]);
            }

        }

            DataTable table = dataSet.Tables["Etudiants"]!;

            DataRow newRow = table.NewRow();
            newRow["Nom"] = "James";
            newRow["Age"] = 18;
            newRow["Classe"] = "Seconde";
            table.Rows.Add(newRow);

            table.Rows[0]["Age"] = 17;

            table.Rows[1].Delete();

            Console.WriteLine("Après modification:");

            foreach(DataRow row in dataSet.Tables["Etudiants"]!.Rows)
            {
                if(row.RowState != DataRowState.Deleted){
                Console.WriteLine(row["Nom"]);
                Console.WriteLine(row["Age"]); 
                }
            }



        
    }
}
