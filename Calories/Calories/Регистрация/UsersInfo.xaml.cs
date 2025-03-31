using Calories.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calories
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    
	public partial class UsersInfo : ContentPage
	{
        private Buttons buttons = new Buttons();
        public UsersInfo ()
		{
			InitializeComponent ();
		}
        //добавления возраста
        private void OnAgePlusPressed(object sender, EventArgs e)
        {
            buttons.OnIntPlusPressedButton(UserAge,99);
        }
        //убавления возраста
        private void OnAgeMinusPressed(object sender, EventArgs e)
        {
            buttons.OnIntMinusPressedButton(UserAge,0);
        }
        //отпустили кнопку
        private void OnButtonReleased(object sender, EventArgs e)
        {
            buttons._isAgePlusPressed = false;
        }
        //изменение возраста
        private void OnUserAgeTextChanged(object sender, TextChangedEventArgs e)
        {
            buttons.validationEntryWithNumbers(sender, e);
        }


        //изменение роста
        private void OnHeightTextChanged(object sender, TextChangedEventArgs e)
        {
            buttons.validationEntryWithNumbers(sender, e);
        }
        //добавления роста
        private void OnHeightPlusPressed(object sender, EventArgs e)
        {
            buttons.OnIntPlusPressedButton(UserHeight, 250);
        }
        //убавление роста
        private void OnHeightMinusPressed(object sender, EventArgs e)
        {
            buttons.OnIntMinusPressedButton(UserHeight, 0);
        }

        //изменение веса
        private void OnWeigthTextChanged(object sender, TextChangedEventArgs e)
        {
            buttons.validationEntryWithСomma(sender, e);
        }
        //добавления веса
        private void OnWeightPlusPressed(object sender, EventArgs e)
        {
            buttons.OnFloatPlusPressedButton(UserWeight);
        }
        //убавление веса
        private void OnWeightMinusPressed(object sender, EventArgs e)
        {
            buttons.OnFloatMinusPressedButton(UserWeight);
        }
    }
}