using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Android.Widget;
using WizzardControl.Controls.CheckboxRadioView;
using WizzardControl.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CRButton), typeof(CRButtonRenderer_Android))]
namespace WizzardControl.Droid
{
    public class CRButtonRenderer_Android : ViewRenderer<CRButton, Android.Views.View>
    {
        private readonly Context _context;
        private TextView _buttonTextView;
        private TextView _buttonIconView;
        private Android.Views.View _rootLayout;

        public CRButtonRenderer_Android(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CRButton> e)
        {
            base.OnElementChanged(e);
            var t = _context.ApplicationContext.Assets;
            var font = Typeface.CreateFromAsset(_context.ApplicationContext.Assets, "materialdesignicons-webfont.ttf");

            if (Control == null)
            {
                // Inflate the IncrementingButton layout
                var inflater = _context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                _rootLayout = inflater.Inflate(Resource.Layout.CRButton, null, false);

                // Set text on Textviews
                _buttonTextView = _rootLayout.FindViewById<TextView>(Resource.Id.ButtonText);
                _buttonTextView.Text = Element.Text;
                _buttonTextView.SetTextSize(ComplexUnitType.Dip, Element.FontSize);
                _buttonTextView.SetTextColor(Element.TextColor.ToAndroid());
                _buttonIconView = _rootLayout.FindViewById<TextView>(Resource.Id.ButtonIcon);
                _buttonIconView.Typeface = font;
                _buttonIconView.SetTextSize(ComplexUnitType.Dip, Element.FontSize);
                SetIcon();
                _buttonIconView.SetTextColor(Element.TextColor.ToAndroid());

                SetBackground();

                // Tell Xamarin to user our layout for the control
                SetNativeControl(_rootLayout);

                // Execute the bound command on click
                _rootLayout.Click += (s, a) => Element.Command?.Execute(Element.Value);
            }
        }

        private void SetBackground(bool IsSelected = false)
        {
            // Get the background color from Forms element
            var backgroundColor = Element.BackgroundColorActive.ToAndroid();

            // Create statelist to handle ripple effect
            var enabledBackground = new GradientDrawable(GradientDrawable.Orientation.LeftRight, new int[] { backgroundColor, backgroundColor });
            enabledBackground.SetCornerRadius(14);
            //var activestateList = new StateListDrawable();
            var activeDrawable = new RippleDrawable(ColorStateList.ValueOf(Android.Graphics.Color.White),
                enabledBackground,
                null);

            //activestateList.AddState(new[] { Android.Resource.Attribute.StateEnabled }, rippleItem);

            var disabledbackgroundColor = Element.BackgroundColorInactive.ToAndroid();
            var disabledBackground = new GradientDrawable(GradientDrawable.Orientation.LeftRight, new int[] { disabledbackgroundColor, disabledbackgroundColor });
            disabledBackground.SetCornerRadius(14);
            var inactiveDrawable = new RippleDrawable(ColorStateList.ValueOf(Android.Graphics.Color.White),
                disabledBackground,
                null);

            // Assign background
            if (IsSelected)
            {
                _rootLayout.Background = activeDrawable;
                _buttonTextView.SetTextColor(Element.BackgroundColorInactive.ToAndroid());
                _buttonIconView.SetTextColor(Element.BackgroundColorInactive.ToAndroid());
            }
            else
            {
                _rootLayout.Background = inactiveDrawable;
                _buttonTextView.SetTextColor(Element.TextColor.ToAndroid());
                _buttonIconView.SetTextColor(Element.TextColor.ToAndroid());
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CRButton.IsSelectedProperty.PropertyName)
            {
                SetIcon();
                SetBackground(Element.IsSelected);
            }
        }

        private void SetIcon()
        {
            if (Element.IsRadioButton)
            {
                _buttonIconView.Text = (Element.IsSelected) ? "" : "";
            }
            else
            {
                _buttonIconView.Text = (Element.IsSelected) ? "" : "";
            }
        }
    }
}