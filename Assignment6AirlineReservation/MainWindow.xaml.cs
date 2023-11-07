using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        wndAddPassenger wndAddPass;
        PlaneDetail selectedPlane;
        PassengerDetail selectedPassenger;
        bool editPassenger;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;


                planeControl.setDatabase();
                cbChooseFlight.ItemsSource = planeControl.Planes;

                CanvasA380.Visibility = Visibility.Hidden;
                Canvas767.Visibility = Visibility.Hidden;

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
                lblPassengersSeatNumber.Content = "";

                if (selectedPlane.Id == 1)
                {
                    CanvasA380.Visibility = Visibility.Hidden;
                    Canvas767.Visibility = Visibility.Visible;

                }
                else
                {
                    Canvas767.Visibility = Visibility.Hidden;
                    CanvasA380.Visibility = Visibility.Visible;
                }
                setSeatColors();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }








        private void setSeatColors()
        {

            try
            {
                Canvas selectedCanvas;
                if (selectedPlane.Id == 1)
                {
                    selectedCanvas = Canvas767.Children.OfType<Canvas>().FirstOrDefault();
                }
                else
                {
                    selectedCanvas = CanvasA380.Children.OfType<Canvas>().FirstOrDefault();
                }
                foreach (Label control in selectedCanvas.Children)
                {
                    if (control.Content.ToString() == lblPassengersSeatNumber.Content.ToString())
                    {
                        control.Background = Brushes.LimeGreen;
                    }
                    else
                    {
                        foreach (PassengerDetail passenger in selectedPlane.Passengers)
                        {
                            if (passenger.SeatNumber != null)
                            {
                                if (passenger.SeatNumber.ToString() == control.Content.ToString())
                                {
                                    control.Background = Brushes.Red;
                                    break;
                                }
                                else
                                {
                                    control.Background = Brushes.Blue;
                                }
                            }
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }



        private void seat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label labelSender = sender as Label;
            string selectedNum = labelSender.Content.ToString();
            lblPassengersSeatNumber.Content = selectedNum;
            bool res = int.TryParse(selectedNum, out int num);
            if (editPassenger)
            {
                if (selectedPlane.setSeatNumber(selectedPassenger, num))
                {
                    editPassenger = false;
                    cbChoosePassenger.IsEnabled = true;
                    cbChooseFlight.IsEnabled = true;
                }
            }
            else
            {
                cbChoosePassenger.SelectedItem = selectedPlane.getPassenger(selectedNum);
            }


            setSeatColors();

        }



        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPassenger = (PassengerDetail)cbChoosePassenger.SelectedItem;

            if (selectedPassenger != null)
            {

                if (selectedPassenger.SeatNumber != null)
                {
                    lblPassengersSeatNumber.Content = selectedPassenger.SeatNumber;
                }
                else
                {
                    lblPassengersSeatNumber.Content = " ";
                }
                setSeatColors();
            }


        }
        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndAddPass = new wndAddPassenger();

                wndAddPass.ShowDialog();
                cbChoosePassenger.SelectedValue = selectedPlane.addPassenger(wndAddPass.txtFirstName.Text, wndAddPass.txtLastName.Text);
                editPassenger = true;
                cbChoosePassenger.IsEnabled = false;
                cbChooseFlight.IsEnabled = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPassenger != null)
            {
                selectedPlane.deletePassenger(selectedPassenger);
            }
        }

        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPassenger != null)
            {
                selectedPassenger.SeatNumber = null;
                lblPassengersSeatNumber.Content = " ";

                cbChoosePassenger.IsEnabled = false;
                cbChooseFlight.IsEnabled = false;
                editPassenger = true;
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
    }
}
