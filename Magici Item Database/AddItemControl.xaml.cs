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

                if (clearEntriesCheckBox.IsChecked.Value == true)
                {
                    nameText.Text = string.Empty;
                    sourceText.Text = string.Empty;
                    pageNumber.Value = 0;
                    descText.Text = string.Empty;
                    tagsText.Text = string.Empty;
                }
            }
            else
            {
                _currentItem = item;
                _editing = true;
                addBtn.Content = "Update";

                nameText.Text = item.Name;
                sourceText.Text = item.Source;
                pageNumber.Value = item.PageNumber;
                descText.Text = item.Description;
                tagsText.Text = item.Tags;
            }
        }

        void AddItem()
        {
            var item = new MagicItem();
            item.Name = CamelCase(nameText.Text);
            item.Source = sourceText.Text;
            item.PageNumber = pageNumber.Value.Value;
            item.Description = descText.Text;
            item.Tags = tagsText.Text;
            DatabaseManager.Add(item);
        }

        void UpdateItem()
        {            
            _currentItem.Name = CamelCase(nameText.Text);
            _currentItem.Source = sourceText.Text;
            _currentItem.PageNumber = pageNumber.Value.Value;
            _currentItem.Description = descText.Text;
            _currentItem.Tags = tagsText.Text;
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
                tagsText.Text = string.Empty;
            }
        }

        string CamelCase(string name)
        {
            var s = name.ToLower().ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                if (i == 0)
                    s[i] = char.ToUpper(s[i]);
                else if (char.IsWhiteSpace(s[i]) && i + 1 < s.Length && char.IsLetter(s[i + 1]))
                    s[i+1] = char.ToUpper(s[i+1]);
            }

            return new string(s);
        }
    }
}
