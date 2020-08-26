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

            if(chicken.IsChecked.HasValue && chicken.IsChecked.Value)
            {
            object order = employee.NewRequest(quantityOfFood,new ChickenOrder(quantityOfFood));
            String result = employee.Inspect(order);
            orderToCook = order;
                    
            Results.Text += result + "\n";

            }
            else
            {
                EggOrder order = (EggOrder)employee.NewRequest(quantityOfFood, new EggOrder(quantityOfFood));
                String result = employee.Inspect(order);
                    //Results.Text = result;
                    orderToCook = order;
                quality.Text = result;
            }
            }catch(Exception ex)
            {
                Results.Text += ex.Message + "\n";
            }
        }   

        private void copyPrevReq_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
            object request = employee.CopyRequest();
                orderToCook = request;
                if(request is EggOrder eggOrder)
                {
                    // setting radioButtons
                    chicken.IsChecked = false ;
                    egg.IsChecked = true ;
                    quantity.Text = eggOrder.GetQuantity() + "";
                    quality.Text = employee.Inspect(eggOrder);


                }
                else
                {
                    chicken.IsChecked = true;
                    egg.IsChecked = false;
                    //settting quantity field
                    quality.Text = "quality";
                    quantity.Text = ((ChickenOrder) request).GetQuantity() + "";
                    Results.Text += employee.Inspect(request) +"\n";

                }
                
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

            }catch(Exception ex)
            {
                Results.Text += ex.Message + "\n";
            }

        }
    }
}
