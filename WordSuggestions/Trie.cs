using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Tries
{
    public class Trie
    {
        public class TrieNode
        {
            public char Value { get; }
            public Dictionary<char, TrieNode> Children;
            public bool IsEndOfWord;
            public TrieNode(char val, bool isEndOfWord)
            {
                Value = val;
                IsEndOfWord = isEndOfWord;
                Children = new Dictionary<char, TrieNode>();
            }
        }
        TrieNode Root;
        public Trie()
        {
            Root = new TrieNode('\n',false);
        }

        public bool Insert(string word)
        {
            return InsertLetters(word.AsSpan(), Root);

            bool InsertLetters(ReadOnlySpan<char> letters, TrieNode node)
            {
                if(letters.Length == 0)
                {
                    if (node.IsEndOfWord) return false;

                    node.IsEndOfWord = true;
                    return true;
                }

                if(!node.Children.TryGetValue(letters[0], out var newNode))
                {
                    newNode = new TrieNode(letters[0], false);
                    node.Children.Add(letters[0], newNode);
                }

                return InsertLetters(letters.Slice(1), newNode);
            }
        }

        public bool Add(string word)
        {
            var currentNode = Root;
            int letterIndex = -1;
            foreach(char character in word)
            {
                letterIndex++;
                if(currentNode.Children.ContainsKey(character))
                {
                    if(letterIndex == word.Length - 1 && currentNode.Children[character].IsEndOfWord)
                    {
                        return false;
                    }
                    currentNode = currentNode.Children[character];
                    continue;
                }
                currentNode.Children.Add(character, new TrieNode(character, false));
                if (letterIndex == word.Length-1)
                {
                    currentNode.Children[character].IsEndOfWord = true;
                }
                currentNode = currentNode.Children[character];
            }
            return true;
        }

        public bool Remove(string word)
        {
            TrieNode currentNode = Root;
            for (int i = 0; i < word.Length; i++)
            {
                if (!currentNode.Children.ContainsKey(word[i])) return false;
                currentNode = currentNode.Children[word[i]];
                if (i == word.Length - 1)
                {
                    if (!currentNode.IsEndOfWord) return false;
                    currentNode.IsEndOfWord = false;
                    return true;
                }
            }
            return true;
        }

        public bool IsThere(string word)
        {
            return Contains(word.AsSpan(), Root);

            bool Contains(ReadOnlySpan<char> wordAsChars, TrieNode node)
            {
                if (wordAsChars.Length == 0 && (node?.IsEndOfWord ?? false)) return true;
                if (wordAsChars.Length == 0 || node == null) return false;

                node.Children.TryGetValue(wordAsChars[0], out var nextNode);
                return Contains(wordAsChars.Slice(1), nextNode);
            }            
        }

        public bool ContainsWord(string word)
        {
            TrieNode currentNode = Root;
            for(int i = 0;i<word.Length;i++) 
            {
                if (!currentNode.Children.ContainsKey(word[i])) return false;
                currentNode = currentNode.Children[word[i]];
                if (i == word.Length - 1 && !currentNode.IsEndOfWord) return false;
            }
            return true;
        }

        public TrieNode FindNode(string prefix)
        {
            var currentNode = Root;
            for(int i = 0;i<prefix.Length;i++)
            {
                if(!currentNode.Children.TryGetValue(prefix[i], out currentNode))
                {
                    return null;
                }
            }
            return currentNode;
        }

        public IEnumerable<string> GetAllWordsMatchingPrefix(string prefix)
        {
            List<string> output = new List<string>();

            TrieNode node = FindNode(prefix);

            if (node is null)
            {
                return Enumerable.Empty<string>();
            }

            GetMatchingWord(node, output, prefix.Substring(0,prefix.Length-1));

            return output;
        }

        private void GetMatchingWord(TrieNode node, List<string> words, string prefix)
        {
            if (node == null) return;
           
            foreach(var child in node.Children)
            {
                GetMatchingWord(child.Value, words, prefix + node.Value);
            }

            if (node.IsEndOfWord)
            {
                words.Add(prefix + node.Value);
            }
        }
    }
}
