using MigroApp.Controls;
using MigroApp.iOS.Renders;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MigroViewCell), typeof(MigroViewCellRender_iOS))]
namespace MigroApp.iOS.Renders
{
    public class MigroViewCellRender_iOS : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            cell.SelectedBackgroundView = new UIView {
                Opaque = false,
                BackgroundColor = UIColor.White
            };

            return cell;
        }
    }
}
