using System;
using System.Collections.Generic;
using System.Text;

namespace console
{
    /// <summary>
    /// Cette classe contient les informations d'un article
    /// </summary>
    class Article
    {
        string name;
        string code;
        int quantity;
        decimal price;


        public Article(string name, string code, int quantity, decimal price)
        {
            this.name = name;
            this.code = code;
            this.quantity = quantity;
            this.price = price;
        }

        public string Name
        {
            get { return name; }
        }
        public string Code
        {
            get { return code; }
        }
        public int Quantity
        {
            get { return quantity; }
        }
        public decimal Price
        {
            get { return price; }
        }

        /// <summary>
        /// Cette methode réduit la quantité du produit saisi
        /// </summary>
        /// <returns></returns>
        public bool Selection()
        {
            if (quantity > 0)
            {
                quantity--;
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
