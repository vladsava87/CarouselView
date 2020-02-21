using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WizzardControl
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

#if DEBUG
            EnableDebugRainbows(false);
#endif

            MainPage = new MainPage();
        }

        private void EnableDebugRainbows(bool shouldUseDebugRainbows)
        {
            Resources.Add(new Style(typeof(ContentPage))
            {
                ApplyToDerivedTypes = true,
                Setters = {
                    new Setter
                    {
                        Property = Xamarin.Forms.DebugRainbows.DebugRainbow.ShowColorsProperty,
                        Value = shouldUseDebugRainbows
                    }
                }
            });
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
