using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using static System.Console;

namespace CoffeeApp
{
    public class DataAccess
    {

        #region Vars
        private static SqlConnection dbConn;
        private static SqlCommand dbCmd;
        private static SqlDataReader dbReader;
        private static string sConnection;
        private static string sql;
        //
        private static Data newData;
        //
        public static DateTime theTodayDate;
        public string todayDate = theTodayDate.ToString("yyyy-MM-dd");

        #endregion

        public static void OpenDB()
        {
            try
            {
                // Sets up the connection to the database. TODO: CHANGE THIS FOR OUR APP.
                sConnection =
                    "Data Source=stusql.ckwia8qkgyyj.us-east-1.rds.amazonaws.com,1433; " +
                    "Initial Catalog=RVcLibrary; User Id=RVCLibrary; Password=RVCLibrary2019";

                dbConn = new SqlConnection(sConnection);
                dbConn.Open();
            }
            catch
            {
                App.Current.MainPage.DisplayAlert("SQLError", "Error SQL OPEN", "CANCEL");
            }
        }// END OPENDB
        public static void CloseDB()
        {
            try
            {
                dbReader.Close();
            }
            catch
            {
                App.Current.MainPage.DisplayAlert("SQLError", "Error SQL Closed", "CANCEL");
            }
            finally
            {
                dbConn.Close();
            }
        } // End CLOSEDB
        public static void ExecCommand(SqlCommand theCmd)
        {
            try
            {
                theCmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                Console.Error.WriteLine(exc.Message); // edit
            }
            finally
            {
                CloseDB();
            }
        } // End ExecCommand

        //CHECKS IF DATE ID IS IN SQL TABLE
        public static int CheckTheDate(string theDateSel)
        {
            try
            {
                OpenDB();
                sql = "IF EXISTS (SELECT * FROM Date WHERE Date = '" + theDateSel + "') " +
                      "BEGIN " +
                    "SELECT DateID FROM Date WHERE Date = '" + theDateSel + "'; " +
                    "END " +
                    "ELSE " +
                    "BEGIN " +
                    "INSERT INTO Date VALUES ('" + theDateSel + "'); " +
                    "SELECT DateID FROM Date WHERE Date = '" + theDateSel + "'; " +
                    "END";

                dbCmd = new SqlCommand();
                dbCmd.CommandText = sql;
                dbCmd.Connection = dbConn;


                dbReader = dbCmd.ExecuteReader();

                while (dbReader.Read())
                {
                    newData = new Data(
                        Convert.ToInt32(dbReader["DateID"])
                        );
                }
                CloseDB();
            }
            catch
            {
                App.Current.MainPage.DisplayAlert("CHECK DATE ERROR", "DATE ERROR", "CANCEL");
            }
            return newData.DateID;
        }

        #region Insert Data 
        //SENDS POTS & PEOPLE TOTAL DATA TO SQL
        public static void InsertDayCoffeeData(int theDay, int thePots, int thePeople)
        {
            try
            {
                sql = "IF EXISTS (SELECT * FROM DailyCount WHERE DateID = " + theDay + ") " +
                  "UPDATE DailyCount SET CoffeeTotalPots = CoffeeTotalPots + " + thePots + ", CoffeeTotalPeople = CoffeeTotalPeople + " + thePeople + " " +
                  " WHERE DateID = " + theDay + "; " +
            "ELSE " +
            "INSERT INTO DailyCount (DateID, CoffeeTotalPots, CoffeeTotalPeople) VALUES(" + theDay + ", " + thePots + ", " + thePeople + ");";
                OpenDB();
                SqlCommand cmdUpdate = new SqlCommand(sql, dbConn);
                ExecCommand(cmdUpdate);
                CloseDB();
            }
            catch
            {
                App.Current.MainPage.DisplayAlert("INSERT ERROR", "INSERT ERROR ON BOTH POTS AND PEOPLE ", "CANCEL");
            }
        }

        //SENDS POTS TOTAL DATA TO SQL 
        public static void InsertDayPotsData(int theDay, int thePots)
        {
            try
            {
                sql = "IF EXISTS (SELECT * FROM DailyCount WHERE DateID = " + theDay + ") " +
                  "UPDATE DailyCount SET CoffeeTotalPots = CoffeeTotalPots + " + thePots + " " +
                  " WHERE DateID = " + theDay + "; " +
            "ELSE " +
            "INSERT INTO DailyCount (DateID, CoffeeTotalPots) VALUES(" + theDay + ", " + thePots + ");";
                OpenDB();
                SqlCommand cmdUpdate = new SqlCommand(sql, dbConn);
                ExecCommand(cmdUpdate);
                CloseDB();
            }
            catch
            {
                App.Current.MainPage.DisplayAlert("INSERT ERROR", "INSERT ERROR ON POTS", "CANCEL");
            }

        }

        //SENDS PEOPLE TOTAL DATA TO SQL 
        public static void InsertDayPeopleData(int the_Day, int the_People)
        {
            try
            {
                sql = "IF EXISTS (SELECT * FROM DailyCount WHERE DateID = " + the_Day + ") " +
                  "UPDATE DailyCount SET CoffeeTotalPeople = CoffeeTotalPeople + " + the_People + " " +
                  " WHERE DateID = " + the_Day + "; " +
            "ELSE " +
            "INSERT INTO DailyCount (DateID, CoffeeTotalPeople) VALUES(" + the_Day + ", " + the_People + ");";
                OpenDB();
                SqlCommand cmdUpdate = new SqlCommand(sql, dbConn);
                ExecCommand(cmdUpdate);
                CloseDB();
            }
            catch
            {
                App.Current.MainPage.DisplayAlert("INSERT ERROR", "INSERT ERROR ON PEOPLE", "CANCEL");
            }

        }
        #endregion

        #region Totals 
        //SHOWS PEOPLE TOTALS
        public static int ShowTodayPeople(int the_Day)
        {
            int number = 0;
            try
            {
            sql = "SELECT SUM(CoffeeTotalPeople) FROM DailyCount WHERE DateID = " + the_Day + ";";
            OpenDB();
            SqlCommand cmdPeopleCount = new SqlCommand(sql, dbConn);
            number = (int)cmdPeopleCount.ExecuteScalar();
            CloseDB();
            }
            catch
            {
                App.Current.MainPage.DisplayAlert("SQL ERROR", "SQL NO DATA FOR TODAY ERROR ON PEOPLE", "CANCEL");                
            }
            return number;
        }

        //SHOWS POTS TOTALS
        public static int ShowTodayPots(int the_Day)
        {
            int number = 0;
            try
            {
            sql = "SELECT SUM(CoffeeTotalPots) FROM DailyCount WHERE DateID = " + the_Day + ";";
            OpenDB();
            SqlCommand cmdPeopleCount = new SqlCommand(sql, dbConn);
            number = (int)cmdPeopleCount.ExecuteScalar();
            CloseDB();
            }
            catch
            {
                App.Current.MainPage.DisplayAlert("SQL ERROR", "SQL NO DATA FOR TODAY ERROR ON POTS", "CANCEL");
            }
            return number;
        }
        #endregion
    }
}
