using HtmlAgilityPack;
using Learn.Classes;
using Learn.Classes.Database;
using Learn.Classes.Indexing;
using Learn.Styles.Templates;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Learn.Styles.Windows
{
    /// <summary>
    /// Interaction logic for InExWindow.xaml
    /// </summary>
    public partial class ImportWindow : Window
    {
        //private List<CardTemplate> ImportedCards = new List<CardTemplate>();
        private IDatabase _db;
        private string[] chosenFile;
        

        public ImportWindow()
        {
            InitializeComponent();
            _db = ConnectionManager.EstablishConnection();
            this.Loaded += ImportWindow_Loaded;
        }

        private void ImportWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Load Import Default Settings
            RowDif.Text = Settings.rowSeperatorDefault;
            TermDif.Text = Settings.termSeperatorDefault;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void closeBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Settings.customExitOption)
                closeBtnI.Source = (ImageSource)this.FindResource("Close_HoverAlt");
            else
                closeBtnI.Source = (ImageSource)this.FindResource("Close_Hover");
        }

        private void closeBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            closeBtnI.Source = (ImageSource)this.FindResource("Close_Base");
        }

        private void import_Click(object sender, RoutedEventArgs e)
        {
            if (chosenFileLB.Content.ToString().Length == 0)
            {
                errorLB.Content = "No File Chosen";
                return;
            }
            if (TermDif.Text.ToString().Length == 0)
            {
                errorLB.Content = "Invalid Term Seperator";
                return;
            }
            if (RowDif.Text.ToString().Length == 0)
            {
                errorLB.Content = "Invalid Row Seperator";
                return; 
            }
            if (SetGroupingTB.Text.ToString().Length == 0)
            {
                errorLB.Content = "Invalid Study Set Name";
                return;
            }

            //TermIndexer termIndexer = new TermIndexer();
            //IIndexer Tindexer = termIndexer.CreateIndexerByFile(chosenFile);
            //if (Tindexer.LoadFile(chosenFile))
            //{
            //    Tindexer.BeginIndexTermsAsync(); // If wait till completed add .Wait();
            //}

            //MyIndexer myI = new MyIndexer();
            //myI.LoadFile(chosenFile);
            //myI.selectBody();

         
            
            //foreach (HtmlNode n in myI.getItems("ul"))
            //{
            //    string[] array = n.InnerText.Trim().Split(".");
            //    for (int i = 0; i < array.Length; i++)
            //    {
            //        Console.WriteLine(array[i].TrimStart().ToString().Replace("\n",string.Empty));
            //    }
            //}

            if ((bool)!TEXTP.IsChecked)
            {
                StreamReader SR = new StreamReader(chosenFile[0]);
                errorLB.Content = "";

                string[] text = SR.ReadToEnd().Split(RowDif.Text);
                //filterFile(SR.ReadToEnd());

                // Uncommet to fix
                Task task = PopulateTableAsync(text.Reverse().Skip(1).Reverse().ToArray(), TermDif.Text, SetGroupingTB.Text);
                task.Wait();

                this.Close();
            }
            else
            {
                Dictionary<string, string> questions = new Dictionary<string, string>();
                
                foreach (string file in chosenFile)
                {
                    StreamReader SR = new StreamReader(file);
                    errorLB.Content = "";

                    string importText = string.Empty;
                    string[] text = SR.ReadToEnd().Split('\n');

                    int counter = 0; 
                    foreach (string s in text)
                    {
                        counter++;
                        string s1 = s.Trim();
                        if (s1.Length > 0)
                        {
                            if (Char.IsDigit(s1.ToCharArray()[0]))
                            {
                                string term = s.Trim().Split(") ")[1];
                                //string term = s.Trim();
                                //Console.WriteLine(s.Split(") ")[1]);
                                //Console.WriteLine(term);
                                
                                importText += term + "|";
                                string definiton = "problem";
                                string[] t = text[counter + 4].Split("Answer:  ");
                                if (t.Length == 2)
                                {
                                    switch (t[1].Trim())
                                    {
                                        case "A":
                                            definiton = text[counter].Split(") ")[1].Trim();
                                            //Console.WriteLine(definiton);
                                            break;
                                        case "B":
                                            definiton = text[counter + 1].Split(") ")[1].Trim();
                                            //Console.WriteLine(definiton);
                                            break;
                                        case "C":
                                            definiton = text[counter + 2].Split(") ")[1].Trim();
                                            //Console.WriteLine(definiton);
                                            break;
                                        case "D":
                                            definiton = text[counter + 3].Split(") ")[1].Trim();
                                            //Console.WriteLine(definiton);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                importText += definiton + "*";
                                if (!questions.TryAdd(term, definiton))
                                {
                                    Console.WriteLine(term + "\n" + definiton + "\n");
                                }
                                
                            }
                        }
                    }
                    //Clipboard.SetText(importText);
                }

                Task task = CreateMCQFromTextSet(questions, SetGroupingTB.Text);
                task.Wait();

                this.Close();
            }
        }
        private Task PopulateTableAsync(string[] sa, string split,string group)
        {
            return Task.Run(() => 
            {
                int num = sa.Length;
                foreach (string s in sa)
                {
                    if (s.Split(split).Length >= 2)
                    {
                        string term = s.Split(split)[0].Trim().Replace("\"", ""); ;
                        string def = s.Split(split)[1].Trim().Replace("\"", ""); ;
                        string iden = term + "_" + group;
                        //Console.WriteLine($"insert or replace into TermCards (identifier,term,definition,grouping) values(\"{inden}\",\"{term}\",\"{def}\",\"{group}\")");
                        _db.RunQuery($"insert or replace into TermCards (identifier,term,definition,grouping) values(\"{iden}\",\"{term}\",\"{def}\",\"{group}\")");
                        _db.RunQuery($"insert or ignore into StudySets (grouping,numOfTerms) values(\"{group}\",{num})");

                    }
                    else
                    {
                        num--;
                        MessageBox.Show($"Card with \"{s}\" was not entered correctly. Enter manually.","Card Entry Error",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                        //Console.WriteLine(num);
                    }
                }
                
            });

        }

        private Task CreateMCQFromTextSet(Dictionary<string,string> questions, string group)
        {
            return Task.Run(() => {
                foreach (KeyValuePair<string, string> item in questions)
                {
                    string term = item.Key;
                    string def = item.Value;
                    string iden = term + "_" + group;
                    int num = questions.Count();
                    _db.RunQuery($"insert or replace into TermCards (identifier,term,definition,grouping) values(\"{iden}\",\"{term}\",\"{def}\",\"{group}\")");
                    _db.RunQuery($"insert or ignore into StudySets (grouping,numOfTerms) values(\"{group}\",{num})");
                }
            });
        }


        private void chooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            
            if (openFileDialog.ShowDialog() == true) {
                chosenFile = openFileDialog.FileNames;
                chosenFileLB.Content = string.Join("\n", openFileDialog.SafeFileNames);
            }   
        }

    }
}
