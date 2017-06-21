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
    /// Interaction logic for complaintWin.xaml
    /// </summary>
    public partial class complaintWin : Window
    {
        Forum forum;
        string subForumSubject;
        string senderUserName;
        bool aboutManager;
        public bool conf;
        public complaintWin(string username,Forum chosenForum,  string subForumSubject)
        {
            this.senderUserName = username;
            InitializeComponent();
            forum = chosenForum;
            this.subForumSubject = subForumSubject;
             managersList.ItemsSource = forum.Managers.Keys;
            conf = false;
            if (subForumSubject != "")
            {
                aboutManager = false;
                moderatorsList.ItemsSource = forum.SubForums[subForumSubject].Moderators.Keys;
               
            }
            else
            {
                aboutManager = true;
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string recipiant;
            //  public bool addComplaint(string senderUserName, string complaintAboutUserNmae,string content, string subforumSubject)
            if (aboutManager)
            {
                recipiant = managersList.SelectedValue.ToString();
            }
            else
            {
               recipiant = moderatorsList.SelectedValue.ToString();
            }
            if (contentTxt.Text == "")
                MessageBox.Show("Plase write content");
            else
            {
                if (forum.addComplaint(senderUserName, recipiant, contentTxt.Text, subForumSubject))
                {
                    conf = true;
                    MessageBox.Show("Thank you");

                }
                else
                {
                    conf = false;
                    MessageBox.Show("Error Occured");
                }
                    
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
