using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using static System.Console;

namespace CoffeeApp
{
    class Data
    {
        private int timeID;
        private int dateID;
        private int coffeeTotalPots;
        private int coffeeTotalPeople;

        public int TimeID { get => timeID; set => timeID = value; }
        public int DateID { get => dateID; set => dateID = value; }
        public int CoffeeTotalPots { get => coffeeTotalPots; set => coffeeTotalPots = value; }
        public int CoffeeTotalPeople { get => coffeeTotalPeople; set => coffeeTotalPeople = value; }

        public Data()
        {

        }


        public Data(int coffeeTotalPots, int coffeeTotalPeople)
        {
            this.coffeeTotalPots = coffeeTotalPots;
            this.coffeeTotalPeople = coffeeTotalPeople;
        }

        public Data(int timeID, int dateID, int coffeeTotalPots, int coffeeTotalPeople)
        {
            this.timeID = timeID;
            this.dateID = dateID;
            this.coffeeTotalPots = coffeeTotalPots;
            this.coffeeTotalPeople = coffeeTotalPeople;
        }






    }
}
