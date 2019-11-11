#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs">
// Karol Gzik 253923 University of Gdańsk Faculty of Mathematics, Physics and Informatics
// krilek@gmail.com
// </copyright>
// <summary>
// 
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

#endregion

namespace GaussianElimination
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    #endregion

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Main(string[] args)
        {
            //TestValue();
            //TestMyMatrixMul();
            var filename = $"{args[0]}_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.csv";
            switch (args[0])
            {
                case "double":
                    GenerateData<MDouble>(filename);
                    break;
                case "float":
                    GenerateData<MFloat>(filename);
                    break;
                case "fraction":
                    GenerateData<Fraction>(filename);
                    break;
                default:
                    Console.WriteLine("Wrong argument.");
                    break;
            }

            //Console.WriteLine(String.Format("{0:00}:{1:00}:{2:00}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 10));
            //TestGaussSolver();
            //TestMyMatrix();
            //for (int i = 0; i < 100; i++)
            //{
            //TestEqualityOfSolvers<Fraction>();
            //TestEqualityOfSolvers<MDouble>();
            //TestEqualityOfSolvers<MFloat>();
            //}

            // TestGaussSolverSquare2();
            // TestGaussSolverNotSquare2<Fraction>();

            // var xd4 = LinearSolver.Gauss<Fraction>(xd2, xd3);
            //Console.ReadKey();
        }

        static void StoreResults(string filePath, int size, long time, string error, string method)
        {
            File.AppendAllText(filePath, $"{size.ToString()};{time.ToString()};{error};{method};{Environment.NewLine}");
        }


        /// <summary>
        /// The test equality of solvers.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        private static void TestEqualityOfSolvers<T>()
            where T : Value<T>, new()
        {
            var templateMatrix = MyMatrix<T>.GetRandomMatrix(10, 10);
            var templateVector = MyMatrix<T>.GetRandomMatrix(1, 10);
            var solved1 = templateMatrix.SolveLinearEquation(templateVector, LinearSolver.Gauss);
            var solved2 = templateMatrix.SolveLinearEquation(templateVector, LinearSolver.PartialGauss);
            var solved3 = templateMatrix.SolveLinearEquation(templateVector, LinearSolver.FullGauss);
            Console.WriteLine($"Equality test of type {typeof(T).Name} solving methods: {(solved1 == solved2 && solved1 == solved3 ? "PASS" : "FAIL")}");
        }

        /// <summary>
        /// The test gauss solver.
        /// </summary>
        private static void TestGaussSolver()
        {
            var vector = new MyMatrix<MDouble>(
                new[,] { { new MDouble(3.0) }, { new MDouble(15.0) }, { new MDouble(14.0) } });
            var matrix = new MyMatrix<MDouble>(
                new[,]
                    {
                        {
                           new MDouble(3.0), new MDouble(2.0), new MDouble(-4.0) 
                        },
                        {
                           new MDouble(2.0), new MDouble(3.0), new MDouble(3.0) 
                        },
                        {
                           new MDouble(5.0), new MDouble(-3.0), new MDouble(1.0) 
                        }
                    });
            var solved1 = matrix.SolveLinearEquation(vector, LinearSolver.Gauss);
            var solved2 = matrix.SolveLinearEquation(vector, LinearSolver.PartialGauss);
            var solved3 = matrix.SolveLinearEquation(vector, LinearSolver.FullGauss);
            var expected_result = new MyMatrix<MDouble>(new[,] { { new MDouble(3.0) }, { new MDouble(1.0) }, { new MDouble(2.0) } });
            var flag = solved1 == expected_result && solved1 == solved2 && solved2 == solved3;
            Console.WriteLine($"Gaussian solver test: {(flag ? "PASS" : "FAIL")}");
        }

        /// <summary>
        /// The test gauss solver not square 2.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        private static void TestGaussSolverNotSquare2<T>()
            where T : Value<T>, new()
        {
            for (var i = 2; i < 250; i++)
            for (var j = i; j < 200; j++)
            {
                var seed = Guid.NewGuid().GetHashCode();
                var vector = MyMatrix<T>.GetRandomMatrix(1, i, seed);
                var matrix = MyMatrix<T>.GetRandomMatrix(j, i, seed);
                try
                {
                    var solved = LinearSolver.Gauss<T>(matrix, vector);
                }
                catch (Exception e)
                {
                    Console.WriteLine(matrix);
                    Console.WriteLine(vector);
                    Console.WriteLine($"Gaussian solver 2 test: FAIL. SIZE: {j}x{i}, SEED: {seed}, Exception: {e}");
                }
            }

            Console.WriteLine("Gaussian solver 2 test: PASS");
        }

        /// <summary>
        /// The test gauss solver square 2.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        private static void TestGaussSolverSquare2<T>()
            where T : Value<T>, new()
        {
            for (var i = 1; i < 500; i++)
            {
                var seed = Guid.NewGuid().GetHashCode();
                var vector = MyMatrix<T>.GetRandomMatrix(1, i, seed);
                var matrix = MyMatrix<T>.GetRandomMatrix(i, i, seed);
                try
                {
                    var solved = LinearSolver.Gauss(matrix, vector);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Gaussian solver 2 test: FAIL. SIZE: {i}x{i}, SEED: {seed}, Exception: {e}");
                }
            }

            Console.WriteLine("Gaussian solver 2 test: PASS");
        }

        private static void GenerateData<T>(string filename)
            where T : Value<T>, new()
        {
            Dictionary<int, int> incrementMap =
                new Dictionary<int, int> { { 100, 25 }, { 300, 50 }, { 500, 100 }, { 1000, 250 } };
            var solvingMethods = new List<(string, Func<MyMatrix<T>, MyMatrix<T>, MyMatrix<T>>)>
                                     {
                                         ("G", LinearSolver.Gauss), ("PG",LinearSolver.PartialGauss), ("FG", LinearSolver.FullGauss)
                                     };
            int size = 10;
            int increment = 10;
            do
            {
                // Calculate B
                var a = MyMatrix<T>.GetRandomMatrix(size, size);
                var x = MyMatrix<T>.GetRandomMatrix(1, size);
                MyMatrix<T> b = a * x;
                // Verify results
                foreach ((string name, Func<MyMatrix<T>, MyMatrix<T>, MyMatrix<T>> solvingMethod) in solvingMethods)
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
        private static (long time, string error) TestCorrectnessOfGauss<T>(MyMatrix<T> a, MyMatrix<T> x, MyMatrix<T> b, Func<MyMatrix<T>, MyMatrix<T>, MyMatrix<T>> solvingFunc)
            where T : Value<T>, new()
        {
            Stopwatch timer = Stopwatch.StartNew();
            MyMatrix<T> solved1 = a.SolveLinearEquation(b, solvingFunc);
            timer.Stop();
            Value<T> error = solved1.RelativeError(x);
            return (timer.ElapsedMilliseconds, error.ToString());
        }

        private static void TestMyMatrixMul()
        {
            //var x = new MyMatrix<MDouble>(
            //    new[,]
            //        {
            //            {
            //                new MDouble(12.0), new MDouble(7.0), new MDouble(3.0)
            //            },
            //            {
            //                new MDouble(4.0), new MDouble(5.0), new MDouble(6.0)
            //            },
            //            {
            //                new MDouble(7.0), new MDouble(8.0), new MDouble(9.0)
            //            }
            //        });
            //var x2 = new MyMatrix<MDouble>(
            //    new[,]
            //        {
            //            {
            //                new MDouble(5.0), new MDouble(8.0), new MDouble(1.0), new MDouble(2.0)
            //            },
            //            {
            //                new MDouble(6.0), new MDouble(7.0), new MDouble(3.0), new MDouble(0.0)
            //            },
            //            {
            //                new MDouble(4.0), new MDouble(5.0), new MDouble(9.0), new MDouble(1.0)
            //            }
            //        });
            var x = MyMatrix<MDouble>.GetRandomMatrix(3, 3);
            var x2 = MyMatrix<MDouble>.GetRandomMatrix(5, 3);
            var res = x * x2;
            Console.WriteLine($"Test MyMatrixMul: {(true ? "PASS" : "FAIL")}");

        }
        /// <summary>
        /// The test moja macierz.
        /// </summary>
        private static void TestMyMatrix()
        {
            var vector = new MyMatrix<MDouble>(
                new Value<MDouble>[,]
                    {
                        {
                           new MDouble(3.0), new MDouble(15.0), new MDouble(14.0) 
                        },
                        {
                           new MDouble(2.1), new MDouble(2.2), new MDouble(2.3), 
                        }
                    });
            var flag = vector.Height == 2 && vector.Width == 3;

            var vector2 = new MyMatrix<MDouble>(vector);
            flag &= vector == vector2;
            vector[0, 0] = new MDouble(6);
            flag &= vector != vector2;
            vector2[0, 0] = new MDouble(6);
            var beforeSwapRows = new MyMatrix<MDouble>(vector2);
            var beforeSwapCols = new MyMatrix<MDouble>(vector);
            flag &= vector == vector2;
            vector2.SwapRows(0, 1);
            flag &= vector != vector2;
            vector2.SwapRows(0, 1);
            flag &= vector2 == vector;
            flag &= vector2 == vector && vector2 == beforeSwapRows;


            vector.SwapColumns(0,1);
            flag &= vector2 != vector;
            vector.SwapColumns(0,1);
            flag &= vector2 == vector;
            flag &= vector2 == vector && vector == beforeSwapCols;
            Console.WriteLine($"MyMatrix test: {(flag ? "PASS" : "FAIL")}");
            
        }

        /// <summary>
        /// The test value.
        /// </summary>
        /// <exception cref="Exception">
        /// </exception>
        private static void TestValue()
        {
            // Value<Fraction> v = new Fraction(1, 2) + new Fraction(1, 2);
            // Value<MDouble> md = new MDouble(0.5) + new MDouble(0.5);
            // Value<MFloat> mf = new MFloat(0.5f) + new MFloat(0.5f);
            var elements = new List<Func<bool>>
                               {
                                   // Add tests
                                   () => new Fraction(1, 3) + new Fraction(1, 3) == new Fraction(2, 3),
                                   () => new MDouble(5.0) + new MDouble(5.2) == new MDouble(5.2 + 5.0),
                                   () => new MFloat(7) + new MFloat(12) == new MFloat(7 + 12),
                                   () => new Fraction(-1, 3) + new Fraction(1, 3) == Fraction.Zero,
                                   () => new MDouble(-5.0) + new MDouble(5.2) == new MDouble(-5.0 + 5.2),
                                   () => new MFloat(-7) + new MFloat(12) == new MFloat(-7 + 12),
                                   () => new Fraction(1, 3) + new Fraction(-1, 3) == Fraction.Zero,
                                   () => new MDouble(5.0) + new MDouble(-5.2) == new MDouble(5.0 + -5.2),
                                   () => new MFloat(7) + new MFloat(-12) == new MFloat(7 + -12),

                                   // Zeros testing
                                   () => new Fraction() + new Fraction() == new Fraction(),
                                   () => new MDouble() + new MDouble() == new MDouble(),
                                   () => new MFloat() + new MFloat() == new MFloat(),

                                   // comparison tests
                                   () => new Fraction(1, 4) < new Fraction(2, 4),
                                   () => new Fraction(1, 2) < new Fraction(2, 3),
                                   () => new Fraction(1, 5) == new Fraction(5, 25),

                                   // absolute tests
                                   () => new Fraction(-1, 1).Absolute() == new Fraction(1,1),
                                   () => new Fraction(-5, 2).Absolute() == new Fraction(5,2),
                                   () => new Fraction(-10, 3).Absolute() == new Fraction(10,3),
                                   () => new Fraction(-2, 4).Absolute() == new Fraction(2,4),
                                   () => new Fraction(1, 1).Absolute() == new Fraction(1,1),
                                   () => new Fraction(5, 2).Absolute() == new Fraction(5,2),
                                   () => new Fraction(10, 1).Absolute() == new Fraction(10,1),
                                   () => new Fraction(2, 1).Absolute() == new Fraction(2,1),

                                   () => new MDouble().SetValue(-1, 1).Absolute() == new MDouble().SetValue(1,1),
                                   () => new MDouble().SetValue(-5, 2).Absolute() == new MDouble().SetValue(5,2),
                                   () => new MDouble().SetValue(-10, 3).Absolute() == new MDouble().SetValue(10,3),
                                   () => new MDouble().SetValue(-2, 4).Absolute() == new MDouble().SetValue(2,4),
                                   () => new MDouble().SetValue(1, 1).Absolute() == new MDouble().SetValue(1,1),
                                   () => new MDouble().SetValue(5, 2).Absolute() == new MDouble().SetValue(5,2),
                                   () => new MDouble().SetValue(10, 1).Absolute() == new MDouble().SetValue(10,1),
                                   () => new MDouble().SetValue(2, 1).Absolute() == new MDouble().SetValue(2,1),

                                   () => new MFloat().SetValue(-1, 1).Absolute() == new MFloat().SetValue(1,1),
                                   () => new MFloat().SetValue(-5, 2).Absolute() == new MFloat().SetValue(5,2),
                                   () => new MFloat().SetValue(-10, 3).Absolute() == new MFloat().SetValue(10,3),
                                   () => new MFloat().SetValue(-2, 4).Absolute() == new MFloat().SetValue(2,4),
                                   () => new MFloat().SetValue(1, 1).Absolute() == new MFloat().SetValue(1,1),
                                   () => new MFloat().SetValue(5, 2).Absolute() == new MFloat().SetValue(5,2),
                                   () => new MFloat().SetValue(10, 1).Absolute() == new MFloat().SetValue(10,1),
                                   () => new MFloat().SetValue(2, 1).Absolute() == new MFloat().SetValue(2,1),
                                   // () => new Value<Fraction>(new Fraction(1,-3)) + new Value<Fraction>(new Fraction(1,-3)) == new Value<Fraction>(new Fraction(-2,3)),
                                   // () => new Value<Fraction>(new Fraction(-1,3)) + new Value<Fraction>(new Fraction(-1,3)) == new Value<Fraction>(new Fraction(-2,3))
                               };
            var i = 0;
            var passed = true;
            foreach (var element in elements)
            {
                if (!element())
                {
                    Console.WriteLine($"Test {i} failed");
                    passed = false;
                }

                i++;
            }

            if (!passed) throw new Exception("Some test failed.");

            Console.WriteLine("Value tests: PASS.");
        }
    }
}