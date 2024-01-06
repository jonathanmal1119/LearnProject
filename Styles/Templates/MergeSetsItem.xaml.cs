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
    /// Interaction logic for MergeSetsItem.xaml
    /// </summary>
    public partial class MergeSetsItem : UserControl
    {
        public string studySetName { get; set; }
        int numOfTerms;
        public bool isChecked { get; set; }

        public MergeSetsItem()
        {
            InitializeComponent();
        }

        public MergeSetsItem(string studySetName, int numOfTerms)
        {
            InitializeComponent();
            this.studySetName = studySetName;
            this.numOfTerms = numOfTerms;
            checkBox.Content = studySetName;
            setSize.Content = numOfTerms;
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            isChecked = true;
            Windows.MergeWindow mergeWindow = (Windows.MergeWindow)Window.GetWindow(this);
            mergeWindow.updateChecked(isChecked,studySetName);
        }

        private void checkBox_UnChecked(object sender, RoutedEventArgs e)
        {
            isChecked = false;
            Windows.MergeWindow mergeWindow = (Windows.MergeWindow)Window.GetWindow(this);
            mergeWindow.updateChecked(isChecked, studySetName);
        }
    }
}
