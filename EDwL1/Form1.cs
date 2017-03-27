using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDwL1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string urlAddress = textBox1.Text;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding("Windows-1250"));
                }

                string data = readStream.ReadToEnd();
                using (FileStream fs = new FileStream("test.htm", FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.GetEncoding("Windows-1250")))
                    {
                        w.Write(data);
                    }
                }
                response.Close();
         
                readStream.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string textHtml = System.IO.File.ReadAllText("test.htm", Encoding.GetEncoding("Windows-1250"));
            using (FileStream fs = new FileStream("test.txt", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.GetEncoding("Windows-1250")))
                {
                    //textHtml = Regex.Replace(textHtml, @"\s+", "");
                    textHtml = Regex.Replace(textHtml, @"<[^>]+>", "");
                    textHtml = Regex.Replace(textHtml, @"[^\w\s]+|[\d]+", "");
                    //textHtml = Regex.Replace(textHtml, @"[\W]", "");
                    textHtml = Regex.Replace(textHtml, @"\s+", " ");
                    w.Write(textHtml);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var fs = new FileStream("test.txt", FileMode.Open, FileAccess.Read,
                                      FileShare.ReadWrite | FileShare.Delete);
            string content;
            using (StreamReader reader = new StreamReader(fs, Encoding.GetEncoding("Windows-1250")))
            {
                content = reader.ReadToEnd();
            }
            string[] words = content.Split(' ');

            foreach(string count in words.Distinct())
            {
                //listView1.Columns.Add("Word", 20, HorizontalAlignment.Left);
                //listView1.Columns.Add("Count", 20, HorizontalAlignment.Left);
                //string[] row = { count + " - " + words.Where(x => x == count).Count() + " times" };
                //var listViewItem = new ListViewItem(row);
                //listView1.Items.Add(listViewItem);


                listView1.Items.Add(count + " - " + words.Where(x => x == count).Count() + " раз");
               
            }
        }
    }
    }

