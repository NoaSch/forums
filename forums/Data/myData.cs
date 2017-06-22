using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using forums.Logic;

namespace forums.Data
{
    //class for the data layer
    public class myData
    {
        OleDbConnection conn;
        OleDbCommand cmd;
        string dbPath;

        public myData()
        {
            conn = new OleDbConnection();
            cmd = new OleDbCommand();
            dbPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Data\\Database.mdb";
        }

        //check if a mail is exist in the db
        public bool checkIfUserExist(string mail)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT * from Profiles WHERE  mail ='" + mail.Trim() + "'", conn);

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }



        internal Dictionary<string, List<string>> getFriendsGroupsOfForum(string subject)
        {
            Dictionary<string, List<string>> ans = new Dictionary<string, List<string>>();
            HashSet<Tuple<string, string>> friendsGroups = new HashSet<Tuple<string, string>>();
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT * from FriendsGroup WHERE Forum ='" + subject.Trim() + "'", conn);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                foreach (DbDataRecord s in reader)
                {
                    string forumName = s.GetString(0);
                    string groupName = s.GetString(1);
                    Tuple<string, string> toadd = new Tuple<string, string>(forumName, groupName);
                    friendsGroups.Add(toadd);

                }
                conn.Close();
                ans = getFriendsGroupsMembers(friendsGroups);
                return ans;
            }
            conn.Close();
            ans = getFriendsGroupsMembers(friendsGroups);
            return ans;
        }

        internal List<string> getFgMembers(string query)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand(query, conn);
            List<string> fgMembers = new List<string>();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                foreach (DbDataRecord s in reader)
                {

                    fgMembers.Add(s.GetString(0));
                }
                conn.Close();
                return fgMembers;
            }
            conn.Close();
            return fgMembers;
        }

        internal Dictionary<string, List<string>> getFriendsGroupsMembers(HashSet<Tuple<string, string>> friendsGroups)
        {
            Dictionary<string, List<string>> ans = new Dictionary<string, List<string>>();
            foreach (Tuple<string, string> tuple in friendsGroups)
            {
                string currentQuery = "SELECT Member from FriendsGroupMembers WHERE Forum ='" + tuple.Item1 + "' and FriendGroup='" + tuple.Item2 + "'";
                List<string> newMembersList = getFgMembers(currentQuery);
                ans.Add(tuple.Item2, newMembersList);
            }
            return ans;
        }


        internal bool addSubForum(string id, string forumSubject, string newSubName)
        {

            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            cmd = new OleDbCommand();
            cmd.CommandText = "INSERT into subForums ([subForumId],[subForumSubject],[forumSubject]) values(@subID,@subSubject, @forumSubject)";
            cmd.Connection = conn;

            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@subID", OleDbType.VarChar).Value = id;
                cmd.Parameters.Add("@subSubject", OleDbType.VarChar).Value = newSubName;
                cmd.Parameters.Add("@forumSubject", OleDbType.VarChar).Value = forumSubject;
                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;

                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.ToString());
                    return false;

                }
            }
            else
            {
                conn.Close();
                return false;
            }
        }

        internal bool addMember(string forumName, string username, string password)
        {

            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            cmd = new OleDbCommand();
            cmd.CommandText = "INSERT into ForumsUsers ([forumSubject], [username], [password], [status],[isManager]) values(@forum, @username,@pass,@status,@isManager)";
            cmd.Connection = conn;

            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@forum", OleDbType.VarChar).Value = forumName;
                cmd.Parameters.Add("@username", OleDbType.VarChar).Value = username;
                cmd.Parameters.Add("@Pass", OleDbType.VarChar).Value = password;
                cmd.Parameters.Add("@status", OleDbType.VarChar).Value = "active";
                cmd.Parameters.Add("@isManager", OleDbType.Boolean).Value = false;
                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;

                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.ToString());
                    return false;

                }
            }
            else
            {
                conn.Close();
                return false;
            }
        }

        internal List<string> getMenagers(string forumId)
        {
            List<string> ans = new List<string>();
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT username from ForumsUsers  WHERE isManager = True AND forumSubject = '" + forumId.Trim() + "'", conn);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                foreach (DbDataRecord s in reader)
                {
                    string menagerName = s.GetString(0);
                    ans.Add(menagerName);

                }
                conn.Close();
                return ans;
            }
            conn.Close();
            return ans;
        }

        internal List<string> getModerators(string subForumID)
        {
            List<string> ans = new List<string>();
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT username from subForumsModerators WHERE  subForumId ='" + subForumID.Trim() + "'", conn);

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                foreach (DbDataRecord s in reader)
                {
                    string modName = s.GetString(0);
                    ans.Add(modName);

                }
                conn.Close();
                return ans;
            }
            conn.Close();
            return ans;
        }



        public List<string> getForums()
        {
            List<string> ans = new List<string>();
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT * from Forums", conn);

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                foreach (DbDataRecord s in reader)
                {
                    string key = s.GetString(0);
                    ans.Add(key);
                }
                conn.Close();
                return ans;
            }
            conn.Close();
            return ans;
        }

        public Dictionary<string, Tuple<string, string>> getMembersOfForum(string forumSubject)
        {
            Dictionary<string, Tuple<string, string>> ans = new Dictionary<string, Tuple<string, string>>();
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT * from ForumsUsers WHERE  forumSubject ='" + forumSubject.Trim() + "'", conn);

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                foreach (DbDataRecord s in reader)
                {
                    string username = s.GetString(1);
                    string password = s.GetString(2);
                    string status = s.GetString(3);
                    ans.Add(username, new Tuple<string, string>(password, status));
                }
                conn.Close();
                return ans;
            }
            conn.Close();
            return ans;
        }

        internal bool addModerator(string id, string name)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            cmd = new OleDbCommand();
            cmd.CommandText = "INSERT into subForumsModerators ([subForumId],[username]) values(@subId, @username)";
            cmd.Connection = conn;

            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@subId", OleDbType.VarChar).Value = id;
                cmd.Parameters.Add("@username", OleDbType.VarChar).Value = name;
                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;

                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.ToString());
                    return false;

                }
            }
            else
            {
                conn.Close();
                return false;
            }
        }

        public Dictionary<string, string> getSubForumsOfForum(string forumSubject)
        {
            Dictionary<string, string> ans = new Dictionary<string, string>();
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT * from subForums WHERE  forumSubject ='" + forumSubject.Trim() + "'", conn);

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                foreach (DbDataRecord s in reader)
                {
                    string subId = s.GetString(0);
                    string subSubject = s.GetString(1);
                    ans.Add(subId, subSubject);
                }
                conn.Close();
                return ans;
            }
            conn.Close();
            return ans;
        }
        //
        internal bool addComplaint(Complaint c)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            cmd = new OleDbCommand();
            cmd.CommandText = "INSERT into Complaints ([Date],[Content],[sender],[recipiant],[subForumID]) values(@date, @content, @sender, @recipiant,@subId)";
            cmd.Connection = conn;

            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@date", OleDbType.Date).Value = c.Date;
                cmd.Parameters.Add("@content", OleDbType.VarChar).Value = c.Content;
                cmd.Parameters.Add("@sender", OleDbType.VarChar).Value = c.Complaintant.Name;
                cmd.Parameters.Add("@recipieant", OleDbType.VarChar).Value = c.Recipiant.Name;
                if (c.SubForum != null)
                    cmd.Parameters.Add("@subId", OleDbType.VarChar).Value = c.SubForum.SubId;
                else
                    cmd.Parameters.Add("@subId", OleDbType.VarChar).Value = "";


                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;

                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.ToString());
                    return false;

                }
            }
            else
            {
                conn.Close();
                return false;
            }
        }



        internal bool addFriendsGroup(FriendGroup fg, string forumName)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            cmd = new OleDbCommand();
            cmd.CommandText = "INSERT into FriendsGroup ([Forum],[FriendGroupName]) values (@forum, @name)";
            cmd.Connection = conn;
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@forum", OleDbType.VarChar).Value = forumName;
                cmd.Parameters.Add("@name", OleDbType.VarChar).Value = fg.name;

                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                conn.Close();
                return false;
            }
        }

        internal bool addFriendsGroupMembers(FriendGroup fg, string forumName)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            cmd = new OleDbCommand();
            //discussionSubject, title, content, dateCreated, publisher
            cmd.CommandText = "INSERT into FriendsGroupMembers ([Forum],[FriendGroup],[Member]) values (@forum, @name, @member)";
            cmd.Connection = conn;
            DateTime date = DateTime.Now;
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@forum", OleDbType.VarChar).Value = forumName;
                cmd.Parameters.Add("@name", OleDbType.VarChar).Value = fg.name;
                cmd.Parameters.Add("@member", OleDbType.VarChar).Value = fg.members[0].Name;

                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                conn.Close();
                return false;
            }
        }



    }
}






