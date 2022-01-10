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

namespace Magici_Item_Database
{
    /// <summary>
    /// Interaction logic for AddItemControl.xaml
    /// </summary>
    public partial class AddItemControl : UserControl
    {
        public AddItemControl()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = new MagicItem();
            item.Name = nameText.Text;
            item.Source = sourceText.Text;
            item.PageNumber = pageNumber.Value.Value;
            item.Description = descText.Text;
            DatabaseManager.Add(item);

            if (clearEntriesCheckBox.IsChecked.Value == true)
            {
                nameText.Text = string.Empty;
                sourceText.Text = string.Empty;
                pageNumber.Value = 0;
                descText.Text = string.Empty;
            }
        }
    }
}
