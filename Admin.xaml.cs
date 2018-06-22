using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows
.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LPS.preClasses;
using Windows.UI.Popups;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238


///THIS PAGE IS SO CROWEDED SO AM GOING TO TAKE OUT TIME TO EXPLAIN EXACTLY WHATS GOING ON HERE.
namespace LPS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Admin : Page
    {
        private string loanid = string.Empty;
         //CALLING THE CLASS CONNECT
            Connect connect = new Connect();

           
       

         
       
        public Admin()
        {
            this.InitializeComponent();

            //ScrollBar sb = new ScrollBar();
            //sb.Orientation = Orientation.Horizontal;
            //sb.Width = 500;
            //sb.HorizontalAlignment = HorizontalAlignment.Left;
            //sb.Height = 50;
            //sb.Minimum = 2;
            //sb.Maximum = 450;
            //sb.Visibility = Windows.UI.Xaml.Visibility.Visible;
            ////me.Children.Add(sb);
           

            //THE ADMIN PAGE IS BASED ON THE LOAN COLLECTED BY EACH CUSTOMER.
            //QUERING THE DATABASE BASED ON THE LOAN TABLE
            string Q = "SELECT * FROM loan";

            MySqlCommand commad = new MySqlCommand(Q, connect.con);
            connect.con.Open();


            //PASSING THE COMMAND AND EXECUTING READER 
            MySqlDataReader reader = commad.ExecuteReader();


            //THIS IS VERY IMPORTANT! HERE,WE ARE TRYING TO LOOP THROUGH THE ROWS AND COLOUMS OF THE DATABASE.
            //rowCount and colCount: variables to read through d rows and coloums.
            int rowCount = 0;
            int colCount = 0;


            while (reader.Read())
            {//FieldCount issa built in method to go through the whole database feild.
                while (colCount < reader.FieldCount)
                {//if the condition is meet, go to the next coloumn
                    colCount++;
                }

                //then add one to d row. so you can keep going through the rows and coloums
                rowCount += 1;


            }
            //CLOSING THE CONNECTION.
            connect.con.Close();

            //WE HAVE TO RE-OPEN THE CONNECTION  AND RE-EXECUTE THE READER.
            connect.con.Open();
            reader = commad.ExecuteReader();


            // DECLEAR A STRING MULTIDIMENTIONAL ARRAY AND STORE THE INFO GOTTEN FROM THE DATABASE IN VARIABLE: "Loanresult"
            
            string[,] Loanresult = new string[rowCount, colCount]; //the Loanresult here is for the loan table from the database.


            //INT i(ROW) AND j(COLUMN) variales to store d rows and colounms for looping.
            int i = 0; int j = 0;
            while (reader.Read())
            {
                while (j < reader.FieldCount)
                {

                    if (reader[j] != null)
                    {
                        Loanresult[i, j] = reader[j].ToString();
                    }
                    else
                    {
                        Loanresult[i, j] = "";
                    }
                    //after looping through,increment the coloumn which is j
                    j++;
                }
                ///now j has been incremented so therefore,we should equate back to zero so it can go through the loop again.
                j = 0;
                //when all of this conditions have been meet,we increment (i) the row!
                i++;
            }
            //LETS MOVE THIS BITCHES TO A FOR LOOP!   
            //here we are trying to generate new rows...hence this (i< rowCount) .
            for (i = 0; i < rowCount; i++)
            {
                //THIS IS A NEW QUERY TO PRINT OUT CUSTOMER INFO FROM customer TABLE.
                string q = "SELECT cust_id,cust_name,username,cust_address,workplace,cust_salary FROM customer WHERE cust_id = '" + Loanresult[i, 1] + "'";
                MySqlCommand cmd = new MySqlCommand(q, connect.con); 


                //LOGIC TO CHANGE THE COLORS AS THEY MOVE DOWN THE ROWS.
                //i % 2 == i.e row mudolus 2 (dividing rows by 2 and printing the remaining)! 
                // variable calc to store d answer of i%2
                int calc = i % 2;

                SolidColorBrush bg;//bg is the object of SolidColorBrush.
                //switch colors
                if(calc == 1){
                //
                    bg = new SolidColorBrush(Color.FromArgb(255,245,245,245));
                }
                else{
                    //
                        bg = new SolidColorBrush(Color.FromArgb(255,220,220,200));
                } //END BLOCK FOR COLOR CHINGING.



                //CONNECTION MUST BE CLOSED!
                connect.con.Close();

                //OPENING THE CONNECTION AND EXCUTING THE COMMAND.
                connect.con.Open();
                MySqlDataReader reader2 = cmd.ExecuteReader();


                int row = 0;
                int col = 0;

                while (reader2.Read())
                {
                    while (col < reader2.FieldCount)
                    {
                        col++;
                    }

                    row += 1;
                }
                connect.con.Close();

                ///we are trying to print out from  customer table here.
                connect.con.Open();
                reader2 = cmd.ExecuteReader();

                //STORE CUSTOMER INFO FROM CUSTOMER TABLE IN A STRING ARRAY "result"
                string[,] result = new string[row, col];


                int a = 0; int y = 0;
                while (reader2.Read())
                {
                    while (y < reader2.FieldCount)
                    {

                        if (reader2[y] != null)
                        {
                            result[a, y] = reader2[y].ToString();
                        }
                        else
                        {
                            result[a, y] = "";
                        }
                        y++;
                    }
                    y = 0;
                    a++;
                }

                //color for foreground.
                SolidColorBrush fd;
                fd = new SolidColorBrush(Color.FromArgb(255,0,0,0));

                double r = 50;
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(r);

                elements.RowDefinitions.Add(rd);
                //puting this bitch in a stack so i can easily switch them colors
                StackPanel gf = new StackPanel();
                gf.Background = bg;

                TextBlock cust_id = new TextBlock();
                cust_id.Text = result[0, 0];
                // cust_id.Width = 100;
                cust_id.Height = 50;
                cust_id.Foreground = fd;
                cust_id.TextAlignment = TextAlignment.Center;

                // double left = 15, top = 10, right = 0, bottom = 0;
                cust_id.Margin = new Thickness(0, 15, 0, 0);
                cust_id.FontSize = 20; 

                Grid.SetRow(gf, i + 1);
                Grid.SetColumn(gf, 0); 
                elements.Children.Add(gf);
                gf.Children.Add(cust_id); //added the textblock in the stackpanel


                StackPanel n = new StackPanel();
                n.Background = bg;
                TextBlock name = new TextBlock();
                name.Text = result[0, 1];

                // name.Width = 200;
                name.Height = 50;
                name.Margin = new Thickness(0, 15, 0, 0);
                name.FontSize = 20;
                name.Foreground = fd;
                name.TextAlignment = TextAlignment.Center;
                //  TextWrapping wrap = new TextWrapping();
                // name.TextWrapping = wrap;


                Grid.SetRow(n, i + 1);
                Grid.SetColumn(n, 1);
                elements.Children.Add(n);
                n.Children.Add(name);


                StackPanel u = new StackPanel();
                u.Background = bg;
                TextBlock username = new TextBlock();
                username.Text = result[0, 2];
                //username.Width = 200;
                username.Height = 50;
                username.Foreground = fd;
                username.TextAlignment = TextAlignment.Center;
                username.Margin = new Thickness(0, 15, 0, 0);
                username.FontSize = 20;
                Grid.SetRow(u, i + 1);
                Grid.SetColumn(u, 2);
                elements.Children.Add(u);
                u.Children.Add(username);



                StackPanel ad = new StackPanel();
                ad.Background = bg;
                TextBlock address = new TextBlock();
                address.Text = result[0, 3];
                //address.Width = 200;
                address.Height = 50;
                address.Foreground = fd;
                address.Margin = new Thickness(0, 15, 0, 0);
                address.FontSize = 20;
                address.TextAlignment = TextAlignment.Center;
                Grid.SetRow(ad, i + 1);
                Grid.SetColumn(ad, 3);
                elements.Children.Add(ad);
                ad.Children.Add(address);


                StackPanel w = new StackPanel();
                w.Background = bg;
                TextBlock work = new TextBlock();
                work.Text = result[0, 4];
                //work.Width = 200;
                //work.Height = 50;
                work.Margin = new Thickness(0, 15, 0, 0);
                work.FontSize = 20;
                work.Foreground = fd;
                work.TextAlignment = TextAlignment.Center;
                Grid.SetRow(w, i + 1);
                Grid.SetColumn(w, 4);
                elements.Children.Add(w);
                w.Children.Add(work);


                StackPanel sa = new StackPanel();
                sa.Background = bg;
                TextBlock salary = new TextBlock();
                salary.Text = result[0, 5];
                //salary.Width = 1000;
                salary.Height = 50;
                salary.Foreground = fd;
                salary.Margin = new Thickness(0, 15, 0, 0);
                salary.FontSize = 20;
                salary.TextAlignment = TextAlignment.Center;

                Grid.SetRow(sa, i + 1);
                Grid.SetColumn(sa, 5);
                elements.Children.Add(sa);
                sa.Children.Add(salary);
                //END OF THE CUSTOMER. 

            

                //SWITCHING TO THE NEXT TABLE LOANTYPE.

                string qu = "SELECT interest_rate FROM loantype WHERE loantype_id ='" + Loanresult[i, 2] + "'";
                MySqlCommand cmmd = new MySqlCommand(qu, connect.con);

                connect.con.Close();

                connect.con.Open();
                MySqlDataReader reader3 = cmmd.ExecuteReader();

                int r_count = 0;
                int c_count = 0;

                while (reader3.Read())
                {
                    while (c_count < reader3.FieldCount)
                    {
                        c_count++;
                    }

                    r_count += 1;
                }
                connect.con.Close();


                ///we are trying to print out from LOANTYPE TABLE here.
                connect.con.Open();
                reader3 = cmmd.ExecuteReader();

                //store loantype info in a new string array
                string[,] loanType_result = new string[r_count, c_count];


                int b = 0; int c = 0;
                while (reader3.Read())
                {
                    while (c < reader3.FieldCount)
                    {

                        if (reader3[c] != null)
                        {
                            loanType_result[b, c] = reader3[c].ToString();
                        }
                        else
                        {
                            loanType_result[b, c] = "";
                        }
                        c++;
                    }
                    c = 0;
                    b++;
                }

                //NOW START PRINTING INSIDE THE TEXTBLOCK.

                StackPanel fw = new StackPanel();
                fw.Background = bg;
                TextBlock interest = new TextBlock();
                interest.Text = loanType_result[0, 0];
                //interest.Width = 200;
                interest.Height = 50;
               interest.Margin = new Thickness(0, 15, 0, 0);
                interest.FontSize = 20;
                interest.Foreground = fd;
                interest.TextAlignment = TextAlignment.Center;
                Grid.SetRow(fw, i + 1);
                Grid.SetColumn(fw, 6);
                elements.Children.Add(fw);
                fw.Children.Add(interest);

                //END OF THIS STORY. 

                //SWITCHING TO A NEW TABLE.


                string jk = "SELECT amount,duration,begin_date,end_date,paid from loan WHERE cust_id = '" + Loanresult[i, 1] + "'";
                MySqlCommand commando = new MySqlCommand(jk, connect.con);

                //CLOSING CONNECTION FOR THE OLD TABLE(CUSTOMER TABLE).
                connect.con.Close();

                //NOW OPENING FOR A NEW ONE.
                connect.con.Open();
                MySqlDataReader reader4 = commando.ExecuteReader();


                int rowC = 0;
                int colC = 0;

                while (reader4.Read())
                {
                    while (colC < reader4.FieldCount)
                    {
                        colC++;
                    }

                    rowC += 1;
                }
                //close the connection again cause we are going to re-execute reader.
                connect.con.Close();

                connect.con.Open();

                reader4 = commando.ExecuteReader();

                //store loan info in a new string array (loanResult).
                string[,] loanResult = new string[rowC, colC];


                int g = 0; int h = 0;
                while (reader4.Read())
                {
                    while (h < reader4.FieldCount)
                    {

                        if (reader4[h] != null)
                        {
                            loanResult[g, h] = reader4[h].ToString();
                        }
                        else
                        {
                            loanResult[g, h] = "";
                        }
                        h++;
                    }
                    h = 0;
                    g++;
                }

                //NOW START PRINTING INSIDE THE TEXTBLOCKS.

                StackPanel am = new StackPanel();
                am.Background = bg;
                TextBlock amount = new TextBlock();
                amount.Text = Loanresult[i, 3];//0
                //amount.Width = 200;
                amount.Height = 50;
                amount.Margin = new Thickness(0, 15, 0, 0);
                amount.FontSize = 20;
                amount.Foreground = fd;
                amount.TextAlignment = TextAlignment.Center;
                Grid.SetRow(am, i + 1);
                Grid.SetColumn(am, 7);
                elements.Children.Add(am);
                am.Children.Add(amount);


                StackPanel du = new StackPanel();
                du.Background = bg;
                TextBlock dur = new TextBlock();
                dur.Text = Loanresult[i, 5];
                //dur.Width = 200;
                dur.Height = 50;
                dur.Margin = new Thickness(0, 15, 0, 0);
                dur.FontSize = 20;
                dur.Foreground = fd;
                dur.TextAlignment = TextAlignment.Center;
                Grid.SetRow(du, i + 1);
                Grid.SetColumn(du, 8);
                elements.Children.Add(du);
                du.Children.Add(dur);


                //calling class filter from precious class.
                Filter F = new Filter();


                StackPanel be = new StackPanel();
                be.Background = bg;
                TextBlock begin = new TextBlock();
                begin.Text = F.Date(Loanresult[i, 6], "yyyy-MM-dd");
                //begin.Width = 200;
                begin.Height = 50;
                begin.Margin = new Thickness(0, 15, 0, 0);
                begin.FontSize = 20;
                begin.Foreground = fd;
                begin.TextAlignment = TextAlignment.Center;
                Grid.SetRow(be, i + 1);
                Grid.SetColumn(be, 9);
                elements.Children.Add(be);
                be.Children.Add(begin);


                StackPanel en = new StackPanel();
                en.Background = bg;
                TextBlock end = new TextBlock();
                end.Text = F.Date(Loanresult[i, 8], "yyyy-MM-dd");
                //end.Width = 200;
                end.Height = 50;
                end.Margin = new Thickness(15, 15, 0, 0);
                end.FontSize = 20;
                end.Foreground = fd;
                end.TextAlignment = TextAlignment.Center;
                Grid.SetRow(en, i + 1);
                Grid.SetColumn(en, 10);
                elements.Children.Add(en);
                en.Children.Add(end);


            
                StackPanel pa =new StackPanel();
                pa.Background = bg;

                TextBlock paid = new TextBlock();
                paid.Height = 50;
                paid.Foreground = fd;
                paid.Margin = new Thickness(15, 15, 0, 0);
                paid.FontSize = 20;
                if (Loanresult[i, 4].ToLower() == "false" || Loanresult[i, 4] == "" || Loanresult[i,4] == "0")
                {
                    paid.Text = "no";
                }
                else if (Loanresult[i, 4].ToLower() == "true" || Loanresult[i,4] == "1")
                {
                    paid.Text = "yes";
                }
               
                Grid.SetRow(pa, i +1);

                Grid.SetColumn(pa, 11);
                elements.Children.Add(pa);
                pa.Children.Add(paid);

                
                ///switching to the next table where we gonna be using hyperlinkbutton to edit
                ///

                StackPanel kj = new StackPanel();
                kj.Background = bg;
                HyperlinkButton[] hb = new HyperlinkButton[rowCount];
                hb[i] = new HyperlinkButton();
                hb[i].Content = "edit";
                hb[i].FontSize = 15;
                hb[i].Height = 50;
                hb[i].ClickMode = ClickMode.Release; 
                Grid.SetColumn(kj, 12);//hb[i]
                Grid.SetRow(kj, i+1 );
                elements.Children.Add(kj);
                kj.Children.Add(hb[i]);
              


                 //Deleting from the admin page.
        //first create the delete button.
        HyperlinkButton[] delB = new HyperlinkButton[rowCount];
        delB[i] = new HyperlinkButton();
        delB[i].Content = "delete";
        delB[i].FontSize = 15;
        delB[i].Height = 50;
        delB[i].Margin = new Thickness(50, 0, 15, 0);
        Grid.SetColumn(delB[i], 12);
        Grid.SetRow(delB[i], i + 1);
        elements.Children.Add(delB[i]);

           

                // a variablle red to decrement i cause its increasing automatically because of += in the event handler.
                //therefore,when i == rowcount i.e has finished looping subtract 1 from i
                 int red = i;
                if (i == rowCount)
                {
                    red = i - 1;
                }


                //EVENT HANDLER FOR DELETING FROM THE DATABASE.
        delB[i].Click += (s, e) =>
            {
                //quering the database.
                string d = "DELETE from loan WHERE loan_id = '" + Loanresult[red, 0] + "' ";//change i to red
                MySqlCommand delCmmd = new MySqlCommand(d, connect.con);
                connect.con.Close();

                connect.con.Open();
                MySqlDataReader delreader = delCmmd.ExecuteReader();

                try
                {
                    while (delreader.Read())
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageDialog le = new MessageDialog(ex.ToString());
                    le.ShowAsync();
                }

                MessageDialog ee = new MessageDialog("Deleted!");
                ee.ShowAsync();
            };



                

                //a variable dec to decrement i which is increasing automatically because of the event handler (+=)
                int dec = i;
                if (i == rowCount)
                {
                    dec = i - 1;
                }
                //EVENT HANDLER....handling the edit button 
                //it accutally opens the popup.
                hb[i].Click += (s, e) =>
                {
                    this.loanid = Loanresult[dec, 0];
                    if (!leggover.IsOpen)
                    {
                        leggover.IsOpen = true;
                    }

                };
            }
        }







        private void okBtn(object sender, RoutedEventArgs e)
        {

            //Y and N are variables to store yes or no options collected from the admin
            //hence, the if statement.
            string Y = "yes";
            string N = "no";
            ///variable val here makes paid.text an int value so it can go back to the database.
            int val = 0;
            if (cub.Text == Y || cub.Text == N)
            {

                if (cub.Text == Y)
                {
                    val = 1;
                }
                else
                {
                    val = 0;
                }
                connect.con.Close();
                //QUERING THE DATABASE.
                string p = "UPDATE loan SET paid = '" + val + "' WHERE loan_id= '" + this.loanid + "' ";
                MySqlCommand com = new MySqlCommand(p, connect.con);
                connect.con.Open();




                MySqlDataReader reader5 = com.ExecuteReader();
                try
                {
                    while (reader5.Read())
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageDialog t = new MessageDialog(ex.ToString());
                    t.ShowAsync();

                }

                MessageDialog z = new MessageDialog("Updated");
                z.ShowAsync();
                connect.con.Close();

            }

            else
            
            {
                MessageDialog bb = new MessageDialog("please input yes or no");
                bb.ShowAsync();
            } 

                this.Frame.Navigate(typeof(Admin));
            
        }







        private void backBtn(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(loginPage));
        }

        private void cancelBtn(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Admin));
        }


        }
    }

