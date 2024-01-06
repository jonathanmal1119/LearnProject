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
    /// Interaction logic for cardTemplate.xaml
    /// </summary>
    public partial class TermDataCardTemplate : UserControl
    {
        private string term;
        private string definition;
        private string studySet;

        public TermDataCardTemplate()
        {
            InitializeComponent();
        }

        public TermDataCardTemplate(string term, string definition,string studySet)
        {
            InitializeComponent();
            this.term = term;
            this.definition = definition;
            this.studySet = studySet;
            StudySetTB.Text = studySet;
            termLB.Text = this.term;
            definitionLB.Text = definition;
            mainBtn.ToolTip = "View " + studySet + " study set.";
        }

        public void setValues(string term, string definition)
        {
            this.term = term;
            this.definition = definition;;
            termLB.Text = term;
            definitionLB.Text = definition;
        }

        private void mainBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.changeCurentSet(studySet);
            parentWindow.changeActivePanel(MainWindow.Ewindows.ViewSets);
        }
    }
}
