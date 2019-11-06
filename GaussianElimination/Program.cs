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
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        // https://stackoverflow.com/questions/32597581/best-way-to-exchange-matrix-rows?fbclid=IwAR2eKy_Uj0JNMRtRv9m450v1MLjTO-VUykX2JaKfWab_5q5muqeVc2qAJv4
        private static void Main(string[] args)
        {
            TestValue();
            TestGaussSolver();
            TestMojaMacierz();

            // TestGaussSolverSquare2();
            TestGaussSolverNotSquare2();

            // var xd = new MojaMacierz<Fraction>(10, 20);
            // xd.FillMatrixWithValue(new Value<Fraction>());
            // var xd2 = MojaMacierz<Fraction>.FillWithRandomFractions(10, 20);
            // var xd3 = MojaMacierz<Fraction>.FillWithRandomFractions(1, 20);
            // var xd4 = LinearSolver.Gauss<Fraction>(xd2, xd3);
            Console.ReadKey();
        }

        private static void TestGaussSolver()
        {
            var vector = new MojaMacierz<MDouble>(
                new[,] { { new MDouble(3.0) }, { new MDouble(15.0) }, { new MDouble(14.0) } });
            var matrix = new MojaMacierz<MDouble>(
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
            var solved = LinearSolver.Gauss(matrix, vector);
            var expected_result = new MojaMacierz<MDouble>(
                new[,] { { new MDouble(3.0) }, { new MDouble(1.0) }, { new MDouble(2.0) } });
            if (solved == expected_result) Console.WriteLine("Gaussian solver test: PASS");
        }

        private static void TestGaussSolverNotSquare2()
        {
            for (var i = 2; i < 250; i++)
            for (var j = 2; j < 200; j++)
            {
                var seed = Guid.NewGuid().GetHashCode();
                var vector = MojaMacierz<MDouble>.GetRandomMatrix(1, i, seed);
                var matrix = MojaMacierz<MDouble>.GetRandomMatrix(j, i, seed);
                try
                {
                    var solved = LinearSolver.Gauss(matrix, vector);
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

        private static void TestGaussSolverSquare2()
        {
            for (var i = 1; i < 500; i++)
            {
                var seed = Guid.NewGuid().GetHashCode();
                var vector = MojaMacierz<MDouble>.GetRandomMatrix(1, i, seed);
                var matrix = MojaMacierz<MDouble>.GetRandomMatrix(i, i, seed);
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

        private static void TestMojaMacierz()
        {
            var vector = new MojaMacierz<MDouble>(
                new[,] { { new MDouble(3.0) }, { new MDouble(15.0) }, { new MDouble(14.0) } });
            var flag = vector.Height == 3 && vector.Width == 1;

            var vector2 = new MojaMacierz<MDouble>(vector);
            flag &= vector == vector2;
            vector[0, 0] = new MDouble(6);
            flag &= vector != vector2;
            vector2[0, 0] = new MDouble(6);
            flag &= vector == vector2;

            if (flag) Console.WriteLine("MojaMacierz test: PASS");
        }

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
                                   () => new MFloat() + new MFloat() == new MFloat()

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