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
        bool _editing = false;
        MagicItem _currentItem = null;

        public AddItemControl()
        {
            InitializeComponent();
        }

        public void Load(MagicItem item)
        {
            if (item == null)
            {
                _currentItem = null;
                _editing = false;
                addBtn.Content = "Add";
            }
            else
            {
                _currentItem = item;
                _editing = true;
                addBtn.Content = "Update";
            }
        }

        void AddItem()
        {
            var item = new MagicItem();
            item.Name = nameText.Text;
            item.Source = sourceText.Text;
            item.PageNumber = pageNumber.Value.Value;
            item.Description = descText.Text;
            DatabaseManager.Add(item);
        }

        void UpdateItem()
        {            
            _currentItem.Name = nameText.Text;
            _currentItem.Source = sourceText.Text;
            _currentItem.PageNumber = pageNumber.Value.Value;
            _currentItem.Description = descText.Text;
            DatabaseManager.Update(_currentItem);
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_editing)
                UpdateItem();
            else
                AddItem();

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
