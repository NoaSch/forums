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
        string forum;
        BusLogic buslogic;
        public CreateFriendsGroup(string f, string user, BusLogic bus)
        {
            InitializeComponent();
            username = user;
            forum = f;
            buslogic = bus;
        }

        private void AddBtn_click(object sender, RoutedEventArgs e)
        {
            if (name.Text != "")
            {
                List<Member> members = new List<Member>();
                Member m = buslogic.ForumsSys.Forums[forum].getMember(username);
                members.Add(m);
                //create new friend group with the current user as a member in it
                FriendGroup fg = new FriendGroup(name.Text, members);
                m.addFriendGroup(fg);
                if (buslogic.ForumsSys.Forums[forum].createNewFriendsGroup(name.Text, username))
                {
                    MessageBox.Show("Friend group created succesfully", "Done");
                    this.Close();
                }
                else
                    MessageBox.Show("Friend group name already exist", "Error");
            }
            else
            {
                MessageBox.Show("please insert a name", "Error");
            }
        }
    }
}
