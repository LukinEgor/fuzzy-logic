using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using ExstensionMethods;
using Genetic_Algorithm.Models;
using Microsoft.Win32;

namespace Genetic_Algorithm
{
    public partial class MainWindow : Window
    {
        private readonly List<Product> _products = new List<Product>();


        public MainWindow()
        {
            InitializeComponent();
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            _products.Add(new Product
            {
                Name = textBoxName.Text,
                Vitamins = textBoxVitamins.Text.ToInt(),
                Minerals = textBoxMinerals.Text.ToInt(),
                Protein = textBoxProtein.Text.ToInt(),
                Fat = textBoxFat.Text.ToInt(),
                Carbohydrates = textBoxCar.Text.ToInt(),
                Calories = textBoxCal.Text.ToInt(),
                Price = textBoxPrice.Text.ToInt()
            });

            Refresh();
        }


        private void Refresh()
        {
            listBox.Items.Clear();

            _products.ForEach(p => listBox.Items.Add(p));
        }


        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.FileOk += DialogOnFileOk;

            dialog.ShowDialog();
        }


        private void DialogOnFileOk(object sender, CancelEventArgs cancelEventArgs)
        {
            var filename = ((OpenFileDialog) sender).FileName;
            using (var sr = new StreamReader(filename))
            {
                string data;
                while ((data = sr.ReadLine()) != null)
                {
                    ParseProduct(data);
                }
            }
        }


        private void ParseProduct(string data)
        {
            var mas = data.Split(' ');
            _products.Add(new Product
            {
                Name = mas[0],
                Vitamins = mas[1].ToInt(),
                Minerals = mas[2].ToInt(),
                Protein = mas[3].ToInt(),
                Fat = mas[4].ToInt(),
                Carbohydrates = mas[5].ToInt(),
                Calories = mas[6].ToInt(),
                Price = mas[7].ToInt()
            });

            Refresh();
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var iteration = textBoxIter.Text.ToInt();

            var menu = CreateMenu();
            var algorithm = new GeneticAlgorithm(menu, iteration);

            algorithm.GettingPopulation += AlgorithmOnGettingPopulation;

            var result = algorithm.Solve();
            MessageBox.Show(GenerateReport(result));
        }


        private void AlgorithmOnGettingPopulation(object sender, Population population)
        {
            textBoxResult.Text += GenerateReport(population) + "------------\n";
        }


        private string GenerateReport(Population population)
        {
            Ration res = population.GetTheBestResult();

            var report = "Лучший результат у следующего рациона:\n";
            res.Products.ForEach(p => report += p.ToString() + "\n");

            report += "Цена равна :" + res.Price;

            return report;
        }


        private RationsList CreateMenu()
        {
            var menu = new RationsList
            {
                Vitamins = textBox_Copy5.Text.ToInt(),
                Minerals = textBox_Copy6.Text.ToInt(),
                Protein = textBox_Copy7.Text.ToInt(),
                Fat = textBox_Copy8.Text.ToInt(),
                Carbohydrates = textBox_Copy9.Text.ToInt(),
                Calories = textBox_Copy10.Text.ToInt()
            };

            var rand = new Random();
            for (var i = 0; i < 100; i++)
            {
                menu.Rations.Add(CreateRation(rand.Next(1, 10)));
            }

            return menu;
        }


        private Ration CreateRation(int size)
        {
            var rand = new Random();
            var ration = new Ration();

            for (var i = 0; i < size; i++)
            {
                ration.Products.Add(_products[rand.Next(0, _products.Count)]);
            }

            return ration;
        }
    }
}