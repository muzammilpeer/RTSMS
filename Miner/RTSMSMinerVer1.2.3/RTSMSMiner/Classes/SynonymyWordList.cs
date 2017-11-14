using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace RTSMSMiner.Classes
{
    class SynonymyWordList
    {
        private ArrayList syn = new ArrayList();
        private String SynonymyName = String.Empty;

        public void AddSynonymy(String sy)
        {
            if (sy != null)
            {
                syn.Add(sy);
            }
        }
        public String GetSynonmy(int i)
        {
            return syn[i].ToString();
        }
        public void SetSynonymyName(String name)
        {
            if (name != null)
            {
                SynonymyName = name;
            }
        }

        public bool IsExist(string word)
        {
            return syn.Contains(word);
        }
        public string GetSynonymyName()
        {
            return SynonymyName;
        }
    }
}
