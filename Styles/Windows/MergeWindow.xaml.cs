using HtmlAgilityPack;
using Learn.Classes;
using Learn.Classes.Database;
using Learn.Classes.Indexing;
using Learn.Styles.Templates;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class  MergeWindow : Window
    {
        IDatabase _db;
        long MergedSetTermCount;
        string MergedSetName;

        public MergeWindow()
        {
            InitializeComponent();
            _db = ConnectionManager.EstablishConnection();
            this.Loaded += MergeWindow_Loaded;
        }

        private void MergeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Task task = PopulateTableAsync();
            //task.Wait();
            PopulateTable();
        }

        #region Pannel Functions

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

        #endregion

        private void merge_Click(object sender, RoutedEventArgs e)
        {
            if (SetGroupingTB.Text.ToString().Length == 0)
            {
                errorLB.Content = "Invalid Study Set Name";
                return;
            }
            if (selectedSets.Count <= 1)
            {
                errorLB.Content = "Not Enough Sets Chosen";
                return;
            }

            MergedSetName = SetGroupingTB.Text;
            //Then do logic
            Console.WriteLine("Processing Merge Click. " + selectedSets.Count + " sets.");
            processSelectedSets();
        }

        DataRowCollection newMergedSet;
        private void processSelectedSets()
        {
            string countQuerry = "SELECT Count(*) FROM TermCards WHERE grouping = ''";
            string setQuerry = "SELECT * FROM TermCards WHERE grouping = ''";
            foreach (string s in selectedSets)
            {
                countQuerry += $" OR grouping = '{s}'";
                setQuerry  += $" OR grouping = '{s}'";
            }

            Console.WriteLine(countQuerry);
            MergedSetTermCount = _db.GetQuery(countQuerry).Rows[0].Field<long>("Count(*)");
            newMergedSet = _db.GetQuery(setQuerry).Rows;

            Task task = StartPopulateTableAsync();
            task.Wait();
            Console.WriteLine(MergedSetTermCount + " Terms Added");

            this.Close();
        }

        private Task StartPopulateTableAsync()
        {
            return Task.Run(() =>
            {
                foreach (DataRow dr in newMergedSet)
                {
                    string inden = dr.Field<string>("term") + "_" + MergedSetName;
                    string term = dr.Field<string>("term");
                    string def = dr.Field<string>("definition");
                    string group = MergedSetName;

                    _db.RunQuery($"insert or replace into TermCards (identifier,term,definition,grouping) values(\"{inden}\",\"{term}\",\"{def}\",\"{group}\")");
                    _db.RunQuery($"insert or ignore into StudySets (grouping,numOfTerms) values(\"{group}\",{MergedSetTermCount})");
                }
            });

        }

        List<MergeSetsItem> MergeSetsList = new List<MergeSetsItem>();
        List<string> selectedSets = new List<string>();
        DataRowCollection StudySetRows;
        private Task PopulateTableAsync()
        {
            return Task.Run(() =>
            {
                StudySetRows = _db.GetQuery($"SELECT grouping, numOfTerms FROM StudySets").Rows;

                foreach (DataRow dr in StudySetRows)
                {
                    string grouping;
                    int numOfTerms;
                    grouping = dr.Field<string>("grouping");
                    numOfTerms = dr.Field<int>("numOfTerms");

                    MergeSetsItem msi = new MergeSetsItem(grouping, numOfTerms);
                    MergeSetsList.Add(msi);
                    setsSP.Children.Add(msi);
                }
            });

        }

        private void PopulateTable()
        {
            StudySetRows = _db.GetQuery($"SELECT grouping, numOfTerms FROM StudySets").Rows;

            foreach (DataRow dr in StudySetRows)
            {
                string grouping;
                int numOfTerms;
                grouping = dr.Field<string>("grouping");
                numOfTerms = dr.Field<int>("numOfTerms");

                MergeSetsItem msi = new MergeSetsItem(grouping, numOfTerms);
                MergeSetsList.Add(msi);
                setsSP.Children.Add(msi);
            }
        }

        public void updateChecked(bool update, string grouping)
        {
            if (update)
            {
                selectedSets.Add(grouping);
            }
            else
            {
                selectedSets.Remove(grouping);
            }
        }
    }
}
