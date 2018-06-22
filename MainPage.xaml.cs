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
using System.Text;
using Windows.UI.Popups;





// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LPS
{ 


    /// <summary>
    /// THE CLASSS THAT CONNECTS TO THE DATABASE.
    /// </summary>
    class Connect{
        //CREATING THE OBJECT OF CONNECTION.==(con) and passing connection string.
        public MySqlConnection con = new MySqlConnection();
      /// <summary>
      /// PASSING CONNECTION STRING.INSIDE PUBLIC CONNECT
      /// </summary>
      /// <param name="connect"></param>
       public Connect(string connect ="server=localhost;port=3306;database=customer_db;uid=root;password=;sslMode=None;")
        {
            this.con.ConnectionString = connect;

       }

    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }




        private void submitBtn(object sender, RoutedEventArgs e)
        {
            ///CALLING THE CLASS CONNECT .

            Connect connect = new Connect();
            
            //CHECKING IF THE USERNAME BEEN TAKEN OR NAH!

            //Quering the database to check if the user name has been taken 
            string q = "SELECT username FROM customer WHERE username = '" + username.Text+ "' ";
            MySqlCommand cmd = new MySqlCommand(q, connect.con);


            //re-open the connection to check if the username has already been taken. 
            connect.con.Open();

            //reading from the database and excuting reader.
            MySqlDataReader reader = cmd.ExecuteReader();


            //variable to store user name 
           
          
            string UN = "";
          
            while (reader.Read())
            {
                UN = reader[0].ToString();          
            }

            //close the connection 
            connect.con.Close();


            //check if the user name already exist .

            if (username.Text == UN)
            {
                MessageDialog d = new MessageDialog("sorry this username has been taken try " + this.name.Text + "002");
                d.ShowAsync();
            }

            else
            {


                //INSERTING INTO THE DATABASE. using the insert query.
                string Query = "INSERT INTO customer(cust_name,username,password,cust_address,workplace,cust_salary,date)VALUES('" + this.name.Text + "', '" + this.username.Text + "','" + this.password.Password + "','" + this.address.Text + "', '" + this.organization.Text + "', '" + this.salary.Text + "' ,NOW())";

                //HANDLING THE QUERY AND CONNECTION OBJECT.
                MySqlCommand command = new MySqlCommand(Query, connect.con);

                //OPENING THE CONNECTION.
                connect.con.Open();



                try
                {///EXECUTING THE QUERY
                    command.ExecuteNonQuery();
                }
                //cactch expception incase of error.
                catch (Exception ex)
                {
                    MessageDialog box = new MessageDialog(ex.ToString());
                    box.ShowAsync();
                }

                ///CLOSING THE CONNECTION.
                connect.con.Close();
         


            //everything works out fine,show user this message.
            MessageDialog box2 = new MessageDialog("Thank you " + this.name.Text + " for choosing quick loan");
            box2.ShowAsync();

          
                //after the whole story,move to the nextpage.
                this.Frame.Navigate(typeof(loginPage));

            }
        }
    
    
       //just incase you wanna go back,this navigates you backwards.
        private void GotoLoginBtn(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(loginPage));
        } 
    }
}
