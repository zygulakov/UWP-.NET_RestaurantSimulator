using App.My_Restaurant.Food;
using App.My_Restaurant.Employees;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using App.My_Restaurant.Table;
using System.Collections.Generic;

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
            DrinksList.Items.Add(new Tea());
            DrinksList.Items.Add(new CocaCola());
            DrinksList.Items.Add(new Pepsi());
            DrinksList.Items.Add(new NoDrink());
            DrinksList.SelectedItem = DrinksList.Items[0];

        }

        private void submitReqButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int eggQuantitiy = int.Parse(amountOfEgg.Text);
                int chickenQuantity = int.Parse(amountOfChicken.Text);
                Drink drink = (Drink)DrinksList.SelectedItem;
                string name = CustomerName.Text;

                string result = employeeServer.RecieveRequest(eggQuantitiy, chickenQuantity, drink, name);

                Results.Text += result + "\n";
                quality.Text = Egg.Quality + "";


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
            //TODO: Subscribing part should go to the MainPage method. You don't need to subscriber to the events each time when sending requests, but it should be done only once.
            try
            {
                // subscribing Servers's ready event
                employeeServer.Ready += employeeCook.Process;
                //subscribing Cook's ProcessedEvent event
                employeeCook.Processed += (() => employeeServer.CanServe()); //TODO: server.Serve() should be subscribed
                string resultOfCook = employeeServer.NotifyToCook();
                Results.Text += resultOfCook + "\n";
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
                List<string> resultOfServing = employeeServer.Serve();
                foreach (string result in resultOfServing)
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
