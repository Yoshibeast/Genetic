using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_4
{
    public class Item
    {
        public string Name { get; set; } public int Cost { get; set; } public int Value { get; set; }
        public override string ToString()
        {
            return "Name : " + Name + "\tCost : " + Cost + "\tValue : " + Value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            int C = 19;                     //Knapsack cannot exceed this Max Cost value.                           
            int popSize = 10;               //Population Size
            double crossoverProb = 0.70;    //Corssover Probability
            double mutationProb = 0.01;    //Mutation probability     
            int terminate = 1;              //Used as a count. Algorithm terminates when terminate value equals generation value
            int generation;                 //Number of generations. Specified by user.

            //List of knapsack items' name, cost and value.
            List<Item> knapsackItem = new List<Item>();
            knapsackItem.Add(new Item() { Name = "Watch", Cost = 4, Value = 7 });
            knapsackItem.Add(new Item() { Name = "Phone", Cost = 8, Value = 7 });
            knapsackItem.Add(new Item() { Name = "Food", Cost = 4, Value = 5 });
            knapsackItem.Add(new Item() { Name = "Book", Cost = 4, Value = 6 });
            knapsackItem.Add(new Item() { Name = "Game", Cost = 4, Value = 6 });
            knapsackItem.Add(new Item() { Name = "TV", Cost = 6, Value = 6 });

            int numItems = knapsackItem.Count;      //Total Number of items in knapsack

            Console.WriteLine("Genetic Algorithm for 0/1 Knapsack problem.\n");

            Console.WriteLine("<Items>\n");
            for (int a = 0; a < knapsackItem.Count; a++)        //Show Items at Start.
            {
                Console.WriteLine(knapsackItem[a]);
            }

            while (true)
            {
                Console.Write("\nEnter number of generations ('e' to Exit): ");   //Ask user for number of generations to end program.
                string userInput = Console.ReadLine();

                if (userInput == "exit" || userInput == "e")
                {
                    break;
                }
                else if (int.TryParse(userInput, out generation))
                {
                    string[] population = new string[popSize];  //Array to store chromosomes as binary strings.
                    var chars = "01";
                    var stringChars = new char[numItems];
                    var random = new Random();
                    int[] chromosomeCost = new int[popSize];        //Array to store cost for each chromosome from population array.
                    int[] chromosomeValue = new int[popSize];       //Array to store value for each chromosome from population array.
                    string str1;
                    string str2;
                    char[] splitStr1;
                    char[] splitStr2;
                    int[] splitChromosome;                          //Splits one chromosome string at a time. Each gene is at an index of this array. 
                    int totalValue;                                 //Total Value of a chromosome
                    int totalCost;                                  //Total cost of a chromosome
                    
                    //Keep looping until terminate value equals generation value
                    //                    
                    while (terminate <= generation)
                    {
                        //Console.WriteLine("\n\t\t\t\tGeneration: " + terminate + "\n");

                        //Generate first random population chromosomes
                        //
                        if (terminate == 1)
                        {
                            for (int j = 0; j < popSize; j++)
                            {
                                for (int i = 0; i < stringChars.Length; i++)
                                {
                                    stringChars[i] = chars[random.Next(chars.Length)];
                                }
                                population[j] = new String(stringChars);
                            }
                        }                      

                        for (int i = 0; i < popSize; i++)
                        {
                            str1 = population[i];
                            splitStr1 = str1.ToCharArray();
                            splitChromosome = Array.ConvertAll(splitStr1, c => (int)Char.GetNumericValue(c));
                            totalValue = 0;
                            totalCost = 0;

                            for (int m = 0; m < splitChromosome.Length; m++)
                            {
                                if (splitChromosome[m] == 1)
                                {
                                    totalCost = totalCost + knapsackItem[m].Cost;
                                    totalValue = totalValue + knapsackItem[m].Value;
                                }
                            }
                            chromosomeCost[i] = totalCost;
                            chromosomeValue[i] = totalValue;
                        }

                        int sumCost = chromosomeCost.Sum();
                        int sumValue = chromosomeValue.Sum();

                        /**
                        Console.WriteLine("\tC\tV");
                        for (int n = 0; n < population.Length; n++)
                        {
                            Console.WriteLine(population[n] + "\t" + chromosomeCost[n] + "\t" + chromosomeValue[n]);
                        }
                        Console.WriteLine("\nTotal\t" + sumCost + "\t" + sumValue);
                        Console.Write("\nValue/Total Value = %");
                        **/

                        //Roulette wheel for selecting 2 chromosomes from population
                        //
                        int[] array = new int[110];
                        float percent;                          //Percent cost for one chromosome
                        int totalPercent = 0;                   //To work out total percent.
                        int q = 0;
                        int s = 0;
                        //Console.Write("\n");
                        for (int c = 0; c < population.Length; c++)
                        {
                            percent = ((float)chromosomeCost[c] / (float)sumCost) * 100;
                            percent = Convert.ToInt32(percent);
                            //Console.WriteLine(percent);
                            for (int r = 0; r < percent; r++)
                            {
                                array[s] = q;
                                s++;
                            }
                            q++;
                            totalPercent = totalPercent + Convert.ToInt32(percent);
                        }

                        /**
                        Console.WriteLine("\n% Values for Roulette Wheel");
                        for (int n = 0; n < totalPercent; n++)
                        {
                            Console.Write(array[n]);
                        }
                        **/

                        int randomNumber1 = random.Next(0, totalPercent);                       //Random number for index of array to select a chromosome.
                        int randomNumber2 = random.Next(0, totalPercent);                       //Random number for index of array to select a chromosome.
                        //Console.WriteLine("\n\nRoulette random no.1 = " + randomNumber1);
                        //Console.WriteLine("Roulette random no.2 = " + randomNumber2);
                        string[] roulette = new string[2];                                      //Array to store the two selected values by roulette wheel.

                        int Chromosome1 = array[randomNumber1];                                 //Index of first chromosome selected from array
                        int Chromosome2 = array[randomNumber2];                                 //Index of second chromosome selected from array
                        roulette[0] = population[Chromosome1];                                  //Chromosome is selected from population array based on chromosome 1 index
                        roulette[1] = population[Chromosome2];                                  //Chromosome is selected from population array based on chromosome 2 index

                        //Console.Write("\n<< ROULETTE WHEEL >>\n");
                        //Console.Write("\nSelected chromosomes by roulette wheel:");
                        //Console.WriteLine("\n" + roulette[0] + "\t" + chromosomeCost[Chromosome1] + "\t" + chromosomeValue[Chromosome1]);
                        //Console.WriteLine(roulette[1] + "\t" + chromosomeCost[Chromosome2] + "\t" + chromosomeValue[Chromosome2]);

                        //Mutation of chromosomes
                        //
                        double randomDoubleNumber1 = random.NextDouble() * (1.000 - 0.000) + 0.000;
                        double mutationOccurs = Math.Round(randomDoubleNumber1, 3);                    //Number to determine if mutation occurs
                        int[] genome1;
                        int[] genome2;
                        if (mutationOccurs <= mutationProb)
                        {
                            //Console.WriteLine("\n<< MUTATION OCCURED >>");
                            int ranChooseChromosome = random.Next(0, 2);
                            //if (ranChooseChromosome == 0)
                            //{
                            //    Console.WriteLine("\nMutate 1st chromosome.");
                            //}
                            //else Console.WriteLine("\nMutate 2nd chromosome.");
                            str1 = roulette[ranChooseChromosome];
                            splitStr1 = str1.ToCharArray();
                            genome1 = Array.ConvertAll(splitStr1, c => (int)Char.GetNumericValue(c));
                            int ranMutateAt = random.Next(0, 4);
                            //Console.WriteLine("Mutation at index: " + ranMutateAt);
                            if (genome1[ranMutateAt] == 0)
                            {
                                genome1[ranMutateAt] = 1;
                            }
                            else if (genome1[ranMutateAt] == 1)
                            {
                                genome1[ranMutateAt] = 0;
                            }
                            
                            totalValue = 0;
                            totalCost = 0;
                            for (int m = 0; m < genome1.Length; m++)
                                {
                                    if (genome1[m] == 1)
                                    {
                                        totalCost = totalCost + knapsackItem[m].Cost;
                                        totalValue = totalValue + knapsackItem[m].Value;
                                    }
                                }
                            if (ranChooseChromosome == 0)
                            {
                                chromosomeCost[Chromosome1] = totalCost;
                                chromosomeValue[Chromosome1] = totalValue;
                            }
                            else if (ranChooseChromosome == 1)
                            {
                                chromosomeCost[Chromosome2] = totalCost;
                                chromosomeValue[Chromosome2] = totalValue;
                            }

                            str1 = string.Join("", genome1);
                            roulette[ranChooseChromosome] = str1;    

                            //Console.Write("\nMutated chromosomes:");
                            //Console.WriteLine("\n" + roulette[0] + "\t" + chromosomeCost[Chromosome1] + "\t" + chromosomeValue[Chromosome1]);
                            //Console.WriteLine(roulette[1] + "\t" + chromosomeCost[Chromosome2] + "\t" + chromosomeValue[Chromosome2]);                            
                        }

                        //Crossover occurs
                        //
                        double randomDoubleNumber2 = random.NextDouble() * (1.00 - 0.00) + 0.00;
                        double crossoverOccurs = Math.Round(randomDoubleNumber2, 2);                    //Number to determine if crossover occurs
                        if (crossoverOccurs <= crossoverProb)
                        {
                            //Console.WriteLine("\n<< CROSSOVER OCCURED >>");
                            str1 = roulette[0];
                            str2 = roulette[1];
                            splitStr1 = str1.ToCharArray();
                            splitStr2 = str2.ToCharArray();
                            genome1 = Array.ConvertAll(splitStr1, c => (int)Char.GetNumericValue(c));
                            genome2 = Array.ConvertAll(splitStr2, c => (int)Char.GetNumericValue(c));
                            int ranCrossoverFrom = random.Next(0, 4);
                            //Console.WriteLine("\nCrossover from index: " + ranCrossoverFrom);
                            for (int i = 0; i <= ranCrossoverFrom; i++)
                            {
                                int getGene1 = genome1[i];
                                int getGene2 = genome2[i];
                                genome1[i] = getGene2;
                                genome2[i] = getGene1;
                            }
                            totalValue = 0;
                            totalCost = 0;
                            for (int m = 0; m < genome1.Length; m++)
                            {
                                if (genome1[m] == 1)
                                {
                                    totalCost = totalCost + knapsackItem[m].Cost;
                                    totalValue = totalValue + knapsackItem[m].Value;
                                }
                                chromosomeCost[Chromosome1] = totalCost;
                                chromosomeValue[Chromosome1] = totalValue;
                            }
                            totalValue = 0;
                            totalCost = 0;
                            for (int m = 0; m < genome1.Length; m++)
                            {
                                if (genome2[m] == 1)
                                {
                                    totalCost = totalCost + knapsackItem[m].Cost;
                                    totalValue = totalValue + knapsackItem[m].Value;
                                }
                                chromosomeCost[Chromosome2] = totalCost;
                                chromosomeValue[Chromosome2] = totalValue;
                            }

                            str1 = string.Join("", genome1);
                            str2 = string.Join("", genome2);
                            roulette[0] = str1;
                            roulette[1] = str2;

                            //Console.Write("\nCrossover chromosomes:");
                            //Console.WriteLine("\n" + roulette[0] + "\t" + chromosomeCost[Chromosome1] + "\t" + chromosomeValue[Chromosome1]);
                            //Console.WriteLine(roulette[1] + "\t" + chromosomeCost[Chromosome2] + "\t" + chromosomeValue[Chromosome2]);      
                        }


                        //Fitness function. Select the highest valued chromosome while less than knapsack Max Cost.
                        //
                        if (chromosomeCost[Chromosome1] > C)
                        {
                            if (chromosomeCost[Chromosome2] <= C)
                            {
                                for (int i = 0; i < population.Length; i++)
                                {
                                    if (chromosomeValue[i] < chromosomeValue[Chromosome2])
                                    {
                                        population[i] = roulette[1];
                                        chromosomeCost[i] = chromosomeCost[Chromosome2];
                                        chromosomeValue[i] = chromosomeValue[Chromosome2];
                                        break;
                                    }
                                    else if (chromosomeCost[i] > C)
                                    {
                                        population[i] = roulette[1];
                                        chromosomeCost[i] = chromosomeCost[Chromosome2];
                                        chromosomeValue[i] = chromosomeValue[Chromosome2];
                                        break;
                                    }
                                }
                            }
                        }
                        else if (chromosomeCost[Chromosome2] > C)
                        {
                            if (chromosomeCost[Chromosome1] <= C)
                            {
                                for (int i = 0; i < population.Length; i++)
                                {
                                    if (chromosomeValue[i] < chromosomeValue[Chromosome1])
                                    {
                                        population[i] = roulette[0];
                                        chromosomeCost[i] = chromosomeCost[Chromosome1];
                                        chromosomeValue[i] = chromosomeValue[Chromosome1];
                                        break;
                                    }
                                    else if (chromosomeCost[i] > C)
                                    {
                                        population[i] = roulette[0];
                                        chromosomeCost[i] = chromosomeCost[Chromosome1];
                                        chromosomeValue[i] = chromosomeValue[Chromosome1];
                                        break;
                                    }
                                }
                            }
                        }
                        else if (chromosomeValue[Chromosome1] >= chromosomeValue[Chromosome2])
                        {
                            for (int i = 0; i < population.Length; i++)
                            {
                                if (chromosomeValue[i] < chromosomeValue[Chromosome1])
                                {
                                    population[i] = roulette[0];
                                    chromosomeCost[i] = chromosomeCost[Chromosome1];
                                    chromosomeValue[i] = chromosomeValue[Chromosome1];
                                    break;
                                }
                                else if (chromosomeCost[i] > C)
                                {
                                    population[i] = roulette[0];
                                    chromosomeCost[i] = chromosomeCost[Chromosome1];
                                    chromosomeValue[i] = chromosomeValue[Chromosome1];
                                    break;
                                }
                            }
                        }
                        else if (chromosomeValue[Chromosome2] >= chromosomeValue[Chromosome1])
                        {
                            for (int i = 0; i < population.Length; i++)
                            {
                                if (chromosomeValue[i] < chromosomeValue[Chromosome2])
                                {
                                    population[i] = roulette[1];
                                    chromosomeCost[i] = chromosomeCost[Chromosome2];
                                    chromosomeValue[i] = chromosomeValue[Chromosome2];
                                    break;
                                }
                                else if (chromosomeCost[i] > C)
                                {
                                    population[i] = roulette[1];
                                    chromosomeCost[i] = chromosomeCost[Chromosome2];
                                    chromosomeValue[i] = chromosomeValue[Chromosome2];
                                    break;
                                }
                            }
                        }
                        //Console.WriteLine("\nThe fitter out of the 2 chromosomes replaces a\nweaker chromosome in the population to create\na new population with fitter chromosomes.");
                        terminate = terminate + 1;
                    }

                    //Solution of the 0/1 knapsack problem.
                    //Pick highest value from population while less than knapsack cost
                    //
                    Console.WriteLine("\n-----------------------------------------------------------------\n");
                    Console.WriteLine("Generations: " + generation);
                    Console.WriteLine("Solution when Knapsack Max Cost = " + C + "\n");

                    for (int i = 0; i < chromosomeCost.Length; i++)
                    {
                        if (chromosomeCost[i] > C)
                        {
                            chromosomeValue[i] = 0;
                        }
                    }
                    int maxValue = chromosomeValue.Max();
                    int maxIndex = chromosomeValue.ToList().IndexOf(maxValue);
                    str1 = population[maxIndex];
                    splitStr1 = str1.ToCharArray();
                    splitChromosome = Array.ConvertAll(splitStr1, c => (int)Char.GetNumericValue(c));
                    totalValue = 0;
                    totalCost = 0;

                    for (int m = 0; m < splitChromosome.Length; m++)
                    {
                        if (splitChromosome[m] == 1)
                        {
                            Console.WriteLine("Name : " + knapsackItem[m].Name + "\tCost : " + knapsackItem[m].Cost + "\tValue : " + knapsackItem[m].Value);
                            totalCost = totalCost + knapsackItem[m].Cost;
                            totalValue = totalValue + knapsackItem[m].Value;
                        }
                    }                    
                    Console.WriteLine("\nTotal Cost = " + totalCost);
                    Console.WriteLine("Total Value = " + totalValue);
                    Console.WriteLine("\n-----------------------------------------------------------------");

                    terminate = 1;
                }
                else                
                    Console.WriteLine("Invalid input. Try again.");    
            }
        }
    }
}
