using System;
using System.ComponentModel;
using System.Reflection;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// The details of the plane and all the passengers on the plane
    /// </summary>
    class PlaneDetail
    {
        /// <summary>
        /// The planeId of the plane from the database
        /// </summary>
        private readonly int planeId;
        /// <summary>
        /// The flight newSeatNumber from the database 
        /// </summary>
        private readonly int flightNumber;
        /// <summary>
        /// The name of the plane
        /// </summary>
        private readonly string name;
        /// <summary>
        /// The details of the passengers on the plane type passengerDetail
        /// </summary>
        private readonly BindingList<PassengerDetail> passengers = new BindingList<PassengerDetail>();


        /// <summary>
        /// Sets all the given items to there respective items
        /// </summary>
        /// <param name="id">The unique planeId of the plane d</param>
        /// <param name="flightNumber">The flight newSeatNumber as an int</param>
        /// <param name="name">The name of the plane</param>
        /// <exception cref="Exception"></exception>
        public PlaneDetail(int planeId, int flightNumber, string name)
        {

            try
            {
                this.planeId = planeId;
                this.flightNumber = flightNumber;
                this.name = name;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        /// <summary>
        /// Get the plane passengerId of the respective plane
        /// </summary>
        public int Id
        {
            get
            {
                try
                {
                    return planeId;

                }
                catch (Exception ex)
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

                }
            }
        }

        /// <summary>
        /// Gets the list of passengers as a bindingList meaning whenever they update it updates
        /// </summary>
        public BindingList<PassengerDetail> Passengers
        {
            get
            {

                try
                {
                    return passengers;
                }
                catch (Exception ex)
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

                }
            }
        }

        /// <summary>
        /// Adds a passenger that does not exist in the database to the database and string
        /// </summary>
        /// <param name="firstName">The first-name of passenger</param>
        /// <param name="lastName">The last-name of passenger</param>
        /// <returns>The newly added passenger</returns>
        /// <exception cref="Exception"></exception>
        public PassengerDetail addPassenger(string firstName, string lastName)
        {
            try
            {
                PassengerDetail x = planeControl.addPassenger(firstName, lastName);
                passengers.Add(x);
                return x;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        /// <summary>
        /// Adds the passengers from the passengers data and adds them to the flight
        /// </summary>
        /// <param name="passengerDetail"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool addPassenger(PassengerDetail passengerDetail)
        {
            try
            {
                passengers.Add(passengerDetail);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        /// <summary>
        /// Gets the flight newSeatNumber and name overrides ToString method used in combo-box
        /// </summary>
        /// <returns>flightNumber - name</returns>
        /// <exception cref="Exception"></exception>
        public override string ToString()
        {
            try
            {
                return flightNumber + " - " + name;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="seatNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public PassengerDetail getPassenger(string seatNumber)
        {

            try
            {
                foreach (PassengerDetail passenger in passengers)
                {
                    if (passenger.SeatNumber.ToString() == seatNumber)
                    {
                        return passenger;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }


        /// <summary>
        /// Deletes the passenger from the database and list. 
        /// </summary>
        /// <param name="deletingPassenger">The passenger you want deleted</param>
        public void deletePassenger(PassengerDetail deletingPassenger)
        {


            try
            {
                passengers.Remove(deletingPassenger);

                planeControl.deletePassenger(planeId, deletingPassenger.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }


        /// <summary>
        /// Takes a given passenger and sets the seat-number to that. Additionally if the user already has a seat the old one is replaced. 
        /// </summary>
        /// <param name="changePassenger">The passenger that should be changed</param>
        /// <param name="newSeatNumber">The new seat number that the passenger should have</param>
        /// <returns>Whether true if the passenger was added. False if the seat was already taken</returns>
        public bool setSeatNumber(PassengerDetail changePassenger, int newSeatNumber)
        {

            try
            {
                foreach (PassengerDetail passenger in passengers)
                {
                    if (passenger.SeatNumber == newSeatNumber)
                    {
                        return false;
                    }
                }
                changePassenger.SeatNumber = newSeatNumber;

                planeControl.changeSeatPassenger(planeId, newSeatNumber, changePassenger.Id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

    }
}
