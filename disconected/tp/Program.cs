using System.Data;
using Microsoft.Data.SqlClient;

class Program
{
    public static bool running = true;
    public static DataSet dataSet = new DataSet();
    static void Main()
    {
        string masterConnectionString= "Server=localhost;Database=SalesDB;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;";
        string productQuery = "SELECT * FROM Produits";
        string regionQuery = "SELECT * FROM Regions";
        string venteQuery =  "SELECT * FROM Ventes";

        using (SqlConnection masterConnection = new SqlConnection(masterConnectionString))
        {
            masterConnection.Open();
            SqlDataAdapter productAdapter = new SqlDataAdapter(productQuery, masterConnection);
            productAdapter.Fill(dataSet, "Produits");

            SqlDataAdapter regionAdapter = new SqlDataAdapter(regionQuery, masterConnection);
            regionAdapter.Fill(dataSet, "Regions");

            SqlDataAdapter venteAdapter = new SqlDataAdapter(venteQuery, masterConnection);
            venteAdapter.Fill(dataSet, "Ventes");

            //declare relations between tables
            dataSet.Relations.Add("VentesProduits",
                dataSet.Tables["Produits"].Columns["pk_produit"],
                dataSet.Tables["Ventes"].Columns["fk_produit"]);
            
            dataSet.Relations.Add("VentesRegions",
                dataSet.Tables["Ventes"].Columns["fk_region"],
                dataSet.Tables["Regions"].Columns["pk_region"]);

            foreach(DataRow produitRow in dataSet.Tables["Produits"].Rows)
            {
                Console.WriteLine($"Produit: {produitRow["nom_produit"]}");
                foreach(DataRow venteRow in produitRow.GetChildRows("VentesProduits"))
                {
                    Console.WriteLine("");
                }
            }


            while(running)
            {
                Options();
            }

        }
    }
        public static void Options()
    {
        Console.WriteLine("Bienvenue dans l'application des rapports de ventes!");
        Console.WriteLine("Merci de selectionner une option: ");
        Console.WriteLine("1. Afficher les ventes par produits.");
        Console.WriteLine("2. Afficher le total des ventes par régions");
        Console.WriteLine("3. Afficher le montant cumulé des ventes");
        Console.WriteLine("4. Modifier une vente");
        Console.WriteLine("5. Recharger une vente");
        Console.WriteLine("6. Quitter");
        string option = Console.ReadLine()!;
        switch(option)
        {
            case "1":
                Console.WriteLine("Option1");
                break;
            case "2":
                Console.WriteLine("Option2");
                break;
            case "3":
                Console.WriteLine("Option3");
                break;
            case "4":
                Console.WriteLine("Option4");
                break;
            case "5":
                Console.WriteLine("Option5");
                break;
            case "6":
                running = false;
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }

    public static void AfficherVentesParProduit(){
        Console.WriteLine("Entrer le nom d'un produit: ");
        string produit = Console.ReadLine()!;
        
    }
}
