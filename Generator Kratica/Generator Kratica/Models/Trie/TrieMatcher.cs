using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Generator_Kratica.Models.Trie
{
    /// <summary>
    /// Search through a trie.
    /// </summary>
    /// <typeparam name="W">Type of value we use in our trie</typeparam>
    class TrieMatcher<W> : ITrieMatcher<W> where W : class
    {
        private TrieNode<W> root;
        private TrieNode<W> current;
        private string wordMatched;

        /// <summary>
        /// Create a word matcher for requested trie. 
        /// </summary>
        /// <param name="root">Trie we want to search in</param>
        public TrieMatcher(TrieNode<W> root)
        {
            this.root = root;
            this.current = root;
        }

        /// <summary>
        /// Get back to root of the trie.
        /// </summary>
        public void ResetMatcher()
        {
            current = root;
            wordMatched = "";
        }

        /// <summary>
        /// Get back to parent from current position in trie.
        /// </summary>
        public void StepBack()
        {
            if (current != root)
            {
                current = current.Parent;
                wordMatched = wordMatched.Substring(0, wordMatched.Length - 1);
            }
        }

        /// <summary>
        /// Add letter to our current word if it's available.
        /// </summary>
        /// <param name="nextLetter">Next letter in our current word</param>
        /// <returns>True if there was child under current node.</returns>
        public bool StepForward(char nextLetter)
        {
            if (current.ContainsChild(nextLetter))
            {
                current = current.GetChild(nextLetter);
                wordMatched += nextLetter;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks whether match is a word.
        /// </summary>
        /// <returns>True if match is actually a word.</returns>
        public bool isMatchWord()
        {
            return current.isWord();
        }

        /// <summary>
        /// Gets match from current position in a trie.
        /// </summary>
        /// <returns>Currently matched word if it's actually a word.</returns>
        public W GetCurrentMatch()
        {
            if (isMatchWord())
            {
                return current.Word;
            }
            return null;
        }
    }
}