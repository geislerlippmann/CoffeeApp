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
        public static double changeAmountBy = 0;

        public static DateTime theDatePicked;

        public static DateTime thePotDatePicked;
        public static DateTime thePeopleDatePicked;

        public static DateTime theTodayDate = DateTime.Today;
        public string todayDate = theTodayDate.ToString("yyyy - MM - dd");

        public Admin ()
		{
			InitializeComponent ();

            lblTotalPeople.Text = Detail.totalPeopleToday.ToString();
            lblTotalPots.Text = Detail.totalPotsToday.ToString();
        }

        private void BtnEditPeople_Clicked(object sender, EventArgs e)
        {         
            btnEditPots.IsEnabled = false;
            
            btnPeopleDate.IsEnabled = true;
            btnPeopleDate.IsVisible = true;
            //lblMessage.IsEnabled = true;
            //lblMessage.IsVisible = true;
            dateRangePeople.IsEnabled = true;
            dateRangePeople.IsVisible = true;

            thePeopleDatePicked = theTodayDate;
            dateRangePeople.Date = theTodayDate;
            //lblMessage.Text = "";
        }

        private void BtnEditPots_Clicked(object sender, EventArgs e)
        {
            btnEditPeople.IsEnabled = false;

            btnPotDate.IsEnabled = true;
            btnPotDate.IsVisible = true;
            //lblMessage.IsEnabled = true;
            //lblMessage.IsVisible = true;
            dateRangePot.IsEnabled = true;
            dateRangePot.IsVisible = true;

            thePotDatePicked = theTodayDate;
            dateRangePot.Date = theTodayDate;
            //lblMessage.Text = "";

        }

        private void BtnAddMore_Clicked(object sender, EventArgs e)
        {
            changeAmountBy++;
            lblEditAmount.Text = changeAmountBy.ToString();
        }

        private void BtnSubMore_Clicked(object sender, EventArgs e)
        {
            changeAmountBy--;
            lblEditAmount.Text = changeAmountBy.ToString();
        }

        private void BtnSubmitPeople_Clicked(object sender, EventArgs e)
        {
            lblEditAmount.IsVisible = false;
            btnAddMore.IsVisible = false;
            btnSubMore.IsVisible = false;
            btnSubmitPeople.IsVisible = false;

            lblEditAmount.IsEnabled = false;
            btnAddMore.IsEnabled = false;
            btnSubMore.IsEnabled = false;
            btnEditPots.IsEnabled = true;

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
            dateRangePeople.Date = theTodayDate;

            


        }
        private void BtnSubmitPots_Clicked(object sender, EventArgs e)
        {
            lblEditAmount.IsVisible = false;
            btnAddMore.IsVisible = false;
            btnSubMore.IsVisible = false;
            btnSubmitPots.IsVisible = false;

            lblEditAmount.IsEnabled = false;
            btnAddMore.IsEnabled = false;
            btnSubMore.IsEnabled = false;
            btnSubmitPots.IsEnabled = false;
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
            dateRangePot.Date = theTodayDate;
        }



        private void BtnPeopleDate_Clicked(object sender, EventArgs e)
        {
            lblEditAmount.IsVisible = true;
            btnAddMore.IsVisible = true;
            btnSubMore.IsVisible = true;
            btnSubmitPeople.IsVisible = true;

            lblEditAmount.IsEnabled = true;
            btnAddMore.IsEnabled = true;
            btnSubMore.IsEnabled = true;
            btnSubmitPeople.IsEnabled = true;

            btnPeopleDate.IsEnabled = false;
            btnPeopleDate.IsVisible = false;
            //lblMessage.IsVisible = false;

            thePeopleDatePicked = dateRangePeople.Date;
            //reset the people date after use 
        }

        private void BtnPotDate_Clicked(object sender, EventArgs e)
        {
            lblEditAmount.IsVisible = true;
            btnAddMore.IsVisible = true;
            btnSubMore.IsVisible = true;
            btnSubmitPots.IsVisible = true;

            lblEditAmount.IsEnabled = true;
            btnAddMore.IsEnabled = true;
            btnSubMore.IsEnabled = true;
            btnSubmitPots.IsEnabled = true;

            btnPotDate.IsEnabled = false;
            btnPotDate.IsVisible = false;
            //lblMessage.IsVisible = false;

            thePotDatePicked = dateRangePot.Date;
        }

        private void DateRangePeople_DateSelected(object sender, DateChangedEventArgs e)
        {
            
            //lblMessage.Text = thePeopleDatePicked.ToString();

        }

        private void DateRangePot_DateSelected(object sender, DateChangedEventArgs e)
        {
            //lblMessage.Text = thePotDatePicked.ToString();
        }



    }
}