using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// The deatils of the plane and all the passengers on the plane
    /// </summary>
    class PlaneDetail
    {
        /// <summary>
        /// The id of the plane from the database
        /// </summary>
        private int id;
        /// <summary>
        /// The flight number from the database 
        /// </summary>
        private int flightNumber;
        /// <summary>
        /// The name of the plane
        /// </summary>
        private string name;
        /// <summary>
        /// The details of the passengers on the plane type passengerDetail
        /// </summary>
        private BindingList<PassengerDetail> passengers = new BindingList<PassengerDetail>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The unique id of the plane d</param>
        /// <param name="flightNumber">The flight number as an int</param>
        /// <param name="name">The name of the plane</param>
        /// <exception cref="Exception"></exception>
        public PlaneDetail(int id, int flightNumber, string name)
        {

            try
            {
                this.id = id;
                this.flightNumber = flightNumber;
                this.name = name;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        public int Id{ 
            get 
            { 
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public BindingList<PassengerDetail> Passengers
        {
            get
            {
                return passengers;
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
        /// Gets the flight number and name overrides ToString method
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
        /// The name of the plane
        /// </summary>
        /// <returns>name</returns>
        /// <exception cref="Exception"></exception>
        public string getPlaneName()
        {
            try
            {
                return name;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }
       
        public PassengerDetail getPassenger(string seatNumber)
        {

            try
            {
                foreach(PassengerDetail passenger in passengers){
                    if(passenger.SeatNumber.ToString() == seatNumber)
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
        /// Get the seat number of the passengers based on name
        /// </summary>
        /// <param name="passengerName">The name of the given passengers</param>
        /// <returns>The respective seat number as int else null</returns>
        /// <exception cref="Exception"></exception>
        public int? getSeatNumber(string passengerName)
        {

            try
            {
                foreach (PassengerDetail passenger in passengers)
                {
                    if (passenger.ToString() == passengerName)
                    {
                        return passenger.SeatNumber;
                    }

                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }
        
    }
}
