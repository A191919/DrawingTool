using System.Linq;
using System;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.WindowsForms;
using System.Diagnostics;

namespace OxyPlotDrawingToolbar.AnnotDrawingManipulators
{

    public class RectangleAnnotationOverride : RectangleAnnotation
    {
        private OxyRect screenRectangle;
        private OxyRect screenRectangle1;

        private OxyRect screenRectangle3;


        private bool first;
        private bool second;
        private bool third;

        public bool CanSee;
        double MouseXRight;
        double MouseXLeft;
        double MouseYUp;
        double MouseYDown;

        public override void Render(IRenderContext rc, PlotModel model)
        {
            base.Render(rc, model);

            double x0 = double.IsNaN(this.MinimumX) || this.MinimumX.Equals(double.MinValue)
                            ? this.XAxis.ActualMinimum
                            : this.MinimumX;
            double x1 = double.IsNaN(this.MaximumX) || this.MaximumX.Equals(double.MaxValue)
                            ? this.XAxis.ActualMaximum
                            : this.MaximumX;
            double y0 = double.IsNaN(this.MinimumY) || this.MinimumY.Equals(double.MinValue)
                            ? this.YAxis.ActualMinimum
                            : this.MinimumY;
            double y1 = double.IsNaN(this.MaximumY) || this.MaximumY.Equals(double.MaxValue)
                            ? this.YAxis.ActualMaximum
                            : this.MaximumY;

            this.screenRectangle = new OxyRect(this.Transform(x0, y0), this.Transform(x1, y1));
            this.screenRectangle1 = new OxyRect(this.Transform(x0 - 0.5, y0 - 1), this.Transform(x0 + 0.5, y0 + 1));
            this.screenRectangle3 = new OxyRect(this.Transform(x1 - 0.5, y1 - 1), this.Transform(x1 + 0.5, y1 + 1));


            var clippingRectangle = this.GetClippingRect();

            rc.DrawClippedRectangle(
                clippingRectangle,
                this.screenRectangle,
                OxyColors.Transparent,
                OxyColors.LightBlue,
                2.0);
            if (CanSee)
            {
                rc.DrawClippedRectangle(
                    clippingRectangle,
                    this.screenRectangle1,
                    OxyColors.Red,
                    this.GetSelectableColor(this.Stroke),
                    this.StrokeThickness);

                rc.DrawClippedRectangle(
                    clippingRectangle,
                    this.screenRectangle3,
                    OxyColors.Red,
                    this.GetSelectableColor(this.Stroke),
                    this.StrokeThickness);

            }


            if (!string.IsNullOrEmpty(this.Text))
            {
                var textPosition = this.GetActualTextPosition(() => this.screenRectangle.Center);
                rc.DrawClippedText(
                    clippingRectangle,
                    textPosition,
                    this.Text,
                    this.ActualTextColor,
                    this.ActualFont,
                    this.ActualFontSize,
                    this.ActualFontWeight,
                    this.TextRotation,
                    HorizontalAlignment.Center,
                    VerticalAlignment.Middle);
            }
        }
        protected override void OnMouseMove(OxyMouseEventArgs e)
        {
            if (first & CanSee)
            {
                HitTestResult Test = new HitTestResult(this, e.Position);
                (Test.Element as RectangleAnnotation).MinimumX = XAxis.InverseTransform(e.Position.X);
                (Test.Element as RectangleAnnotation).MinimumY = YAxis.InverseTransform(e.Position.Y);
                this.PlotModel.InvalidatePlot(false);
            }
            if (second)
            {
                HitTestResult Test = new HitTestResult(this, e.Position);
                if (((Test.Element as RectangleAnnotation).MaximumY < (Test.Element as RectangleAnnotation).MinimumY) && ((Test.Element as RectangleAnnotation).MinimumX < (Test.Element as RectangleAnnotation).MaximumX))
                {
                    (Test.Element as RectangleAnnotation).MinimumX = (XAxis.InverseTransform(e.Position.X) - MouseXRight );
                    (Test.Element as RectangleAnnotation).MaximumX = (XAxis.InverseTransform(e.Position.X) + MouseXLeft );
                    (Test.Element as RectangleAnnotation).MinimumY = (YAxis.InverseTransform(e.Position.Y) + MouseYUp );
                    (Test.Element as RectangleAnnotation).MaximumY = (YAxis.InverseTransform(e.Position.Y) - MouseYDown );
                }
                if (((Test.Element as RectangleAnnotation).MaximumY < (Test.Element as RectangleAnnotation).MinimumY) && ((Test.Element as RectangleAnnotation).MinimumX > (Test.Element as RectangleAnnotation).MaximumX))
                {
                    (Test.Element as RectangleAnnotation).MinimumX = (XAxis.InverseTransform(e.Position.X) + MouseXRight);
                    (Test.Element as RectangleAnnotation).MaximumX = (XAxis.InverseTransform(e.Position.X) - MouseXLeft);
                    (Test.Element as RectangleAnnotation).MinimumY = (YAxis.InverseTransform(e.Position.Y) + MouseYUp);
                    (Test.Element as RectangleAnnotation).MaximumY = (YAxis.InverseTransform(e.Position.Y) - MouseYDown);
                }
                if (((Test.Element as RectangleAnnotation).MaximumY > (Test.Element as RectangleAnnotation).MinimumY) && ((Test.Element as RectangleAnnotation).MinimumX < (Test.Element as RectangleAnnotation).MaximumX))
                {
                    (Test.Element as RectangleAnnotation).MinimumX = (XAxis.InverseTransform(e.Position.X) -MouseXRight);
                    (Test.Element as RectangleAnnotation).MaximumX = (XAxis.InverseTransform(e.Position.X) + MouseXLeft);
                    (Test.Element as RectangleAnnotation).MinimumY = (YAxis.InverseTransform(e.Position.Y) - MouseYUp);
                    (Test.Element as RectangleAnnotation).MaximumY = (YAxis.InverseTransform(e.Position.Y) + MouseYDown);
                }
                if (((Test.Element as RectangleAnnotation).MaximumY > (Test.Element as RectangleAnnotation).MinimumY) && ((Test.Element as RectangleAnnotation).MinimumX > (Test.Element as RectangleAnnotation).MaximumX))
                {
                    (Test.Element as RectangleAnnotation).MinimumX = (XAxis.InverseTransform(e.Position.X) + MouseXRight);
                    (Test.Element as RectangleAnnotation).MaximumX = (XAxis.InverseTransform(e.Position.X) - MouseXLeft);
                    (Test.Element as RectangleAnnotation).MinimumY = (YAxis.InverseTransform(e.Position.Y) - MouseYUp);
                    (Test.Element as RectangleAnnotation).MaximumY = (YAxis.InverseTransform(e.Position.Y) + MouseYDown);
                }
                if (this.PlotModel!=null)
                this.PlotModel.InvalidatePlot(false);
            }
            if (third & CanSee)
            {
                HitTestResult Test = new HitTestResult(this, e.Position);

                (Test.Element as RectangleAnnotation).MaximumX = XAxis.InverseTransform(e.Position.X);
                (Test.Element as RectangleAnnotation).MaximumY = YAxis.InverseTransform(e.Position.Y);
                this.PlotModel.InvalidatePlot(false);
            }
        }
        protected override void OnMouseUp(OxyMouseEventArgs e)
        {
            first = false;
            second = false;
            third = false;
            base.OnMouseUp(e);
        }
        protected override HitTestResult HitTestOverride(HitTestArguments args)
        {
            if (this.screenRectangle.Contains(args.Point))
            {
                second = true;
                HitTestResult HTR = new HitTestResult(this, args.Point);
                RectangleAnnotation Rectum = HTR.Element as RectangleAnnotation;
                this.PlotModel.SelectionColor = OxyColor.FromAColor(30, OxyColors.LightSkyBlue);
                MouseXRight = Math.Sqrt((XAxis.InverseTransform(args.Point.X) - Rectum.MinimumX) * (XAxis.InverseTransform(args.Point.X) - Rectum.MinimumX));
                MouseXLeft = Math.Sqrt((Rectum.MaximumX - XAxis.InverseTransform(args.Point.X)) * (Rectum.MaximumX - XAxis.InverseTransform(args.Point.X)));
                MouseYDown = Math.Sqrt((Rectum.MaximumY - YAxis.InverseTransform(args.Point.Y)) * (Rectum.MaximumY - YAxis.InverseTransform(args.Point.Y)));
                MouseYUp = Math.Sqrt((YAxis.InverseTransform(args.Point.Y) - Rectum.MinimumY) * (YAxis.InverseTransform(args.Point.Y) - Rectum.MinimumY));
                return HTR;
            }
            if (this.screenRectangle1.Contains(args.Point))
            {
                first = true;
                return new HitTestResult(this, args.Point);
            }
            if (this.screenRectangle3.Contains(args.Point))
            {
                third = true;
                return new HitTestResult(this, args.Point);
            }


            return null;
        }
    }

