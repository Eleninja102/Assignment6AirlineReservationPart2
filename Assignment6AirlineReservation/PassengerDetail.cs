using System;
using System.Reflection;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// The details of a given passengers
    /// </summary>
    class PassengerDetail
    {
        /// <summary>
        /// The passengerId of the passengers from the database
        /// </summary>
        private readonly int passengerId;
        /// <summary>
        /// The first name of the passengers
        /// </summary>
        private readonly string firstName;
        /// <summary>
        /// The last name of the passengers
        /// </summary>
        private readonly string lastName;
        /// <summary>
        /// If the passengers has a seat or not and what the seat number is
        /// </summary>
        private int? seatNumber;

        /// <summary>
        /// A generic constructor that takes all parameters and defines them directly
        /// </summary>
        /// <param name="id">A unique identifier</param>
        /// <param name="firstName">The first name of the passengers</param>
        /// <param name="lastName">The last name of the passengers</param>
        /// <param name="seatNumber">Optionally the seat number of the passengers</param>
        /// <exception cref="Exception"></exception>
        public PassengerDetail(int passengerId, string firstName, string lastName, int? seatNumber = null)
        {

            try
            {
                this.passengerId = passengerId;
                this.firstName = firstName;
                this.lastName = lastName;
                this.seatNumber = seatNumber;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }
        /// <summary>
        /// Returns the seat number of the passengers. Or sets the seat to any given number.
        /// </summary>
        public int? SeatNumber
        {
            get
            {
                try
                {
                    return seatNumber;
                }
                catch (Exception ex)
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

                }
            }
            set
            {
                try
                {
                    seatNumber = value;
                }
                catch (Exception ex)
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

                }
            }
        }

        /// <summary>
        /// Returns the passengerId of the passenger
        /// </summary>
        public int Id
        {
            get
            {

                try
                {
                    return passengerId;
                }
                catch (Exception ex)
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

                }
            }
        }

        /// <summary>
        /// Overrides the ToString method to get the name. Used in the combo-box
        /// </summary>
        /// <returns>"firstName lastName" format string</returns>
        /// <exception cref="Exception"></exception>
        public override string ToString()
        {
            try
            {
                return firstName + " " + lastName;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        
    }
}
