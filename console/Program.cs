//Robin Schmutz
//29.05.2021
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console
{
    class Program
    {
        static string montant;
        static decimal[] historiqueAchat = new decimal[24];
        static DateTime tempsAchat;
        static decimal resteArgent;
        static decimal argentActuel;
        static List<Article> articles = new List<Article>();

        /// <summary>
        /// La fonction Main met en place les articles pour ensuite lire la commande qu'a écrit l'utilisateur pour lancer l'action demandé
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            articles.Add(new Article("Smarlies", "A01", 10, (decimal)1.60));
            articles.Add(new Article("Carampar", "A02", 5, (decimal)0.60));
            articles.Add(new Article("Avril", "A03", 2, (decimal)2.10));
            articles.Add(new Article("KokoKola", "A04", 1, (decimal)2.95));
            Console.WriteLine("Nom   code  quantité  prix");
            foreach (Article article in articles)
            {
                Console.WriteLine(article.Name +" "+article.Code + " " + article.Quantity + " " + article.Price);
            }
            do
            {
                
                string action;
                action = Console.ReadLine();
                string[] separateurString = action.Split('(');
                
                switch (separateurString[0])
                {
                    case "Insert":
                        Console.WriteLine("Combien voulez-vous mettre");
                        montant = Console.ReadLine();
                        resteArgent = resteArgent + Convert.ToDecimal(montant);
                        break;
                    case "Choose":
                        Choose(separateurString[1].Split('"')[1]);
                        break;
                    case "GetChange":
                        Console.WriteLine(resteArgent);
                        break;
                    case "GetBalance":
                        Console.WriteLine(argentActuel);
                        break;
                    case "SetTime":
                        string[] seperateurDate = separateurString[1].Split('"')[1].Split('T');
                        SetTime(Convert.ToDateTime(seperateurDate[0] + " " + seperateurDate[1]));
                        break;
                    case "Validation":
                        Validation();
                        break;
                    default:
                        break;
                }
            } while (true);
        }


        /// <summary>
        /// Cette fonction permet de selectionner un article dans la machine
        /// </summary>
        /// <param name="code">code est le numéro du produit à la sélectionner</param>
        static void Choose(string code)
        {
            
            bool findArticleSuccess = false;
            foreach(Article article in articles)
            {
                if(article.Code == code)
                {
                    if(resteArgent >= article.Price)
                    {
                        if (article.Selection())
                        {
                            if (tempsAchat != null)
                            {
                                historiqueAchat[tempsAchat.Hour] = historiqueAchat[tempsAchat.Hour]+ article.Price;
                            }
                            else
                            {
                                historiqueAchat[DateTime.Now.Hour] = historiqueAchat[DateTime.Now.Hour] + article.Price;
                            }
                            Console.WriteLine("Vending " + article.Name);
                            argentActuel = argentActuel + article.Price;
                            resteArgent = resteArgent - article.Price;
                        }
                        else
                        {
                            Console.WriteLine("Item " + article.Name + ": Out of stock!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not enough money!");
                    }
                    findArticleSuccess = true;
                }
                
            }
            if (findArticleSuccess == false)
            {
                Console.WriteLine("Invalid selection!");
            }
            else
            {
                Console.WriteLine("Nom   code  quantité  prix");
                foreach (Article article in articles)
                {
                    Console.WriteLine(article.Name + " " + article.Code + " " + article.Quantity + " " + article.Price);
                }
            }
            

        }


        /// <summary>
        /// cette fonction permet de fixer le temps de la machine
        /// </summary>
        /// <param name="temps"></param>
        static void SetTime(DateTime temps)
        {
            tempsAchat = temps;
        }
        /// <summary>
        /// Cette fonction montre l'historique des achats qui ont été fait dans la machine
        /// </summary>
        static void Validation()
        {
            int maxAffichage = 3;
            int compteurHeure = 0;
            List<List<decimal>> triListe = new List<List<decimal>>();
            foreach (decimal valeur in historiqueAchat)
            {
                int listeCompte = 0;
                int indexChange = 1;

                    if (valeur != 0)
                    {

                        foreach (List<decimal> valeurs in triListe)
                        {
                            if (valeur > valeurs[1] & indexChange == 1)
                            {
                                indexChange = listeCompte;
                            }
                            listeCompte++;
                        }

                        if (indexChange == 1)
                        {
                            triListe.Add(new List<decimal> { compteurHeure, valeur });
                        }
                        else
                        {
                            triListe.Insert(indexChange, new List<decimal> { compteurHeure, valeur });
                        }
                    }
                    compteurHeure = compteurHeure + 1;

                


            }
            if(triListe.Count > 3)
            {
                triListe.RemoveRange(maxAffichage, 1);
                foreach (List<decimal> valeurs in triListe)
                {
                    Console.WriteLine("Hour " + valeurs[0] + " generated a revenue of " + valeurs[1].ToString("0.00"));
                }
            }
            else
            {
                foreach (List<decimal> valeurs in triListe)
                {
                    Console.WriteLine("Hour " + valeurs[0] + " generated a revenue of " + valeurs[1].ToString("0.00"));
                }
            }
            
            
            
        }
    }
}
