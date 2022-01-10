using AdonisUI.Controls;
using System;
using System.Text;
using System.Windows;
using System.IO;
using System.Text.RegularExpressions;

namespace Magici_Item_Database
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow
    {
        DatabaseManager _db = null;

        public MainWindow()
        {
            InitializeComponent();

            DatabaseManager.OnItemsChanged += DatabaseManager_OnItemsChanged;
        }

        private void DatabaseManager_OnItemsChanged(MagicItem specificItem = null)
        {
            itemListBox.ItemsSource = DatabaseManager.GetAll();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_db == null)
            {
                #region -> ask
                const string CREATE_BTN = "create";
                const string OPEN_BTN = "open";

                var messageBox = new MessageBoxModel
                {
                    Text = "Open exist or create new item database",
                    Caption = "Existing or New Item Database",
                    Icon = AdonisUI.Controls.MessageBoxImage.Question,
                    Buttons = new[]
                    {                        
                        MessageBoxButtons.Custom("Open Existing", OPEN_BTN),
                        MessageBoxButtons.Custom("Create New", CREATE_BTN),
                    },
                    IsSoundEnabled = false,
                };
                #endregion

                AdonisUI.Controls.MessageBox.Show(messageBox);
                switch (messageBox.Result)
                {                    
                    case AdonisUI.Controls.MessageBoxResult.Custom:
                        if ((string)messageBox.ButtonPressed.Id == OPEN_BTN)
                        {
                            #region -> open
                            // Create OpenFileDialog
                            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                            // Set filter for file extension and default file extension 
                            dlg.DefaultExt = ".db";
                            dlg.Filter = "Database (*.db)|*.db";

                            // Display OpenFileDialog by calling ShowDialog method 
                            Nullable<bool> result = dlg.ShowDialog();

                            // Get the selected file name and display in a TextBox 
                            if (result == true)
                                _db = DatabaseManager.Open(dlg.FileName);
                            #endregion
                        }
                        else if ((string)messageBox.ButtonPressed.Id == CREATE_BTN)
                        {
                            #region -> new
                            // Configure save file dialog box
                            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                            dlg.FileName = "Item Database"; // Default file name
                            dlg.DefaultExt = ".db";
                            dlg.Filter = "Database (*.db)|*.db";

                            // Show save file dialog box
                            Nullable<bool> result = dlg.ShowDialog();

                            // Process save file dialog box results
                            if (result == true)
                                _db = DatabaseManager.Open(dlg.FileName);
                            #endregion
                        }
                        break;
                }                
            }

            itemListBox.ItemsSource = DatabaseManager.GetAll();
        }

        private void perchanceBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var list = DatabaseManager.GetAll();
            foreach (var i in list)
            {
                sb.Append("<p><b>")
                    .Append(i.Name)
                    .Append("</b></p><p>")
                    .Append(i.Description)
                    .Append("</p><p>")
                    .Append("<b>").Append("Source ").Append("</b><br>").Append(i.Source).Append(" pg").Append(i.PageNumber)
                    .Append("</p>")
                    .Append("@");
            }

            var s = sb.ToString();

            RegexOptions options = RegexOptions.Multiline;
            Regex regex = new Regex(System.Environment.NewLine, options);
            s = regex.Replace(s, "<br>");
            s = s.Replace("@", System.Environment.NewLine);

            var filename = Path.GetTempFileName();
            File.WriteAllText(filename, s);
            System.Diagnostics.Process.Start("notepad", filename);
        }

        private void newBtn_Click(object sender, RoutedEventArgs e)
        {
            itemEditor.Load(null);
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = itemListBox.SelectedItem as MagicItem;
            DatabaseManager.Delete(item);
        }

        private void itemListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var item = itemListBox.SelectedItem as MagicItem;
            itemEditor.Load(item);
        }

        private void pickRandomBtn_Click(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            var list = DatabaseManager.GetAll();
            if (list.Count > 0)
                AdonisUI.Controls.MessageBox.Show(list[r.Next(0, list.Count)].ToStringFull(),caption: "Random Item", buttons: AdonisUI.Controls.MessageBoxButton.OK);
        }
    }
}
