using System;

using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.Runtime;
using Android.OS;
using Android.Content.Res;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;

namespace Calories.Droid
{
    [Activity(Label = "Calories", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Базовые настройки
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            // Инициализация Xamarin.Forms
            Forms.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.ViewInitialized += (sender, e) =>
            {
                if (e.View is Entry && e.NativeView is Android.Widget.EditText editText)
                {
                    editText.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
                }
            };

            // Загрузка главного приложения
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // Правильное преобразование цвета в Android.Graphics.Color
                var androidColor = Android.Graphics.Color.Rgb(0, 120, 215); // Синий цвет (#0078D7)

                // Для API Level 21+ (Lollipop)
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    Control.TextCursorDrawable?.SetTint(androidColor);
                }

                // Альтернативный способ для API 29+ (Android 10)
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
                {
                    Control.TextCursorDrawable?.SetTintList(
                        ColorStateList.ValueOf(androidColor));
                }

                // Убираем подчеркивание (дополнительно)
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            }
        }
    }
}