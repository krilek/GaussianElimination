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

    using GaussianElimination.Lib;

    #endregion

    /// <summary>
    ///     The program.
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
            if (args.Length < 1)
            {
                Console.WriteLine("Specify parameter for operation. double, float, fraction.");
                return;
            }

            var filename = $"{args[0]}_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.csv";
            int startingSize =
                Convert.ToInt32(string.IsNullOrEmpty(args.Length >= 2 ? args[1] : null) ? "10" : args[1]);
            switch (args[0])
            {
                case "double":
                    GaussTests.GenerateData<MDouble>(filename, startingSize);
                    break;
                case "float":
                    GaussTests.GenerateData<MFloat>(filename, startingSize);
                    break;
                case "fraction":
                    GaussTests.GenerateData<Fraction>(filename, startingSize);
                    break;
                default:
                    Console.WriteLine("Wrong argument.");
                    break;
            }

            Console.WriteLine("Press anything to quit.");
            Console.ReadKey();
        }
    }
}