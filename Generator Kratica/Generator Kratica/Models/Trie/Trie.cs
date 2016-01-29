using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Generator_Kratica.Models.Trie
{
    public class Trie<W> where W : class
    {
        private TrieNode<W> root;

        public ITrieMatcher<W> Matcher { get; private set; }

        /// <summary>
        /// Create an empty trie.
        /// </summary>
        public Trie()
        {
            this.root = new TrieNode<W>(null, '\0');
            this.Matcher = new TrieMatcher<W>(this.root);
        }

        /// <summary>
        /// Puts a new word in trie.
        /// </summary>
        /// <param name="key">A word.</param>
        /// <param name="value">Word stored on node defined by last letter.</param>
        public void Put(string key, W value)
        {
            TrieNode<W> node = root;
            foreach (char c in key)
            {
                node = node.AddChild(c);
            }
            node.Word = value;
        }

    }
}