using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for MCQBtn.xaml
    /// </summary>
    public partial class MCQBtn : UserControl
    {
        private string def;
        private bool isCorrectChoice;
        

        public MCQBtn()
        {
            InitializeComponent();
        }

        public MCQBtn(string def, bool choice)
        {
            InitializeComponent();
            this.def = def;
            isCorrectChoice = choice;
            MCQTB.Text = def;
        }

        public void setDefinition(string def)
        {
            this.def = def;
        }

        public void setIsCorrectChoice(bool choice)
        {
            this.isCorrectChoice = choice;
        }

        public bool beenChosen = false;
        private void MCQBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            if (parentWindow.MCQ_Atempts > 0)
            {
                if (!beenChosen)
                {
                    if (isCorrectChoice)
                        parentWindow.MCQ_Atempts = 0;
                    else
                        parentWindow.MCQ_Atempts--;

                    beenChosen = true;
                    updateBtnColor();
                    parentWindow.qlChoiceAsync(isCorrectChoice);
                }
            }
            else
            {
                //Console.WriteLine("Attempts Used. Log and Continue");
                //parentWindow.QL_Choice(false);
            }
        }

        private void updateBtnColor()
        {
            MCQBtn_BTN.Dispatcher.Invoke((Action)delegate
            {
                if (isCorrectChoice)
                {
                    MCQBtn_BTN.BorderBrush = (SolidColorBrush)this.FindResource("CorrectColorBrush");
                }
                else
                {
                    MCQBtn_BTN.BorderBrush = (SolidColorBrush)this.FindResource("WrongColorBrush");
                }
            });
        }
    }
}
