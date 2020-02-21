using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Input;
using WizzardControl.Controls.CheckboxRadioView;
using Xamarin.Forms;

namespace WizzardControl.Controls
{
    public class WizzardView : ContentView, INotifyPropertyChanged, IDisposable
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
            if (newvalue != null)
                (bindable as WizzardView).ReDraw();
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
            if (newvalue != null)
                (bindable as WizzardView).ReDraw();
        }

        public double ViewWidth
        {
            get { return (double) GetValue(ViewWidthProperty); }
            set { SetValue(ViewWidthProperty, value); }
        }
        
        #endregion

        private Button _continueButton = new Button
        {
            Text = "Continue",
        };

        private int _step = 0;

        private Button _backButton = new Button
        {
            Text = "Back",
        };

        private AbsoluteLayout _absoluteLayout = new AbsoluteLayout
        {
            CascadeInputTransparent = true,
        };

        public WizzardView()
        {
            _continueButton.Clicked += ContinueButtonOnClicked;
            btn.Clicked += BtnOnClicked;
            _backButton.Clicked += BackButtonOnClicked;
        }

        private async void BackButtonOnClicked(object sender, EventArgs e)
        {
            _continueButton.IsEnabled = true;
            ReDraw();
            _step = 0;
        }

        private void BtnOnClicked(object sender, EventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Hello", "IT works", "Ok");
        }

        private async void ContinueButtonOnClicked(object sender, EventArgs e)
        {
            //await _absoluteLayout.TranslateTo(_absoluteLayout.TranslationX - ViewWidth, 0, 200);
            
            var currentElement = (_absoluteLayout.Children[_step] as StackLayout);
            _step++;
            if (_step <= WizzardPages.Count)
            {
                var nextElement = (_absoluteLayout.Children[_step] as StackLayout);
                var newBoundsOld = new Rectangle(currentElement.Bounds.X - ViewWidth, currentElement.Bounds.Y, currentElement.Bounds.Width, currentElement.Bounds.Height);


                var newBounds = new Rectangle(nextElement.Bounds.X - ViewWidth, nextElement.Bounds.Y, nextElement.Bounds.Width, nextElement.Bounds.Height);
                currentElement.LayoutTo(newBoundsOld, 300, Easing.Linear); 
                nextElement.LayoutTo(newBounds, 300, Easing.Linear); 
            }
            

            //DrawinThisView(view);

            //AddWizzardPage();

            if (_step == WizzardPages.Count - 1)
            {
                _continueButton.IsEnabled = false;

            }
        }

        private void AddWizzardPage()
        {
            var pag = new ContentView
            {
                //InputTransparent = true
            };

            pag.Content = StackLayout();
            var st = StackLayout();

            AbsoluteLayout.SetLayoutBounds(st, new Rectangle(ViewWidth * _step,
                0, 
                ViewWidth,
                800));
            AbsoluteLayout.SetLayoutFlags(st, AbsoluteLayoutFlags.None);

            _absoluteLayout.Children.Add(st);
        }

        private void DrawinThisView(ContentView view)
        {
            view.Content = StackLayout();
        }

        private StackLayout StackLayout()
        {
            var st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                InputTransparent = true
            };

            st.Children.Add(new Label
            {
                Text = WizzardPages[_step].Title
            });

            st.Children.Add(new Label
            {
                Text = WizzardPages[_step].Info
            });

            st.Children.Add(new Label
            {
                Text = WizzardPages[_step].Info2
            });
            if (WizzardPages[_step].Items is List<ICRViewElements> items)
            {
                foreach (var item in items)
                {
                    if (item is CheckBoxElement)
                    {
                        var newButton = new CRButton()
                        {
                            Text = item.Text,
                            Value = item.Value,
                            TextColor = Color.Black,
                            FontSize = 18,
                            BackgroundColorInactive = Color.AliceBlue,
                            BackgroundColorActive = Color.Orange,
                            HeightRequest = 40,
                            Command = CheckBoxOnClickedCommand
                        };

                        st.Children.Add(newButton);
                    }

                    if (item is RadioButtonElement)
                    {
                        var newButton = new CRButton()
                        {
                            Text = item.Text,
                            Value = item.Value,
                            TextColor = Color.Black,
                            FontSize = 14,
                            IsRadioButton = true,
                            BackgroundColorInactive = Color.AliceBlue,
                            BackgroundColorActive = Color.Orange,
                            HeightRequest = 40,
                            Command = RadioOnClickedCommand
                        };

                        st.Children.Add(newButton);
                    }
                }
            }

            st.Children.Add(btn);
            return st;
        }


        //private void DrawHiddenContentView()
        //{
        //    _hiddenWizzardContentView.WidthRequest = ViewWidth;
        //    _hiddenWizzardContentView.BackgroundColor = Color.BlanchedAlmond;

        //    _hiddenWizzardContentView.Children.Add(new Label { Text = "box3" }, Constraint.RelativeToParent((parent) =>
        //        {
        //            return 0;
        //        }),
        //        Constraint.RelativeToParent((parent) =>
        //        {
        //            return 0;
        //        }),
        //        Constraint.Constant(50), Constraint.Constant(50));

        //    _hiddenWizzardContentView.Children.Add(new Label { Text = "box4" }, Constraint.RelativeToParent((parent) =>
        //        {
        //            return (.5 * parent.Width) - 50;
        //        }),
        //        Constraint.RelativeToParent((parent) =>
        //        {
        //            return (.5 * parent.Height) - 50;
        //        }),
        //        Constraint.Constant(50), Constraint.Constant(50));

        //    AbsoluteLayout.SetLayoutBounds(_hiddenWizzardContentView, new Rectangle(ViewWidth, 0, ViewWidth, 200));
        //    AbsoluteLayout.SetLayoutFlags(_hiddenWizzardContentView, AbsoluteLayoutFlags.None);
        //    _absoluteLayout.Children.Add(_hiddenWizzardContentView);
        //}

        private void ReDraw()
        {
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Star),
                    },
                    new RowDefinition
                    {
                        Height = new GridLength(10, GridUnitType.Star),
                    },
                    new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Star),
                    },
                    
                    new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Star),
                    }
                }
            };
            
            _absoluteLayout.Children.Clear();
            _absoluteLayout.WidthRequest = ViewWidth * 10;
            DrawWizzardContiner();
            
            grid.Children.Clear();

            grid.Children.Add(_absoluteLayout, 0, 1);
            grid.Children.Add(_backButton, 0, 2);
            grid.Children.Add(_continueButton, 0, 3);

            Content = grid;
        }

        public Button btn = new Button
        {
            Text = "Click me!",
            FontSize = 40
        };

        public ICommand CheckBoxOnClickedCommand => new Command<string>(CheckBoxOnClicked);
        public ICommand RadioOnClickedCommand => new Command<string>(RadioOnClicked);

        private void CheckBoxOnClicked(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                //ItemsSource.FirstOrDefault(item => item.Value == value).IsSelected
                //    = !ItemsSource.FirstOrDefault(item => item.Value == value).IsSelected;

                var cv = _absoluteLayout.Children[_step] as ContentView;

                var st = cv.Content as StackLayout;

                //if (ShowOnTwoColumns)
                //{
                //    foreach (var child in layout1.Children)
                //    {
                //        if (child is CRButton cbx)
                //        {
                //            if (cbx.Value != value && cbx.IsSelected)
                //            {
                //                continue;
                //            }
                //            else if (cbx.Value == value)
                //            {
                //                cbx.IsSelected = !cbx.IsSelected;

                //                if (cbx.IsSelected && !_results.Contains(value))
                //                {
                //                    _results.Add(value);
                //                }

                //                if (!cbx.IsSelected && _results.Contains(value))
                //                {
                //                    _results.Remove(value);
                //                }
                //            }
                //        }
                //    }

                //    foreach (var child in layout2.Children)
                //    {
                //        if (child is CRButton cbx)
                //        {
                //            if (cbx.Value != value && cbx.IsSelected)
                //            {
                //                continue;
                //            }
                //            else if (cbx.Value == value)
                //            {
                //                cbx.IsSelected = !cbx.IsSelected;

                //                if (cbx.IsSelected && !_results.Contains(value))
                //                {
                //                    _results.Add(value);
                //                }

                //                if (!cbx.IsSelected && _results.Contains(value))
                //                {
                //                    _results.Remove(value);
                //                }
                //            }
                //        }
                //    }
                //}
                //else
                {
                    //foreach (var child in layout.Children)
                    //{
                    //    if (child is CRButton cbx)
                    //    {
                    //        if (cbx.Value != value && cbx.IsSelected)
                    //        {
                    //            continue;
                    //        }
                    //        else if (cbx.Value == value)
                    //        {
                    //            cbx.IsSelected = !cbx.IsSelected;

                    //            if (cbx.IsSelected && !_results.Contains(value))
                    //            {
                    //                _results.Add(value);
                    //            }

                    //            if (!cbx.IsSelected && _results.Contains(value))
                    //            {
                    //                _results.Remove(value);
                    //            }
                    //        }
                    //    }
                    //}
                }

                //OnSelectedCommand?.Execute(_results);
            }
        } 


        private void RadioOnClicked(string obj)
        {
            //throw new NotImplementedException();
        }

        private void DrawWizzardContiner()
        {
            _absoluteLayout.Children.Clear();
            
            if (WizzardPages == null)
                return;

            var i = 0;

            foreach (var page in WizzardPages)
            {
                var pag = new StackLayout
                {
                    WidthRequest = ViewWidth,
                    BackgroundColor = Color.CadetBlue
                };

                var buntt = new Button
                {
                    Text = "new button"
                };
                buntt.Clicked += (sender, args) => { App.Current.MainPage.DisplayAlert("o", "m", "G"); };

                pag.Children.Add(buntt);
                //if (i == 0)
                {
                    //pag.Children.Add(new Label { Text = $"box1 {i}" }, Constraint.RelativeToParent((parent) =>
                    //    {
                    //        return 0;
                    //    }),
                    //    Constraint.RelativeToParent((parent) =>
                    //    {
                    //        return 0;
                    //    }),
                    //    Constraint.Constant(50), Constraint.Constant(50));

                    //pag.Children.Add(new Label { Text = $"box2 {i}" }, Constraint.RelativeToParent((parent) =>
                    //    {
                    //        return (.5 * parent.Width) - 50;
                    //    }),
                    //    Constraint.RelativeToParent((parent) =>
                    //    {
                    //        return (.5 * parent.Height) - 50;
                    //    }),
                    //    Constraint.Constant(50), Constraint.Constant(50));


                    var st = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical
                    };

                    pag.Children.Add(new Label
                    {
                        Text = page.Title
                    });

                    pag.Children.Add(new Label
                    {
                        Text = page.Info
                    });

                    pag.Children.Add(new Label
                    {
                        Text = page.Info2
                    });

                    

                    if (page.Items is List<ICRViewElements> items)
                    {
                        foreach (var item in items)
                        {
                            if (item is CheckBoxElement)
                            {
                                var newButton = new CRButton()
                                {
                                    Text = item.Text,
                                    Value = item.Value,
                                    TextColor = Color.Black,
                                    FontSize = 18,
                                    BackgroundColorInactive = Color.AliceBlue,
                                    BackgroundColorActive = Color.Orange,
                                    HeightRequest = 40,
                                    Command = CheckBoxOnClickedCommand
                                };

                                pag.Children.Add(newButton);
                            }

                            if (item is RadioButtonElement)
                            {
                                var newButton = new CRButton()
                                {
                                    Text = item.Text,
                                    Value = item.Value,
                                    TextColor = Color.Black,
                                    FontSize = 14,
                                    IsRadioButton = true,
                                    BackgroundColorInactive = Color.AliceBlue,
                                    BackgroundColorActive = Color.Orange,
                                    HeightRequest = 40,
                                    Command = RadioOnClickedCommand
                                };

                                pag.Children.Add(newButton);
                            }
                        }
                    }
                    
                    //pag.Content = st;
                }

                AbsoluteLayout.SetLayoutBounds(pag, new Rectangle((i == 0) ? i : ViewWidth,
                    0, 
                    ViewWidth,
                    800));
                AbsoluteLayout.SetLayoutFlags(pag, AbsoluteLayoutFlags.None);

                _absoluteLayout.Children.Add(pag);
                i++;
            }
            //DrawHiddenContentView();
        }

        public void Dispose()
        {
            _continueButton.Clicked -= ContinueButtonOnClicked;
        }
    }
}