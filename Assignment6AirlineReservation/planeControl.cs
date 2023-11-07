using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace Assignment6AirlineReservation
{

    /// <summary>
    /// A static method that holds the lists and helps control everything
    /// </summary>
    static class planeControl
    {

        /// <summary>
        /// The BingingList of planes as a PlaneDetail
        /// </summary>
        private static BindingList<PlaneDetail> planes = new BindingList<PlaneDetail>();

        /// <summary>
        /// Adds the sql info to the planes list and adds all the passengers to the list.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void setDatabase()
        {

            try
            {
                DataSet dsPlane = new DataSet();
                DataSet dsPassenger = new DataSet();
                int iRetPlane = 0;   //Number of return values
                int iRetPassenger = 0;   //Number of return values
                string sSQL = "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";
                dsPlane = clsDataAccess.ExecuteSQLStatement(sSQL, ref iRetPlane);

                for (int plane = 0; plane < iRetPlane; plane++)
                {
                    bool res;
                    int id = (int)dsPlane.Tables[0].Rows[plane]["Flight_ID"];
                    res = int.TryParse(dsPlane.Tables[0].Rows[plane]["Flight_Number"].ToString(), out int flightNumber);
                    string flightName = (string)dsPlane.Tables[0].Rows[plane]["Aircraft_Type"];
                    planes.Add(new PlaneDetail(id, flightNumber, flightName));
                    sSQL = "SELECT PASSENGER.Passenger_ID, First_Name, Last_Name, Seat_Number " +
                  "FROM FLIGHT_PASSENGER_LINK, FLIGHT, PASSENGER " +
              "WHERE FLIGHT.FLIGHT_ID = FLIGHT_PASSENGER_LINK.FLIGHT_ID AND " +
              "FLIGHT_PASSENGER_LINK.PASSENGER_ID = PASSENGER.PASSENGER_ID AND " +
              "FLIGHT.FLIGHT_ID =" + id;
                    dsPassenger = clsDataAccess.ExecuteSQLStatement(sSQL, ref iRetPassenger);
                    for (int passenger = 0; passenger < iRetPassenger; passenger++)
                    {
                        int passengerId = (int)dsPassenger.Tables[0].Rows[passenger]["Passenger_ID"];
                        string firstName = (string)dsPassenger.Tables[0].Rows[passenger]["First_Name"];
                        string lastName = (string)dsPassenger.Tables[0].Rows[passenger]["Last_Name"];
                        res = int.TryParse(dsPassenger.Tables[0].Rows[passenger]["Seat_Number"].ToString(), out int seatNumber);
                        planes[plane].addPassenger(new PassengerDetail(passengerId, firstName, lastName, seatNumber));
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        /// <summary>
        /// Returns the list of plane objects as a bindingList. Technically could be a list as we never add planes but works
        /// </summary>
        public static BindingList<PlaneDetail> Planes
        {
            get
            {

                try
                {
                    return planes;
                }
                catch (Exception ex)
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

                }
            }
        }

        /// <summary>
        /// Adds the passenger to the given database
        /// </summary>
        /// <param name="FirstName">The first name</param>
        /// <param name="LastName">The last name</param>
        /// <returns>A passenger object with the newly given Id</returns>
        /// <exception cref="Exception"></exception>
        public static PassengerDetail addPassenger(string FirstName, string LastName)
        {

            try
            {
                string sSQL = $"INSERT INTO PASSENGER(First_Name, Last_Name) VALUES('{FirstName}','{LastName}')";
                clsDataAccess.ExecuteNonQuery(sSQL);
                sSQL = $"SELECT Passenger_ID from Passenger where First_Name = '{FirstName}' AND Last_Name = '{LastName}'";
                string id = clsDataAccess.ExecuteScalarSQL(sSQL);
                bool res = int.TryParse(id, out int passengerId);
                return new PassengerDetail(passengerId, FirstName, LastName);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        /// <summary>
        /// Deletes the passenger
        /// </summary>
        /// <param name="flightId">The flightId the passenger is on</param>
        /// <param name="passengerId">The passengerId of the flight</param>
        /// <exception cref="Exception"></exception>
        public static void deletePassenger(int flightId, int passengerId)
        {

            try
            {
                deletePassengerLink(flightId, passengerId);

                string sSQL = $"Delete FROM PASSENGER WHERE PASSENGER_ID = {passengerId}";
                clsDataAccess.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }

        }
        /// <summary>
        /// Deletes the link between a passenger and a flight needs to be run before deleting a passenger
        /// </summary>
        /// <param name="flightId">The flightId of the given flight</param>
        /// <param name="passengerId">The passengerId of the passenger</param>
        /// <exception cref="Exception"></exception>
        public static void deletePassengerLink(int flightId, int passengerId)
        {

            try
            {
                string sSQL = "Delete FROM FLIGHT_PASSENGER_LINK " + $"WHERE FLIGHT_ID = {flightId} AND " + $"PASSENGER_ID = {passengerId}";

                clsDataAccess.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        /// <summary>
        /// Given a the data it deletes the old connection and creates a new one
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="seatNumber"></param>
        /// <param name="passengerId"></param>
        /// <exception cref="Exception"></exception>
        public static void changeSeatPassenger(int flightId, int seatNumber, int passengerId)
        {
            try
            {
                try
                {
                    deletePassengerLink(flightId, passengerId);
                }
                finally
                {

                    string sSQL = "INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) " +
                    $"VALUES( {flightId} , {passengerId} , {seatNumber})";
                    clsDataAccess.ExecuteNonQuery(sSQL);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }
    }
}
