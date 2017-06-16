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
    /// Interaction logic for RegisterWin.xaml
    /// </summary>
    public partial class RegisterWin : Window
    {
        BusLogic busLogic;
        public string usr;
        public bool conf;
        string forumName;
        public RegisterWin(BusLogic busLogic, string ForumName)
        {
            InitializeComponent();
            conf = false;
            this.busLogic = busLogic;
            this.forumName = ForumName;
        }



        private void register_Click(object sender, RoutedEventArgs e)
        {

            if (passBox.Password == "" || usrText.Text == "")
            {
                System.Windows.MessageBox.Show("All fields are mandatory", "Error");

            }


            else
            {

                //check if the mail exists and the password correct
                if (busLogic.ForumsSys.Forums[forumName].Members.ContainsKey(usrText.Text))
                {
                    System.Windows.MessageBox.Show("UserName Already Exist", "Error");

                }
                else
                {
                    if (busLogic.ForumsSys.Forums[forumName].addMember(usrText.Text, passBox.Password))
                    {
                        MessageBox.Show("Welcome to the forum now please login");
                        conf = true;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Error please try again","Error");
                    }

                }


            }
        }
    }
}

