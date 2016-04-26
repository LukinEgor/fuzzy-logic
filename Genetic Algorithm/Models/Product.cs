using System.Collections.Generic;

namespace Genetic_Algorithm.Models
{
    public class Product
    {
        public string Name { get; set; }

        public float Vitamins { get; set; }

        public float Minerals { get; set; }

        public float Protein { get; set; }

        public float Fat { get; set; }

        public float Carbohydrates { get; set; }

        public float Calories { get; set; }

        public float Price { get; set; }

        public override string ToString()
        {
            return $"{Name} - Витамины : {Vitamins}, Минералы : {Minerals}, Белки : {Protein}, " +
                   $"Жиры : {Fat}, Углеводы : {Carbohydrates}, Калории : {Calories}, Цена : {Price}$";
        }
    }
}
