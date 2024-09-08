using System;
using System.Threading.Tasks;

namespace Calculations.ConsoleClient
{
    internal static class Program
    {
        /// <summary>
        /// Calculates the sum from 1 to n synchronously.
        /// The value of n is set by the user from the console.
        /// The user can change the boundary n during the calculation, which causes the calculation to be restarted,
        /// this should not crash the application.
        /// After receiving the result, be able to continue calculations without leaving the console.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task Main()
        {
            while (true)
            {
                using var cts = new CancellationTokenSource();
                if (int.TryParse(Console.ReadLine(), out int n))
                {
                    var progress = new Progress<(int, long)>(i =>
                    {
                        Console.Write("number: " + i.Item1);
                        Console.WriteLine("sum:" + i.Item2);
                    });

                    await Calculator.CalculateSumAsync(n, cts.Token, progress);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
