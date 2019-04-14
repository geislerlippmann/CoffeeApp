using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Master : ContentPage
	{
		public Master ()
		{
			InitializeComponent ();

            buttonA.Clicked += async (sender, e) =>
            {
                await App.NavigationMasterDetail(new Admin());
            };
            buttonB.Clicked += async (sender, e) =>
            {
                await App.NavigationMasterDetail(new B());
            };

        }
	}
}