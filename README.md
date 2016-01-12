# Genetic-Algorithm
Genetic Algorithm for 0/1 knapsack problem.

This was an assignment for my Intelligent Systems class at University.

I created a program which solves the knapsack problem by using a genetic algorithm. The user specifies how many generations the algorithm runs for to determine which combination of items provides the highest value and least cost to fit in the knapsack.  In the program, the combination of items are converted to a 'chromosome' which is like binary string. For example say you have 1 book, 1 pen and 0 pencil. The chromosome would be '110'.

During a generaton, there is a small possibility for either both 'crossover' or 'mutation' to occur which alters the chromosome. The purpose of this is to find a fitter chromosome so that it can determine whether the new combination of items has a higher value and lesser cost than the previous chromosomes. This means that the more generations you specify, the more accurate the result is for the knapsack problem.
