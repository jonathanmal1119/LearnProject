using Learn.Classes;
using Learn.Classes.Database;
using Learn.Styles.Templates;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CreateSetWindow.xaml
    /// </summary>
    public partial class CreateSetWindow : Window
    {
        private List<EditableCardTemplate> cards = new List<EditableCardTemplate>();
        private IDatabase _db;
        public CreateSetWindow()
        {
            InitializeComponent();
            _db = ConnectionManager.EstablishConnection();
            EditableCardTemplate card = new EditableCardTemplate();
            AddCardList.Children.Add(card);
            cards.Add(card);
        }

        #region Visual Button Functions
       

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

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }
        private void addCardBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            addCardBtnI.Source = (ImageSource)this.FindResource("plusIconAlt");
        }

        private void addCardBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            addCardBtnI.Source = (ImageSource)this.FindResource("plusIcon");
        }

        private void StudySetTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            setNameLB.Content = StudySetTB.Text;
        }

        #endregion


        private void addCardBtn_Click(object sender, RoutedEventArgs e)
        {
            EditableCardTemplate card = new EditableCardTemplate();
            if (cards.Last().getTerm().Length != 0)
            {
                
                AddCardList.Children.Add(card);
                cards.Add(card);
                numOfCardsLB.Content = cards.Count;
            }
            else
            {
                errorLB.Content = "Enter a term into the last card";
            }
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StudySetTB.Text.ToString().Length == 0)
            {
                errorLB.Content = "No Study Set name.";
                return;
            }
            
            errorLB.Content = "";
            Task task = StartPopulateTableAsync(StudySetTB.Text);
            task.Wait();

            this.Close();
        }

        private Task StartPopulateTableAsync(string group)
        {
            return Task.Run(() =>
            {
                foreach (EditableCardTemplate e in cards)
                {
                    string term = e.getTerm();
                    string def = e.getDefinition();
                    string inden = term + "_" + group;
                    int count = cards.Count;
                    _db.RunQuery($"insert or replace into TermCards (identifier,term,definition,grouping) values(\"{inden}\",\"{term}\",\"{def}\",\"{group}\")");
                    _db.RunQuery($"insert or ignore into StudySets (grouping,numOfTerms) values(\"{group}\",{count})");
                }

            });

        }

        private void importBtn_Click(object sender, RoutedEventArgs e)
        {
            ImportWindow win = new ImportWindow();
            this.Close();
            App.CenterWindow(win, this);
            win.ShowDialog();
        }

        private void mergerBtn_Click(object sender, RoutedEventArgs e)
        {
            MergeWindow mwin = new MergeWindow();
            this.Close();
            App.CenterWindow(mwin, this);
            mwin.ShowDialog();
        }
    }
}
