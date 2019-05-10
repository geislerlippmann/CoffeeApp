using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Data.SqlClient;
using static System.Console;

namespace CoffeeApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Admin : ContentPage
	{
           #region Vars
        public static double changeAmountBy = 0;
        public static int changePotAmountBy = 0;
        public static int changePeopleAmountBy = 0;

        public static DateTime theDatePicked;

        public static DateTime thePotDatePicked;
        public static DateTime thePeopleDatePicked;

        public static string thePotDatePickedString;
        public static string thePeopleDatePickedString;

        public static DateTime theTodayDate = DateTime.Today;
        public string todayDate = theTodayDate.ToString("yyyy-MM-dd");
        //

        public DateTime theDateVar;
        //
        public static int thePeopleNumber = 0;
        public static int thePotsNumber = 0;
        #endregion

        public Admin ()
		{
			InitializeComponent ();
            ShowToday();
        }

        #region Edit Buttons
        private void BtnEditPeople_Clicked(object sender, EventArgs e)
        {         
            btnEditPots.IsEnabled = false;
            btnEditPeople.IsEnabled = false;
            btnPeopleDate.IsEnabled = true;
            btnPeopleDate.IsVisible = true;
           
            dateRangePeople.IsEnabled = true;
            dateRangePeople.IsVisible = true;

            changePeopleAmountBy = 0;
            changePotAmountBy = 0;

            btnSQLTotalsToday.IsVisible = false;
            btnSQLTotalsToday.IsEnabled = false;
        }

        private void BtnEditPots_Clicked(object sender, EventArgs e)
        {
            btnEditPots.IsEnabled = false;
            btnEditPeople.IsEnabled = false;
            btnPotDate.IsEnabled = true;
            btnPotDate.IsVisible = true;
            
            dateRangePot.IsEnabled = true;
            dateRangePot.IsVisible = true;

            changePotAmountBy = 0;
            changePeopleAmountBy = 0;

            btnSQLTotalsToday.IsVisible = false;
            btnSQLTotalsToday.IsEnabled = false;
        }
        #endregion

        #region ADD / SUB TOTALS
        private void BtnAddMorePeople_Clicked(object sender, EventArgs e)
        {
            changePeopleAmountBy++;
            lblEditAmount.Text = changePeopleAmountBy.ToString();
        }

        private void BtnSubMorePeople_Clicked(object sender, EventArgs e)
        {
            changePeopleAmountBy--;
            lblEditAmount.Text = changePeopleAmountBy.ToString();
        }

        private void BtnAddMorePot_Clicked(object sender, EventArgs e)
        {
            changePotAmountBy++;
            lblEditAmount.Text = changePotAmountBy.ToString();
        }

        private void BtnSubMorePot_Clicked(object sender, EventArgs e)
        {
            changePotAmountBy--;
            lblEditAmount.Text = changePotAmountBy.ToString();
        }
        #endregion

        #region Submit Buttons
        private void BtnSubmitPeople_Clicked(object sender, EventArgs e)
        {
            lblEditAmount.IsVisible = false;
            btnAddMorePeople.IsVisible = false;
            btnSubMorePeople.IsVisible = false;
            btnSubmitPeople.IsVisible = false;

            lblEditAmount.IsEnabled = false;
            btnAddMorePeople.IsEnabled = false;
            btnSubMorePeople.IsEnabled = false;
            btnEditPots.IsEnabled = true;
            btnEditPeople.IsEnabled = true;

            int newPeopleAmount; 

            if(changeAmountBy > 0)
            {
                newPeopleAmount = Detail.totalPeopleToday + Convert.ToInt32(changeAmountBy);
                Detail.totalPeopleToday = newPeopleAmount;
                lblTotalPeople.Text = Detail.totalPeopleToday.ToString();
            }
            else if(changeAmountBy < 0)
            {
                newPeopleAmount = Detail.totalPeopleToday + Convert.ToInt32(changeAmountBy);
                Detail.totalPeopleToday = newPeopleAmount;
                lblTotalPeople.Text = Detail.totalPeopleToday.ToString();
            }
            changeAmountBy = 0;
            lblEditAmount.Text = changeAmountBy.ToString();

            dateRangePeople.IsVisible = false;
            dateRangePeople.IsEnabled = false;

            int new_DataID = DataAccess.CheckTheDate(thePeopleDatePicked.ToString("yyyy-MM-dd"));
            DataAccess.InsertDayPeopleData(new_DataID, changePeopleAmountBy);
            //ShowToday();
            ShowTheSelectedDay(dateRangePeople.Date);
            btnSQLTotalsToday.IsVisible = true;
            btnSQLTotalsToday.IsEnabled = true;

            dateRangePeople.Date = DateTime.Today;
            dateRangePot.Date = DateTime.Today;
        }
        private void BtnSubmitPots_Clicked(object sender, EventArgs e)
        {
            lblEditAmount.IsVisible = false;
            btnAddMorePot.IsVisible = false;
            btnSubMorePot.IsVisible = false;
            btnSubmitPots.IsVisible = false;

            lblEditAmount.IsEnabled = false;
            btnAddMorePot.IsEnabled = false;
            btnSubMorePot.IsEnabled = false;
            btnSubmitPots.IsEnabled = false;
            btnEditPots.IsEnabled = true;
            btnEditPeople.IsEnabled = true;

            int newPotsAmount;

            if (changeAmountBy > 0)
            {
                newPotsAmount = Detail.totalPotsToday + Convert.ToInt32(changeAmountBy);
                Detail.totalPotsToday = newPotsAmount;
                lblTotalPots.Text = Detail.totalPotsToday.ToString();
            }
            else if (changeAmountBy < 0)
            {
                newPotsAmount = Detail.totalPotsToday + Convert.ToInt32(changeAmountBy);
                Detail.totalPotsToday = newPotsAmount;
                lblTotalPots.Text = Detail.totalPotsToday.ToString();
            }
            changeAmountBy = 0;
            lblEditAmount.Text = changeAmountBy.ToString();

            dateRangePot.IsVisible = false;
            dateRangePot.IsEnabled = false;

            int new_DataID = DataAccess.CheckTheDate(thePotDatePicked.ToString("yyyy-MM-dd"));
            DataAccess.InsertDayPotsData(new_DataID, changePotAmountBy);
            //ShowToday();
            ShowTheSelectedDay(dateRangePot.Date);
            btnSQLTotalsToday.IsVisible = true;
            btnSQLTotalsToday.IsEnabled = true;
            dateRangePeople.Date = DateTime.Today;
            dateRangePot.Date = DateTime.Today;
        }
        #endregion

        #region Date Send
        private void BtnPeopleDate_Clicked(object sender, EventArgs e)
        {
            lblEditAmount.IsVisible = true;
            btnAddMorePeople.IsVisible = true;
            btnSubMorePeople.IsVisible = true;
            btnSubmitPeople.IsVisible = true;

            lblEditAmount.IsEnabled = true;
            btnAddMorePeople.IsEnabled = true;
            btnSubMorePeople.IsEnabled = true;
            btnSubmitPeople.IsEnabled = true;

            btnPeopleDate.IsEnabled = false;
            btnPeopleDate.IsVisible = false;

            thePeopleDatePicked = dateRangePeople.Date;
            
        }

        private void BtnPotDate_Clicked(object sender, EventArgs e)
        {
            lblEditAmount.IsVisible = true;
            btnAddMorePot.IsVisible = true;
            btnSubMorePot.IsVisible = true;
            btnSubmitPots.IsVisible = true;

            lblEditAmount.IsEnabled = true;
            btnAddMorePot.IsEnabled = true;
            btnSubMorePot.IsEnabled = true;
            btnSubmitPots.IsEnabled = true;

            btnPotDate.IsEnabled = false;
            btnPotDate.IsVisible = false;

            thePotDatePicked = dateRangePot.Date;
      
        }

        private void DateRangePeople_DateSelected(object sender, DateChangedEventArgs e)
        {
            ShowTheSelectedDay(dateRangePeople.Date);
        }

        private void DateRangePot_DateSelected(object sender, DateChangedEventArgs e)
        {
            ShowTheSelectedDay(dateRangePot.Date);
        }
        #endregion

        private void BtnSQLTotalsToday_Clicked(object sender, EventArgs e)
        {
            ShowToday();
        }

        #region Classes Display
        public void ShowToday()
        {
            DateTime dateToday = DateTime.Today;
            int new_DataID = DataAccess.CheckTheDate(dateToday.ToString("yyyy-MM-dd"));

            thePeopleNumber = DataAccess.ShowTodayPeople(new_DataID);
            lblTotalPeople.Text = thePeopleNumber.ToString();

            thePotsNumber = DataAccess.ShowTodayPots(new_DataID);
            lblTotalPots.Text = thePotsNumber.ToString();

        }

        public void ShowTheSelectedDay(DateTime the_Date)
        {
            
            int new_DataID = DataAccess.CheckTheDate(the_Date.ToString("yyyy-MM-dd"));

            thePeopleNumber = DataAccess.ShowTodayPeople(new_DataID);
            lblTotalPeople.Text = thePeopleNumber.ToString();

            thePotsNumber = DataAccess.ShowTodayPots(new_DataID);
            lblTotalPots.Text = thePotsNumber.ToString();

        }
        #endregion
    }
}