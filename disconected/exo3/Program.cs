using System.Data;
using System.Data.Common;
using System.Security.AccessControl;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        string masterConnectionString= "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;";
        string querySelect = "Select * FROM Produits;";
        string query = @"
                    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Produits]'))
                    BEGIN
                    CREATE TABLE Produits (
                    Id INT PRIMARY KEY IDENTITY,
                    NomProduit NVARCHAR(100),
                    Prix DECIMAL(10, 2),
                    QuantiteEnStock INT
                    );
                    INSERT INTO Produits (NomProduit, Prix,
                    QuantiteEnStock) VALUES ('Telephone', 350.00, 50);
                    INSERT INTO Produits (NomProduit, Prix,
                    QuantiteEnStock) VALUES ('PC', 2000.00, 30);
                    INSERT INTO Produits (NomProduit, Prix,
                    QuantiteEnStock) VALUES ('Imprimante', 50.50, 200);
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
            
            dataAdapter.Fill(dataSet, "Produits");

            Console.WriteLine("Produits à la création de la table:");

            foreach(DataRow row in dataSet.Tables["Produits"].Rows)
            {     
                Console.WriteLine($"Nom du produit: {row["NomProduit"]}");
                Console.WriteLine($"Prix: {row["Prix"]}");
                Console.WriteLine($"Quantité: {row["QuantiteEnStock"]}");
                Console.WriteLine("-----------");
            }

            DataTable table = dataSet.Tables["Produits"]!;

            table.Rows[0]["Prix"] = 255.99;
            Console.WriteLine("Modifications executed");

            DataRow newRow = table.NewRow();
            newRow["NomProduit"] = "Tarte au fraises 8 personnes";
            newRow["Prix"] = 24.50;
            newRow["QuantiteEnStock"] = "4";
            table.Rows.Add(newRow);

            

            table.Rows[1].Delete();

            Console.WriteLine("Après modification:");
            Console.WriteLine("-----------");

            foreach(DataRow row in dataSet.Tables["Produits"]!.Rows)
            {
                if(row.RowState != DataRowState.Deleted){
                Console.WriteLine(row["NomProduit"]);
                Console.WriteLine(row["Prix"]); 
                Console.WriteLine(row["QuantiteEnStock"]);
                Console.WriteLine("-----------");
                }
            }      
        
        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
        dataAdapter.Update(dataSet, "Produits");
        }


    }
}
