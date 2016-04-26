using System.Collections.Generic;

namespace Genetic_Algorithm
{
    public interface Population
    {
        void Crossover();

        void Selection();

        dynamic GetTheBestResult();

        int Size();
    }
}