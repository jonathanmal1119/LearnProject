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
    /// Interaction logic for MainCardTemp.xaml
    /// </summary>
    

    public partial class MainCardTemp : UserControl
    {
        private string term;
        private string def;
        private bool onTerm;

        public MainCardTemp()
        {
            InitializeComponent();
            term = "";
            def = "";
            onTerm = true;
            stateLB.Content = "Term";
            updateCard();
            //this.SizeChanged += MainCardTemp_SizeChanged;
        }

        public MainCardTemp(string term, string def)
        {
            InitializeComponent();
            this.term = term;
            this.def = def;
            onTerm = true;
            stateLB.Content = "Term";
            updateCard();
            //this.SizeChanged += MainCardTemp_SizeChanged;
        }

        private void MainCardTemp_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Console.WriteLine(calculateFontSize());
            
            mainLB.FontSize = calculateFontSize();
        }

        private void updateCard()
        {
            if (onTerm)
            {
                mainLB.Text = term;
            }
            else
            {
                mainLB.Text = def;
            }
        }

        private void flipCard()
        {
            if (onTerm)
            {
                mainLB.Text = def;
                stateLB.Content = "Definition";
                onTerm = false;
            }
            else
            {
                mainLB.Text = term;
                stateLB.Content = "Term";
                onTerm = true;
            }
        }
        public void updateCurrentCardCounter(int currentNum, int totalNum)
        {
            cardLB.Content = (currentNum+1).ToString() + @"/" + totalNum.ToString();
        }

        public void incrementCard(string nextTerm, string nextDef,int nextIndex, int totalNum)
        {
            this.term = nextTerm;
            this.def = nextDef;
            updateCard();
            updateCurrentCardCounter(nextIndex, totalNum);
        }

        public void decrementCard(string nextTerm, string nextDef, int nextIndex, int totalNum)
        {
            this.term = nextTerm;
            this.def = nextDef;
            updateCard();
            updateCurrentCardCounter(nextIndex, totalNum);
        }

        private void mainCardBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(this.ActualHeight + " | " + this.ActualWidth);
            
            flipCard();
        }

        int minH = 321;
        int minW = 443;
        int maxH = 501;
        int maxW = 793;
        int incrementFont = 0;
        bool maxHit = false;
        private int calculateFontSize()
        {
            Console.WriteLine(incrementFont);
            if (incrementFont == 12)
            {
                Console.WriteLine("maxhit");
                maxHit = true;
            }
            if ((this.ActualHeight - 321) % 12  ==  0 && !maxHit)
            {
                incrementFont = Math.Clamp(incrementFont++,0,12);
            }
            else if ((this.ActualHeight - 321) % 12 == 0 && maxHit)
            {
                incrementFont = Math.Clamp(incrementFont--, 0, 12);
            }
            

            return Math.Clamp((int)(mainLB.FontSize + incrementFont),30,45);
        }
    }
}
