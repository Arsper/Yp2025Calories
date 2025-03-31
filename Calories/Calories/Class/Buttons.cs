using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calories.Class
{
     class Buttons
    {
        public bool _isAgePlusPressed;
        private readonly object _locker = new object();

        //Кнопка плюс возраст
        public async void OnIntPlusPressedButton(Entry UserAge, int max)
        {
            _isAgePlusPressed = true;

            // Первое нажатие - мгновенное увеличение
            IncreaseInt(UserAge,max);

            // Пауза перед автоповтором
            await Task.Delay(600);

            // Автоповтор при удержании
            while (_isAgePlusPressed)
            {
                IncreaseInt(UserAge, max);
                await Task.Delay(100); // Интервал повтора
            }
        }
        private void IncreaseInt(Entry UserAge, int max)
        {
            lock (_locker)
            {
                if (int.TryParse(UserAge.Text, out int age) && age < max)
                {
                    UserAge.Text = (age + 1).ToString();
                }
            }
        }
        //Кнопка минус возраст
        public async void OnIntMinusPressedButton(Entry UserAge, int min)
        {
            _isAgePlusPressed = true;

            // Первое нажатие - мгновенное уменьшение

            reductionInt(UserAge,min);

            // Пауза перед автоповтором
            await Task.Delay(600);

            // Автоповтор при удержании
            while (_isAgePlusPressed)
            {
                reductionInt(UserAge,min);
                await Task.Delay(100); // Интервал повтора
            }
        }
        private void reductionInt(Entry UserAge, int min)
        {
            lock (_locker)
            {
                if (int.TryParse(UserAge.Text, out int age) && age > min)
                {
                    UserAge.Text = (age - 1).ToString();
                }
            }
        }
        public void validationEntryWithСomma(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;

            // Разрешаем пустое значение
            if (string.IsNullOrEmpty(e.NewTextValue))
                return;

            // Проверяем каждый символ (только цифры и запятая)
            bool isValid = e.NewTextValue.All(c => char.IsDigit(c) || c == '.');

            // Проверяем количество запятых (не более одной)
            int commaCount = e.NewTextValue.Count(c => c == '.');
            if (commaCount > 1)
                isValid = false;

            // Если есть запятая, разделяем на части
            if (isValid && commaCount == 1)
            {
                var parts = e.NewTextValue.Split('.');
                // До запятой максимум 3 цифры, после - максимум 1
                if (parts[0].Length > 3 || parts.Length > 1 && parts[1].Length > 1)
                    isValid = false;
            }
            else if (isValid)
            {
                // Если запятой нет - максимум 3 цифры
                if (e.NewTextValue.Length > 3)
                    isValid = false;
            }

            // Если ввод невалидный - возвращаем старое значение
            if (!isValid)
            {
                entry.Text = e.OldTextValue;
            }
        }
        public void validationEntryWithNumbers(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                return;

            // Проверяем, что введены только цифры
            if (!e.NewTextValue.All(char.IsDigit))
            {
                ((Entry)sender).Text = e.OldTextValue; // Откатываем ввод, если символ не цифра
            }
            if (Convert.ToInt16(((Entry)sender).Text) > 250)
            {
                ((Entry)sender).Text = 250.ToString();
            }
        }
        public async void OnFloatPlusPressedButton(Entry UserAge)
        {
            _isAgePlusPressed = true;

            // Первое нажатие - мгновенное увеличение
            IncreaseFloat(UserAge);

            // Пауза перед автоповтором
            await Task.Delay(600);

            // Автоповтор при удержании
            while (_isAgePlusPressed)
            {
                IncreaseFloat(UserAge);
                await Task.Delay(100); // Интервал повтора
            }
        }
        private void IncreaseFloat(Entry UserAge)
        {
            lock (_locker)
            {
                if (double.TryParse(UserAge.Text, out double weight) && weight < 999.9)
                {
                    UserAge.Text = (weight + 0.1).ToString();
                }
            }
        }
        public async void OnFloatMinusPressedButton(Entry UserAge)
        {
            _isAgePlusPressed = true;

            // Первое нажатие - мгновенное увеличение
            reductionFloat(UserAge);

            // Пауза перед автоповтором
            await Task.Delay(600);

            // Автоповтор при удержании
            while (_isAgePlusPressed)
            {
                reductionFloat(UserAge);
                await Task.Delay(100); // Интервал повтора
            }
        }
        private void reductionFloat(Entry UserAge)
        {
            lock (_locker)
            {
                if (double.TryParse(UserAge.Text, out double weight) && weight < 999.9)
                {
                    UserAge.Text = (weight - 0.1).ToString();
                }
            }
        }
    }
}
