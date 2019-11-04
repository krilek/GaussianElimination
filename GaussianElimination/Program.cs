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
            var xd = new MojaMacierz<Fraction>(10, 20);
            xd.FillMatrixWithValue(new Value<Fraction>(new Fraction(1,2)));
            var xd2 = MojaMacierz<Fraction>.FillWithRandom(10, 20);
            Console.ReadKey();
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
                                         == new Value<BigInteger>(7 + -12)

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