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
        bool newPassenger;

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



        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndAddPass = new wndAddPassenger();

                wndAddPass.ShowDialog();
                if (planeControl.NewPassenger != null)
                {
                    selectedPlane.addPassenger(planeControl.NewPassenger);
                    newPassenger = true;
                    cbChoosePassenger.SelectedValue = planeControl.NewPassenger;
                    cbChoosePassenger.IsEnabled = false;
                    cbChooseFlight.IsEnabled = false;

                }
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
                   
                    if (control.Content.ToString() != (lblPassengersSeatNumber.Content.ToString()))
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
                    else
                    {
                        control.Background = Brushes.Green;
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
            string selectedNum = label.Content.ToString();
            bool res = int.TryParse(selectedNum, out int num);
            if (newPassenger)
            {
                if (label.Background == Brushes.Blue)
                {
                    planeControl.NewPassenger.SeatNumber = num;
                    planeControl.addFlightPassenger(selectedPlane.Id);
                    //cbChoosePassenger.ItemsSource = selectedPlane.Passengers;
                    newPassenger = false;
                    cbChoosePassenger.IsEnabled = true;
                    cbChooseFlight.IsEnabled = true;

                }
            }
            else
            {
                cbChoosePassenger.SelectedItem = selectedPlane.getPassenger(selectedNum);
            }

            lblPassengersSeatNumber.Content = label.Content.ToString();

            setSeatColors();

        }



        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = cbChoosePassenger.Items;
            if (cbChoosePassenger.SelectedItem != null)
            {

                if (((PassengerDetail)cbChoosePassenger.SelectedItem).SeatNumber != null)
                {
                    lblPassengersSeatNumber.Content = ((PassengerDetail)cbChoosePassenger.SelectedItem).SeatNumber;
                }
                else
                {
                    lblPassengersSeatNumber.Content = " ";
                }
                setSeatColors();
            }


        }
    }
}
