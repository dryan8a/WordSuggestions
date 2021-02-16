﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Tries;
using System.Text.RegularExpressions;

namespace WordSuggestions
{
    public partial class Form1 : Form
    {
        const int NumberOfSuggestionsCap = 25; //The number of suggestions given to the user if CapSuggestions == true
        bool CapSuggestions = true;
        const double MaxLevenshteinRatio = 0.3; //The max ratio of the recieved Levenshtein distance to the max Levenshtein of a suggestion and the typed word needed to display the suggestion

        string[] words;
        Trie wordsTrie = new Trie();
        Dictionary<string, List<string>> soundexToWords = new Dictionary<string, List<string>>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CompletionSuggestions.Items.Clear();
            SpellingSuggestions.Items.Clear();
            words = File.ReadAllLines("words_alpha.txt");
            foreach(var word in words) //fills the wordsTrie and soundexToWords
            {
                wordsTrie.Add(word);

                string soundex = Soundex.GetSoundex(word);
                if(soundexToWords.ContainsKey(soundex))
                {
                    soundexToWords[soundex].Add(word);
                }
                else
                {
                    soundexToWords.Add(soundex, new List<string>() { word });
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateSuggestions();
        }

        private void UpdateSuggestions()
        {
            CompletionSuggestions.Items.Clear();
            SpellingSuggestions.Items.Clear();
            ValidWordCheckBox.Checked = false;

            string currentWord = (InputBox.Text.Contains(' ') ? InputBox.Text.Substring(InputBox.Text.LastIndexOf(' ')) : InputBox.Text).Trim(' '); //gets the last or only word from the input
            if (currentWord == "" || Regex.IsMatch(currentWord, "/[^A-Za-z]")) return; //ensures the word isn't empty and entirely alphabetic
            var completionSuggestions = wordsTrie.GetAllWordsMatchingPrefix(currentWord);

            ValidWordCheckBox.Checked = completionSuggestions.Contains(currentWord.ToLower());

            for (int i = 0; i < (CapSuggestions ? NumberOfSuggestionsCap : completionSuggestions.Count()); i++) //fills in all completion suggestions using the collection generated by the Trie
            {
                if (i >= completionSuggestions.Count()) break;
                CompletionSuggestions.Items.Add(completionSuggestions.ElementAt(i));
            }

            string wordSoundex = Soundex.GetSoundex(currentWord);
            if (soundexToWords.ContainsKey(wordSoundex))
            {
                AddItemsToSpellingSuggestions(soundexToWords[wordSoundex],currentWord);
            }
            foreach (var keySoundex in soundexToWords.Keys)
            {
                if (CapSuggestions && SpellingSuggestions.Items.Count == NumberOfSuggestionsCap) break;
                if (Soundex.FindSoundexDifference(keySoundex, wordSoundex) == 3)
                {
                    AddItemsToSpellingSuggestions(soundexToWords[keySoundex],currentWord);
                }
            }
        }

        private void AddItemsToSpellingSuggestions(List<string> words, string currentWord)
        {
            int i = 0;
            foreach(var word in words)
            {
                if (CapSuggestions && i == NumberOfSuggestionsCap - SpellingSuggestions.Items.Count) return;

                if (Levenshtein.GetFullMatrixLevenshteinRatio(currentWord, word) <= MaxLevenshteinRatio)
                {
                    SpellingSuggestions.Items.Add(word);
                    i++;
                } 
            }
        }

        private void ScrollBarCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CapSuggestions = !ScrollBarCheckBox.Checked;
            UpdateSuggestions();
        }
    }
}
