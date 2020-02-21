using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WizzardControl
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            WizzardControl.WizzardPages = GetTaxWizzardPages();// new List<object>
            //{
            //    new {Name = "page 1", Order = 1, ViewConente = new ContentView
            //    {
            //        Content = new Label{ Text =  "page 1 values"}
            //    }},
            //    new {Name = "page 2", Order = 2, ViewConente = new ContentView
            //    {
            //        Content = new Label{ Text =  "page 1 values"}
            //    }},
            //    new {Name = "page 3", Order = 3, ViewConente = new ContentView
            //    {
            //        Content = new Label{ Text =  "page 1 values"}
            //    }},
            //    new {Name = "page 4", Order = 4, ViewConente = new ContentView
            //    {
            //        Content = new Label{ Text =  "page 1 values"}
            //    }},
            //};
            
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            WizzardControl.ViewWidth = mainDisplayInfo.Width / mainDisplayInfo.Density;
        }

        public List<TaxWizzardPage> GetTaxWizzardPages(EventHandler<object> OnWizzardPageInvoke = null)
        {
            var radioYear = new List<ICRViewElements>();

            radioYear.Add(new RadioButtonElement
            {
                Text = "2006",
                Value = "2006",
            });
                radioYear.Add(new RadioButtonElement
                {
                    Text = "2008",
                    Value = "2008",
                });
                radioYear.Add(new RadioButtonElement
                {
                    Text = "2009",
                    Value = "2009",
                });
                radioYear.Add(new RadioButtonElement
                {
                    Text = "2010",
                    Value = "2010",
                });

            var checkBoxMonths = new List<ICRViewElements>();
            checkBoxMonths.Add(new CheckBoxElement
            {
                Text = "ianuarie",
                Value = "1"
            });
            checkBoxMonths.Add(new CheckBoxElement
            {
                Text = "februarie",
                Value = "2"
            });
            checkBoxMonths.Add(new CheckBoxElement
            {
                Text = "martie",
                Value = "3"
            });
            checkBoxMonths.Add(new CheckBoxElement
            {
                Text = "aprilie",
                Value = "4"
            });
            checkBoxMonths.Add(new CheckBoxElement
            {
                Text = "mai",
                Value = "5"
            });
            checkBoxMonths.Add(new CheckBoxElement
            {
                Text = "iunie",
                Value = "6"
            });
            checkBoxMonths.Add(new CheckBoxElement
            {
                Text = "iulie",
                Value = "7"
            });
            checkBoxMonths.Add(new CheckBoxElement
            {
                Text = "august",
                Value = "8"
            });
            checkBoxMonths.Add(new CheckBoxElement
            {
                Text = "septembrie",
                Value = "9"
            });
            checkBoxMonths.Add(new CheckBoxElement
            {
                Text = "octombrie",
                Value = "10"
            });
            checkBoxMonths.Add(new CheckBoxElement
            {
                Text = "noiembrie",
                Value = "11"
            });
            checkBoxMonths.Add(new CheckBoxElement
            {
                Text = "decembrie",
                Value = "12"
            });

            var wpAb = new TaxWizzardPage( OnWizzardPageInvoke)
            {
                Order = 0,
                Title = $"Calculator de impozit",
                Info =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                    "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            };
            
            var wpYear = new TaxWizzardPage( OnWizzardPageInvoke)
            {
                Order = 1,
                Title = $"Pas {1}",
                Info =
                "Alegeti anul in cara ati lucrat:",
                Items = radioYear
            }; 
            
            var wpMonths = new TaxWizzardPage( OnWizzardPageInvoke)
            {
                ListValues = new Dictionary<int, string>(),
                Order = 2,
                Title = $"Pas {2}",
                Info =
                    "Alegeti perioada cat ati lucrat:",
                Items = checkBoxMonths,
                ShowTwoColumns = true,
            };

            var wpAmount = new TaxWizzardPage( OnWizzardPageInvoke)
            {
                ListValues = new Dictionary<int, string>(),
                Order = 3,
                Title = $"Pas {3}",
                Info =
                    "Alegeti salariul brut/luna lucrata:",
                Min = 1000,
                Max = 2000,
                Step = 100
            };

            var wpEnd = new TaxWizzardPage( OnWizzardPageInvoke)
            {
                Order = 4,
                Title = $"Calculator de impozit",
            };

            return new List<TaxWizzardPage>
            {
                wpAb, wpYear, wpMonths, 
                wpAmount, wpEnd
            };
        }
    }
}
