using System;
using WordSuggestions;
using System.Diagnostics;
using System.IO;

namespace WordSuggestionsBenchmarkingTests
{
    class LevenshteinTests
    {
        static void Main(string[] args)
        {
            var words = File.ReadAllLines("words_alpha.txt");
            Stopwatch timer = new Stopwatch();
            Random gen = new Random();
            int testsToRun = 1000;

            double fullMatrixSum = 0;
            double partialMatrixSum = 0;
            
            for(int i = 0;i<testsToRun;i++)
            {
                string firstWord = words[gen.Next(0,words.Length-1)];
                string secondWord = words[gen.Next(0, words.Length - 1)];

                timer.Restart();
                _ = Levenshtein.GetFullMatrixDistance(firstWord, secondWord);
                timer.Stop();
                fullMatrixSum += timer.Elapsed.TotalMilliseconds;

                timer.Restart();
                _ = Levenshtein.GetPartialMatrixDistance(firstWord, secondWord);
                timer.Stop();
                partialMatrixSum += timer.Elapsed.TotalMilliseconds;
            }

            Console.WriteLine($"Full Matrix Algo Average Time for {testsToRun} tests: {fullMatrixSum / testsToRun} ms");
            Console.WriteLine($"Partial Matrix Algo Average Time for {testsToRun} tests: {partialMatrixSum / testsToRun} ms");
        }
    }
}
