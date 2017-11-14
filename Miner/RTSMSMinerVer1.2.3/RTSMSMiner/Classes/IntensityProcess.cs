using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Collections;

namespace RTSMSMiner.Classes
{

    class IntensityProcess
    {
        public static ArrayList activity = new ArrayList();
        public static ArrayList rset = new ArrayList();
        public static string[] tempPieces = new string[100];
        public String command1 = "SELECT * FROM [RSSItem]";
        public String command2 = "SELECT * FROM [activitylist]";
        public String countquery = "SELECT COUNT(*) FROM [RSSItem]";            

        public void Process()
        {
            
            int countqueryvalue = 0;
            int countfilevalue = 0;
            string temp = String.Empty;

            using (SqlConnection sql = new SqlConnection())
            {
                sql.ConnectionString = @Properties.Settings.Default.RTSMSDBConnectionString;
                sql.Open();
                SqlCommand sqlcommand = new SqlCommand(countquery,sql);
                SqlDataReader reader = sqlcommand.ExecuteReader();
                reader.Read();
                countqueryvalue = reader.GetInt32(0);
                Console.WriteLine(countqueryvalue.ToString());
                reader.Close();
                sql.Close();
            }           

            
            StreamReader filereader = new StreamReader("File/count.txt");
            temp = filereader.ReadLine();
            Console.WriteLine("the string is: " + temp);
            if(temp != null)
            {
                countfilevalue = int.Parse(temp);
                Console.WriteLine(countfilevalue.ToString());
            }
            
            filereader.Close();          

            if (countqueryvalue > countfilevalue)
            {

                StreamWriter filewriter = new StreamWriter("File/count.txt",false);
                // filewriter.Flush();
                filewriter.Write(countqueryvalue);
                filewriter.Close();

                // making sql connection and retrieving rssitems and activitylist
                using (SqlConnection sql = new SqlConnection())
                {
                    sql.ConnectionString = @Properties.Settings.Default.RTSMSDBConnectionString;
                    sql.Open();
                    SqlCommand sqlcommand = new SqlCommand(command1, sql);
                    SqlDataReader reader = sqlcommand.ExecuteReader();
                    while (reader.Read())
                    {
                        rset.Add(reader.GetString(2));
                    }
                    reader.Close();

                    sqlcommand = new SqlCommand(command2, sql);
                    reader = sqlcommand.ExecuteReader();

                    while (reader.Read())
                    {
                        activity.Add(reader.GetString(1));
                    }
                    reader.Close();

                    sql.Close();
                }



                string[] separray = { ". " };
                ArrayList tokenized = new ArrayList();

                for (int i = 0; i < rset.Count-countfilevalue; i++)
                {
                    rset[i + countfilevalue] = rset[i + countfilevalue].ToString().Replace("<br>", "");
                    rset[i + countfilevalue] = rset[i + countfilevalue].ToString().Replace("\"", "");
                    rset[i + countfilevalue] = rset[i + countfilevalue].ToString().Replace("'", "");
                    rset[i + countfilevalue] = rset[i + countfilevalue].ToString().Replace(";", "");
                    //                tokenized.Add(Regex.Replace(rset[i].ToString(), "<br>", " "));
                }

                //Console.WriteLine(rset.Count);

                ArrayList rsetarray = new ArrayList(); //= new Array[rset.Count-countfilevalue];
                for (int loop = 0; loop < rset.Count - countfilevalue; loop++)
                {
                    rsetarray.Insert(loop,rset[loop + countfilevalue]);                   
                }
                ArrayList Pieces = new ArrayList();
                //  string[] tempPieces = new string[100];
                ArrayList impsen = new ArrayList();
                int found = 0;
                string query = String.Empty;

                using (SqlConnection sql1 = new SqlConnection())
                {
                    sql1.ConnectionString = @Properties.Settings.Default.RTSMSDBConnectionString;
                    sql1.Open();
                    for (int i = 0; i < rset.Count-countfilevalue; i++)
                    {
                        tempPieces = rsetarray[i].ToString().Split(separray, StringSplitOptions.RemoveEmptyEntries);

                        for (int j = 0; j < tempPieces.Length; j++)
                        {
                            //  Pieces.Add(tempPieces[j]);
                            for (int k = 0; k < activity.Count; k++)
                            {
                                found = tempPieces[j].ToString().IndexOf(activity[k].ToString());

                                if (found > 0)
                                {
                                    //         impsen.Add(Pieces[i]);
                                    intensitymeasure(tempPieces[j].ToString(),i);
                                    query = "INSERT INTO [token] ([rssitemid],[token]) VALUES ('" + i + "','" + tempPieces[j].ToString() + "')";
                                    SqlCommand sqlcommand1 = new SqlCommand(query, sql1);
                                    sqlcommand1.ExecuteNonQuery();
                                    break;
                                }
                            }
                        }
                        //Console.WriteLine(Pieces[i].ToString());
                        //Console.WriteLine("**********************************************");
                    }
                    sql1.Close();
                }

                Console.WriteLine("Everything is updated!");
            }

            else Console.WriteLine("No new items found!");
            //  Console.WriteLine(Pieces.Count);


        }
        public bool intensitymeasure(string token, int rssid)
        {
            if (adjectivecheck(token, rssid))
                return true;

            else if (quantitymeasure(token, rssid))
                return true;

            else return false;
        }

        bool adjectivecheck(string token,int rssid)
        {
            string store = String.Empty;
            int intstore = 0;
            string query = String.Empty;
            int intensitycheck = 0;

            StreamReader readfile = new StreamReader("File/adjectives.txt");
            while (!readfile.EndOfStream)
            {
                store = readfile.ReadLine();

                intstore = token.IndexOf(store);

                if (intstore > 0)
                {
                    intensitycheck = 1;
                    using (SqlConnection sql1 = new SqlConnection())
                    {
                        sql1.ConnectionString = @Properties.Settings.Default.RTSMSDBConnectionString;
                        sql1.Open();
                        query = "INSERT INTO [intensity] ([rssitemid],[imptoken],[intensitylevel]) VALUES ('" + rssid + "','" + token + "','" + intensitycheck + "')";
                        SqlCommand sqlcommand1 = new SqlCommand(query, sql1);
                        sqlcommand1.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            return false;
        }

        public bool quantitymeasure(string token, int rssid)
        {
            char[] seprator = {' '};
            string[] tokenpieces = new string[100];
            string query = String.Empty;
            int intensitycheck = 0;
            int value = 0;
            bool parsed = false;

            tokenpieces = token.Split(seprator);

            for (int i = 0; i < tokenpieces.Length; i++)
            {
                parsed = int.TryParse(tokenpieces[i], out value);

                if (parsed && value>3)
                {
                    intensitycheck = 1;
                    using (SqlConnection sql1 = new SqlConnection())
                    {
                        sql1.ConnectionString = @Properties.Settings.Default.RTSMSDBConnectionString;
                        sql1.Open();
                        query = "INSERT INTO [intensity] ([rssitemid],[imptoken],[intensitylevel]) VALUES ('" + rssid + "','" + token + "','" + intensitycheck + "')";
                        SqlCommand sqlcommand1 = new SqlCommand(query, sql1);
                        sqlcommand1.ExecuteNonQuery();
                        return true;
                    }
                }                
            }

            return false;
        }

    }
}
