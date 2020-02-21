using System;
using System.ComponentModel;
using MigroApp.Controls.CheckboxRadioView;
using MigroApp.iOS.Renders;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CRButton), typeof(CRButtonRenderer_iOS))]
namespace MigroApp.iOS.Renders
{
    class CRButtonRenderer_iOS : ViewRenderer<CRButton, UIView>
    {
        private UILabel _buttonTextLabel;
        private UILabel _buttonIconLabel;
        private UIColor _activeBackgroundColor;
        private UIColor _backgroundColor;
        private UIButton _rootView;

        protected override void OnElementChanged(ElementChangedEventArgs<CRButton> e)
        {
            base.OnElementChanged(e);

            _rootView = CreateLayout();

            AddTouchBehavior();

            // Tell Xamarin to user our layout for the control
            SetNativeControl(_rootView);
        }

        private UIButton CreateLayout()
        {
            _rootView = new UIButton
            {
                BackgroundColor = Element?.BackgroundColor.ToUIColor(),                
                UserInteractionEnabled = true
            };

            if (Element != null)
            {
                _buttonIconLabel = new UILabel
                {
                    TextColor = Element.TextColor.ToUIColor(),
                    TranslatesAutoresizingMaskIntoConstraints = false,
                };
                _buttonIconLabel.Font = UIFont.FromName("Material Design Icons", Element.FontSize);
                SetIcon();
                _rootView.AddSubview(_buttonIconLabel);
                _buttonIconLabel.TextAlignment = UITextAlignment.Center;
                _buttonIconLabel.TopAnchor.ConstraintEqualTo(_rootView.TopAnchor).Active = true;
                _buttonIconLabel.TopAnchor.ConstraintEqualTo(_rootView.BottomAnchor).Active = true;
                _buttonIconLabel.LeftAnchor.ConstraintEqualTo(_rootView.LeftAnchor).Active = true;

                _buttonTextLabel = new UILabel
                {
                    Text = Element.Text,
                    TextColor = Element.TextColor.ToUIColor(),
                    TranslatesAutoresizingMaskIntoConstraints = false,
                };
                _buttonTextLabel.Font = _buttonTextLabel.Font.WithSize(Element.FontSize);
                _rootView.AddSubview(_buttonTextLabel);
                _buttonTextLabel.TopAnchor.ConstraintEqualTo(_rootView.TopAnchor).Active = true;
                _buttonTextLabel.LeftAnchor.ConstraintEqualTo(_buttonIconLabel.RightAnchor).Active = true;
                _buttonTextLabel.RightAnchor.ConstraintEqualTo(_rootView.RightAnchor).Active = true;

                _activeBackgroundColor = Element.BackgroundColorActive.ToUIColor();
                _backgroundColor = Element.BackgroundColorInactive.ToUIColor();
            }

            return _rootView;
        }

        private void AddTouchBehavior()
        {
            var tapGesture = new UITapGestureRecognizer(() => Element.Command?.Execute(Element.Value));
            _rootView.AddGestureRecognizer(tapGesture);

            _rootView.TouchDown += AddTouchBackgroundColor;
            _rootView.TouchDragEnter += AddTouchBackgroundColor;
            _rootView.TouchUpInside += RemoveTouchBackgroundColor;
            _rootView.TouchCancel += RemoveTouchBackgroundColor;
            _rootView.TouchDragExit += RemoveTouchBackgroundColor;
        }

        private void AddTouchBackgroundColor(object sender, EventArgs e)
        {
            (sender as UIView).BackgroundColor = (Element.IsSelected) ? _activeBackgroundColor.ColorWithAlpha(.5f) : _backgroundColor.ColorWithAlpha(.5f);
        }

        private void RemoveTouchBackgroundColor(object sender, EventArgs e)
        {
            (sender as UIView).BackgroundColor = (Element.IsSelected) ? _activeBackgroundColor : _backgroundColor;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CRButton.IsSelectedProperty.PropertyName)
            {
                SetIcon();
                _rootView.BackgroundColor = (Element.IsSelected) ? _activeBackgroundColor : _backgroundColor;
            }
        }

        private void SetIcon()
        {
            if (Element.IsRadioButton)
            {
                _buttonIconLabel.Text = (Element.IsSelected) ? "" : "";
            }
            else
            {
                _buttonIconLabel.Text = (Element.IsSelected) ? "" : "";
            }
        }
    }
}