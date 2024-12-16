// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;

class Program
{
    static void Main()
    {
        //Exercice1();
        //Exercice2();
        //Exercice3();
        //Exercice4();
        //Exercice5();
        // Livre livre = new Livre();
        // livre.Titre = "Les Thanatonautes";
        // livre.NbPages = 502;
        // livre.AfficherInfos();
        //Exercice7();
        //Exercice8();
        //Exercice9();
        //Exercice10();
        Exercice11();
    }

    public static void Exercice1() 
    {
        Console.WriteLine("Bienvenue au cours de programmation en C# !");
    }

    public static void Exercice2()
    {
        int nombre1= 10;
        int nombre2= 20;

        int nombre3= nombre1 + nombre2;

        Console.WriteLine("La somme de " +nombre1+ " et " +nombre2+ " est: " +nombre3);
    }

    public static void Exercice3()
    {
        Console.WriteLine("Ecrire un chiffre entre 0 et 20:");
        string valeur = Console.ReadLine();
        if ( valeur == "")
        {
            Console.WriteLine("Ecrire un chiffre entre 0 et 20:");
        } else 
        {
            int number = int.Parse(valeur);
            switch(number)
            {
                case < 9:
                Console.WriteLine("Insuffisant.");
                break;
                case > 15:
                Console.WriteLine("Assez bien.");
                break;
                case < 20:
                Console.WriteLine("Bien.");
                break;
            };
        }
    }

    public static void Exercice4()
    {
        Console.WriteLine("Ecrire un chiffre entre 1 et 20:");
        string valeur = Console.ReadLine();
        int nombre = int.Parse(valeur);
        for(int i = 1; i <= 10; i++)
        {
            int resultat =  i * nombre;
             Console.WriteLine($"{nombre} x {i} = {resultat}");
        }
    }

    public static void Exercice5()
    {
        Console.WriteLine("Ecrire un chiffre entre 1 et 20:");
        string valeur = Console.ReadLine();
        int result = 0;
         if ( valeur != "" && valeur != null)
        {
            int nombre = int.Parse(valeur);
            int iterateur = 1;
            while( iterateur < nombre)
            {
                iterateur++;
                if(iterateur % 2 == 0)
                {
                    result = result + iterateur;
                }
            }
        }
        Console.WriteLine(result);
    } 

    //à revoir
    // public static void Exercice7() {
    //     Console.WriteLine("Saisissez 5 notes (nombre entier), séparées par des virgules:");
    //      string entree = Console.ReadLine();
    //      if ( entree != "" && entree != null)
    //     {
    //         string[] valeurs = entree.Split(',');
    //         for (int i = 0; i< valeurs.Length; i++)
    //         {
    //             int note = int.Parse(valeurs[i]);
    //         }
    //     }
    // } 

    public static void Exercice8() {
        List<int> list = new List<int>();
        for(int i=0; i< 5; i++)
        {
            Console.WriteLine("Saisir un nombre entier:");
            string valeur = Console.ReadLine();
            if(valeur != null)
            {
                int nbre = int.Parse(valeur);
                list.Add(nbre);
            }
        }
        Console.WriteLine("Elements pairs de la liste: ");
        foreach(var elm in list)
        {
            if(elm % 2 == 0)
            {
                Console.WriteLine(elm);
            }
        }
    }

    public static void Exercice9() {
        Dictionary<string, int> produits = new Dictionary<string, int>();
        produits.Add("coupe ongles", 9);
        produits.Add("marteau", 15);
        produits.Add("courgette", 2);

        Console.WriteLine("Saisir le nom d'un article pour en afficher le prix: ");
        string saisie = Console.ReadLine();
        if(produits.ContainsKey(saisie))
        {
            Console.WriteLine($"le prix d'un {saisie} est {produits[saisie]}euros.");
        }
        else 
        {
            Console.WriteLine("Cet article est introuvable.");
        }
    }

    public static void Exercice10(){
        Console.WriteLine("Entrez un nombre entier");
        string saisie = Console.ReadLine();
        try
        {
            int nombre = int.Parse(saisie);
            Console.WriteLine($"La convertion est réussie: {nombre} est converti en type int.");
        }
        catch(FormatException exp)
        {
            Console.WriteLine(exp.Message);
        }
        finally
        {
            Console.WriteLine("Programme terminé.");
        }

    }

    public static void Exercice11(){
        Dictionary<string, int> nombres = new Dictionary<string, int>();
        nombres.Add("un", 1);
        nombres.Add("deux", 2);
        nombres.Add("trois", 3);
        nombres.Add("quatre", 4);
        nombres.Add("cinq", 5);
        nombres.Add("six", 6);
        nombres.Add("sept", 7);
        nombres.Add("huit", 8);
        nombres.Add("neuf", 9);
        nombres.Add("dix", 10);
        Console.WriteLine("Entrez un nombre entier entre 1 et 10 en toutes lettres pour le convertir en chiffre(s):");
        string saisie = Console.ReadLine();
        try
        {   
            int nombre = nombres[saisie];
            Console.WriteLine($"La convertion est réussie: {nombre} est converti en chiffre.");
        }
        catch(FormatException exp)
        {
            Console.WriteLine(exp.Message);
        }
        catch(Exception exp)
        {
            Console.WriteLine("Ce nombre n'est pas reconnu et ne peu être converti");
        }
        finally
        {
            Console.WriteLine("Programme terminé.");
        }

    }


}
        class Livre()
        {
            private string titre; 
            public string Titre {
                get{ return titre;}
                set{
                    titre= value;
                }
            }

            private int nbrePages;
            public int NbPages {
                get{ return nbrePages;}
                set{
                    if(value >= 1)
                    {
                        nbrePages = value;
                    }
                    else 
                    {
                        Console.WriteLine("Le nombre de pages doit être supérieur ou égal à 1");
                        //throw new ArgumentException("Number of pages needs to be higher than 1");
                    };
                }   
            }

            public void AfficherInfos()
            {
                Console.WriteLine($"Titre: {titre}, Nombre de pages: {nbrePages}");
            }  
        };
    





