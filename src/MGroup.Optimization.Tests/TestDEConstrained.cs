﻿using System;
using MGroup.LinearAlgebra.Vectors;
using MGroup.Optimization.Algorithms.Metaheuristics.DifferentialEvolution;
using MGroup.Optimization.Benchmarks.Constrained;
using MGroup.Optimization.Constraints.Penalties;
using MGroup.Optimization.Convergence;
using MGroup.Optimization.Problems;
using Xunit;

namespace MGroup.Optimization.Tests
{
    public static class TestDEConstrained
    {
        [Fact]
        public static void Run()
        {
            int seed = 2;
            var rng = new Random(seed);

            OptimizationProblem optimizationProblem = new S_CRES();

            var builder = new DifferentialEvolutionAlgorithmConstrained.Builder(optimizationProblem);
            builder.PopulationSize = 20;
            builder.MutationFactor = 0.6;
            builder.CrossoverProbability = 0.9;
            builder.ConvergenceCriterion = new MaxFunctionEvaluations(100000);
            builder.Penalty = new DeathPenalty();
            builder.RandomNumberGenerator = rng;
            IOptimizationAlgorithm de = builder.Build();

            IOptimizationAnalyzer analyzer = new OptimizationAnalyzer(de);
            analyzer.Optimize();

            //TODO: Not sure this is the exact solution. Needs research.
            double expectedFitness = 13.590841691859703;
            var expectedDesign = Vector.CreateFromArray(new double[] { 2.246825836986833, 2.3818634605759064 });
            Assert.Equal(expectedFitness, de.BestFitness, 6);
            Assert.True(Vector.CreateFromArray(de.BestPosition).Equals(expectedDesign, 1E-6));
        }
    }
}
