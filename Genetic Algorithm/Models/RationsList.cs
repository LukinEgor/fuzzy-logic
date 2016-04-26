using System;
using System.Collections.Generic;
using System.Linq;

namespace Genetic_Algorithm.Models
{
    public class RationsList : Population
    {
        public List<Ration> Rations { get; set; } 

        public float Vitamins { get; set; }

        public float Minerals { get; set; }

        public float Protein { get; set; }

        public float Fat { get; set; }

        public float Carbohydrates { get; set; }

        public float Calories { get; set; }

        public RationsList()
        {
            Rations = new List<Ration>();
        }

        public void Crossover()
        {
            float[] levels = new float[Rations.Count];
            float sum = 0;

            for (int i = 0; i < Rations.Count; i++)
            {
                sum += Rations[i].Likelihood;
                levels[i] = sum;
            }

            Random rand = new Random();
            List<Ration> newRation = new List<Ration>();
            for (int i = 0; i < Rations.Count; i++)
            {
                Ration fParent = GetRation(levels, (float)rand.NextDouble());
                Ration sParent = GetRation(levels, (float)rand.NextDouble());
                
                newRation.Add(Crossover(fParent, sParent));
            }

            Rations = newRation;
        }

        private Ration GetRation(float[] levels, float item)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                if (item < levels[i])
                    return Rations[i];
            }

            return Rations.Last();
        }

        private Ration Crossover(Ration fParent, Ration sParent)
        {
            Random rand = new Random();

            Ration crossRation = new Ration();

            int max = fParent.Products.Count < sParent.Products.Count ? sParent.Products.Count : fParent.Products.Count;

            for (int i = 0; i < max; i++)
            {
                int k = rand.Next(0, 2);
                if(k == 0 && fParent.Products.Count > i)
                    crossRation.Products.Add(fParent.Products[i]);
                else if (k == 1 && sParent.Products.Count > i)
                    crossRation.Products.Add(sParent.Products[i]);
                else if (sParent.Products.Count > i)
                    crossRation.Products.Add(sParent.Products[i]);
                else
                    crossRation.Products.Add(fParent.Products[i]);
            }

            return crossRation;
        }

        public void Selection()
        {
            float sum = 0;
            Rations.ForEach(r =>
            {
                r.Dispersion = Math.Abs(Vitamins - r.Vitamins) +
                               Math.Abs(Minerals - r.Minerals) +
                               Math.Abs(Protein - r.Protein) +
                               Math.Abs(Fat - r.Fat) +
                               Math.Abs(Carbohydrates - r.Carbohydrates) +
                               Math.Abs(Calories + r.Calories);

                sum += r.Dispersion;
            });

            Rations.ForEach(r => r.Likelihood = r.Dispersion / sum);
        }

        public dynamic GetTheBestResult()
        {
            var ration = Rations.Where(r => r.Vitamins >= Vitamins &&
                               r.Minerals >= Minerals &&
                               r.Protein >= Protein &&
                               r.Fat >= Fat &&
                               r.Carbohydrates >= Carbohydrates &&
                               r.Calories >= Calories).OrderBy(r => r.Price).First();

            return ration;
        }

        public int Size()
        {
            return Rations.Count;
        }
    }
}
