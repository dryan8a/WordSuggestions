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

namespace WordSuggestions
{
    public partial class Form1 : Form
    {
        int NumberOfSuggestions = 5;
        string[] words;
        Trie wordsTrie = new Trie();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CompletionSuggestions.Items.Clear();
            SpellingSuggestions.Items.Clear();
            string[] words = File.ReadAllLines("words_alpha.txt");
            foreach(var word in words)
            {
                wordsTrie.Add(word);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            CompletionSuggestions.Items.Clear();
            SpellingSuggestions.Items.Clear();

            string currentWord = (InputBox.Text.Contains(' ') ? InputBox.Text.Substring(InputBox.Text.LastIndexOf(' ')) : InputBox.Text).Trim(' '); //gets the last or only word from the input
            if (currentWord == "") return;
            var completionSuggestions = wordsTrie.GetAllWordsMatchingPrefix(currentWord);

            for(int i = 0;i<NumberOfSuggestions;i++)
            {
                if (completionSuggestions.Count() <= i) break;
                CompletionSuggestions.Items.Add(completionSuggestions.ElementAt(i));
            }
        }

    }
}
