using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_Kratica.Models.Trie
{
    // KOJU KARIRANU KOŠULJU DANAS OBUĆI (kakao)

    /// <summary>
    /// ČVOR, djeca su slova, ako je neko slovo nekog ČVORA zadnje slovo neke riječi, W nije null.
    /// </summary>
    /// <typeparam name="W">Riječ koja završava na ČVORU</typeparam>
    class TrieNode<W> where W : class
    {
        // Ako je null, na ČVORU ne završava riječ
        public W Word { get; set; }

        // Slovo koje se nalazi na ČVORU
        public Char Letter { get; private set; }

        // Pointer za ČVOR roditelja
        public TrieNode<W> Parent { get; private set; }

        // Knjižnica dječjih ČVOROVA
        private Dictionary<Char, TrieNode<W>> children;

        // Konstruktor za root ČVOR
        public TrieNode(TrieNode<W> parent, Char letter)
        {
            this.Letter = letter;
            this.Word = null;
            this.Parent = parent;
            this.children = new Dictionary<Char, TrieNode<W>>();
        }

        // Vraća null ako ČVOR nema dijete sa traženim slovom
        public TrieNode<W> GetChild(char letter)
        {
            if (children.ContainsKey(letter))
            {
                return children[letter];
            }
            return null;
        }

        // Vraća true ako na ČVORU završava riječ
        public bool isWord()
        {
            return Word != null;
        }

        // Vraća broj djece ČVORA
        public int GetNumberofChildren()
        {
            return children.Count();
        }

        // Vraća true ako ČVOR nema djece
        public bool isLast()
        {
            return GetNumberofChildren() == 0;
        }

        // Vraća true ako ČVOR ima traženo dijete
        /// <summary>
        /// Checks whether there is node for requested letter.
        /// </summary>
        /// <param name="letter">Child we're searching for</param>
        /// <returns>True or False obv.</returns>
        public bool ContainsChild(char letter)
        {
            return children.ContainsKey(letter);
        }

        // Dodaje ČVOR ako ne postoji, vraća ČVOR za traženo slovo koje se dodaje
        /// <summary>
        /// Adds node for requested letter.
        /// </summary>
        /// <param name="letter">Child to be added</param>
        /// <returns>Requested node</returns>
        public TrieNode<W> AddChild(char letter)
        {
            if (children.ContainsKey(letter))
            {
                return children[letter];
            }
            else
            {
                TrieNode<W> newChild = new TrieNode<W>(this, letter);
                children.Add(letter, newChild);
                return newChild;
            }
        }

        // Dobijemo true ako smo izbrisali željeni ČVOR
        /// <summary>
        /// Removes child from trie
        /// </summary>
        /// <param name="letter">Child to be removed</param>
        /// <returns>True if child was removed, false if there was nothing to remove</returns>
        public bool RemoveChild(char letter)
        {
            if (children.ContainsKey(letter))
            {
                children.Remove(letter);
                return true;
            }
            return false;
        }
    }
}
