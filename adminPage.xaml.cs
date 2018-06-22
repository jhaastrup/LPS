using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LPS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class adminPage : Page
    {
        public adminPage()
        {
            this.InitializeComponent();
        }

        private void loginBtn(object sender, RoutedEventArgs e)
        {
            //calling the class connect to the admin page.
            Connect connect = new Connect();

            //quering the database 
            string Q = "SELECT username,password FROM admin WHERE username = '" + username.Text + "' AND password = '" + password.Password + "' ";
            //handling the query and connection object.
            MySqlCommand command = new MySqlCommand(Q, connect.con);

            //opening the connection.

            connect.con.Open();

            //reading from the database.

            {
                MySqlDataReader reader = command.ExecuteReader();

                int count = 0;

                while (reader.Read())
                {
                    count += 1;
                }

                if (count == 1)
                {
                    this.Frame.Navigate(typeof(Admin));
                }

                else
                {

                    MessageDialog box = new MessageDialog("Access Denied!");
                    box.ShowAsync();


                }

            }
            connect.con.Close();


        }

        private void backToSignUpbtn(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }


    }
}
