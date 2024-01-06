using Learn.Classes;
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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        
        
        public SettingsWindow()
        {
            InitializeComponent();
            this.Loaded += SettingsWindow_Loaded;
        }


        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            customExitbtnCB.IsChecked = Settings.customExitOption;

            numBT_TB.Text = Settings.numBestTermsToShow.ToString();
            numWT_TB.Text = Settings.numWorstTermsToShow.ToString();

            termSepDefaultTB.Text = Settings.termSeperatorDefault;
            rowSepDefaultTB.Text = Settings.rowSeperatorDefault;

            waitIntervalTB.Text = Settings.waitIntervalQL.ToString();
            termsPerGroupTB.Text = Settings.termsPerGroupQL.ToString();
            maxMCQattempsTB.Text = Settings.MCQMaxAttempts.ToString();
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

        private void applyBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateUserSettings();
            Settings.SaveSettings();
        }

        private void UpdateUserSettings()
        {
            Settings.customExitOption = (bool)customExitbtnCB.IsChecked;

            Settings.numBestTermsToShow = Convert.ToInt32(numBT_TB.Text);
            Settings.numWorstTermsToShow = Convert.ToInt32(numWT_TB.Text);

            Settings.termSeperatorDefault = termSepDefaultTB.Text;
            Settings.rowSeperatorDefault = rowSepDefaultTB.Text;

            Settings.waitIntervalQL = Convert.ToInt32(waitIntervalTB.Text);
            Settings.termsPerGroupQL = Convert.ToInt32(termsPerGroupTB.Text);
            Settings.MCQMaxAttempts = Convert.ToInt32(maxMCQattempsTB.Text);

        }

        private void exitOptionCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
