﻿using App.My_Restaurant.Food;
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
        private bool anythingToCook;

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
                TableRequests reqTable = employeeServer.tableOfRequests;
                Results.Text += "Cooking ....." + "\n";
                //TODO: You are giving one tableRequest to 2 cooks. Only one who are free should take the table request to process. 
                Task<string> t = employeeCook1.ProcessAsync(reqTable);
                Task<string> t2 = employeeCook2.ProcessAsync(reqTable);
                await Task.WhenAll(t, t2);
                Results.Text += "Cooked ....." + "\n";

                Results.Text += "Getting ready to serve ....." + "\n";
                resutlsOfCooking = await employeeServer.ServeAsync();
                Results.Text += "Ready to Serve" + "\n";
                anythingToCook = false;
            }
            catch (Exception ex)
            {
                Results.Text += ex.Message + "\n";
            }
        }

        //TODO: For the project #5 this ServePreparedFoodToTheCustomer button should be deleted from UI. Please review the design again.
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
