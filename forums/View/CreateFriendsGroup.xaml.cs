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
    /// Interaction logic for CreateFriendsGroup.xaml
    /// </summary>
    public partial class CreateFriendsGroup : Window
    {
        string username;
        public CreateFriendsGroup(string user)
        {
            InitializeComponent();
            username = user;
        }

        private void AddBtn_click(object sender, RoutedEventArgs e)
        {
            if (name.Text != "")
            {
                List<string> members = new List<string>();
                members.Add(username);
                //create new friend group with the current user as a member in it
                FriendGroup fg = new FriendGroup(name.Text, members);
                MessageBox.Show("Friend group created succesfully", "Done");
                this.Close();
            }
            else
            {
                MessageBox.Show("please insert a name", "Error");
            }
        }
    }
}
