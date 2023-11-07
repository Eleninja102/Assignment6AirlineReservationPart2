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
        /// <summary>
        /// The Add passenger window object
        /// </summary>
        private wndAddPassenger wndAddPass;
        /// <summary>
        /// The plane the user has selected
        /// </summary>
        private PlaneDetail selectedPlane;
        /// <summary>
        /// The passenger the user has selected
        /// </summary>
        private PassengerDetail selectedPassenger;
        /// <summary>
        /// Whether a passenger is being edit (new seat or changing seat)
        /// </summary>
        private bool editPassenger;

        /// <summary>
        /// When window starts it loads database and sets the starting data and content
        /// </summary>
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

        /// <summary>
        /// When the flight is changed it enables the rest of the screen and loads the passenger items with the correct list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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







        /// <summary>
        /// Selects the canvas depending on the selected plane. Then it updates the colors of the seats
        /// </summary>
        /// <exception cref="Exception"></exception>
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


        /// <summary>
        /// When a label is selected from the canvas it checks if a passenger is being edited. If it is it sets the new seat number.
        /// Otherwise it shows the passenger selected on the combo box or the null value
        /// If it is null it has to run the setSeatColors to have them turn green
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void seat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Label labelSender = sender as Label;
                string selectedNum = labelSender.Content.ToString();
                bool res = int.TryParse(selectedNum, out int num);
                if (editPassenger)
                {
                    if (selectedPlane.setSeatNumber(selectedPassenger, num))
                    {
                        cbChoosePassenger.IsEnabled = true;
                        cbChooseFlight.IsEnabled = true;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    cbChoosePassenger.SelectedItem = selectedPlane.getPassenger(selectedNum);
                }
                lblPassengersSeatNumber.Content = selectedNum;


                if (selectedPassenger == null || editPassenger)
                {
                    setSeatColors();
                }
                editPassenger = false;


            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// When a passenger is selected from the combo-box or if it changes during seat selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
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
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// Opens the add passenger popup and as long as the user clicks save starts saving the passenger. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndAddPass = new wndAddPassenger();

                wndAddPass.ShowDialog();
                if (wndAddPass.NewPassenger)
                {
                    cbChoosePassenger.SelectedValue = selectedPlane.addPassenger(wndAddPass.txtFirstName.Text, wndAddPass.txtLastName.Text);
                    editPassenger = true;
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
        /// <summary>
        /// When the delete passenger is selected as long as a passenger is also selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (selectedPassenger != null)
                {
                    lblPassengersSeatNumber.Content = " ";
                    selectedPlane.deletePassenger(selectedPassenger);
                    setSeatColors();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// When change seat is pressed what happens to the application; including waiting for a seat to be selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (selectedPassenger != null)
                {
                    selectedPassenger.SeatNumber = null;
                    lblPassengersSeatNumber.Content = " ";
                    setSeatColors();
                    cbChoosePassenger.IsEnabled = false;
                    cbChooseFlight.IsEnabled = false;
                    editPassenger = true;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }



        /// <summary>
        /// exception handler that shows the error
        /// </summary>
        /// <param name="sClass">the class</param>
        /// <param name="sMethod">the method</param>
        /// <param name="sMessage">the error message</param>
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
