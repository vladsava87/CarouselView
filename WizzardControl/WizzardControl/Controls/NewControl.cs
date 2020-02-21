using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WizzardControl.Controls
{
    public class NewControl : ContentView, INotifyPropertyChanged, IDisposable
    {
        #region WizzardPages bindable property

        public static BindableProperty WizzardPagesProperty = BindableProperty.Create(
            propertyName: "WizzardPages",
            returnType: typeof(List<TaxWizzardPage>),
            declaringType: typeof(WizzardView),
            defaultValue: null,
            propertyChanged: PropertyChanged);

        private static void PropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            //if (newvalue != null)
            //    (bindable as WizzardView).ReDraw();
        }

        public List<TaxWizzardPage> WizzardPages
        {
            get { return (List<TaxWizzardPage>) GetValue(WizzardPagesProperty); }
            set { SetValue(WizzardPagesProperty, value); }
        }

        #endregion

        #region ViewWidth bindable property

        public static BindableProperty ViewWidthProperty = BindableProperty.Create(
            propertyName: "ViewWidth",
            returnType: typeof(double),
            declaringType: typeof(WizzardView),
            propertyChanged: PropertyChanged2);

        private static void PropertyChanged2(BindableObject bindable, object oldvalue, object newvalue)
        {
            //if (newvalue != null)
            //    (bindable as WizzardView).ReDraw();
        }

        public double ViewWidth
        {
            get { return (double) GetValue(ViewWidthProperty); }
            set { SetValue(ViewWidthProperty, value); }
        }

        public ICommand SomeCOmmand => new Command( async () =>
        {
           await App.Current.MainPage.DisplayAlert ("Alert", "You have been alerted", "OK");
        });

        #endregion

        private Button _continueButton = new Button
        {
            Text = "Continue",
            VerticalOptions = LayoutOptions.EndAndExpand
        };

        private Button _startButton = new Button
        {
            Text = "Start",
            VerticalOptions = LayoutOptions.EndAndExpand
        };

        private int _step = 1;

        private DisabledScrollView _disabledScrollView = new DisabledScrollView
        {
            Orientation = ScrollOrientation.Horizontal,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
        };

        private double _deviceWidth;

        private Button btn = new Button
        {
            Text = "Click me!",
            FontSize = 40,
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand
        };

        public NewControl()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            _deviceWidth = mainDisplayInfo.Width / mainDisplayInfo.Density;

            _continueButton.Clicked += ContinueButtonOnClicked;
            _startButton.Clicked += StartButtonOnClicked;

            DrawStuff();
        }

        private void StartButtonOnClicked(object sender, EventArgs e)
        {
            _disabledScrollView.ScrollToAsync(0, 0, true);
            //_disabledScrollView.Children[_step]
                InvalidateMeasure();
            _step = 1;
        }

        private void ContinueButtonOnClicked(object sender, EventArgs e)
        {
            _disabledScrollView.ScrollToAsync(_deviceWidth * _step, 0, true);

            _step++;
        }

        private void DrawStuff()
        {
            var mainContainer = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var sl = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 0
            };

            sl.Children.Add(new StackLayout
            {
                BackgroundColor = Color.Orange,
                WidthRequest = _deviceWidth,
                HeightRequest = 500,
                Children = { new Label
                {
                    Text = "Page 1"
                }, new Button
                {
                    Text = "Click me!",
                    FontSize = 25,
                    Command = SomeCOmmand
                }, new Entry()}
            });

            btn.Clicked += async (sender, args) =>
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You have been alerted", "OKAY!");
            };

            sl.Children.Add(new StackLayout
            {
                BackgroundColor = Color.DarkKhaki,
                WidthRequest = _deviceWidth,
                HeightRequest = 500,
                Children = { new Label
                {
                    Text = "Page 2"
                }, btn }
            });

            sl.Children.Add(new StackLayout
            {
                BackgroundColor = Color.DarkKhaki,
                WidthRequest = _deviceWidth,
                HeightRequest = 500,
                Children = { new Label
                {
                    Text = "Page 3",
                }, new Button
                {
                    Text = "Click me!",
                    FontSize = 25,
                    Command = SomeCOmmand
                }}
            });


            sl.Children.Add(new StackLayout
            {
                BackgroundColor = Color.DarkKhaki,
                WidthRequest = _deviceWidth,
                HeightRequest = 500,
                Children = { new Label
                {
                    Text = "Page 4"
                }, new Button
                {
                    Text = "Click me!",
                    FontSize = 25,
                    Command = SomeCOmmand
                }}
            });


            _disabledScrollView.Content = sl;

            mainContainer.Children.Add(_disabledScrollView);
            mainContainer.Children.Add(_startButton);
            mainContainer.Children.Add(_continueButton);

            Content = mainContainer;
        }

        public void Dispose()
        {
            _continueButton.Clicked -= ContinueButtonOnClicked;
        }
    }
}