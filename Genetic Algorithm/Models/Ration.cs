using System;
using System.Collections.Generic;
using System.Linq;

namespace Genetic_Algorithm.Models
{
    public class Ration
    {
        public List<Product> Products { get; set; }

        public float Likelihood { get; set; }

        public float Dispersion { get; set; }

        public float Vitamins => Products.Sum(p => p.Vitamins);

        public float Minerals => Products.Sum(p => p.Minerals);

        public float Protein => Products.Sum(p => p.Protein);

        public float Fat => Products.Sum(p => p.Fat);

        public float Carbohydrates => Products.Sum(p => p.Carbohydrates);

        public float Calories => Products.Sum(p => p.Calories);

        public float Price => Products.Sum(p => p.Price);

        public Ration()
        {
            Products = new List<Product>();
        }
    }
}
