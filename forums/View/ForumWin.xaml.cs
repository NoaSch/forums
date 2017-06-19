using forums.Logic;
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

namespace forums.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ForumWin : Window
    {
        BusLogic busLogic;
        string forumSubject;
        string username;
        public ForumWin(string forumSubject,BusLogic busLogic)
        {
            InitializeComponent();
            this.busLogic = busLogic;
            subForums.ItemsSource = this.busLogic.ForumsSys.Forums[forumSubject].SubForums.Keys;
            this.forumSubject = forumSubject;
            forumName.Text = this.forumSubject;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWin lw = new LoginWin(busLogic, forumSubject);
            lw.ShowDialog();
            if (lw.conf == true)
            {
                username = lw.usr;
                userLbl.Content = username;
                if(busLogic.ForumsSys.Forums[forumSubject].Managers.ContainsKey(username))
                {
                    newSubBtn.Visibility = Visibility.Visible;
                }

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegisterWin rw = new RegisterWin(busLogic, forumSubject);
            rw.ShowDialog();
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            newSubForumWin nsb = new newSubForumWin(busLogic, forumSubject);
            nsb.ShowDialog();
            if(nsb.conf== true)
                subForums.ItemsSource = this.busLogic.ForumsSys.Forums[forumSubject].SubForums.Keys;
        }
    }
}
