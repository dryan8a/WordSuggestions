using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSuggestions
{
    public static class Levenshtein
    {
        /// <summary>
        /// Gets Levenshtein distance of two strings using the full matrix implementation
        /// </summary>
        /// <param name="left">The first string to compare</param>
        /// <param name="right">The second string to compare</param>
        /// <returns></returns>
        public static int GetFullMatrixDistance(string left, string right)
        {
            var matrix = new int[left.Length+1,right.Length+1];
            for(int i = 0;i<=left.Length;i++)
            {
                matrix[i, 0] = i;
            }
            for(int i = 1;i<=right.Length;i++)
            {
                matrix[0, i] = i;
            }

            for(int n = 1;n<=right.Length;n++)
            {
                for(int m = 1;m<=left.Length;m++)
                {
                    int subCost = left[m-1] == right[n-1] ? 0 : 1;
                    matrix[m, n] = Math.Min(matrix[m - 1, n] + 1, Math.Min(matrix[m, n - 1] + 1, matrix[m - 1, n - 1] + subCost));
                }
            }

            return matrix[left.Length, right.Length];
        }

        /// <summary>
        /// Gets the Maximum Possible Levenshtein distance between two words
        /// </summary>
        /// <param name="left">The first string to compare</param>
        /// <param name="right">The second string to compare</param>
        /// <returns></returns>
        public static int GetMaxDistance(string left, string right)
        {
            return left.Length > right.Length ? left.Length : right.Length;
        }

        /// <summary>
        /// Gets the Ratio of the Actual Levenshtein distance to the Max Levenshtein distance of two words
        /// </summary>
        /// <param name="left">The first string to compare</param>
        /// <param name="right">The second string to compare</param>
        /// <returns></returns>
        public static double GetFullMatrixLevenshteinRatio(string left,string right)
        {
            return (double)GetFullMatrixDistance(left, right) / GetMaxDistance(left, right);
        }
    }
}
