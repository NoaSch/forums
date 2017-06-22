using forums.Logic;
using forums.View;
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

namespace forums
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BusLogic model;
     
        public MainWindow()
        {
            InitializeComponent();
            model = new BusLogic(true);

            forums.ItemsSource = model.ForumsSys.Forums.Keys;
            
        }

        private string GetSelectedValue()
        {
            if (forums.SelectedValue == null)
                return "";
            return forums.SelectedValue.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string selectedForum = GetSelectedValue();
            if (selectedForum != "")
            {
                ForumWin fw = new ForumWin(selectedForum, model);
                fw.ShowDialog();
            }

        }
    }
}
