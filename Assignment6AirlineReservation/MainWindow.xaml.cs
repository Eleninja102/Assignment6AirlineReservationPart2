using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        wndAddPassenger wndAddPass;
        PlaneDetail selectedPlane;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

              
                planeControl.setDatabase();
                cbChooseFlight.ItemsSource = planeControl.Planes;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cbChoosePassenger.IsEnabled = true;
                gPassengerCommands.IsEnabled = true;
                selectedPlane = (PlaneDetail)cbChooseFlight.SelectedItem;

                cbChoosePassenger.ItemsSource = selectedPlane.Passengers;

                if (selectedPlane.Id == 1)
                {
                    CanvasA380.Visibility = Visibility.Hidden;
                    Canvas767.Visibility = Visibility.Visible;
                    setSeatColors(Canvas767.Children.OfType<Canvas>().FirstOrDefault());
                    
                       
                }
                else
                {
                    Canvas767.Visibility = Visibility.Hidden;
                    CanvasA380.Visibility = Visibility.Visible;
                    setSeatColors(CanvasA380.Children.OfType<Canvas>().FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }



        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndAddPass = new wndAddPassenger();
                wndAddPass.ShowDialog();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }




        private void setSeatColors(Canvas canvasObject)
        {

            try
            {
                foreach (Label control in canvasObject.Children)
                {

                    string color = selectedPlane.getSeatColor(control.Content.ToString());
                    if (color == "blue")
                    {
                        control.Background = Brushes.Blue;
                    }
                    else if (color == "red")
                    {
                        control.Background = Brushes.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        private void seat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            Label label = sender as Label;

            label.Content.ToString();

            selectedPlane.ge


        }

        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
