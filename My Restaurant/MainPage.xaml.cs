using App.My_Restaurant.Food;
using App.My_Restaurant.Employees;
using System;
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
        private List<EmployeeCook> employeeCooks;
        private List<Task> cookRes;
        private bool anythingToCook;
        public MainPage()
        {
            this.InitializeComponent();
            employeeServer = new EmployeeServer();
            employeeCooks = new List<EmployeeCook>() { new EmployeeCook(), new EmployeeCook() };
            cookRes = new List<Task>();
            DrinksList.Items.Add(typeof(Tea).Name);
            DrinksList.Items.Add(typeof(CocaCola).Name);
            DrinksList.Items.Add(typeof(Pepsi).Name);
            DrinksList.Items.Add(typeof(NoDrink).Name);
            DrinksList.SelectedItem = DrinksList.Items[0];
        }

        private void submitReqButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int eggQuantitiy = int.Parse(amountOfEgg.Text);
                int chickenQuantity = int.Parse(amountOfChicken.Text);
                string drink = (string)DrinksList.SelectedItem;
                string name = CustomerName.Text;
                if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                    throw new Exception("please enter valid name");

                string result = string.Empty;
                lock (employeeServer)
                {
                    result = employeeServer.RecieveRequest(eggQuantitiy, chickenQuantity, drink, name);
                }

                Results.Text += result + "\n";
                quality.Text = Egg.Quality + "";
                anythingToCook = true;
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
               if (!anythingToCook)
                    throw new Exception("already served");
                
                anythingToCook = false;
                TableRequests reqTable = employeeServer.tableOfRequests;
                Results.Text += "Sent to Cook!" + "\n";
                //TODO: You are giving one tableRequest to 2 cooks. Only one who are free should take the table request to process. ***
                employeeCooks.ForEach(emp =>
                {
                    if (emp.IsAvailable)
                        cookRes.Add(emp.ProcessAsync(reqTable));
                });

                await Task.WhenAll(cookRes);
                Results.Text += "Cooked ....." + "\n";

                Results.Text += "Getting ready to serve ....." + "\n";
                List<string> resutlsOfCooking = await employeeServer.ServeAsync();
                Results.Text += "Serving" + "\n";
                resutlsOfCooking.ForEach(result => Results.Text += result + "\n");
                cookRes.Clear();
            }
            catch (Exception ex)
            {
                Results.Text += ex.Message + "\n";
            }
        }

        //TODO: For the project #5 this ServePreparedFoodToTheCustomer button should be deleted from UI. Please review the design again.***

    }
}
