using Calories.Class;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersInfo : ContentPage
    {
        private Buttons buttons = new Buttons();
        private NutritionCalculator calculator = new NutritionCalculator();

        public UsersInfo()
        {
            InitializeComponent();
        }

        // Обработчики для возраста
        private void OnAgePlusPressed(object sender, EventArgs e) => buttons.OnIntPlusPressedButton(UserAge, 99);
        private void OnAgeMinusPressed(object sender, EventArgs e) => buttons.OnIntMinusPressedButton(UserAge, 0);
        private void OnUserAgeTextChanged(object sender, TextChangedEventArgs e) => buttons.validationEntryWithNumbers(sender, e);

        // Обработчики для роста
        private void OnHeightTextChanged(object sender, TextChangedEventArgs e) => buttons.validationEntryWithNumbers(sender, e);
        private void OnHeightPlusPressed(object sender, EventArgs e) => buttons.OnIntPlusPressedButton(UserHeight, 250);
        private void OnHeightMinusPressed(object sender, EventArgs e) => buttons.OnIntMinusPressedButton(UserHeight, 0);

        // Обработчики для веса
        private void OnWeigthTextChanged(object sender, TextChangedEventArgs e) => buttons.validationEntryWithСomma(sender, e);
        private void OnWeightPlusPressed(object sender, EventArgs e) => buttons.OnFloatPlusPressedButton(UserWeight);
        private void OnWeightMinusPressed(object sender, EventArgs e) => buttons.OnFloatMinusPressedButton(UserWeight);

        private void OnButtonReleased(object sender, EventArgs e) => buttons._isAgePlusPressed = false;

        // Обработчик кнопки подтверждения
        private async void OnConfirmClicked(object sender, EventArgs e)
        {
            try
            {
                // Получаем значения из полей ввода
                int age = int.Parse(UserAge.Text);
                double height = double.Parse(UserHeight.Text);
                double weight = double.Parse(UserWeight.Text.Replace('.', ','));

                // Получаем выбранные значения
                string gender = (GenderPicker.SelectedItem as string)?.Contains("Муж") == true ? "male" : "female";
                string activity = GetActivityLevel(ActivityPicker.SelectedItem as string);
                string goal = GetGoal(GoalPicker.SelectedItem as string);

                // Выполняем расчет
                var result = calculator.Calculate(age, height, weight, gender, activity, goal);
                var registrationService = new UserRegistration("http://f1110995.xsph.ru/CreateUser.php");
                var request = new UserRegistration.RegistrationRequest
                {
                    Login = "testUser",
                    Email = "test@example.com",
                    Password = "securePass123",
                    Age = age,
                    Height = height,
                    Weight = weight,
                    Gender = gender.Contains("male") == true ? true : false,
                    Activity = activity,
                    Aim = goal,
                    Calories = result.calories,
                    Water = result.water,
                    Fats = result.bju.fat,
                    Proteins = result.bju.protein,
                    Carbs = result.bju.carbs
                };
                var response = await registrationService.RegisterUserAsync(request);
                if (response.Status == "success")
                {
                    await DisplayAlert("Супер", $"Success! User ID: {response.UserId}", "ОК");
                }
                else
                {
                    await DisplayAlert("Не супер", $"Error: {response.Message}", "ОК");
                }
                //// Показываем результат
                //await DisplayAlert("Результаты",
                //    $"Калории: {result.calories} ккал\n" +
                //    $"Белки: {result.bju.protein}g\n" +
                //    $"Жиры: {result.bju.fat}g\n" +
                //    $"Углеводы: {result.bju.carbs}g\n" +
                //    $"Вода: {result.water} мл", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", "Проверьте введенные данные", "OK");
            }
        }

        private string GetActivityLevel(string selectedActivity)
        {
            switch (selectedActivity)
            {
                case "Минимальная активность": return "sedentary";
                case "Лёгкая активность": return "light";
                case "Умеренная активность": return "moderate";
                case "Высокая активность": return "active";
                default: return "moderate";
            }
        }

        private string GetGoal(string selectedGoal)
        {
            switch (selectedGoal)
            {
                case "Потеря веса": return "weight_loss";
                case "Сохранение моего веса": return "maintenance";
                case "Увеличение веса": return "muscle_gain";
                default: return "maintenance";
            }
        }
    }
}