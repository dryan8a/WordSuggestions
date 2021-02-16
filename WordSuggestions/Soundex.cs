using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSuggestions
{
    public static class Soundex
    {
        /// <summary>
        /// Gets the soundex of a word
        /// </summary>
        /// <param name="word">the word to get the soundex of</param>
        /// <returns></returns>
        public static string GetSoundex(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) return "";

            StringBuilder equivalentSoundex = new StringBuilder(word[0].ToString().ToUpper());

            for(int i = 1;i<word.Length;i++)
            {
                if (equivalentSoundex.Length == 4) break;

                char numToAppend = GetEquivalentNumber(word[i]);
                
                if(equivalentSoundex[equivalentSoundex.Length-1] != numToAppend && numToAppend != ' ')
                {
                    equivalentSoundex.Append(numToAppend);
                }
            }

            while(equivalentSoundex.Length < 4)
            {
                equivalentSoundex.Append("0");
            }

            return equivalentSoundex.ToString();
        }

        /// <summary>
        /// Part of the Soundex Algo responsible for converting a character to its respective Soundex number
        /// </summary>
        /// <param name="character">The character to convert</param>
        /// <returns></returns>
        private static char GetEquivalentNumber(char character)
        {
            if("bfpv".IndexOf(character) > -1)
            {
                return '1';
            }
            if("cgjkqsxz".IndexOf(character) > -1)
            {
                return '2';
            }
            if("dt".IndexOf(character) > -1)
            {
                return '3';
            }
            if(character == 'l')
            {
                return '4';
            }
            if("mn".IndexOf(character) > -1)
            {
                return '5';
            }
            if(character == 'r')
            {
                return '6';
            }
            return ' '; //returns ' ' for invalid or ignored characters
        }

        /// <summary>
        /// Finds the Soundex difference of two Soundex
        /// </summary>
        /// <param name="soundex1">The first Soundex to compare</param>
        /// <param name="soundex2">The second Soundex to compare</param>
        /// <returns></returns>
        public static int FindSoundexDifference(string soundex1, string soundex2)
        {
            if(soundex1 == soundex2)
            {
                return 4;
            }
            if(soundex1.Substring(1,3) == soundex2.Substring(1,3))
            {
                return 3;
            }

            int currentRank = 0;
            if (soundex1.Substring(1,2) == soundex2.Substring(1,2) || soundex1.Substring(2,2) == soundex2.Substring(2,2))
            {
                currentRank = 2;
            }
            else
            {
                for(int i = 1;i<4; i++)
                {
                    currentRank += soundex2.Contains(soundex1[i]) ? 1 : 0;
                }
            }
            if(soundex1[0] == soundex2[0])
            {
                currentRank++;
            }

            return currentRank == 0 ? 1 : currentRank;
        }
    }
}