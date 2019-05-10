using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static System.Console;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Detail : ContentPage
	{

        public static int totalPeopleToday = 0; // TOTAL PEOPLE
        public static int totalPotsToday = 0;   //TOTAL POTS
        public static int totalCupsAvalable = 5;   //TOTAL CUPS AVALABLE 
        public static int clickCounter = 0; //CLICK COUNTER

        public static int progressMax = 1;  //ProgressBar Max
        public static double decrementBy = .2;

        public static double turnOrange = 4; // WHEN 4 PEOPLE TAKE A CUP 1 LEFT 
        public static double turnYellow = 3;    // WHEN 3 PEOPLE TAKE A CUP 2 LEFT
        public static double turnRed = 0;   // WHEN 5 PEOPLE TAKE A CUP 0 LEFT

        int topCounter = totalCupsAvalable; // COUNTER ON TOP 
        public DateTime theDateVar;

        public Detail ()
		{
			InitializeComponent ();

            theDateVar = DateTime.Today;

            coffeebar.Progress = progressMax;   //SETS COFFEE BAR TO MAX SET
            lblCounter.Text = topCounter.ToString();    //DISPLAYS COUNTER 
            totalPotsToday = 0;
            totalPeopleToday = 0;

        }

        private void Coffee_Button_Clicked(object sender, EventArgs e)
        {
            totalPeopleToday++;
            clickCounter++;
            topCounter = topCounter - 1;
            lblCounter.Text = topCounter.ToString();
            //("yyyy-MM-dd")
            theDateVar = DateTime.Today;
            int new_DataID = DataAccess.CheckTheDate(theDateVar.ToString("yyyy-MM-dd"));
            DataAccess.InsertDayCoffeeData(new_DataID, totalPotsToday, totalPeopleToday);
            totalPotsToday = 0;
            totalPeopleToday = 0;
            ///
            if (clickCounter <= totalCupsAvalable)
            {
                coffeebar.Progress = coffeebar.Progress - decrementBy;  //LOWERS COFFEE BAR

            }
            if (topCounter == turnRed)   //HIT RED
            {
                coffeebar.BackgroundColor = Color.FromHex("D9544F");             
                btnMainCoffee.IsEnabled = false;
                btn_Empty.IsEnabled = false;
                btn_Empty.IsVisible = false;
                btn_Reset.IsEnabled = true;
                btn_Reset.IsVisible = true;
                btn_Reset.IsEnabled = true;
                
            }
            else if (clickCounter == turnYellow)  // HIT YELLOW
            {
                coffeebar.BackgroundColor = Color.FromHex("FFCB49");
            }
            else if (clickCounter == turnOrange)  //HIT ORANGE
            {
                coffeebar.BackgroundColor = Color.FromHex("FD8D02");
            }
        } // Main Btn

        private void BtnEmpty(object sender, EventArgs e)
        {
            int holdIntCounter = topCounter;
            totalPeopleToday = totalPeopleToday + holdIntCounter; //ADDS WHATS LEFT IN THE COUNTER TO TOTAL PEOPLE
            topCounter = 0;
            lblCounter.Text = topCounter.ToString();
            coffeebar.Progress = 0;
            coffeebar.BackgroundColor = Color.FromHex("D9544F");
            btn_Reset.IsEnabled = true;
            btn_Reset.IsVisible = true;
            btn_Empty.IsEnabled = false;
            btn_Empty.IsVisible = false;

            theDateVar = DateTime.Today;
            int new_DataID = DataAccess.CheckTheDate(theDateVar.ToString("yyyy-MM-dd"));
            DataAccess.InsertDayPeopleData(new_DataID,totalPeopleToday);
            holdIntCounter = 0;
            totalPotsToday = 0;
            totalPeopleToday = 0;
        }
    
        private void BtnReset(object sender, EventArgs e)
        {       
            totalPotsToday++;
            coffeebar.BackgroundColor = Color.FromHex("4e3530");
            topCounter = totalCupsAvalable;
            lblCounter.Text = topCounter.ToString();
            coffeebar.Progress = progressMax;
            clickCounter = 0;

            btnMainCoffee.IsEnabled = true;
            btn_Empty.IsEnabled = true;
            btn_Empty.IsVisible = true;
            btn_Reset.IsEnabled = false;
            btn_Reset.IsVisible = false;

            theDateVar = DateTime.Today;
            int new_DataID = DataAccess.CheckTheDate(theDateVar.ToString("yyyy-MM-dd"));
            DataAccess.InsertDayPotsData(new_DataID, totalPotsToday);
            totalPotsToday = 0;
            totalPeopleToday = 0;
        }
    }
}