﻿using System;
using UIKit;
using Foundation;
using ExtendedCells.Forms.Plugin.Abstractions;
using Xamarin.Forms.Platform.iOS;
using ExtendedCells.Forms.Plugin.iOSUnified;

namespace ExtendedCells.Forms.Plugin.Models
{
	/// <summary>
	/// Sample of a custom cell layout, taken from the iOS docs at
	/// http://developer.xamarin.com/guides/ios/user_interface/tables/part_3_-_customizing_a_table's_appearance/
	/// </summary>
	public class NativeiOSCell : UITableViewCell
	{
		UILabel leftTextUILabel, leftDetailUILabel, rightTextUILabel, rightDetailUILabel;

		TwoColumnCell twoColumnCell;

		public NativeiOSCell (NSString cellId, TwoColumnCell twoColumnCell) : base (UITableViewCellStyle.Default, cellId)
		{
			this.twoColumnCell = twoColumnCell;
			SelectionStyle = UITableViewCellSelectionStyle.Default;

			leftTextUILabel = new UILabel ();
			leftDetailUILabel = new UILabel ();
			rightTextUILabel = new UILabel ();
			rightDetailUILabel = new UILabel ();

			ContentView.Add (leftTextUILabel);
			ContentView.Add (rightTextUILabel);

			if (!string.IsNullOrEmpty (twoColumnCell.LeftDetail) || !string.IsNullOrEmpty (twoColumnCell.RightDetail)) {
				ContentView.Add (leftDetailUILabel);
				ContentView.Add (rightDetailUILabel);
			}
		}

		public void UpdateCell (TwoColumnCell twoColumnCell)
		{
			this.twoColumnCell = twoColumnCell;
			ContentView.BackgroundColor = twoColumnCell.BackgroundColor.ToUIColor ();

			leftTextUILabel.UpdateFromFormsControl (twoColumnCell.LeftText, twoColumnCell.LeftTextAlignment, twoColumnCell.LeftTextColor, twoColumnCell.LeftTextFont);
			leftDetailUILabel.UpdateFromFormsControl (twoColumnCell.LeftDetail, twoColumnCell.LeftDetailAlignment, twoColumnCell.LeftDetailColor, twoColumnCell.LeftDetailFont);
			rightTextUILabel.UpdateFromFormsControl (twoColumnCell.RightText, twoColumnCell.RightTextAlignment, twoColumnCell.RightTextColor, twoColumnCell.RightTextFont);
			rightDetailUILabel.UpdateFromFormsControl (twoColumnCell.RightDetail, twoColumnCell.RightDetailAlignment, twoColumnCell.RightDetailColor, twoColumnCell.RightDetailFont);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			var widthWithMargins = ContentView.Bounds.Width- (nfloat)(twoColumnCell.Thickness.Left + twoColumnCell.Thickness.Right);
			var leftColumnXWithMargin = ContentView.Bounds.X + (nfloat)(twoColumnCell.Thickness.Left);

			ContentView.Bounds = new CoreGraphics.CGRect(leftColumnXWithMargin,ContentView.Bounds.Y, widthWithMargins, ContentView.Bounds.Height);

			var leftColumnWidth = (float)(ContentView.Bounds.Width * twoColumnCell.LeftColumnWidth.Value);
			var rightColumnWidth = (float)(ContentView.Bounds.Width * twoColumnCell.RightColumnWidth.Value);
			var rightColumnX = leftColumnWidth + leftColumnXWithMargin;

			var firstRowHeight = ContentView.Bounds.Height / 2;
			var secondRowHeight = ContentView.Bounds.Height / 2;


			if (!string.IsNullOrEmpty (twoColumnCell.LeftDetail) || !string.IsNullOrEmpty (twoColumnCell.LeftDetail)) {

				leftTextUILabel.Frame = new CoreGraphics.CGRect (leftColumnXWithMargin, 0, leftColumnWidth, firstRowHeight);
				leftDetailUILabel.Frame = new CoreGraphics.CGRect (leftColumnXWithMargin, firstRowHeight, leftColumnWidth, secondRowHeight);

				rightTextUILabel.Frame = new CoreGraphics.CGRect (rightColumnX, 0, rightColumnWidth, firstRowHeight);
				rightDetailUILabel.Frame = new CoreGraphics.CGRect (rightColumnX, firstRowHeight, rightColumnWidth, secondRowHeight);

			} else {
				leftTextUILabel.Frame = new CoreGraphics.CGRect (leftColumnXWithMargin, 0, leftColumnWidth, ContentView.Bounds.Height);
				rightTextUILabel.Frame = new CoreGraphics.CGRect (rightColumnX, 0, rightColumnWidth, ContentView.Bounds.Height);
			}
		}
	}

}
