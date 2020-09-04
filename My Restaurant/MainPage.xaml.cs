using System;
using Windows.Security.Cryptography.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace My_Restaurant
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private EmployeeServer employeeServer;
        private EmployeeCook employeeCook;
        public MainPage()
        {
            this.InitializeComponent();
            employeeServer = new EmployeeServer();
            employeeCook = new EmployeeCook();
            foreach (MenuItemBeverage drink in Enum.GetValues(typeof(MenuItemBeverage)))
                DrinksList.Items.Add(drink);

        }

        private void submitReqButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int eggQuantitiy = int.Parse(amountOfEgg.Text);
                int chickenQuantity = int.Parse(amountOfChicken.Text);
                MenuItemBeverage drink =(MenuItemBeverage) DrinksList.SelectedItem;
                
                string result = employeeServer.RecieveRequest(eggQuantitiy, chickenQuantity, drink);
                Results.Text += result + "\n";
            }
            catch (FormatException)
            {
                Results.Text += "please enter valid amount" + "\n";
            }
            catch (NullReferenceException)
            {
                Results.Text += "plese select a drink" + "\n";
            }
            catch (Exception ex)
            {
                Results.Text += ex.Message + "\n";
            }

        }

        private void SendAllCustomerReqToCook_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string result = employeeServer.SendReqToCook(employeeCook);
                int? qualityOfEgg = employeeCook.GetEggQuality();
                quality.Text = qualityOfEgg+"";
                Results.Text += result + "\n";
            }
            catch (Exception ex)
            {
                Results.Text += ex.Message + "\n";
            }
        }

        private void ServePreparedFoodToTheCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] resultsOfServing = employeeServer.Serve();
                foreach (string result in resultsOfServing)
                {
                    Results.Text += result + "\n";
                }

            }
            catch (Exception ex)
            {

                Results.Text += ex.Message + "\n"; 
            }
        }
    }
}
