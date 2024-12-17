using System;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString= "Server=localhost;Database=MyDatabase;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;";

        string query="CREATE TABLE Livre(title VARCHAR(50), publication_year INT, author VARCHAR(50));";
        string query1="INSERT INTO Livre(title, publication_year, author) VALUES('Les Thanatonautes', 1994, 'Bernard Werber'), ('The ice princess', 2003,'Camilla Lackberg'), ('Culotees', 2016, 'Penelope Bagieu'), ('Le seigneur des anneaux', 1954, 'JRR Tolkien'), ('Royal assassin', 1996, 'Robin Hobb')";
        string query2="UPDATE Livre SET title = 'Lord of the rings' WHERE title = 'Le seigneur des anneaux";
        string query3= "DELETE FROM Livre WHERE publication_year = (SELECT MAX(publication_year) FROM Livre);";

        string selectQuery="SELECT title, publication_year FROM Livre";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(selectQuery, connection);
            try
            {
                connection.Open();
                using (SqlDataReader reader= command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string title = reader["title"].ToString();
                        int publication_year = Convert.ToInt32(reader["publication_year"]);
                        Console.WriteLine($"Titre: {title}, année de publication: {publication_year}");
                    }
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}

