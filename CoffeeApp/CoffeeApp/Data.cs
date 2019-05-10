using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using static System.Console;

namespace CoffeeApp
{
    public class Data
    {
        private int dateID;

        public int DateID { get => dateID; set => dateID = value; }

        public Data()
        {
        }
        public Data(int dateId)
        {
            DateID = dateId;
        }

    }
}