    internal class DrawRectangleManipulator : DrawAnnotManipulator
    {
        public DrawRectangleManipulator(IPlotView plotView, AnnotDrawingToolBar drawingToolBar)
            : base(plotView, drawingToolBar)
        {
        }

        public override void Completed(OxyMouseEventArgs args)
        {
            var annot = DrawAnnot as RectangleAnnotationOverride;
            if (annot == null)
                return;
            annot.Fill = OxyColor.FromAColor(0, OxyColors.LightSkyBlue);
            annot.Layer = DrawingToolBar.DrawLayer;
            annot.Text = DrawingToolBar.DrawText;

            DrawAnnot = null;
            PlotView.InvalidatePlot(false);
        }

        public override void Delta(OxyMouseEventArgs args)
        {
            var annot = DrawAnnot as RectangleAnnotationOverride;
            if (annot == null)
                return;

            if (DrawingToolBar.DrawLimitType == AnnotDrawingToolBar.LimitType.None
                || DrawingToolBar.DrawLimitType == AnnotDrawingToolBar.LimitType.Vertical)
                annot.MaximumX = XAxis.InverseTransform(args.Position.X);
            if (DrawingToolBar.DrawLimitType == AnnotDrawingToolBar.LimitType.None
                || DrawingToolBar.DrawLimitType == AnnotDrawingToolBar.LimitType.Horizontal)
                annot.MaximumY = YAxis.InverseTransform(args.Position.Y);

            PlotView.InvalidatePlot(false);
        }

