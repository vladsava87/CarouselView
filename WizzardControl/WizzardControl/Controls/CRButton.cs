using Xamarin.Forms.Internals;
using System.Windows.Input;
using Xamarin.Forms;

namespace WizzardControl.Controls.CheckboxRadioView
{
    [Preserve (AllMembers = true)]
    public class CRButton : View
    {
        public static BindableProperty TextProperty = BindableProperty.Create(
            propertyName: "Text",
            returnType: typeof(string),
            declaringType: typeof(CRButton));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: "TextColor",
            returnType: typeof(Color),
            declaringType: typeof(CRButton));

        public Color TextColor
        {
            get { return (Color) GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static BindableProperty CommandProperty = BindableProperty.Create(
            propertyName: "Command",
            returnType: typeof(ICommand),
            declaringType: typeof(CRButton));

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static BindableProperty ValueProperty = BindableProperty.Create(
            propertyName: "Value",
            returnType: typeof(string),
            declaringType: typeof(CRButton));

        public string Value
        {
            get { return (string) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static BindableProperty IsSelectedProperty = BindableProperty.Create(
            propertyName: "IsSelected",
            returnType: typeof(bool),
            declaringType: typeof(CRButton),
            defaultValue: false);

        public bool IsSelected
        {
            get { return (bool) GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static BindableProperty BackgroundColorActiveProperty = BindableProperty.Create(
            propertyName: "BackgroundColorActive",
            returnType: typeof(Color),
            declaringType: typeof(CRButton));

        public Color BackgroundColorActive
        {
            get { return (Color) GetValue(BackgroundColorActiveProperty); }
            set { SetValue(BackgroundColorActiveProperty, value); }
        }

        public static BindableProperty BackgroundColorInactiveProperty = BindableProperty.Create(
            propertyName: "BackgroundColorInactive",
            returnType: typeof(Color),
            declaringType: typeof(CRButton));

        public Color BackgroundColorInactive
        {
            get { return (Color) GetValue(BackgroundColorInactiveProperty); }
            set { SetValue(BackgroundColorInactiveProperty, value); }
        }

        public static BindableProperty IsRadioButtonProperty = BindableProperty.Create(
            propertyName: "IsRadioButton",
            returnType: typeof(bool),
            defaultValue: false,
            declaringType: typeof(CRButton));

        public bool IsRadioButton
        {
            get { return (bool) GetValue(IsRadioButtonProperty); }
            set { SetValue(IsRadioButtonProperty, value); }
        }

        #region FontSize bindable property

        public static BindableProperty FontSizeProperty = BindableProperty.Create(
            propertyName: "FontSize",
            returnType: typeof(float),
            declaringType: typeof(CRButton));

        public float FontSize
        {
            get { return (float) GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        #endregion
    }
}
