using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using static System.Console;

namespace CoffeeApp
{
    class DataAccess
    {

        private static SqlConnection dbConn;
        private static SqlCommand dbCmd;
        private static SqlDataReader dbReader;
        private static SqlDataAdapter dbAdapter;
        private static string sConnection;
        private static string sql;
        //
        private static Data newData;
        //
        public static DateTime theTodayDate = DateTime.Today;
        public string todayDate = theTodayDate.ToString("yyyy - MM - dd");



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
            catch (Exception exc)
            {
                Console.Error.WriteLine(exc.Message); // edit

            }
        }// END OPENDB
        public static void CloseDB()
        {
            try
            {
                dbReader.Close();
            }
            catch (Exception exc)
            {
                Console.Error.WriteLine(exc.Message); // edit
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



        public static SqlDataReader TheReadData(string testDate) // edit
        {
            try
            {
                string dateSelection = testDate; 

                sql = " "; /// Select Here

                dbCmd = new SqlCommand();
                dbCmd.CommandText = sql;
                dbCmd.Connection = dbConn;

                // Runs the sql command and returns result defined by reader
                dbReader = dbCmd.ExecuteReader();
            }
            catch (Exception exc)
            {
                Console.Error.WriteLine(exc.Message);
            }
            return dbReader;
        } // End ReadData




    }
}
