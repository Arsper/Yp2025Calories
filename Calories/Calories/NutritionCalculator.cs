using System;
using System.Collections.Generic;
using System.Text;

namespace Calories
{
    public class NutritionCalculator
    {
        public (int calories, (double protein, double fat, double carbs) bju, double water) Calculate(
            int age,
            double height,
            double weight,
            string gender,
            string activityLevel,
            string goal)
        {
            // 1. BMR расчёт
            double bmr = (10 * weight) + (6.25 * height) - (5 * age);
            bmr += gender.ToLower() == "male" ? 5 : -161;

            // 2. Учёт активности
            double activityMultiplier = GetActivityMultiplier(activityLevel);
            double calories = bmr * activityMultiplier;

            // 3. Коррекция по цели
            calories = AdjustForGoal(calories, goal, weight);

            // 4. Расчёт БЖУ
            var bju = CalculateBju(weight, calories);

            // 5. Норма воды
            double water = CalculateWater(weight, activityLevel);

            return ((int)Math.Round(calories), bju, water);
        }

        private double AdjustForGoal(double calories, string goal, double weight)
        {
            goal = goal?.ToLower();
            if (goal == "weight_loss")
            {
                double minSafeCalories = weight * 22;
                return Math.Max(calories * 0.85, minSafeCalories);
            }
            else if (goal == "muscle_gain")
            {
                return calories * 1.1;
            }
            return calories;
        }

        private double GetActivityMultiplier(string level)
        {
            level = level?.ToLower();
            switch (level)
            {
                case "sedentary": return 1.2;
                case "light": return 1.375;
                case "moderate": return 1.55;
                case "active": return 1.725;
                case "very_active": return 1.9;
                default: return 1.375;
            }
        }

        private (double protein, double fat, double carbs) CalculateBju(double weight, double calories)
        {
            double protein = weight * 1.5;
            double fatKcal = calories * 0.3;
            double fat = fatKcal / 9;
            double carbsKcal = calories - (protein * 4) - fatKcal;
            double carbs = carbsKcal / 4;

            return (
                Math.Round(protein, 1),
                Math.Round(fat, 1),
                Math.Round(carbs, 1)
            );
        }

        private double CalculateWater(double weight, string activityLevel)
        {
            double baseWater = weight * 30;
            activityLevel = activityLevel?.ToLower();
            switch (activityLevel)
            {
                case "moderate": return baseWater + 300;
                case "active": return baseWater + 500;
                case "very_active": return baseWater + 700;
                default: return baseWater;
            }
        }
    }
}
