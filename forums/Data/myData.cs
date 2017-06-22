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

        internal List<string> getDiscussions(string subForumSbject)
        {
            List<string> ans = new List<string>();
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT * from discussions WHERE  subForumSubject ='" + subForumSbject.Trim() + "'", conn);

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                foreach (DbDataRecord s in reader)
                {
                    string discSubject = s.GetString(1);
                    ans.Add(discSubject);

                }
                conn.Close();
                return ans;
            }
            conn.Close();
            return ans;
        }

        //elinor func
        /*
        internal List<FriendGroup> getFriendsGroupsOfForum(string subject)
        {
            List<FriendGroup> ans = new List<FriendGroup>();
            HashSet<Tuple<string, string>> friendsGroups = new HashSet<Tuple<string, string>>();
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT * from FriendsGroupMembers WHERE Forum ='" + subject.Trim() + "'", conn);
            List<string> members = new List<string>();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                foreach (DbDataRecord s in reader)
                {
                    string forumName = s.GetString(0);
                    string groupName = s.GetString(1);
                    friendsGroups.Add
                    //members.Add(s.GetString(2));
                    //ans.Add(new FriendGroup(forumName, groupName));
                }
                conn.Close();
                return ans;
            }
            conn.Close();
            return ans;
        }

        internal List<FriendGroup> getFriendsGroupsMOfForum(string subject)
        {
            List<FriendGroup> ans = new List<FriendGroup>();
            HashSet<Tuple<string, string>> friendsGroups = new HashSet<Tuple<string, string>>();
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT * from FriendsGroupMembers WHERE Forum ='" + subject.Trim() + "'", conn);
            List<string> members = new List<string>();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                foreach (DbDataRecord s in reader)
                {
                    string forumName = s.GetString(0);
                    string groupName = s.GetString(1);
                    friendsGroups.Add
                    //members.Add(s.GetString(2));
                    //ans.Add(new FriendGroup(forumName, groupName));
                }
                conn.Close();
                return ans;
            }
            conn.Close();
            return ans;
        }
        */

        internal bool addSubForum(string id, string forumSubject, string newSubName)
        {

            conn = new OleDbConnection();
            //conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
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
            //conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
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
            //cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = True AND Loc = '" + chosenArea.Trim() + "'" + " AND minAge = " + minAge + " AND maxAge = " + maxAge + " AND gender = '" + gender.Trim() + "'" + " AND smoke = " + smoke + " AND kosher = " + kosher + " AND quiet = " + quiet + " AND animals = " + animals + " AND play = " + play;
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
            //conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
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
            //conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
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

        internal List<Message> getMessages(string discSubject)
        {
            List<Message> ans = new List<Message>();
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT * from Messages WHERE discussionSubject ='" + discSubject.Trim() + "'", conn);

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                foreach (DbDataRecord s in reader)
                {
                    //string discSubject = s.GetString(1);
                    int a = s.GetInt32(0);
                    string b = s.GetString(1);
                    string c = s.GetString(2);
                    string d = s.GetString(3);
                    DateTime e = s.GetDateTime(4);
                    string f = s.GetString(5);
                    string g = s.GetString(6);
                    Message m = new Message(a, b, c, d, e, f, g);
                    ans.Add(m);

                }
                conn.Close();
                return ans;
            }
            conn.Close();
            return ans;
        }

        internal bool addMessage(Message m)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            cmd = new OleDbCommand();
            //discussionSubject, title, content, dateCreated, publisher
            cmd.CommandText = "INSERT into Messages ([discussionSubject],[title],[content],[dateCreated],[publisher]) values (@discnSub, @title, @content, @dateCreated, @publisher)";
            cmd.Connection = conn;
            string query2 = "Select @@Identity";
            int ID;
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@discnSub", OleDbType.VarChar).Value = m.discussionSubject;
                cmd.Parameters.Add("@title", OleDbType.VarChar).Value = m.title;
                cmd.Parameters.Add("@content", OleDbType.VarChar).Value = m.content;
                cmd.Parameters.Add("@dateCreated", OleDbType.Date).Value = m.dateCreated;
                cmd.Parameters.Add("@publisher", OleDbType.VarChar).Value = m.publisher;

                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = query2;
                    ID = (int)cmd.ExecuteScalar();
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

        public Dictionary<string, Discussion> getDiscussionsOfSubForum(string subForumSubject)
        {
            Dictionary<string, Discussion> ans = new Dictionary<string, Discussion>();
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT * from discussions WHERE subForumSubject ='" + subForumSubject.Trim() + "'", conn);

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                foreach (DbDataRecord s in reader)
                {
                    string discSubject = s.GetString(1);
                    Discussion disc = new Discussion(s.GetString(0), discSubject);
                    ans.Add(discSubject, disc);
                }
                conn.Close();
                return ans;
            }
            conn.Close();
            return ans;
        }

        internal bool addFriendsGroup(FriendGroup fg, string forumName)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            cmd = new OleDbCommand();
            //discussionSubject, title, content, dateCreated, publisher
            cmd.CommandText = "INSERT into FriendsGroup ([Forum],[FriendsGroupName]) values (@forum, @name)";
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
            cmd.CommandText = "INSERT into FriendsGroupMembers ([Forum],[FriendsGroup],[Member]) values (@forum, @name, @member)";
            cmd.Connection = conn;
            DateTime date = DateTime.Now;
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@forum", OleDbType.VarChar).Value = forumName;
                cmd.Parameters.Add("@name", OleDbType.VarChar).Value = fg.name;
                cmd.Parameters.Add("@member", OleDbType.Date).Value = fg.members[0];

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

        //add new user to the db
        /*public bool AddUserToDB(string mail, string pass, int age, string gender, bool? smoke, string name, bool? kosher, bool? quiet, bool? animals, bool? play, string about)
        {
            conn = new OleDbConnection();
            //conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+ dbPath +";";
            cmd = new OleDbCommand();
            cmd.CommandText = "INSERT into Profiles (mail, Pass, age, gender, smoke, fullName, kosher, quiet, animals, play,about) values(@mail, @Pass,@age,@gender, @smoke,@name, @kosher, @quiet,@animals,@play,@about)";
            cmd.Connection = conn;

            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@mail", OleDbType.VarChar).Value = mail;
                cmd.Parameters.Add("@Pass", OleDbType.VarChar).Value = pass;
                cmd.Parameters.Add("@age", OleDbType.Integer).Value = age;
                cmd.Parameters.Add("@gender", OleDbType.VarChar).Value = gender;
                cmd.Parameters.Add("@smoke", OleDbType.Boolean).Value = smoke;
                cmd.Parameters.Add("@name", OleDbType.VarChar).Value = name;
                cmd.Parameters.Add("@kosher", OleDbType.Boolean).Value = kosher;
                cmd.Parameters.Add("@quiet", OleDbType.Boolean).Value = quiet;
                cmd.Parameters.Add("@animals", OleDbType.Boolean).Value = animals;
                cmd.Parameters.Add("@play", OleDbType.Boolean).Value = play;
                cmd.Parameters.Add("@about", OleDbType.VarChar).Value = about;

                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    return false;
                }
            }
            else
            {
                return false;
            }
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

        public void AdvancedSearchDates(string chosenArea, string chosenKind, ref DataTable dt, bool payed, int minAge, int maxAge, string gender, bool? smoke, bool? kosher, bool? quiet, bool? animals, bool? play)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";

            OleDbCommand cmd = new OleDbCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            if (payed)
            {

                cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = True AND Loc = '" + chosenArea.Trim() + "'" + " AND minAge = " + minAge + " AND maxAge = " + maxAge + " AND gender = '" + gender.Trim() + "'" + " AND smoke = " + smoke + " AND kosher = " + kosher + " AND quiet = " + quiet + " AND animals = " + animals + " AND play = " + play;
            }
            else
            {
                cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = False AND Loc = '" + chosenArea.Trim() + "'" + " AND minAge = " + minAge + " AND maxAge = " + maxAge + " AND gender = '" + gender.Trim() + "'" + " AND smoke = " + smoke + " AND kosher = " + kosher + " AND quiet = " + quiet + " AND animals = " + animals + " AND play = " + play;
            }


            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            deleteCols(ref dt);
            //draw the results as a table


        }

        public void AdvancedSearchSport(string chosenArea, string chosenKind, ref DataTable dt, bool payed, int minAge, int maxAge, string type, int level, bool? smoke)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";

            OleDbCommand cmd = new OleDbCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            if (payed)
            {

                cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = True AND Loc = '" + chosenArea.Trim() + "'" + " AND minAge = " + minAge + " AND maxAge = " + maxAge + " AND Type = '" + type.Trim() + "'" + " AND level = " + level + " AND smoke = " + smoke;
            }
            else
            {
                cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = False AND Loc = '" + chosenArea.Trim() + "'" + " AND minAge = " + minAge + " AND maxAge = " + maxAge + " AND Type = '" + type.Trim() + "'" + " AND level = " + level + " AND smoke = " + smoke;
            }


            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            deleteColsSports(ref dt);
            //draw the results as a table
        }

        public void AdvancedSearchApartment(string chosenArea, string chosenKind, ref DataTable dt, bool payed, int minAge, int maxAge, int roomNum, bool? smoke, bool? kosher, bool? quiet, bool? animals, bool? play)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";

            OleDbCommand cmd = new OleDbCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            if (payed)
            {

                cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = True AND Loc = '" + chosenArea.Trim() + "'" + " AND minAge = " + minAge + " AND maxAge = " + maxAge + " AND numberOfRooms = " + roomNum + " AND smoke = " + smoke + " AND kosher = " + kosher + " AND quiet = " + quiet + " AND animals = " + animals;
            }
            else
            {
                cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = False AND Loc = '" + chosenArea.Trim() + "'" + " AND minAge = " + minAge + " AND maxAge = " + maxAge + " AND numberOfRooms = " + roomNum + " AND smoke = " + smoke + " AND kosher = " + kosher + " AND quiet = " + quiet + " AND animals = " + animals;
            }


            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            deleteCols(ref dt);
            //draw the results as a table


        }

        public void AdvancedSearchTrips(string chosenArea, string chosenKind, ref DataTable dt, bool payed, int minAge, int maxAge, string kind, string country, bool? smoke, bool? kosher, bool? quiet)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";

            OleDbCommand cmd = new OleDbCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            if (payed)
            {

                cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = True AND Loc = '" + chosenArea.Trim() + "'" + " AND minAge = " + minAge + " AND maxAge = " + maxAge + " AND KindOfTrip = '" + kind.Trim() + "'" + " AND Country = '" + country.Trim() + "'" + " AND smoke = " + smoke + " AND kosher = " + kosher + " AND quiet = " + quiet;
            }
            else
            {
                cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = False AND Loc = '" + chosenArea.Trim() + "'" + " AND minAge = " + minAge + " AND maxAge = " + maxAge + " AND KindOfTrip = '" + kind.Trim() + "'" + " AND Country = '" + country.Trim() + "'" + " AND smoke = " + smoke + " AND kosher = " + kosher + " AND quiet = " + quiet;
            }



            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            deleteColsTrips(ref dt);
            //draw the results as a table


        }

        private void deleteColsTrips(ref DataTable dt)
        {
            // dt.Columns.Remove("mail");
            dt.Columns.Remove("Payed");
            // dt.Columns.Remove("addID");
            dt.Columns.Remove("smoke");
            dt.Columns.Remove("kosher");
            dt.Columns.Remove("quiet");
        }

        private void deleteColsSports(ref DataTable dt)
        {
            // dt.Columns.Remove("mail");
            dt.Columns.Remove("Payed");
            // dt.Columns.Remove("addID");
            dt.Columns.Remove("smoke");
            //dt.Columns.Remove("kosher");
            //dt.Columns.Remove("quiet");
        }

        public bool saveRequest(string mail, DateTime date, int activityId, string kindName, int adId, string content, int status)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            cmd = new OleDbCommand();
            cmd.CommandText = "INSERT into RequestId (askerMail, RequsetDate, ActivityId, KindName, AdId, Content, Status) values(@askerMail, @RequsetDate ,@activityId ,@kindName, @adId, @content, @status)";
            cmd.Connection = conn;

            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@askerMail", OleDbType.VarChar).Value = mail;
                cmd.Parameters.Add("@date", OleDbType.DBDate).Value = date;
                cmd.Parameters.Add("@activityId", OleDbType.Integer).Value = activityId;
                cmd.Parameters.Add("@KindName", OleDbType.VarChar).Value = kindName;
                cmd.Parameters.Add("@AdId", OleDbType.Integer).Value = adId;
                cmd.Parameters.Add("@Content", OleDbType.VarChar).Value = content;
                cmd.Parameters.Add("@Status", OleDbType.Integer).Value = status;

                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //ckeck if the password of user is correct
        public bool checkPassword(string mail, string password)
        {
            //get the user's password from the db
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT Pass, FullName from Profiles WHERE  mail ='" + mail.Trim() + "'", conn);
            reader = cmd.ExecuteReader();
            //check if the mail exists
            if (!reader.HasRows)
            {
                conn.Close();
                return false;
            }
            else
            {
                while (reader.Read())
                {
                    string ans = reader[0].ToString();
                    if (ans != password)
                    {
                        conn.Close();
                        return false;
                    }
                    else
                    {
                        conn.Close();
                        return true;
                    }
                }
            }

            return false;
        }

        //get the user's name
        public string getName(string mail)
        {
            string ans = "";
            //get the user's password from the db
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            cmd = new OleDbCommand("SELECT FullName from Profiles WHERE  mail ='" + mail.Trim() + "'", conn);
            reader = cmd.ExecuteReader();
            //check if the mail exists
            if (!reader.HasRows)
            {
                return "";
            }
            else
            {
                while (reader.Read())
                {
                    ans = reader[0].ToString();
                    return ans;
                }
            }

            return ans;
        }

        //list of all the members in a requested activity
        public List<string> getMembersActivity(int id)
        {
            OleDbDataReader reader = null;
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";

            OleDbCommand cmd = new OleDbCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * from ActivitiesMembers" + " WHERE ActivityId = " + id;
            // OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            // DataTable dt = new DataTable();
            //  da.Fill(dt);
            reader = cmd.ExecuteReader();
            List<string> members = new List<string>();
            while (reader.Read())
            {
                members.Add(reader[1].ToString());
            }
            return members;
        }

        //list of all the activities that the user is a member in them
        public Dictionary<int, string> getMemberActivities(string userMail)
        {
            OleDbDataReader reader = null;
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            string tableName = "ActivitiesMembers";
            //OleDbCommand cmd = new OleDbCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd = new OleDbCommand("SELECT ActivityId FROM " + tableName + " WHERE Mail = '" + userMail.Trim() + "'", conn);
            reader = cmd.ExecuteReader();
            List<string> activities = new List<string>();
            Dictionary<int, string> results = new Dictionary<int, string>();
            while (reader.Read())
            {
                activities.Add(reader[0].ToString());
            }
            tableName = "Activities";
            for (int i = 0; i < activities.Count; i++)
            {
                reader = null;
                cmd = new OleDbCommand("SELECT * FROM " + tableName + " WHERE ActivityId = " + activities[i], conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string line = reader[1].ToString() + "\t" + reader[2].ToString() + "\t" + reader[3].ToString() + "\t" + reader[4].ToString() + "\t" + reader[5].ToString();
                    results.Add(Int32.Parse(reader[0].ToString()), line);
                }
            }
            return results;
        }

        //remove unnececery colums from the search results
        private void deleteCols(ref DataTable dt)
        {
            try
            {
                // dt.Columns.Remove("mail");
                dt.Columns.Remove("Payed");
                // dt.Columns.Remove("addID");
                dt.Columns.Remove("smoke");
                dt.Columns.Remove("kosher");
                dt.Columns.Remove("quiet");
                dt.Columns.Remove("animals");
                dt.Columns.Remove("play");
            }
            catch { }
        }


        //find matching adds from the db
        public void find(string chosenArea, string chosenKind, ref DataTable dt, bool payed)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";

            OleDbCommand cmd = new OleDbCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            if (payed)
            {
                if (chosenArea == "")
                    cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = True";
                else
                    cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = True AND Loc = '" + chosenArea.Trim() + "'";
            }
            else
            {
                if (chosenArea == "")
                    cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = False";
                else
                    cmd.CommandText = "SELECT * from " + chosenKind + " WHERE Payed = False AND Loc = '" + chosenArea.Trim() + "'";
            }

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            deleteCols(ref dt);
            //draw the results as a table
        }

        //get the list of kinds 
        internal List<string> getKinds()
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;

            cmd = new OleDbCommand("SELECT * From Kinds", conn);
            reader = cmd.ExecuteReader();
            List<string> kinds = new List<string>();
            while (reader.Read())
            {
                kinds.Add(reader[0].ToString());
            }
            return kinds;
        }

        //get the list of areas from the db
        internal List<string> getAreas()
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";

            OleDbDataReader reader = null;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd = new OleDbCommand("SELECT * From Areas", conn);
            reader = cmd.ExecuteReader();
            List<string> areas = new List<string>();
            while (reader.Read())
            {
                areas.Add(reader[0].ToString());
            }
            return areas;
        }

        internal int CreateNewActivity(string area, string kind, string name, string head, string userMail, string info)
        {
            DateTime creation = DateTime.Today;
            int actId = 0;
            conn = new OleDbConnection();

            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            cmd = new OleDbCommand();
            cmd.CommandText = "INSERT into Activities ([ActivityName], [area], [kind], [CreationDate], [Info]) VALUES('" + name + "','" + area + "','" + kind + "','" + creation + "','" + info + "')";
            cmd.Connection = conn;
            conn.Open();

            if (conn.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@activityName", OleDbType.VarChar).Value = name;
                cmd.Parameters.Add("@area", OleDbType.VarChar).Value = area;
                cmd.Parameters.Add("@kind", OleDbType.VarChar).Value = kind;
                cmd.Parameters.Add("@CreationDate", OleDbType.DBDate).Value = creation;
                cmd.Parameters.Add("@Info", OleDbType.VarChar).Value = info;

                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //OleDbDataReader reader = null;
                    conn = new OleDbConnection();
                    conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT TOP 1 ActivityId FROM Activities ORDER BY ActivityId desc";
                    cmd.Connection = conn;
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        actId = Int32.Parse(reader[0].ToString());

                    return actId;
                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        internal bool AddnewActivityMember(int actId, string userMail, bool isHead)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            cmd = new OleDbCommand();
            cmd.CommandText = "INSERT into ActivitiesMembers ([ActivityId], [Mail], [IsHead]) values(@actId, @userMail, @isHead)";
            cmd.Connection = conn;

            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                cmd.Parameters.Add("@ActivityId", OleDbType.VarChar).Value = actId;
                cmd.Parameters.Add("@Mail", OleDbType.VarChar).Value = userMail;
                cmd.Parameters.Add("@IsHead", OleDbType.Integer).Value = isHead;
                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //return a list of all the kinds fields
        internal List<string> GetKindFields(string kind)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            conn.Open();
            OleDbDataReader reader = null;
            List<string> kindFields = new List<string>();
            cmd = new OleDbCommand("SELECT * From " + kind, conn);
            //reader = cmd.ExecuteReader();
            //reader.Read();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int counter = 0;
            foreach (DataColumn column in dt.Columns)
            {
                if (counter < 5) //the first six columns are irrelevant
                {
                    counter++;
                    continue;
                }
                kindFields.Add(column.ColumnName);
            }
            return kindFields;
        }

        public bool AddTripsAd(string userMail, string area, int activityId, int minAge, int maxAge, bool kosher, bool quiet, bool smoke, string country, DateTime date, string kindof, string content)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            cmd = new OleDbCommand();
            bool Payed = false;
            DateTime Date = DateTime.Today;
            DateTime startDate = date;
            //cmd.CommandText = "INSERT into Trips ([AdvertiserMail], [Date], [loc], [Payed], [Country], [StartDate], [KindOfTrip], [minAge], [maxAge], [smoke], [kosher], [quiet], [Content], [Activityid]) values('" + userMail + "', '" + startDate + "','" + area + "','" + Payed + "','" + country + "','" + date + "','" + kindof + "','" + minAge + "','" + maxAge + "','" + smoke + "','" + kosher + "','" + quiet + "','" + content + "','" + activityId + "')";
            cmd.CommandText = "INSERT into Trips ([AdvertiserMail], [Date], [loc], [Payed], [Country], [StartDate], [KindOfTrip], [minAge], [maxAge], [smoke], [kosher], [quiet], [Content], [Activityid]) values(@userMail, @Date, @area, @Payed, @country, @startDate, @kindof, @minAge, @maxAge, @smoke, @kosher, @quiet, @content, @activityId)";
            cmd.Connection = conn;
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                //[AdvertiserMail], [Date], [loc], [Payed], [Country], [StartDate], [KindOfTrip], [minAge], [maxAge], [smoke], [kosher], [quiet], [Content], [Activityid]
                //(@userMail, @Date, @area, @Payed, @country, @date, @kindof, @minAge, @maxAge, @smoke, @kosher, @quiet, @Content, @activityId)
                cmd.Parameters.Add("@userMail", OleDbType.VarChar).Value = userMail;
                cmd.Parameters.Add("@Date", OleDbType.DBDate).Value = Date;
                cmd.Parameters.Add("@area", OleDbType.VarChar).Value = area;
                cmd.Parameters.Add("@Payed", OleDbType.Boolean).Value = Payed;
                cmd.Parameters.Add("@country", OleDbType.VarChar).Value = country;
                cmd.Parameters.Add("@startDate", OleDbType.DBDate).Value = startDate;
                cmd.Parameters.Add("@kindof", OleDbType.VarChar).Value = kindof;
                cmd.Parameters.Add("@minAge", OleDbType.Integer).Value = minAge;
                cmd.Parameters.Add("@maxAge", OleDbType.Integer).Value = maxAge;
                cmd.Parameters.Add("@smoke", OleDbType.Boolean).Value = smoke;
                cmd.Parameters.Add("@kosher", OleDbType.Boolean).Value = kosher;
                cmd.Parameters.Add("@quiet", OleDbType.Boolean).Value = quiet;
                cmd.Parameters.Add("@content", OleDbType.LongVarChar).Value = content;
                cmd.Parameters.Add("@activityId", OleDbType.Integer).Value = activityId;
                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool AddDatesAd(string userMail, string area, int activityId, int minAge, int maxAge, bool kosher, bool quiet, bool play, bool animals, bool smoke, string iWant, string gender, string about, string content)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            cmd = new OleDbCommand();
            bool Payed = false;
            DateTime Date = DateTime.Today;
            cmd.CommandText = "INSERT into Dates ([AdvertiserMail], [Date], [Loc], [Payed], [gender], [about], [IWant], [minAge], [maxAge], [smoke], [kosher], [quiet], [animals], [play], [Content], [Activityid]) values(@userMail, @Date, @loc, @Payed, @gender, @about, @IWant, @minAge, @maxAge, @smoke, @kosher, @quiet, @animals, @play, @content, @activityId)";
            cmd.Connection = conn;
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                //cmd.CommandText = "INSERT into Dates ([AdvertiserMail], [Date], [Loc], [Payed], [gender], [about], [IWant], [minAge], [maxAge], [smoke], [kosher], [quiet], [animals], [play], [Content], [Activityid]) values(@userMail, @Date, @Loc, @Payed, @gender, @about, @IWant, @minAge, @maxAge, @smoke, @kosher, @quiet, @animals, @play, @content, @activityId)";
                cmd.Parameters.Add("@userMail", OleDbType.VarChar).Value = userMail;
                cmd.Parameters.Add("@Date", OleDbType.DBDate).Value = Date;
                cmd.Parameters.Add("@loc", OleDbType.VarChar).Value = area;
                cmd.Parameters.Add("@Payed", OleDbType.Boolean).Value = Payed;
                cmd.Parameters.Add("@address", OleDbType.VarChar).Value = gender;
                cmd.Parameters.Add("@numofrooms", OleDbType.VarChar).Value = about;
                cmd.Parameters.Add("@numofrooms", OleDbType.VarChar).Value = iWant;
                cmd.Parameters.Add("@minAge", OleDbType.Integer).Value = minAge;
                cmd.Parameters.Add("@maxAge", OleDbType.Integer).Value = maxAge;
                cmd.Parameters.Add("@smoke", OleDbType.Boolean).Value = smoke;
                cmd.Parameters.Add("@kosher", OleDbType.Boolean).Value = kosher;
                cmd.Parameters.Add("@quiet", OleDbType.Boolean).Value = quiet;
                cmd.Parameters.Add("@animals", OleDbType.Boolean).Value = animals;
                cmd.Parameters.Add("@play", OleDbType.Boolean).Value = play;
                cmd.Parameters.Add("@content", OleDbType.LongVarChar).Value = content;
                cmd.Parameters.Add("@activityId", OleDbType.Integer).Value = activityId;
                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool AddSportsAd(string userMail, string area, int activityId, int minAge, int maxAge, bool smoke, int level, string type, string content)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            cmd = new OleDbCommand();
            bool Payed = false;
            DateTime Date = DateTime.Today;
            cmd.CommandText = "INSERT into Sport ([AdvertiserMail], [Date], [Loc], [Payed], [Type], [level], [minAge], [maxAge], [smoke], [Content], [Activityid]) values(@userMail, @Date, @area, @Payed, @type, @level, @minAge, @maxAge, @smoke, @content, @activityId)";
            cmd.Connection = conn;
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                //values(@userMail, @Date, @area, @payed, @type, @level, @minAge, @maxAge, @smoke, @content, @activityId)";

                cmd.Parameters.Add("@userMail", OleDbType.VarChar).Value = userMail;
                cmd.Parameters.Add("@Date", OleDbType.DBDate).Value = Date;
                cmd.Parameters.Add("@area", OleDbType.VarChar).Value = area;
                cmd.Parameters.Add("@Payed", OleDbType.Boolean).Value = Payed;
                cmd.Parameters.Add("@type", OleDbType.VarChar).Value = type;
                cmd.Parameters.Add("@level", OleDbType.Integer).Value = level;
                cmd.Parameters.Add("@minAge", OleDbType.Integer).Value = minAge;
                cmd.Parameters.Add("@maxAge", OleDbType.Integer).Value = maxAge;
                cmd.Parameters.Add("@smoke", OleDbType.Boolean).Value = smoke;
                cmd.Parameters.Add("@content", OleDbType.LongVarChar).Value = content;
                cmd.Parameters.Add("@activityId", OleDbType.Integer).Value = activityId;
                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public bool AddEstaeteAd(string userMail, string area, int activityId, int minAge, int maxAge, bool kosher, bool quiet, bool animals, bool smoke, string address, int numofrooms, string content)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath + ";";
            cmd = new OleDbCommand();
            bool Payed = false;
            DateTime Date = DateTime.Today;
            cmd.CommandText = "INSERT into Real_Estate ([AdvertiserMail], [Date], [Loc], [Payed], [address], [numberOfRooms], [minAge], [maxAge], [smoke], [kosher], [quiet], [animals], [Content], [Activityid]) values(@userMail, @Date, @Loc, @Payed, @address, @numofrooms, @minAge, @maxAge, @smoke, @kosher, @quiet, @animals, @content, @activityId)";
            cmd.Connection = conn;
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                //@userMail, @Date, @Loc, @Payed, @address, @numberOfRooms, @minAge, @maxAge, @smoke, @kosher, @quiet, @content, @activityId)
                cmd.Parameters.Add("@userMail", OleDbType.VarChar).Value = userMail;
                cmd.Parameters.Add("@Date", OleDbType.DBDate).Value = Date;
                cmd.Parameters.Add("@area", OleDbType.VarChar).Value = area;
                cmd.Parameters.Add("@Payed", OleDbType.Boolean).Value = Payed;
                cmd.Parameters.Add("@address", OleDbType.VarChar).Value = address;
                cmd.Parameters.Add("@numofrooms", OleDbType.Integer).Value = numofrooms;
                cmd.Parameters.Add("@minAge", OleDbType.Integer).Value = minAge;
                cmd.Parameters.Add("@maxAge", OleDbType.Integer).Value = maxAge;
                cmd.Parameters.Add("@smoke", OleDbType.Boolean).Value = smoke;
                cmd.Parameters.Add("@kosher", OleDbType.Boolean).Value = kosher;
                cmd.Parameters.Add("@quiet", OleDbType.Boolean).Value = quiet;
                cmd.Parameters.Add("@quiet", OleDbType.Boolean).Value = animals;
                cmd.Parameters.Add("@content", OleDbType.LongVarChar).Value = content;
                cmd.Parameters.Add("@activityId", OleDbType.Integer).Value = activityId;
                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                catch (OleDbException ex)
                {
                    conn.Close();
                    return false;
                }
            }
            else
            {
                return false;
            }

        }*/

    }
}






