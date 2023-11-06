using Assignment6AirlineReservation;
using System;
using System.Collections.Generic;
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
        /// The list of planes as a PlaneDetail
        /// </summary>
        private static BindingList<PlaneDetail> planes = new BindingList<PlaneDetail>();

        private static PassengerDetail newPassenger;

        public static PassengerDetail NewPassenger
        {
            get { return newPassenger; }
        }

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


        public static BindingList<PlaneDetail> Planes
        {
            get
            {
                return planes;
            }
        }

        public static void addPassenger(string FirstName, string LastName)
        {
            string sSQL = $"INSERT INTO PASSENGER(First_Name, Last_Name) VALUES('{FirstName}','{LastName}')";
            clsDataAccess.ExecuteNonQuery(sSQL);
            sSQL = $"SELECT Passenger_ID from Passenger where First_Name = '{FirstName}' AND Last_Name = '{LastName}'";
            string id = clsDataAccess.ExecuteScalarSQL(sSQL);
            bool res = int.TryParse(id, out int passengerId);
            newPassenger = new PassengerDetail(passengerId, FirstName, LastName);
        }

        public static void addFlightPassenger(int flightId)
        {
            string sSQL = "INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) " +
       $"VALUES( {flightId} , {newPassenger.Id} , {newPassenger.SeatNumber})";
            clsDataAccess.ExecuteNonQuery(sSQL);
            newPassenger = null;
        }
    }
}
