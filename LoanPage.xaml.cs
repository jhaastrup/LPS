using MySql.Data.MySqlClient;
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
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LPS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoanPage : Page
    {
        public LoanPage() 

        {
          
            this.InitializeComponent();
        }

        public void vechLoanBtn(object sender, RoutedEventArgs e)
        {
            //varible load_id  to store the loan_token so we can move loans from page to page.
            int loan_id = 1;

           
            (App.Current as App).loan_token = loan_id.ToString();

            this.Frame.Navigate(typeof(VechicleLoan));
        }
        

        private void HouseLoanBtn(object sender, RoutedEventArgs e)
        {
            int loan_id = 2;

           

            (App.Current as App).loan_token = loan_id.ToString();

            this.Frame.Navigate(typeof(VechicleLoan));
        }

        private void personalLoanBtn(object sender, RoutedEventArgs e)
        {
            int loan_id = 3;

           

            (App.Current as App).loan_token = loan_id.ToString();

            this.Frame.Navigate(typeof(VechicleLoan));
        }

        private void EducationLoanBtn(object sender, RoutedEventArgs e)
        {
            int loan_id = 4;

         

            (App.Current as App).loan_token = loan_id.ToString();

            this.Frame.Navigate(typeof(VechicleLoan));

        }
    }
}
