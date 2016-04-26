using System;

namespace Genetic_Algorithm
{
    public class GeneticAlgorithm
    {
        private readonly int _iteration;

        private readonly Population _currentPopulation;


        public GeneticAlgorithm(Population population, int iteration)
        {
            _currentPopulation = population;

            _iteration = iteration;
        }


        public Population Solve()
        {
            var i = 0;
            while (i++ < _iteration)
            {
                _currentPopulation.Selection();
                _currentPopulation.Crossover();

                OnGettingPopulation(_currentPopulation);
            }

            return _currentPopulation;
        }

        public event EventHandler<Population> GettingPopulation = delegate { };

        protected virtual void OnGettingPopulation(Population e)
        {
            GettingPopulation(this, e);
        }
    }
}