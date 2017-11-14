using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace RTSMSMiner.Classes
{
    // This class containing List of Sentences in a RSSItemFeed.(tokenized by Delimiter "^" ).
    class RSSItemSentences
    {
        private ArrayList sentences = new ArrayList();
        private Guid primary_key;
        public void AddSentence(String sent)
        {
            if (sent != null)
            {
                sentences.Add(sent.ToLower());
            }
        }
        public ArrayList GetFeeds()
        {
            return sentences;
        }
        public void SetGuid(string guidid)
        {
            if (guidid != null)
            {
                primary_key = Guid.Parse(guidid);
            }
        }
        public String GetGuid()
        {
            return primary_key.ToString();
        }
    }
}
