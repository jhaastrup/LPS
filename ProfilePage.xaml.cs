using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LPS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            this.InitializeComponent();

            //CALLING THE CLASS CONNECTION.
            Connect connect = new Connect();

            //QUERING THE DATABASE.
            string Query = "SELECT cust_name,username,cust_address,workplace,cust_salary FROM customer WHERE cust_id = '" + (App.Current as App).token + "' ";
            MySqlCommand command = new MySqlCommand(Query, connect.con);

            connect.con.Open();
            //READING FROM THE DATABASE AND EXECUTING THE READER.
            MySqlDataReader reader = command.ExecuteReader();

            //DECLEARING THE VARIABLES TO DISPLAY IN THE TEXTBOX. 



            string N = "";//N == name
            string U = "";//U == username
            string A = "";//A == address
            string W = "";//W == workplace
            string S = "";//S == salary
            while (reader.Read())
            {
                N = reader[0].ToString();
                U = reader[1].ToString();
                A = reader[2].ToString();
                W = reader[3].ToString();
                S = reader[4].ToString();
            }

            //DISPLAYING INTO THE TEXTBOX.
            name.Text = N;
            username.Text = U;
            address.Text = A;
            company.Text = W;
            salary.Text = S;

            //CLOSING THE CONNECTION.
            connect.con.Close();

        }

        private void backBtn(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void editBtn(object sender, RoutedEventArgs e)
        {
            //CALLING THE CLASS CONNECT.
            Connect connect = new Connect();


            //QUERING THE DATABASE.TO EDIT USERS PROFILE..(UPDATE database_name SET table_name = "'+textboxname.Text+"')
            string Q = "UPDATE  customer SET cust_name = '" +name.Text+ "', username = '" +username.Text+ "',cust_address = '" +address.Text+ "', workplace = '" +company.Text+ "', cust_salary = '" +salary.Text+ "' WHERE cust_id = '" + (App.Current as App).token + "' ";
            MySqlCommand cmd = new MySqlCommand(Q, connect.con);

            connect.con.Open();
            MySqlDataReader reader2 = cmd.ExecuteReader();
            try
            {

                while (reader2.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageDialog box = new MessageDialog(ex.ToString());
                box.ShowAsync();
            }

            MessageDialog B = new MessageDialog("Succsefully Edited");
            B.ShowAsync();

            connect.con.Close();

        }
    }
}
