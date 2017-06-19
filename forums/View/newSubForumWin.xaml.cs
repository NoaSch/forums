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
    /// Interaction logic for newSubForumWin.xaml
    /// </summary>
    public partial class newSubForumWin : Window
    {
        string ForumName;
        BusLogic busLogic;
        public bool conf = false;
        public newSubForumWin(BusLogic busLogic, string ForumName)
        {
            InitializeComponent();
            this.busLogic = busLogic;
            this.ForumName = ForumName;
            //get all forum members
            List<string> membersNames = this.busLogic.ForumsSys.Forums[ForumName].Members.Keys.ToList();
            members.ItemsSource = membersNames;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string newSubName = "";
            if (newName.Text == "")
                MessageBox.Show("please choose name", "Error");
            else if (!busLogic.ForumsSys.Forums[ForumName].isFreeSubforumSubject(newName.Text))
                MessageBox.Show("the name already exist", "Error");
            else
            {
                newSubName = newName.Text;
                string nextID = busLogic.ForumsSys.Forums[ForumName].getNextSubID();
                List<string> newModList = new List<string>();
                foreach (var item in members.SelectedItems)
                {
                    newModList.Add(item.ToString());
                }

                if (busLogic.ForumsSys.Forums[ForumName].addSubForum(nextID, newSubName, newModList))

                {

                    /*foreach (var item in members.SelectedItems)
                    {
                        busLogic.ForumsSys.Forums[ForumName].SubForums[newSubName].addModerator(item.ToString());
                    }*/
                    conf = true;
                    
                    MessageBox.Show("creation complete");
                }
                else
                {
                    MessageBox.Show("Error", "Error");
                }

            }
        }
    }
}
