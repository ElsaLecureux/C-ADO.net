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

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query3, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Table updated successfully");
            }
            catch(SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}

//create table livre
//

//insert 5 livres
//

//update livre title
//'

