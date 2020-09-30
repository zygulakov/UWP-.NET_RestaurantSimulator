using App.My_Restaurant.Food;
using App.My_Restaurant.Employees;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using App.My_Restaurant.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace My_Restaurant
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private EmployeeServer employeeServer;
        private EmployeeCook employeeCook1;
        private EmployeeCook employeeCook2;
        private List<string> resutlsOfCooking;



        public MainPage()
        {
            this.InitializeComponent();
            employeeServer = new EmployeeServer();
            employeeCook1 = new EmployeeCook();
            employeeCook2 = new EmployeeCook();


            DrinksList.Items.Add(new Tea(1));
            DrinksList.Items.Add(new CocaCola(1));
            DrinksList.Items.Add(new Pepsi(1));
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

                string result = default;
                lock (employeeServer)
                {
                       result = employeeServer.RecieveRequest(eggQuantitiy, chickenQuantity, drink, name);
                }

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

        private async void SendAllCustomerReqToCook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TableRequests reqTable = employeeServer.tableOfRequests;
                Results.Text += "Cooking ....."+ "\n";
                string resultFromCook1 = await employeeCook1.ProcessAsync(reqTable);
                Results.Text += $"Chef 1 {resultFromCook1}  \n";
                string resultFromCook2 = await employeeCook2.ProcessAsync(reqTable);
                Results.Text += $"Chef 2 { resultFromCook2}  \n";
                resutlsOfCooking = await employeeServer.ServeAsync();
                Results.Text += "Ready to Serve" + "\n";
                

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
                if (resutlsOfCooking == null)
                    throw new Exception("No food to serve , please order...");
                foreach (string result in resutlsOfCooking)
                {
                    Results.Text += result + "\n";
                }
                resutlsOfCooking = null;

            }
            catch (Exception ex)
            {
                Results.Text += ex.Message + "\n";
            }
        }
    }
}
