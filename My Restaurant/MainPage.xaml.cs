using System;
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
        private Employee employee;
        private object orderToCook;
        public MainPage()
        {
            this.InitializeComponent();
            employee = new Employee();
        }

        private void submitReqButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int quantityOfFood = int.Parse(quantity.Text);

                if (chicken.IsChecked.HasValue && chicken.IsChecked.Value)                
                    orderToCook = employee.NewRequest(quantityOfFood, "Chicken");                
                else                
                    orderToCook = employee.NewRequest(quantityOfFood, "Egg");              

                string result = employee.Inspect(orderToCook);
                if (orderToCook is EggOrder)               
                    quality.Text = result;//for egg result of inspetion goes to quality TextBox                
                else               
                    Results.Text += result + "\n";               
            }
            catch (Exception ex)
            {
                Results.Text += ex.Message + "\n";
            }
        }
        private void copyPrevReq_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                orderToCook = employee.CopyRequest();
                string result = employee.Inspect(orderToCook);
                if (orderToCook is EggOrder)                
                    quality.Text = result;             
                else
                    Results.Text += result + "\n";
            }
            catch (Exception ex)
            {
                Results.Text += ex.Message + "\n";
            }
        }
        private void prepareFoodButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string result = employee.PrepareFood(orderToCook);
                Results.Text += result + "\n";
            }
            catch (Exception ex)
            {
                Results.Text += ex.Message + "\n";
            }
        }
    }
}
