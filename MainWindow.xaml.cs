using Learn.Classes;
using Learn.Classes.Database;
using Learn.Styles;
using Learn.Styles.Templates;
using Learn.Styles.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing.Design;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Learn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IDatabase database;
        public Ewindows currentWindow = Ewindows.Home;
        public event EventHandler<EventArgs> ChangeToHome;
        public event EventHandler<EventArgs> ChangeViewSet;
        public event EventHandler<EventArgs> ChangeToQuickLearnSet;

        string currentSet;
        int currentIndex = 0;
        int currentTotalCards;
        List<CardTemplate> cardTemplates = new List<CardTemplate>();
        MainCardTemp mainCard;
        DataRowCollection currentSetDRC;

        List<CardTemplate> cardsInSet = new List<CardTemplate>();

        
        public enum Ewindows
        {
            Home,
            ViewSets,
            QuickLearn,
            FlashCards,
            MatchCards
        }
        public MainWindow()
        {
            InitializeComponent();
            database = ConnectionManager.EstablishConnection();database = ConnectionManager.EstablishConnection();
            this.Loaded += MainWindow_Loaded;
            //ChangeToHome += MainWindow_Loaded;
            ChangeViewSet += ViewSets_Loaded;
            ChangeToQuickLearnSet += QuickLearn_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            populateRecentStudiedSets();
            populateBestTermsList();
            populateWorstTermsList();
        }

        public void changeActivePanel(Ewindows window)
        {

            switch (window)
            {
                case Ewindows.Home:
                    pageName.Content = "Dashboard";
                    currentWindow = Ewindows.Home;
                    clearViewSets();
                    HomeGrid.Visibility = Visibility.Visible;
                    ViewSetGrid.Visibility = Visibility.Hidden;
                    QuicklearnDP.Visibility = Visibility.Hidden;
                    ChangeToHome?.Invoke(this, new EventArgs());
                    break;
                case Ewindows.ViewSets:
                    pageName.Content = "View Study Sets";
                    currentWindow = Ewindows.ViewSets;
                    HomeGrid.Visibility = Visibility.Hidden;
                    ViewSetGrid.Visibility = Visibility.Visible;
                    QuicklearnDP.Visibility = Visibility.Hidden;
                    ChangeViewSet?.Invoke(this, new EventArgs());
                    break;
                case Ewindows.QuickLearn:
                    pageName.Content = "Quick Learn";
                    currentWindow = Ewindows.QuickLearn;
                    HomeGrid.Visibility = Visibility.Hidden;
                    ViewSetGrid.Visibility = Visibility.Hidden;
                    QuicklearnDP.Visibility = Visibility.Visible;
                    ChangeToQuickLearnSet?.Invoke(this, new EventArgs());
                    break;
            }

        }

        
        public void changeCurentSet(string setName)
        {
            currentSet = setName;
            currentTotalCards = database.GetQuery($"SELECT numOfTerms FROM StudySets WHERE grouping = \"{currentSet}\" LIMIT 1").Rows[0].Field<int>("numOfterms");
        }


        #region Header 
        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void minimizeBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Settings.customExitOption)
               minimizeBtnI.Source = (ImageSource)this.FindResource("Minus_HoverAlt");
            else
               minimizeBtnI.Source = (ImageSource)this.FindResource("Minus_Hover");
        }

        private void minimizeBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            minimizeBtnI.Source = (ImageSource)this.FindResource("Minus_Base");
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window w in App.Current.Windows)
            {
                if (w != this)
                {
                    w.Close();
                }
            }
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

        #region Home Pannel

            #region Recent Sets

            #region Button Functions
            private void AddBtn_MouseEnter(object sender, MouseEventArgs e)
            {
                AddBtnI.Source = (ImageSource)this.FindResource("plusIconAlt");
            }

            private void AddBtn_MouseLeave(object sender, MouseEventArgs e)
            {
                AddBtnI.Source = (ImageSource)this.FindResource("plusIcon");
            }

            private void AddBtn_Click(object sender, RoutedEventArgs e)
            {
                CreateSetWindow win = new CreateSetWindow();
                App.CenterWindow(win, this);
                win.ShowDialog();
            }
            #endregion

            private List<HomeSetsVisual> recentStudySets = new List<HomeSetsVisual>();
            private void populateRecentStudiedSets()
            {
                DataTable db = database.GetQuery("SELECT grouping FROM TermCards GROUP BY grouping ORDER BY lastStudied DESC LIMIT 5");

                foreach (DataRow row in db.Rows)
                {
                    string group = row.Field<string>("grouping");

                    DataRowCollection rows = database.GetQuery($"SELECT numOfTerms FROM StudySets WHERE grouping = \"{group}\" LIMIT 1").Rows;
                    if (rows.Count >= 1)
                    {
                        int numOfCards = rows[0].Field<int>("numOfterms");
                        if (group != null)
                        {
                            HomeSetsVisual HSV = new HomeSetsVisual(group, numOfCards);
                            RecentSetsSP.Children.Add(HSV);
                            recentStudySets.Add(HSV);
                        }

                    }
                    else
                    {
                        Console.WriteLine(rows.Count);
                    }

                }

            }


        #endregion

            #region Best and Worst Terms
        private void bestTermsFilter_Click(object sender, RoutedEventArgs e)
        {
            bestTermsSP.Children.Clear();
            populateBestTermsList();
        }

        private int bestTermsFilterNum = Settings.numBestTermsToShow;
        private int bestTermsFilterNumMax = 20;
        private int bestTermsFilterNumMin = 0;
        private void bestTermsFilter_MouseWheel(object sender, MouseWheelEventArgs e)
        {

            if (e.Delta > 0)
            {
                //Scrolling Up
                bestTermsFilterNum = Math.Clamp(bestTermsFilterNum + 5, bestTermsFilterNumMin, bestTermsFilterNumMax);
                bestTermsFilterByNumTB.Text = bestTermsFilterNum.ToString();

            }
            else
            {
                //Scrolling down
                bestTermsFilterNum = Math.Clamp(bestTermsFilterNum - 5, bestTermsFilterNumMin, bestTermsFilterNumMax);
                bestTermsFilterByNumTB.Text = bestTermsFilterNum.ToString();
            }
        }

        private void populateBestTermsList()
        {
            DataRowCollection db = database.GetQuery($"SELECT term, definition, grouping FROM TermCards WHERE times_Correct > 0 ORDER BY times_Correct DESC LIMIT {bestTermsFilterNum}").Rows;
            foreach (DataRow dr in db)
            {
                TermDataCardTemplate TDCT = new TermDataCardTemplate(dr.Field<string>("term"), dr.Field<string>("definition"), dr.Field<string>("grouping"));
                bestTermsSP.Children.Add(TDCT);
            }
        }

        private int worstTermsFilterNum = Settings.numWorstTermsToShow;
        private int worstTermsFilterNumMax = 20;
        private int worstTermsFilterNumMin = 0;
        private void populateWorstTermsList()
        {
            DataRowCollection db = database.GetQuery($"SELECT term, definition, grouping FROM TermCards WHERE times_Wrong > 0 ORDER BY times_Wrong DESC LIMIT {worstTermsFilterNum}").Rows;
            foreach (DataRow dr in db)
            {
                TermDataCardTemplate TDCT = new TermDataCardTemplate(dr.Field<string>("term"), dr.Field<string>("definition"), dr.Field<string>("grouping"));
                worstTermsSP.Children.Add(TDCT);
            }
        }

        private void worstTermsFilter_Click(object sender, RoutedEventArgs e)
        {
            worstTermsSP.Children.Clear();
            populateWorstTermsList();
        }

        private void worstTermsFilter_MouseWheel(object sender, MouseWheelEventArgs e)
        {

            if (e.Delta > 0)
            {
                //Scrolling Up
                worstTermsFilterNum = Math.Clamp(worstTermsFilterNum + 5, worstTermsFilterNumMin, worstTermsFilterNumMax);
                worstTermsFilterByNumTB.Text = worstTermsFilterNum.ToString();

            }
            else
            {
                //Scrolling down
                worstTermsFilterNum = Math.Clamp(worstTermsFilterNum - 5, worstTermsFilterNumMin, worstTermsFilterNumMax);
                worstTermsFilterByNumTB.Text = worstTermsFilterNum.ToString();
            }
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            RecentSetsSP.Children.Clear();
            recentStudySets.Clear();
            populateRecentStudiedSets();
        }

        private void refreshBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            refreshBtnI.Source = (ImageSource)this.FindResource("refreshIconHover");
        }

        private void refreshBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            refreshBtnI.Source = (ImageSource)this.FindResource("refreshIcon");
        }
        #endregion

        #endregion

        #region View Sets Panel
        public void ViewSets_Loaded(object sender, EventArgs e)
        {
            currentSetDRC = database.GetQuery($"SELECT term, definition, favorite FROM TermCards WHERE grouping = \"{currentSet}\"").Rows;
            currentTotalCards = database.GetQuery($"SELECT numOfTerms FROM StudySets WHERE grouping = \"{currentSet}\" LIMIT 1").Rows[0].Field<int>("numOfterms");
            //StartUpdateTermsListAsync();
            updateTermsList();



            createAndAddMainCard(currentSetDRC[0].Field<string>("term"), currentSetDRC[0].Field<string>("definition"));
        }

        #region Main View Sets Panel

        #region Button Functions
        private void leftArrowClick(object sender, RoutedEventArgs e)
        {
            if (currentIndex - 1 < currentTotalCards && currentIndex - 1 >= 0)
            {
                mainCard.decrementCard(currentSetDRC[currentIndex - 1].Field<string>("term"), currentSetDRC[currentIndex - 1].Field<string>("definition"), currentIndex - 1, currentTotalCards);
                currentIndex--;
            }
        }

        private void rightarrowClick(object sender, RoutedEventArgs e)
        {
            if (currentIndex + 1 < currentTotalCards)
            {
                mainCard.incrementCard(currentSetDRC[currentIndex + 1].Field<string>("term"), currentSetDRC[currentIndex + 1].Field<string>("definition"), currentIndex + 1, currentTotalCards);
                currentIndex++;
            }

        }

        private void flashcardsBtn_Click(object sender, RoutedEventArgs e)
        {
            //changeActivePanel(Ewindows.FlashCards);
        }

        private void quicklearnBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentTotalCards > 1)
                changeActivePanel(Ewindows.QuickLearn);
            else
                MessageBox.Show("Not enough Terms to quick learn", "Quick Learn Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void matchCardsBtn_Click(object sender, RoutedEventArgs e)
        {
            //changeActivePanel(Ewindows.MatchCards);
        }
        #endregion

        private void createAndAddMainCard(string term, string def)
        {
            mainCard = new MainCardTemp(term, def);
            mainCard.updateCurrentCardCounter(currentIndex, currentTotalCards);

            mainCard.HorizontalAlignment = HorizontalAlignment.Stretch;
            mainCard.VerticalAlignment = VerticalAlignment.Stretch;
            mainCard.Margin = new Thickness(300, 10, 300, 10);
            ViewSetGrid.Children.Add(mainCard);

        }

        private void updateTermsList()
        {
            if (currentSet != null)
            {
                foreach (DataRow dr in currentSetDRC)
                {
                    string term = dr.Field<string>("term");
                    string def = dr.Field<string>("definition");
                    bool fav = dr.Field<bool>("favorite");
                    CardTemplate card = new CardTemplate(term, def, fav);


                    CardList.Children.Add(card);
                    cardTemplates.Add(card);

                }
            }
        }
        private Task StartUpdateTermsListAsync()
        {
            List<Card> cardsInSet = new List<Card>();
            return Task.Run(() =>
            {
                if (currentSet != null)
                {
                    foreach (DataRow dr in currentSetDRC)
                    {
                        string term = dr.Field<string>("term");
                        string def = dr.Field<string>("definition");
                        bool fav = dr.Field<bool>("favorite");
                        cardsInSet.Add(new Card(term, def, currentSet, fav));
                        CardTemplate card = new CardTemplate();
                        card.setValues(term, def);
                        CardList.Children.Add(card);
                        cardTemplates.Add(card);
                    }
                }
            });

        }

        public void clearViewSets()
        {
            CardList.Children.Clear();
        }

        #endregion

        #region Quick learn Panel

        private DataRowCollection QL_RowCollection;

        private void QuickLearn_Loaded(object? sender, EventArgs e)
        {

            if (btns[0] != null)
            {
                clearMCQ();
            }
            if (cardChoicesLeft.Count > 0)
            {
                cardChoicesLeft.Clear();
                totalCardChoices.Clear();
            }
            QL_RowCollection = database.GetQuery($"SELECT term, definition, favorite FROM TermCards WHERE grouping = \"{currentSet}\"").Rows;
            foreach (DataRow item in QL_RowCollection)
            {
                cardChoicesLeft.Add(item);
                totalCardChoices.Add(item);
            }
            populateMCQ();
            MCQ_Atempts = 2;
        }

        private List<DataRow> cardChoicesLeft = new List<DataRow>();
        private List<DataRow> totalCardChoices = new List<DataRow>();
        private List<DataRow> correctCards = new List<DataRow>();
        private string chosenTerm;
        private string chosenDef;
        private int chosenIndex;
        MCQBtn[] btns = new MCQBtn[4];
        private List<int> chosenIndexes = new List<int>();


        private void populateMCQ()
        {
            // Base Case
            if (cardChoicesLeft.Count == 0)
            {
                MCQ_Term.Dispatcher.Invoke((Action)delegate
                {
                    MCQ_Term.Text = "End of List";
                });
                return;
            }
            MCQ_Term.Dispatcher.Invoke((Action)delegate
            {
                termStatusTB.Text = (cardChoicesLeft.Count.ToString() + "/" + currentTotalCards.ToString());
            });
            //Console.WriteLine("Total: " + currentTotalCards.ToString() + " | left | " + cardChoicesLeft.Count.ToString() + " | correct | " + correctCards.Count.ToString());
            Random rand = new Random();
            int randIndex = rand.Next(cardChoicesLeft.Count);
            chosenIndexes.Add(randIndex);
            chosenIndex = randIndex;
            chosenTerm = cardChoicesLeft[randIndex].Field<string>("term");

            MCQ_Term.Dispatcher.Invoke((Action)delegate
            {
                MCQ_Term.Text = chosenTerm;
            });
            chosenDef = cardChoicesLeft[randIndex].Field<string>("definition");

            //Create 3 Dummy def and 1 Correct Def
            int correctMCQChoiceIndex = rand.Next(4);
            DataRowCollection dummyChoices = database.GetQuery($"SELECT definition FROM TermCards WHERE term != \"{chosenTerm}\" and grouping = \"{currentSet}\" ORDER BY RANDOM() LIMIT 3;").Rows;

            int count = 0;
            MCQ_grid.Dispatcher.Invoke((Action)delegate
            {
                for (int i = 0; i < 4; i++)
                {
                    if (correctMCQChoiceIndex == i)
                    {
                        btns[i] = new MCQBtn(chosenDef, true);
                        MCQ_grid.Children.Add(btns[i]);
                        switch (i)
                        {
                            case (0):
                                btns[i].SetValue(Grid.RowProperty, 0);
                                btns[i].SetValue(Grid.ColumnProperty, 0);
                                break;

                            case (1):
                                btns[i].SetValue(Grid.RowProperty, 1);
                                btns[i].SetValue(Grid.ColumnProperty, 0); ;
                                break;

                            case (2):
                                btns[i].SetValue(Grid.RowProperty, 0);
                                btns[i].SetValue(Grid.ColumnProperty, 1);
                                break;

                            case (3):
                                btns[i].SetValue(Grid.RowProperty, 1);
                                btns[i].SetValue(Grid.ColumnProperty, 1);
                                break;
                        }
                    }
                    else
                    {

                        btns[i] = new MCQBtn(dummyChoices[count].Field<string>("definition"), false);
                        MCQ_grid.Children.Add(btns[i]);
                        count++;
                        switch (i)
                        {
                            case (0):
                                btns[i].SetValue(Grid.RowProperty, 0);
                                btns[i].SetValue(Grid.ColumnProperty, 0);
                                break;

                            case (1):
                                btns[i].SetValue(Grid.RowProperty, 1);
                                btns[i].SetValue(Grid.ColumnProperty, 0); ;
                                break;

                            case (2):
                                btns[i].SetValue(Grid.RowProperty, 0);
                                btns[i].SetValue(Grid.ColumnProperty, 1);
                                break;

                            case (3):
                                btns[i].SetValue(Grid.RowProperty, 1);
                                btns[i].SetValue(Grid.ColumnProperty, 1);
                                break;

                        }
                    }
                }
            });
        }


        private void PopulateMCQFromSession()
        {
            if (cardChoicesLeft.Count == 0)
            {
                MCQ_Term.Dispatcher.Invoke((Action)delegate
                {
                    MCQ_Term.Text = "End of List";
                });
                return;
            }
            MCQ_Term.Dispatcher.Invoke((Action)delegate
            {
                termStatusTB.Text = (cardChoicesLeft.Count.ToString() + "/" + currentTotalCards.ToString());
            });
        }

        public bool answerChosen;
        public int MCQ_Atempts { get; set; } = Settings.MCQMaxAttempts;
        public void QL_Choice(bool isCorrect)
        {
            if (isCorrect)
            {
                answerChosen = true;
                correctCards.Add(cardChoicesLeft[chosenIndex]);
                cardChoicesLeft.Remove(cardChoicesLeft[chosenIndex]);

                foreach (MCQBtn b in btns)
                {
                    b.Dispatcher.Invoke((Action)delegate
                    {
                        b.IsEnabled = false;
                    });
                }
                //Console.WriteLine("Correct");
                database.RunQuery($"UPDATE TermCards SET times_Correct = times_Correct + 1 WHERE term = \"{chosenTerm}\"");

                Task.Factory.StartNew(() => { }).ContinueWith(async task => {
                    await Task.Delay(Settings.waitIntervalQL * 1000);
                    clearMCQ();
                    populateMCQ();
                    MCQ_Atempts = Settings.MCQMaxAttempts;
                });
            }
            else if (!isCorrect && MCQ_Atempts == 0)
            {
                //Console.WriteLine("Attempts Used. Log and Continue");
                database.RunQuery($"UPDATE TermCards SET times_Wrong = times_Wrong + 1 WHERE term = \"{chosenTerm}\"");

                Task.Factory.StartNew(() => { }).ContinueWith(async task => {
                    await Task.Delay(Settings.waitIntervalQL * 1000);
                    clearMCQ();
                    populateMCQ();
                    MCQ_Atempts = Settings.MCQMaxAttempts;
                });

            }
            else
            {
                //Console.WriteLine("Wrong");
            }
        }


        public Task qlChoiceAsync(bool isCorrect)
        {
            return Task.Run(() => { QL_Choice(isCorrect); });
        }

        private void clearMCQ()
        {
            MCQ_grid.Dispatcher.Invoke((Action)delegate
            {
                foreach (MCQBtn item in btns)
                {
                    MCQ_grid.Children.Remove(item);
                }
            });

        }

        #endregion



        #endregion

        #region Side Bar Buttons

        private void HomeSBB_MouseEnter(object sender, MouseEventArgs e)
        {
            HomeSBI.Source = (ImageSource)this.FindResource("homeIconAlt");
        }

        private void HomeSBB_MouseLeave(object sender, MouseEventArgs e)
        {
            HomeSBI.Source = (ImageSource)this.FindResource("homeIcon");
        }

        private void StudySBB_MouseEnter(object sender, MouseEventArgs e)
        {
            StudySBI.Source = (ImageSource)this.FindResource("folderIconAlt");
        }

        private void StudySBB_MouseLeave(object sender, MouseEventArgs e)
        {
            StudySBI.Source = (ImageSource)this.FindResource("folderIcon");
        }

        private void DelSBB_MouseEnter(object sender, MouseEventArgs e)
        {
            DelSBI.Source = (ImageSource)this.FindResource("trashIconAlt");
            
        }

        private void DelSBB_MouseLeave(object sender, MouseEventArgs e)
        {
            DelSBI.Source = (ImageSource)this.FindResource("trashIcon");
        }

        private void SettingSBB_MouseEnter(object sender, MouseEventArgs e)
        {
            SettingSBI.Source = (ImageSource)this.FindResource("settingsIconAlt");
        }

        private void SettingSBB_MouseLeave(object sender, MouseEventArgs e)
        {
            SettingSBI.Source = (ImageSource)this.FindResource("settingsIcon");
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            changeActivePanel(Ewindows.Home);
        }
        private void StudyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentSet != null)
            {
                changeActivePanel(Ewindows.ViewSets);
            }
        }
        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow win = new SettingsWindow();
            App.CenterWindow(win, this);
            win.ShowDialog();
        }


        #endregion

        private List<List<MCQ>> MCQsets;
        private void exportBtn_Click(object sender, RoutedEventArgs e)
        {
            //MCQManager QLManager = new MCQManager(currentSet);
            //MCQsets = QLManager.CreateStudySession();
            string exportStr = string.Empty;
            foreach (DataRow dr in currentSetDRC)
            {
                string term = dr.Field<string>("term");
                string definition = dr.Field<string>("definition");
                string termSep = "|";
                string rowSep = "[";
                exportStr += term + termSep + definition + rowSep;
            }
            Clipboard.SetText(exportStr);

            
        }
        string errors = string.Empty;
        private void logBtnClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(chosenTerm);
            errors += chosenTerm;
        }
    }
}
