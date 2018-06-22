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
//using System.Windows.Documents;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LPS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VechicleLoan : Page
    {
        public VechicleLoan()
        {
            //CALLING THE CLASS CONNECT.
            Connect connect = new Connect();
            string Query = "SELECT loantype_name,interest_rate FROM loantype WHERE loantype_id = '" + (App.Current as App).loan_token + "'";


            //HANDLING THE QUERY AND CONNECTION OBJECT.
            MySqlCommand command = new MySqlCommand(Query, connect.con);

            connect.con.Open();

            //READING FROM THE DATABASE.
            MySqlDataReader reader = command.ExecuteReader();
            

            //VARIABLES TO PRINT TO THE TEXTBOX
            string _name = "";
            string _intrest = "";

            while (reader.Read())
            {
                _name = reader[0].ToString();
                _intrest = reader[1].ToString();
            }



            {
                this.InitializeComponent();
                vech.Text = _name;
                rate.Text = _intrest;

               
               
                //CLOSE CONNECTION
                connect.con.Close();


            }
        }
        private void GobackBtn(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoanPage));
        }

        private void getLoanBtn(object sender, RoutedEventArgs e)
        {
            MessageDialog d;
            //CHECKING FOR SPACE
            if (amount.Text.Trim() == "")
            {
                d = new MessageDialog("Input an amount");
                d.ShowAsync();
            }
            if (period.SelectionBoxItem.ToString().Trim() == "")
            {
                d = new MessageDialog("Select a duration");
                d.ShowAsync();
            }

            else
            {
                if (amount.Text.Trim() == "" || period.SelectionBoxItem.ToString().Trim() == "")
                {
                    d = new MessageDialog("AN error occured");
                    d.ShowAsync();
                }
                else
                {
                    //calling the class connect 
                    Connect connect = new Connect();

                    //OPENING THE CONNECTION AGAIN TO CHECK IF SALARY IS ENOUGH TO BORROW SPECIFIC AMOUNT OF MONEY.
                    connect.con.Open();

                    string q = "SELECT  cust_salary from customer WHERE cust_id = '" + (App.Current as App).token + "'";
                    MySqlCommand cmd = new MySqlCommand(q, connect.con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    int salary = 0;
                    while (reader.Read())
                    {
                        salary = Convert.ToInt32(reader[0]);
                    } 
                    //CLOSE CONNECTION
                    connect.con.Close();
                    //LOGIC TO BORROW MONEY
                    int x = salary * 2;

                    //CHECKING IF USER CAN BORROW
                    if (!(x >= Convert.ToInt32(amount.Text)))
                    {
                        d = new MessageDialog("Sorry, You Can only borrow Money 200% of your salary. ");
                        d.ShowAsync();
                    }

                    else
                    {

                        //VARIABLE TO STORE begin_date.
                        string date = "";
                        DateTime dt = DateTime.Now;
                        date = dt.Date.ToString("yyyy-MM-dd");

                        //VARIABLE TO STORE duration.
                        string per;
                        per = period.SelectionBoxItem.ToString();

                        //VARIABLE TO STORE end_date.
                        string enddate = "";

                        //CHECKING EACH INSTANCES.
                        if (per == "6 months")
                        {
                            enddate = dt.AddMonths(6).ToString("yyyy-MM-dd");

                        }

                        else if (per == "1 year")
                        {
                            enddate = dt.AddYears(1).ToString("yyyy-MM-dd");
                        }
                        else if (per == "3 years")
                        {
                            enddate = dt.AddYears(3).ToString("yyyy-MM-dd");
                        }
                        else if (per == "5 years")
                        {
                            enddate = dt.AddYears(5).ToString("yyyy-MM-dd");
                        }
                        else if (per == "7 years")
                        {
                            enddate = dt.AddYears(7).ToString("yyyy-MM-dd");
                        }
                        else if (per == "10 years")
                        {
                            enddate = dt.AddYears(10).ToString("yyyy-MM-dd");
                        }

                       

                        //QUERING THE DATABASE
                        string Query = "INSERT INTO loan( cust_id,loantype_id,amount,duration,begin_date,end_date) VALUES ( '" + (App.Current as App).token + "', '" + (App.Current as App).loan_token + "','" + this.amount.Text + "','" + per + "','" + date + "','" + enddate + "')";


                        // DateTime d = DateTime.Now;
                        //DateTime eventer = d.AddMonths(6);
                        //eventer.GetDateTimeFormats();

                       
                        
                        //HANDLING THE QUERY AND CONNECTION OBJECT.
                        MySqlCommand command = new MySqlCommand(Query, connect.con);


                        //OPENING THE CONNECTION AGAIN! '_'
                        connect.con.Open();

                        try
                        {
                            command.ExecuteNonQuery();
                        }

                        catch (Exception ex)
                        {
                            MessageDialog box = new MessageDialog(ex.ToString());
                            box.ShowAsync();
                        }


                        MessageDialog pop = new MessageDialog("Transaction successful");
                        pop.ShowAsync();

                        //CLOSING THE CONNECTION.
                        connect.con.Close();
                    }
                            

                }

            }
        }
    }

}
    

