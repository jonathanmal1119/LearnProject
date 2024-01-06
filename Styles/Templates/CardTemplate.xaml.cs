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
    public partial class CardTemplate : UserControl
    {
        private string term;
        private string definition;
        private bool favorite;

        public CardTemplate()
        {
            InitializeComponent();
            this.Loaded += CardTemplate_Loaded;
        }

        private void CardTemplate_Loaded(object sender, RoutedEventArgs e)
        {
            termLB.Text = this.term;
            definitionLB.Text = definition;
        }

        public CardTemplate(string term, string definition)
        {
            InitializeComponent();
            this.term = term;
            this.definition = definition;
            termLB.Text = this.term;
            definitionLB.Text = definition;
        }
        public CardTemplate(string term, string definition,bool favorite)
        {
            InitializeComponent();

            this.term = term;
            this.definition = definition;
            this.favorite = favorite;
            termLB.Text = this.term;
            definitionLB.Text = definition;
        }

        public void setValues(string term, string definition)
        {
            this.term = term;
            this.definition = definition;;
            termLB.Text = term;
            definitionLB.Text = definition;
        }

    }
}
