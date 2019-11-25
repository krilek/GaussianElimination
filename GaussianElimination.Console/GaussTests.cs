#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GaussTests.cs">
// Karol Gzik 253923 University of Gdańsk Faculty of Mathematics, Physics and Informatics
// krilek@gmail.com
// </copyright>
// <summary>
// Wrapper for getting relevant data about errors and time which generates Gauss solving methods.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

#endregion

namespace GaussianElimination.Console
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using GaussianElimination.DataTypes;

    #endregion

    /// <summary>
    /// The gauss tests.
    /// </summary>
    public static class GaussTests
    {
        /// <summary>
        /// The generate data.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="startingSize">
        /// The starting size.
        /// </param>
        /// <typeparam name="T">
        /// Implementations of Value class.
        /// </typeparam>
        public static void GenerateData<T>(string filename, int startingSize)
            where T : Value<T>, new()
        {
            var incrementMap =
                new Dictionary<int, int> { { 100, 25 }, { 300, 50 }, { 500, 100 }, { 1000, 250 } };
            var solvingMethods = new List<(string, Func<MyMatrix<T>, MyMatrix<T>, MyMatrix<T>>)>
                                     {
                                         ("G", LinearSolver.Gauss),
                                         ("PG", LinearSolver.PartialGauss),
                                         ("FG", LinearSolver.FullGauss)
                                     };
            int size = startingSize;
            int increment = FindIncrement(startingSize, incrementMap);
            do
            {
                // Calculate B
                var a = MyMatrix<T>.GetRandomMatrix(size, size);
                var x = MyMatrix<T>.GetRandomMatrix(1, size);
                MyMatrix<T> b = a * x;

                // Verify results
                foreach ((string name, var solvingMethod) in solvingMethods)
                {
                    try
                    {
                        (long time, string error) = TestCorrectnessOfGauss<T>(a, x, b, solvingMethod);
                        StoreResults(filename, size, time, error, name);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Exception {name} {size}");
                        Console.WriteLine(e);
                    }
                }

                size += increment;
                if (incrementMap.Keys.Contains(size))
                {
                    increment = incrementMap[size];
                }
            }
            while (true);
        }

        /// <summary>
        /// The find increment.
        /// </summary>
        /// <param name="startingSize">
        /// The starting size.
        /// </param>
        /// <param name="incrementMap">
        /// The increment map.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private static int FindIncrement(int startingSize, Dictionary<int, int> incrementMap)
        {
            int increment = 10;
            foreach (KeyValuePair<int, int> i in incrementMap)
            {
                if (startingSize >= i.Key)
                {
                    increment = i.Value;
                }
            }

            return increment;
        }

        /// <summary>
        /// The store results.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <param name="time">
        /// The time.
        /// </param>
        /// <param name="error">
        /// The error.
        /// </param>
        /// <param name="method">
        /// The method.
        /// </param>
        private static void StoreResults(string filePath, int size, long time, string error, string method)
        {
            File.AppendAllText(filePath, $"{size.ToString()};{time.ToString()};{error};{method};{Environment.NewLine}");
        }

        /// <summary>
        /// The test correctness of gauss.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <param name="solvingFunc">
        /// The solving func.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="(long time, string error)"/>.
        /// </returns>
        private static (long time, string error) TestCorrectnessOfGauss<T>(
            MyMatrix<T> a,
            MyMatrix<T> x,
            MyMatrix<T> b,
            Func<MyMatrix<T>, MyMatrix<T>, MyMatrix<T>> solvingFunc)
            where T : Value<T>, new()
        {
            Stopwatch timer = Stopwatch.StartNew();
            MyMatrix<T> solved1 = a.SolveLinearEquation(b, solvingFunc);
            timer.Stop();
            Value<T> error = solved1.RelativeError(x);
            return (timer.ElapsedMilliseconds, error.ToString());
        }
    }
}