using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace RTSMSMiner.Classes
{
    class CategorySynonymyList : SynonymyWordList
    {
        private ArrayList CategoryData = new ArrayList();
        private ArrayList filenames = new ArrayList();
        public string CategoryName = String.Empty;
        private int NoOfMatch = 0;
        public void Matched()
        {
            NoOfMatch++;
        }
        public void ResetCounter()
        {
            NoOfMatch = 0;
        }
        public int GetMatchedWordNumber()
        {
            return NoOfMatch;
        }
        public bool FindWord(string str)
        {
            for (int i = 0; i < CategoryData.Count; i++)
            {
                SynonymyWordList tmp = new SynonymyWordList();
                tmp = (SynonymyWordList)CategoryData[i];
                if (tmp.IsExist(str))
                    return true;
            }
            return false;
        }

        public void LoadSynonymy(ArrayList paths, ArrayList names)
        {
            for (int i = 0; i < paths.Count; i++)
            {
                SynonymyWordList syn = new SynonymyWordList();
                syn.SetSynonymyName(names[i].ToString());
                using (StreamReader sr = File.OpenText(paths[i].ToString()))
                {
                    string input = null;
                    int j = 0;
                    while ((input = sr.ReadLine()) != null)
                    {
                        syn.AddSynonymy(input);
                        j++;
                    }
                }
                CategoryData.Add(syn);
            }
        }
        public void LoadCategory(string filepaths)
        {
            ArrayList SynonymyName = new ArrayList();
            using (StreamReader sr = File.OpenText(@"c:\event\" + filepaths + ".txt"))
            {
                string input = null;
                int j = 0;
                while ((input = sr.ReadLine()) != null)
                {
                    filenames.Add(@"c:\event\" + filepaths + "\\" + input);

                    SynonymyName.Add(input.Remove(input.Length - 6, 6));
                    j++;
                }
                LoadSynonymy(filenames, SynonymyName);
            }
        }

    }
}
