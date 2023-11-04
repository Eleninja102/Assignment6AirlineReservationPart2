using System;
using System.Reflection;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// The details of a given passenger
    /// </summary>
    class PassengerDetail
    {
        /// <summary>
        /// The id of the passenger from the database
        /// </summary>
        private int id;
        /// <summary>
        /// The first name of the passenger
        /// </summary>
        private readonly string firstName;
        /// <summary>
        /// The last name of the passenger
        /// </summary>
        private readonly string lastName;
        /// <summary>
        /// If the passenger has a seat or not and what the seat number is
        /// </summary>
        private readonly int? seatNumber;

        /// <summary>
        /// A generic constructor that takes all parameters and defines them directly
        /// </summary>
        /// <param name="id">A unique identifier</param>
        /// <param name="firstName">The first name of the passenger</param>
        /// <param name="lastName">The last name of the passenger</param>
        /// <param name="seatNumber">Optionally the seat number of the passenger</param>
        /// <exception cref="Exception"></exception>
        public PassengerDetail(int id, string firstName, string lastName, int? seatNumber = null)
        {

            try
            {
                this.id = id;
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
        /// Returns the seat number of the passenger
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
        }

        /// <summary>
        /// Overrides the ToString method to get the name
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
