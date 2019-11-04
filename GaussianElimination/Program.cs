namespace GaussianElimination
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    internal class Program
    {
        //https://stackoverflow.com/questions/32597581/best-way-to-exchange-matrix-rows?fbclid=IwAR2eKy_Uj0JNMRtRv9m450v1MLjTO-VUykX2JaKfWab_5q5muqeVc2qAJv4
        private static void Main(string[] args)
        {
            TestValue();
            //TestGaussSolver();
            //var xd = new MojaMacierz<Fraction>(10, 20);
            //xd.FillMatrixWithValue(new Value<Fraction>());
            //var xd2 = MojaMacierz<Fraction>.FillWithRandomFractions(10, 20);
            //var xd3 = MojaMacierz<Fraction>.FillWithRandomFractions(1, 20);
            //var xd4 = LinearSolver.Gauss<Fraction>(xd2, xd3);
            Console.ReadKey();
        }
        private static void TestGaussSolver()
        {
            var vector = new MojaMacierz<double>(1, 3);
            var matrix = new MojaMacierz<double>(3, 3);
            matrix.Matrix = new Value<double>[,] {
                                { new Value<double>(3.0), new Value<double>(2.0), new Value<double>(-4.0) },
                                { new Value<double>(2.0), new Value<double>(3.0), new Value<double>(3.0) },
                                { new Value<double>(5.0), new Value<double>(-3.0), new Value<double>(1.0) }
                            };
            vector.Matrix = new Value<double>[,] {
                                { new Value<double>(3.0)},
                                { new Value<double>(15.0)},
                                { new Value<double>(14.0)}
                            };
            var solved = LinearSolver.Gauss<double>(matrix, vector);
            var expected_result = new MojaMacierz<double>(1, 3);
            expected_result.Matrix = new Value<double>[,] {
                                { new Value<double>(3.0)},
                                { new Value<double>(1.0)},
                                { new Value<double>(2.0)}
                            };
            if (solved == expected_result) //TODO: Fix me!
            {
                Console.WriteLine("PASS");
            }
        }
        private static void TestValue()
        {
            var elements = new List<Func<bool>>
                               {
                                   // Add tests
                                   () => new Value<Fraction>(new Fraction(1, 3))
                                         + new Value<Fraction>(new Fraction(1, 3))
                                         == new Value<Fraction>(new Fraction(2, 3)),
                                   () => new Value<double>(5.0) + new Value<double>(5.2)
                                         == new Value<double>(5.2 + 5.0),
                                   () => new Value<BigInteger>(7) + new Value<BigInteger>(12)
                                         == new Value<BigInteger>(7 + 12),
                                   () => new Value<Fraction>(new Fraction(-1, 3))
                                         + new Value<Fraction>(new Fraction(1, 3))
                                         == new Value<Fraction>(Fraction.Zero),
                                   () => new Value<double>(-5.0) + new Value<double>(5.2)
                                         == new Value<double>(-5.0 + 5.2),
                                   () => new Value<BigInteger>(-7) + new Value<BigInteger>(12)
                                         == new Value<BigInteger>(-7 + 12),
                                   () => new Value<Fraction>(new Fraction(1, 3))
                                         + new Value<Fraction>(new Fraction(-1, 3))
                                         == new Value<Fraction>(Fraction.Zero),
                                   () => new Value<double>(5.0) + new Value<double>(-5.2)
                                         == new Value<double>(5.0 + -5.2),
                                   () => new Value<BigInteger>(7) + new Value<BigInteger>(-12)
                                         == new Value<BigInteger>(7 + -12),
                                    // Zeros testing
                                   () => new Value<Fraction>() + new Value<Fraction>() 
                                         == new Value<Fraction>(),
                                   () => new Value<double>() + new Value<double>()
                                         == new Value<double>(),
                                   () => new Value<float>() + new Value<float>()
                                         == new Value<float>(),
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

            Console.WriteLine("Tests passed.");
        }
    }
}