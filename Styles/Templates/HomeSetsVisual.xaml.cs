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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Learn.Styles.Templates
{
    /// <summary>
    /// Interaction logic for HomeSetsVisual.xaml
    /// </summary>
    public partial class HomeSetsVisual : UserControl
    {
        private string group;
        private int numOfCards;
        private bool fav = false;
        public HomeSetsVisual()
        {
            InitializeComponent();
            group = "";
            numOfCards = 0;
        }
        public HomeSetsVisual(string group, int numOfCards)
        {
            InitializeComponent();
            this.group = group;
            this.numOfCards = numOfCards;
            SetNameLB.Text = this.group;
            CardNumLB.Content = this.numOfCards.ToString();
        }

        private void bookmarkBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (fav)
            {
                fav = false;
                favoriteBtnI.Source = (ImageSource)this.FindResource("bookmarkIcon");
            }
            else
            {
                fav = true;
                favoriteBtnI.Source = (ImageSource)this.FindResource("bookmarkIconAlt");
            }
        }

        private void favoriteBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (fav)
            {
                favoriteBtnI.Source = (ImageSource)this.FindResource("bookmarkIcon");
            }
            else
            {
                favoriteBtnI.Source = (ImageSource)this.FindResource("bookmarkIconAlt");
            }
        }

        private void favoriteBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (fav)
            {
                favoriteBtnI.Source = (ImageSource)this.FindResource("bookmarkIconAlt");
            }
            else
            {
                favoriteBtnI.Source = (ImageSource)this.FindResource("bookmarkIcon");
            }
        }
       
        private void HomeVis_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.changeCurentSet(group);
            parentWindow.changeActivePanel(MainWindow.Ewindows.ViewSets);
        }
    }
}