        public override void Started(OxyMouseEventArgs args)
        {
            XAxis = PlotView.ActualModel.Axes.First(a => a.IsHorizontal());
            YAxis = PlotView.ActualModel.Axes.First(a => a.IsVertical());

            DrawAnnot = new RectangleAnnotationOverride
            {
                Fill = HoverColor,
                Layer = HoverLayer
            };

            var annot = DrawAnnot as RectangleAnnotationOverride;
            if (DrawingToolBar.DrawLimitType == AnnotDrawingToolBar.LimitType.None
                || DrawingToolBar.DrawLimitType == AnnotDrawingToolBar.LimitType.Vertical)
            {
                annot.MinimumX = XAxis.InverseTransform(args.Position.X);
                annot.MaximumX = XAxis.InverseTransform(args.Position.X);
            }
            if (DrawingToolBar.DrawLimitType == AnnotDrawingToolBar.LimitType.None
                || DrawingToolBar.DrawLimitType == AnnotDrawingToolBar.LimitType.Horizontal)
            {
                annot.MinimumY = YAxis.InverseTransform(args.Position.Y);
                annot.MaximumY = YAxis.InverseTransform(args.Position.Y);
            }

            PlotView.ActualModel.Annotations.Add(DrawAnnot);
            PlotView.InvalidatePlot(false);
        }
    }
}
