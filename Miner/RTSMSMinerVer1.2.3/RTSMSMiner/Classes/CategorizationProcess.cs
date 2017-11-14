using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace RTSMSMiner.Classes
{
    class CategorizationProcess
    {
        private ArrayList CategoryObject = new ArrayList();
        public ArrayList CategorizeFeed(ArrayList feed)
        {
            StringBuilder feed_con = new StringBuilder();


            for (int k = 0; k < feed.Count; k++)
            {  // getting each sentence.
                feed_con.AppendLine(feed[k].ToString());
            }

            //Console.WriteLine(feed_con.ToString()+"\n");
            ArrayList results = new ArrayList();
            //string str = "In Karachi 4 people die in accident at malir";
            string str = feed_con.ToString();
            char[] separray = { ' ' };
            string[] wordslist = str.Split(separray, 100);
            for (int i = 0; i < wordslist.Length; i++)
            {
                //   Thread.Sleep(50);
                // Console.WriteLine(wordslist[i]);
            }
            for (int j = 0; j < CategoryObject.Count; j++)
            {
                CategorySynonymyList ctg = (CategorySynonymyList)CategoryObject[j];
                for (int i = 0; i < wordslist.Length; i++)
                {
                    if (ctg.FindWord(wordslist[i]) == true)
                        ctg.Matched();
                }
                CategoryResultSet tmp = new CategoryResultSet();
                tmp.categoryname = ctg.CategoryName;
                tmp.noofmatched = ctg.GetMatchedWordNumber();
                results.Add(tmp);
            }
            return results;
        }

        public void LoadCategories(ArrayList CategoryNames)
        {
            for (int i = 0; i < CategoryNames.Count; i++)
            {
                CategorySynonymyList ctg = new CategorySynonymyList();
                ctg.LoadCategory(CategoryNames[i].ToString());
                ctg.CategoryName = CategoryNames[i].ToString();
                CategoryObject.Add(ctg);

            }
        }

    }
}
