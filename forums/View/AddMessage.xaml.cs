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
    public partial class AddMessage : Window
    {
        BusLogic busLogic;
        string discussionName;
        public AddMessage(BusLogic bus, string disc)
        {
            InitializeComponent();
            busLogic = bus;
            discussionName = disc;
        }

        private void AddBtn_click(object sender, RoutedEventArgs e)
        {
            string newTitle = "", newContent = "";
            if (title.Text == "" && content.Text == "")
                MessageBox.Show("Please enter a subject or a content", "Error");
            else
            {
                newTitle = title.Text;
                newContent = content.Text;
                //Message m = new Message(discussionName, title, content, date, publisher)
                //if (busLogic.ForumsSys.Forums[ForumName].addSubForum(nextID, newSubName, newModList))

            }
        }
    }
}
