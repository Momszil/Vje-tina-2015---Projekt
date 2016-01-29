using System;
using System.Collections.Generic;

namespace Generator_Kratica.Models.Trie
{
    public interface ITrieMatcher<W> where W : class
    {
        W GetCurrentMatch();
        bool isMatchWord();
        void ResetMatcher();
        void StepBack();
        bool StepForward(char nextLetter);
    }
}