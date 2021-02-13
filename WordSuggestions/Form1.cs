using System;
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
        int NumberOfSuggestionsCap = 25;
        bool CapSuggestions = true;
        int MaxSpellingDifferences = 10;

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
            string[] words = File.ReadAllLines("words_alpha.txt");
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
            if (currentWord == "" || Regex.IsMatch(currentWord, "/[^A-Za-z]")) return;
            var completionSuggestions = wordsTrie.GetAllWordsMatchingPrefix(currentWord);


            if (completionSuggestions.Contains(currentWord.ToLower()))
            {
                ValidWordCheckBox.Checked = true;
            }

            for (int i = 0; i < NumberOfSuggestionsCap; i++)
            {
                if (i >= completionSuggestions.Count()) break;
                CompletionSuggestions.Items.Add(completionSuggestions.ElementAt(i));
            }

            string wordSoundex = Soundex.GetSoundex(currentWord);
            if (soundexToWords.ContainsKey(wordSoundex))
            {
                AddItemsToSpellingSuggestions(soundexToWords[wordSoundex]);
            }
            foreach (var keySoundex in soundexToWords.Keys)
            {
                if (CapSuggestions && SpellingSuggestions.Items.Count == NumberOfSuggestionsCap) break;
                if (Soundex.FindSoundexDifference(keySoundex, wordSoundex) == 3)
                {
                    AddItemsToSpellingSuggestions(soundexToWords[keySoundex]);
                }
            }
        }

        private void AddItemsToSpellingSuggestions(List<string> words)
        {
            if (CapSuggestions && words.Count > NumberOfSuggestionsCap - SpellingSuggestions.Items.Count)
            {
                SpellingSuggestions.Items.AddRange(words.GetRange(0, NumberOfSuggestionsCap - SpellingSuggestions.Items.Count).ToArray());
            }
            else
            {
                SpellingSuggestions.Items.AddRange(words.ToArray());
            }
        }

        private void ScrollBarCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CapSuggestions = !ScrollBarCheckBox.Checked;
            UpdateSuggestions();
        }
    }
}
