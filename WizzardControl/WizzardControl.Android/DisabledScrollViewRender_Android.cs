using Android.Content;
using Android.Views;
using WizzardControl.Controls;
using WizzardControl.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DisabledScrollView), typeof(DisabledScrollViewRender_Android))]
namespace WizzardControl.Droid
{
    public class DisabledScrollViewRender_Android : ScrollViewRenderer
    {
        private readonly Context _context;

        public DisabledScrollViewRender_Android(Context context) : base(context)
        {
            _context = context;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev) 
        { 
            return OnTouchEvent(ev);
        }

        public override bool OnTouchEvent(MotionEvent ev)
        {
            if (ev.Action == MotionEventActions.Move)
            {
                return true;
            }

            else
            {
                return false;
            }

            return base.OnInterceptTouchEvent(ev);
        }
    }
}