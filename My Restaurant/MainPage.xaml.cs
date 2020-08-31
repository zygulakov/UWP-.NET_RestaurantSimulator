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
        private string[] resultToServe;
        public MainPage()
        {
            this.InitializeComponent();
            employeeServer = new EmployeeServer();
            
        }

        private void submitReqButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                int eggQuantitiy = int.Parse(amountOfEgg.Text);
                int chickenQuantity = int.Parse(amountOfChicken.Text);
                string drinkAsString = ((ComboBoxItem)DrinksList.SelectedItem).Content.ToString();
                MenuItem drink = (MenuItem)Enum.Parse(typeof(MenuItem), drinkAsString);
                string result = employeeServer.RecieveRequest(eggQuantitiy, chickenQuantity, drink);
                Results.Text += result + "\n";
            }
            catch (FormatException )
            {
                Results.Text += "please enter valid amount" + "\n";
            }
            catch (NullReferenceException )
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
               resultToServe = employeeServer.SendReqToCook();
                Results.Text += "Cooking ... \n" +
                    "ready! \n";
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
            foreach (string str in resultToServe)
            {
                Results.Text += str + "\n";
            }

            }
            catch (NullReferenceException ex)
            {

                Results.Text += "Nothing to Serve! \n";
            }
            resultToServe = null;
        }
    }
}
