﻿using System;
using MGroup.Optimization.Algorithms.Metaheuristics.DifferentialEvolution;
using MGroup.Optimization.Constraints.Penalties;
using MGroup.Optimization.Convergence;
using MGroup.Optimization.Problems;
using MGroup.Optimization.Structural.Benchmarks;

namespace MGroup.Optimization.Structural.Tests.Benchmarks
{
    class TestTruss10Benchmark
    {
        public static void Run()
        {
            OptimizationProblem sizingOptimizationProblem = new Truss10Benchmark();

            var builder = new DifferentialEvolutionAlgorithmConstrained.Builder(sizingOptimizationProblem);
            builder.PopulationSize = 100;
            builder.MutationFactor = 0.6;
            builder.CrossoverProbability = 0.9;
            builder.ConvergenceCriterion = new MaxFunctionEvaluations(200000);
            builder.Penalty = new DeathPenalty();
            IOptimizationAlgorithm de = builder.Build();

            IOptimizationAnalyzer analyzer = new OptimizationAnalyzer(de);
            analyzer.Optimize();

            // Print results
            Console.WriteLine("\n Best Position:");
            for (int i = 0; i < sizingOptimizationProblem.Dimension; i++)
            {
                Console.WriteLine(String.Format(@"  x[{0}] = {1} ", i, de.BestPosition[i]));
            }
            Console.WriteLine(String.Format(@"Best Fitness: {0}", de.BestFitness));
        }
    }
}
