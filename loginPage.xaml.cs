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
    public sealed partial class loginPage : Page
    {
        public loginPage()
        {
            this.InitializeComponent();
        }

        private void NextBtn(object sender, RoutedEventArgs e)
        {
            
            //CALLING THE CLASS CONNECT TO THE LOGIN PAGE.
            Connect connect = new Connect();

            //QUERYING THE DATABASE TO CHECK IF USER IS REGISTERD.
            
            string Query = "SELECT cust_id,username,password FROM customer WHERE username = '" + this.username.Text + "'AND password ='" + this.password.Password + "'";
            //HANDLING THE  QUERY AND CONNECTION OBJECT.
            MySqlCommand command = new MySqlCommand(Query, connect.con);
           

            //OPENING THE CONNECTION
                connect.con.Open();

               // try
                //{
                    //READING FROM THE DATABASE.AND EXECUTING THE READER.
                   MySqlDataReader reader = command.ExecuteReader();
                
            
            ///VARIABLE COUNT TO CHECK THE NUMBER OF RESULT RETURNED
                   int count = 0;
                    // A STRING VARIABLE CONV TO STORE THE TOKEN 'cause it was in int.
                   string conv = string.Empty;

            //WHILE LOOP TO SHOW WHILE THE READER IS READING FROM THE DATABASE.
                   while (reader.Read())
                       //reader[0] was put in an array.
                        conv = reader[0].ToString();
                     //WHILE COUNT IS READING, IF A USER IS LOGED IN, IT SHOULD INCREMENT BY 1.
                       count += 1;
                   


                   if (count == 1)
                   {//THE USER IS VALID

                       //(App.Current as App).token = conv REMEMBER,conv yeah d string variable that was initially decleared.
                       //to store the token.the app.current blah blah is how data is transfered from page to page.
                       (App.Current as App).token = conv;

                       //after all this conditions has been meet,you can now allow the user to login.
                       this.Frame.Navigate(typeof(MenuPage));
                   }
                   else
                   {
                       MessageDialog box = new MessageDialog("username or password incorrect");
                       box.ShowAsync();
                       
                   }
                //}

                /* catch (Exception ex)
                {
                    MessageDialog box = new MessageDialog(ex.ToString());
                    box.ShowAsync();
                }
                 */ 

                //CLOSING THE CONNECTION.
                connect.con.Close();

           
            
           
        }


    //BACK BUTTONS .taking them back to where they came from.
        private void backToSignUpbtn(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void adminBtn(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(adminPage));
        }
    }
}
