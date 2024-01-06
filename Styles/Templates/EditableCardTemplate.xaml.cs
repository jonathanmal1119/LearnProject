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
    public partial class EditableCardTemplate : UserControl
    {
        private string term;
        private string definition;

        public EditableCardTemplate()
        {
            InitializeComponent();
            term = "";
            definition = "";
        }
        public EditableCardTemplate(string term, string definition)
        {
            InitializeComponent();
            this.term = term;
            this.definition = definition;
            termLB.Text = this.term;
            definitionLB.Text = definition;
        }

        public string getTerm()
        {
            return term;
        }

        public string getDefinition()
        {
            return definition;
        }

        private void definitionLB_TextChanged(object sender, TextChangedEventArgs e)
        {
            definition = definitionLB.Text;
        }

        private void termLB_TextChanged(object sender, TextChangedEventArgs e)
        {
            term = termLB.Text;
        }
    }
}
