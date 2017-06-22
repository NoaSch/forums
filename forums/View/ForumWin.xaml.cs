using forums.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        string username;
        Forum forum;
        public ObservableCollection<string> observableSubForum;
        public ForumWin(string forumSubject, BusLogic busLogic)
        {
            InitializeComponent();
            this.busLogic = busLogic;
            forum = this.busLogic.ForumsSys.getForumBySubject(forumSubject);
            observableSubForum = new ObservableCollection<string>();
            subForums.ItemsSource = observableSubForum;
            forumName.Text = forumSubject;
            foreach (string item in forum.SubForums.Keys)
            {
                observableSubForum.Add(item);

            }
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWin lw = new LoginWin(busLogic, forum.ForumSubject);
            lw.ShowDialog();
            if (lw.conf == true)
            {
                username = lw.usr;
                userLbl.Content = username;
                if (forum.Managers.ContainsKey(username))
                {
                    newSubBtn.Visibility = Visibility.Visible;
                }

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegisterWin rw = new RegisterWin(busLogic, forum.ForumSubject);
            rw.ShowDialog();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            newSubForumWin nsb = new newSubForumWin(busLogic, forum.ForumSubject);
            nsb.ShowDialog();
            if (nsb.conf == true)
            {
                observableSubForum.Add(nsb.newSubName);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (username == null)
            {
                MessageBox.Show("please login");
            }
            else
            {
                complaintWin cw;
                if (subForums.SelectedItem != null)
                {
                    cw = new complaintWin(username, forum, subForums.SelectedItem.ToString());
                    cw.ShowDialog();

                }
                else
                {
                    cw = new complaintWin(username, forum, "");
                    cw.ShowDialog();
                }
            }
        }

        private void subForums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Createfg_Click(object sender, RoutedEventArgs e)
        {
            if (username == null)
            {
                MessageBox.Show("please login");
            }
            else
            {
                CreateFriendsGroup fg;
                fg = new CreateFriendsGroup(forum.Subject, username, busLogic);
                fg.ShowDialog();
            }
        }
    }
}
