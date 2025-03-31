using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Essentials;

namespace Calories
{
    class ChekData
    {
        static private string emailRegex = "^((?!\\.)[\\w\\-_.]*[^.])(@\\w+)(\\.\\w+(\\.\\w+)?[^.\\W])$";
        static private string passwordRegex = "^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\d\\s:])([^\\s]){8,16}$";
        static private async void ErrorMessage(string errorMeseg)
        {
            await App.Current.MainPage.DisplayAlert("Ошибка", errorMeseg, "OK");
        }

        static public bool CheckLogin(string Login)
        {
            if (!Regex.IsMatch(Login ?? "", ".{5,16}", RegexOptions.IgnoreCase))
            {
                ErrorMessage("Логин должен состоять минимум из 5 символов.");
                return false;
            }
            return true;
        }

        static public bool CheckEmail(string Email)
        {
            if (!Regex.IsMatch(Email ?? "", emailRegex, RegexOptions.IgnoreCase))
            {
                ErrorMessage("Почта введена неверно");
                return false;
            }
            return true;
        }

        static public bool CheckPassword(string Password)
        {
            if (!Regex.IsMatch(Password ?? "", passwordRegex, RegexOptions.IgnoreCase))
            {
                ErrorMessage("Пароль введён неверно.\n" +
                    "Пароль должен содержать:\n" +
                    "-не меньше 8 символов;\n" +
                    "-одну заглавную букву;\n" +
                    "-одну маленькую букву;\n" +
                    "-одну цифру от (0-9);\n" +
                    "-один специальный символ. ");
                return false;
            }
            return true;
        }
        static public bool CheckTwoPassword(string firstPassword, string secondPassword)
        {
            if (!Regex.IsMatch(firstPassword ?? "", secondPassword ?? "", RegexOptions.IgnoreCase))
            {
                ErrorMessage("Пароли не совпадают");
                return false;
            }
            return true;
        }

    }
}
