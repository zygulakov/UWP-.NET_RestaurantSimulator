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
        private List<EmployeeCook> employeeCookList;
        private List<TableRequests> tableOfReqsList;
        private List<Task> cooksResultTasks;
        private bool anythingToCook;
        private readonly int MAX_CUSTOMER_PER_TABLE = 8;
        public MainPage()
        {
            this.InitializeComponent();
            
            employeeServer = new EmployeeServer(MAX_CUSTOMER_PER_TABLE);
            employeeCookList = new List<EmployeeCook>() { new EmployeeCook(), new EmployeeCook() };
            cooksResultTasks = new List<Task>();
            tableOfReqsList = new List<TableRequests>();
            employeeServer.TableOfRequests = new TableRequests();
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
                //adding table to list of waiting tables to be cooked
                tableOfReqsList.Add(employeeServer.TableOfRequests);
                //giving server new table for new reqs
                employeeServer.TableOfRequests = new TableRequests();
                
                Results.Text += "Sent to Cook!" + "\n";
                employeeCookList.ForEach(emp =>
                {
                    //cooking all waiting requestTables
                    if (emp.IsAvailable)
                        tableOfReqsList.ForEach(table =>
                        {
                            Task<string> cookTask = emp.ProcessAsync(table);
                            cooksResultTasks.Add(cookTask);
                        });
                });

                await Task.WhenAll(cooksResultTasks);

                Results.Text += "Cooked ....." + "\n";
                Results.Text += "Getting ready to serve ....." + "\n";
                //serving all requestTable list
                tableOfReqsList.ForEach(async table => { 
                List<string> resutlsOfCooking = await employeeServer.ServeAsync(table);
                Results.Text += "Serving" + "\n";
                resutlsOfCooking.ForEach(result => Results.Text += result + "\n");
                });
                cooksResultTasks.Clear();
                tableOfReqsList.Clear();
            }
            catch (Exception ex)
            {
                Results.Text += ex.Message + "\n";
            }
        }
    }
}
