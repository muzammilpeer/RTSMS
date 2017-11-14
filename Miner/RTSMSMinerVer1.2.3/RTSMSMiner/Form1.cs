using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using RTSMSMiner.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RTSMSMiner
{
    public partial class Form1 : Form
    {
//        public const string connectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Backup_11_11_2010.mdb;Persist Security Info=True";
        private const string unMappedRSSItemquery = "select * from [RSSItem]  where [RSSItem].RSSItemID NOT IN( (select [MainMiner].RSSItemID from [MainMiner] where ([MainMiner].Categorization != 0)));";
        private const string unMappedIntensityRSSItemquery = "select * from [RSSItem]  where [RSSItem].RSSItemID NOT IN( (select [MainMiner].RSSItemID from [MainMiner] where ([MainMiner].Intensity != 0)));";
        private const string unMappedLocatingRSSItemquery = "select * from [RSSItem]  where [RSSItem].RSSItemID NOT IN( (select [MainMiner].RSSItemID from [MainMiner] where ([MainMiner].Locating != 0)));";
        private const string countquery = "SELECT count(*) FROM [RSSItem];";
        private const string selectrssitemquery = "SELECT * FROM [RSSItem];";
        private bool LoopBreak = false;
        private delegate void stringDelegate(string s);
        
        public Form1()
        {
            InitializeComponent();
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            StartBtn.Enabled = false;
            StopBtn.Enabled = true;
            
            CategorizationStatus.BackColor = Color.Green;
            Thread.Sleep(1 * 1000);
            IntensityStatus.BackColor = Color.Green;
            LocationStatus.BackColor = Color.Green;
        }
        private void StopBtn_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            LoopBreak = true;
            StopBtn.Enabled = false;
            StartBtn.Enabled = true;
            listBox1.Items.Clear();
            CategorizationStatus.BackColor = Color.Red;
            IntensityStatus.BackColor = Color.Red;
            LocationStatus.BackColor = Color.Red;
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            LoopBreak = false;
            if (backgroundWorker1.CancellationPending)//checks for cancel request
            {
                e.Cancel = true;
                return;
            }
            while (true)
            {
                if (LoopBreak == true)
                    break;
                if (CheckForUpdate() == true)
                {
                    AddItem("true");
                    IntensityProcess ls = new IntensityProcess();
                    ls.Process();
                    Categorization();
                    EventModule();
                }
                else
                {
//                    IntensityProcess ls = new IntensityProcess();
  //                  ls.Process();
//                    EventModule();
                    AddItem("No New Entry");
                }
                Thread.Sleep(10 * 1000);
            }
//            Thread.Sleep(30 * 1000);  // 2 Second Wait to report the status. and Reset the Progress Bar.
           // backgroundWorker1.ReportProgress(0);
        }        

        private int classifydatetime(string input)      // 0 for live, less than zero for history, greater then zero for future, 404 error
        {
            DateTime result = DateTime.Now;
            //Console.WriteLine(result.Date.ToString());

            if (DateTime.TryParse(input, out result))
            {
                //Console.WriteLine("This is in the loop: " + result.ToString());

                //if (DateTime.Compare(result, DateTime.Now) < 0)
                if (result.Date.CompareTo(DateTime.Now.Date) < 0)
                {
                    //Console.WriteLine("History");
                    return -1;
                }

                //if (DateTime.Compare(result, DateTime.Now) > 0)
                if (result.Date.CompareTo(DateTime.Now.Date) > 0)
                {
                    //Console.WriteLine("Future");
                    return 1;
                }

                //if (DateTime.Compare(result,DateTime.Now) == 0)
                if (result.Date.CompareTo(DateTime.Now.Date) == 0)
                {
                    //Console.WriteLine("Live");
                    return 0;
                }

                else
                    return 404;
            }

            else
            {
                //Console.WriteLine("loop not called!");
                return 404;
            }
        }

        private void EventModule()
        {
            ArrayList rset = new ArrayList();
            ArrayList TitleFeed = new ArrayList();
            ArrayList fetchedrssid = new ArrayList();
            const string command = unMappedLocatingRSSItemquery; //"SELECT * FROM [rtsms_db].[dbo].[RSSItem]";

            using (SqlConnection sql = new SqlConnection())
            {
                sql.ConnectionString = Properties.Settings.Default.RTSMSDBConnectionString;
                sql.Open();
                SqlCommand sqlcommand = new SqlCommand(command, sql);
                SqlDataReader reader = sqlcommand.ExecuteReader();
                while (reader.Read())
                {
                    rset.Add(reader.GetString(3));
                    TitleFeed.Add(reader.GetString(2));
                    fetchedrssid.Add(reader.GetString(0));
                }
                reader.Close();
                sql.Close();
            }


            char[] separray = { ' ' };
            string[] tokenized = new string[500];
            string query = String.Empty;
            int check = 0;
            int historycount = 0;
            int livecount = 0;
            int futurecount = 0;
            int livecheck = 0;
            int historycheck = 0;
            int futurecheck = 0;

            using (SqlConnection sql1 = new SqlConnection())
            {
                sql1.ConnectionString = Properties.Settings.Default.RTSMSDBConnectionString;
                sql1.Open();
                for (int i = 0; i < rset.Count; i++)
                {
                    //tempPieces = rsetarray[i].ToString().Split(separray, StringSplitOptions.RemoveEmptyEntries);
                    tokenized = rset[i].ToString().Split(separray, StringSplitOptions.RemoveEmptyEntries);

                    for (int j = 0; j < tokenized.Length; j++)
                    {
                        check = 0;
                        historycount = 0;
                        livecount = 0;
                        futurecount = 0;
                        livecheck = 0;
                        historycheck = 0;
                        futurecheck = 0;

                        //Console.WriteLine("This is the argument: " + tokenized[j].ToString());
                        check = classifydatetime(tokenized[j].ToString());

                        if (check != 404)
                        {
                            if (check == 0)
                                livecount++;
                            if (check > 0)
                                futurecount++;
                            if (check < 0)
                                historycount++;
                        }
                    }

                    if (livecount > futurecount && livecount > historycount)
                        livecheck = 1;
                    else
                        if (futurecount > livecount && futurecount > historycount)
                            futurecheck = 1;
                        else
                            if (historycount > livecount && historycount > futurecount)
                                historycheck = 1;
                            else
                                if (futurecheck == 0 && historycheck == 0 && livecheck == 0)
                                    livecheck = 1;
                    rset[i] = rset[i].ToString().Replace("<br>", "");
                    rset[i] = rset[i].ToString().Replace("'", "");
                    rset[i] = rset[i].ToString().Replace(";", "");
                    rset[i] = rset[i].ToString().Replace("</br>", "");
                    rset[i] = rset[i].ToString().Replace("<b>", "");
                    rset[i] = rset[i].ToString().Replace("<p>", "");
                    TitleFeed[i] = TitleFeed[i].ToString().Replace("'","");
                    TitleFeed[i] = TitleFeed[i].ToString().Replace(";","");
                    TitleFeed[i] = TitleFeed[i].ToString().Replace("<br>","");
                    TitleFeed[i] = TitleFeed[i].ToString().Replace("<p>","");
                    TitleFeed[i] = TitleFeed[i].ToString().Replace("<b>","");
                    query = @"INSERT INTO [rtsms_db].[dbo].[Event] ([RSSItemID],[CategoryID],[LocationID],[Title],[Description],[Link],[RiskLevel],[Status],[Alert],[Message],[pubDate],[heldDate],[ispresent],[isfuture],[ishistory]) VALUES ('" + fetchedrssid[i].ToString() + "'," + 1 + "," + 1 + ",'" + TitleFeed[i].ToString() + "','" + rset[i].ToString() + "','" + "abc" + "'," + 1 + "," + 1 + ",1,'" + "abc" + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "'," + livecheck + "," + futurecheck + "," + historycheck + ")";
                    //Console.WriteLine(query);
                    SqlCommand sqlcommand1 = new SqlCommand(query, sql1);
                    sqlcommand1.ExecuteNonQuery();
                    //Console.WriteLine("livecheck:" + livecount.ToString());
                    //Console.WriteLine("futurecheck:" + futurecount.ToString());
                    //Console.WriteLine("historycheck:" + historycount.ToString()); 
                    UpdateEventModuleRow(fetchedrssid[i].ToString());
                }
            //    Console.WriteLine("Records have been Updated!");
                sql1.Close();
            }
        }

        private bool CheckForUpdate()
        {
            bool checks = false;
            int newtmp=0;
            using (SqlConnection sql = new SqlConnection())
            {
                sql.ConnectionString = Properties.Settings.Default.RTSMSDBConnectionString;
                sql.Open();
                SqlCommand sqlcommand = new SqlCommand(countquery, sql);
                SqlDataReader reader = sqlcommand.ExecuteReader();
                reader.Read();
                newtmp = reader.GetInt32(0);
                reader.Close();
                sql.Close();

            }
            int oldtmp = Properties.Settings.Default.RSSItemCount;
            if (newtmp > oldtmp)
            {
                Properties.Settings.Default.RSSItemCount = newtmp;
                checks = true;
            }
            else
            {
                checks = false;
            }
            AddItem("New Val:" + newtmp);
            return checks;
        }
        //rset ,guid list
        private void Categorization()
        {
            ArrayList names = new ArrayList();
            names.Add("accident");
            names.Add("earth quick");
            names.Add("fire and kill");
            names.Add("kidnap");
            names.Add("public call strike");
            names.Add("terrorism");

            ArrayList rset = new ArrayList();
            ArrayList guid = new ArrayList();
            ArrayList importantfeed_sentences = new ArrayList();

            String conn = Properties.Settings.Default.RTSMSDBConnectionString;
            Properties.Settings.Default.AppName = "Hello World";
            AddItem(Properties.Settings.Default.AppName);
            using (SqlConnection sql = new SqlConnection())
            {
                sql.ConnectionString = @conn;
                sql.Open();
                SqlCommand sqlcommand = new SqlCommand(unMappedRSSItemquery, sql);
                SqlDataReader reader = sqlcommand.ExecuteReader();
                while (reader.Read())
                {
                    rset.Add(reader.GetString(2));
                    guid.Add(reader.GetString(0));
                }
                AddItem("Total Rows:" + rset.Count);
                reader.Close();
                sql.Close();
            }

            for (int i = 0; i < rset.Count; i++)
            {
                //                 Console.WriteLine("Counter = " + rset.Count);
                char[] separray = { '^' };
                RSSItemSentences feed = new RSSItemSentences();
                feed.SetGuid((string)guid[i]);
                string Tokens = (string)rset[i];
                Tokens = Tokens.Replace("<STRONG>", " ");
                Tokens = Tokens.Replace("</STRONG>", " ");
                Tokens = Tokens.Replace("<br>", " ");
                Tokens = Tokens.Replace("</br>", " ");
                Tokens = Tokens.Replace("<strong>", " ");
                Tokens = Tokens.Replace("</strong>", " ");
                Tokens = Tokens.Replace(". ", "^");
                Tokens = Tokens.Replace(".", "");
                Tokens = Tokens.Replace(",", "");
                Tokens = Tokens.Replace(":", "");
                Tokens = Tokens.Replace(";", "");
                Tokens = Tokens.Replace("&", "");
                Tokens = Tokens.Replace("?", "");
                Tokens = Tokens.Replace("#", "");
                Tokens = Tokens.Replace("'", "");
                Tokens = Tokens.Replace("\"", "");
                Tokens = Tokens.Replace("/", "");
                Tokens = Tokens.Replace("'", "");
                Tokens = Tokens.Replace(";", "");
                string[] Pieces = Tokens.Split(separray, 2048);

                for (int j = 0; j < Pieces.Length; j++)
                {
                  //  AddItem(Pieces[j]);
//                    Console.WriteLine(Pieces[j]);
                    feed.AddSentence(Pieces[j]);
                }
                ArrayList results = new ArrayList();
                CategorizationProcess ctg = new CategorizationProcess();
                ctg.LoadCategories(names);
                results = ctg.CategorizeFeed(feed.GetFeeds());
//                Console.WriteLine();
                int matchcount = 0;
                List<CategoryResultSet> listofresult = new List<CategoryResultSet>();
                for (int k = 0; k < results.Count; k++)
                {
                    CategoryResultSet tmp = (CategoryResultSet)results[k];
                    AddItem(tmp.categoryname + ", Match(s) " + tmp.noofmatched.ToString());
                    matchcount += tmp.noofmatched;
                    listofresult.Add(tmp);
                }
                String json = JsonConvert.SerializeObject(listofresult);
                InsertCategorizationResult((string)guid[i],json);

                AddItem("Total Matched(" + matchcount);
                // if totalmatchcount > 0 then add this rssitemsentencesfeed in 
                //FinalArrayList and update MainMiner Table [Categorization] Column
                //value to 1 according to each RSSItemID
                if (matchcount > 0)
                {
                    //Update MainMiner Table
                    //feed.GetGuid();   // these two fields will make the RSSItem back.
                    //feed.GetFeeds();
                    //write sql to update MainMiner
                    importantfeed_sentences.Add(feed);

                }
                InsertCategorizationRow((string)guid[i]);
                UpdateCategorizationRow((string)guid[i]);

                AddItem("==========================");

            }

            AddItem("Total Count of Feed(Categorized Found)" + guid.Count);
            // Thread.Sleep(2 * 1000);
            //  change progress of the status bar.
            // backgroundWorker1.ReportProgress(i * 100);
        }

        private void InsertCategorizationResult(string p, string json)
        {
            String query = @"INSERT INTO [CategorizationResult] VALUES ('" + p + "' ,'" +json+ "');";
            using (SqlConnection sql = new SqlConnection())
            {
                sql.ConnectionString = Properties.Settings.Default.RTSMSDBConnectionString;
                sql.Open();
                SqlCommand sqlcommand = new SqlCommand(query, sql);
                sqlcommand.ExecuteNonQuery();
                sql.Close();
            }
        }
        private void InsertCategorizationRow(String guid)
        {
            String query = @"INSERT INTO [MainMiner] ([RSSItemID]) VALUES ('" + guid + "')";
            using (SqlConnection sql = new SqlConnection())
            {
                sql.ConnectionString = Properties.Settings.Default.RTSMSDBConnectionString;
                sql.Open();
                SqlCommand sqlcommand = new SqlCommand(query, sql);
                sqlcommand.ExecuteNonQuery();
                sql.Close();
            }
        }

        private void UpdateCategorizationRow(String guid)
        {
            String query = @"update [MainMiner] set [MainMiner].Categorization =" + 1 + " where ([MainMiner].RSSItemID='" + guid + "');";
            using (SqlConnection sql = new SqlConnection())
            {
                sql.ConnectionString = Properties.Settings.Default.RTSMSDBConnectionString;
                sql.Open();
                SqlCommand sqlcommand = new SqlCommand(query, sql);
                sqlcommand.ExecuteNonQuery();
                sql.Close();
            }
        }
        private void UpdateEventModuleRow(String guid)
        {
            String query = @"update [MainMiner] set [MainMiner].Locating =" + 1 + " where ([MainMiner].RSSItemID='" + guid + "');";
            using (SqlConnection sql = new SqlConnection())
            {
                sql.ConnectionString = Properties.Settings.Default.RTSMSDBConnectionString;
                sql.Open();
                SqlCommand sqlcommand = new SqlCommand(query, sql);
                sqlcommand.ExecuteNonQuery();
                sql.Close();
            }
        }
        
        private void AddItem(string s)
        {
            if (listBox1.InvokeRequired)
            {
                stringDelegate sd = new stringDelegate(AddItem);
                this.Invoke(sd, new object[] { s });
            }
            else
            {
                listBox1.Items.Add(s);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            //    ProgressBar_ToolStrip.Value = e.ProgressPercentage;
            // Set the text.
            //    Status_Bar.Text = e.ProgressPercentage.ToString();
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)//it doesn't matter if the BG worker ends normally, or gets cancelled,
            {			   //both cases RunWorkerCompleted is invoked, so we need to check what has happened
                //    Status_Bar.Text = "You Have Stopped The Server.";
            }
            else
            {
                //         Status_Bar.Text = "Working...";
            }
        }



    }
}
