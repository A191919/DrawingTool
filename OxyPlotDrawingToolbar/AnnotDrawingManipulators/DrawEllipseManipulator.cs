using System.Linq;

using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.WindowsForms;
using System.Diagnostics;
using System;
using System.Collections.Generic;


namespace OxyPlotDrawingToolbar.AnnotDrawingManipulators
{
    public class EllipseAnnotationOverride : EllipseAnnotation
    {
        private OxyRect screenRectangle;
        private OxyRect screenRectangle1;
        private OxyRect screenRectangle3;

        private bool first;
        private bool second;
        private bool third;

        double MouseXRight;
        double MouseXLeft;
        double MouseYUp;
        double MouseYDown;
        double StartWidth;
        double StartHeight;
        double StartX;
        double StartY;
        public bool CanSee;
        protected override HitTestResult HitTestOverride(HitTestArguments args)
        {
            if (this.screenRectangle.Contains(args.Point))
            {
                second = true;
                HitTestResult HTR = new HitTestResult(this, args.Point);
                EllipseAnnotation Rectum = HTR.Element as EllipseAnnotation;

                this.PlotModel.SelectionColor = OxyColor.FromAColor(30, OxyColors.LightSkyBlue);
                //MouseXRight = Math.Sqrt((XAxis.InverseTransform(args.Point.X) - (Rectum.X - Rectum.Width)) * (XAxis.InverseTransform(args.Point.X) - (Rectum.X - Rectum.Width)));
                //MouseXLeft = Math.Sqrt(((Rectum.X + Rectum.Width) - XAxis.InverseTransform(args.Point.X)) * ((Rectum.X + Rectum.Width) - XAxis.InverseTransform(args.Point.X)));
                //MouseYDown = Math.Sqrt(((Rectum.Y + Rectum.Height) - YAxis.InverseTransform(args.Point.Y)) * ((Rectum.Y + Rectum.Height) - YAxis.InverseTransform(args.Point.Y)));
                //MouseYUp = Math.Sqrt((YAxis.InverseTransform(args.Point.Y) - (Rectum.Y - Rectum.Height)) * (YAxis.InverseTransform(args.Point.Y) - (Rectum.Y - Rectum.Height)));
                return HTR;
            }
            if (this.screenRectangle1.Contains(args.Point))
            {
                first = true;
                HitTestResult HTR = new HitTestResult(this, args.Point);
                EllipseAnnotation Rectum = HTR.Element as EllipseAnnotation;
                StartWidth = Rectum.Width;
                StartHeight = Rectum.Height;
                StartX = Rectum.X;
                StartY = Rectum.Y;
                return HTR;
            }
            if (this.screenRectangle3.Contains(args.Point))
            {
                third = true;
                HitTestResult HTR = new HitTestResult(this, args.Point);
                EllipseAnnotation Rectum = HTR.Element as EllipseAnnotation;
                StartWidth = Rectum.Width;
                StartHeight = Rectum.Height;
                StartX = Rectum.X;
                StartY = Rectum.Y;
                return HTR ;
            }
            return null;
        }
        protected override void OnMouseMove(OxyMouseEventArgs e)
        {
            if (first & CanSee)
            {
                HitTestResult Test = new HitTestResult(this, e.Position);

                double StartPositionCorrectX = StartX - StartWidth / 2;
                double Xresult = Math.Sqrt((XAxis.InverseTransform(e.Position.X) - StartPositionCorrectX) * (XAxis.InverseTransform(e.Position.X) - StartPositionCorrectX));
                if (StartPositionCorrectX > XAxis.InverseTransform(e.Position.X))
                {
                    (Test.Element as EllipseAnnotation).X = StartX - Xresult / 2;
                    (Test.Element as EllipseAnnotation).Width = StartWidth + Xresult;
                }
                else
                {
                    (Test.Element as EllipseAnnotation).X = StartX + Xresult / 2;
                    (Test.Element as EllipseAnnotation).Width = StartWidth - Xresult;
                }

                double StartPositionCorrectY = StartY - StartHeight / 2;
                double Yresult = Math.Sqrt((YAxis.InverseTransform(e.Position.Y) - StartPositionCorrectY) * (YAxis.InverseTransform(e.Position.Y) - StartPositionCorrectY));
                if (StartPositionCorrectY < YAxis.InverseTransform(e.Position.Y))
                {
                    (Test.Element as EllipseAnnotation).Y = StartY + Yresult / 2;
                    (Test.Element as EllipseAnnotation).Height = StartHeight - Yresult;
                }
                else
                {
                    (Test.Element as EllipseAnnotation).Y = StartY - Yresult / 2;
                    (Test.Element as EllipseAnnotation).Height = StartHeight + Yresult;
                }
                this.PlotModel.InvalidatePlot(false);
            }
            //if (second)
            //{
            //    HitTestResult Test = new HitTestResult(this, e.Position);
            //    if (((Test.Element as EllipseAnnotation).MaximumY < (Test.Element as EllipseAnnotation).MinimumY) && ((Test.Element as EllipseAnnotation).MinimumX < (Test.Element as EllipseAnnotation).MaximumX))
            //    {
            //        (Test.Element as EllipseAnnotation).MinimumX = (XAxis.InverseTransform(e.Position.X) - MouseXRight);
            //        (Test.Element as EllipseAnnotation).MaximumX = (XAxis.InverseTransform(e.Position.X) + MouseXLeft);
            //        (Test.Element as EllipseAnnotation).MinimumY = (YAxis.InverseTransform(e.Position.Y) + MouseYUp);
            //        (Test.Element as EllipseAnnotation).MaximumY = (YAxis.InverseTransform(e.Position.Y) - MouseYDown);
            //    }
            //    if (((Test.Element as EllipseAnnotation).MaximumY < (Test.Element as EllipseAnnotation).MinimumY) && ((Test.Element as EllipseAnnotation).MinimumX > (Test.Element as EllipseAnnotation).MaximumX))
            //    {
            //        (Test.Element as EllipseAnnotation).MinimumX = (XAxis.InverseTransform(e.Position.X) + MouseXRight);
            //        (Test.Element as EllipseAnnotation).MaximumX = (XAxis.InverseTransform(e.Position.X) - MouseXLeft);
            //        (Test.Element as EllipseAnnotation).MinimumY = (YAxis.InverseTransform(e.Position.Y) + MouseYUp);
            //        (Test.Element as EllipseAnnotation).MaximumY = (YAxis.InverseTransform(e.Position.Y) - MouseYDown);
            //    }
            //    if (((Test.Element as EllipseAnnotation).MaximumY > (Test.Element as EllipseAnnotation).MinimumY) && ((Test.Element as EllipseAnnotation).MinimumX < (Test.Element as EllipseAnnotation).MaximumX))
            //    {
            //        (Test.Element as EllipseAnnotation).MinimumX = (XAxis.InverseTransform(e.Position.X) - MouseXRight);
            //        (Test.Element as EllipseAnnotation).MaximumX = (XAxis.InverseTransform(e.Position.X) + MouseXLeft);
            //        (Test.Element as EllipseAnnotation).MinimumY = (YAxis.InverseTransform(e.Position.Y) - MouseYUp);
            //        (Test.Element as EllipseAnnotation).MaximumY = (YAxis.InverseTransform(e.Position.Y) + MouseYDown);
            //    }
            //    if (((Test.Element as EllipseAnnotation).MaximumY > (Test.Element as EllipseAnnotation).MinimumY) && ((Test.Element as EllipseAnnotation).MinimumX > (Test.Element as EllipseAnnotation).MaximumX))
            //    {
            //        (Test.Element as EllipseAnnotation).MinimumX = (XAxis.InverseTransform(e.Position.X) + MouseXRight);
            //        (Test.Element as EllipseAnnotation).MaximumX = (XAxis.InverseTransform(e.Position.X) - MouseXLeft);
            //        (Test.Element as EllipseAnnotation).MinimumY = (YAxis.InverseTransform(e.Position.Y) - MouseYUp);
            //        (Test.Element as EllipseAnnotation).MaximumY = (YAxis.InverseTransform(e.Position.Y) + MouseYDown);
            //    }
            //    if (this.PlotModel != null)
            //        this.PlotModel.InvalidatePlot(false);
            //}
            if (third & CanSee)
            {
                HitTestResult Test = new HitTestResult(this, e.Position);
                
                double StartPositionCorrectX = StartX + StartWidth / 2;
                double Xresult = Math.Sqrt((XAxis.InverseTransform(e.Position.X) - StartPositionCorrectX) * (XAxis.InverseTransform(e.Position.X) - StartPositionCorrectX));
                if (StartPositionCorrectX < XAxis.InverseTransform(e.Position.X))
                {
                    (Test.Element as EllipseAnnotation).X = StartX + Xresult / 2;
                    (Test.Element as EllipseAnnotation).Width = StartWidth + Xresult;
                }
                else
                {
                    (Test.Element as EllipseAnnotation).X = StartX - Xresult / 2;
                    (Test.Element as EllipseAnnotation).Width = StartWidth - Xresult;
                }
                double StartPositionCorrectY = StartY + StartHeight / 2;
                double Yresult = Math.Sqrt((YAxis.InverseTransform(e.Position.Y) - StartPositionCorrectY) * (YAxis.InverseTransform(e.Position.Y) - StartPositionCorrectY));
                if (StartPositionCorrectY > YAxis.InverseTransform(e.Position.Y))
                {
                    (Test.Element as EllipseAnnotation).Y = StartY - Yresult / 2;
                    (Test.Element as EllipseAnnotation).Height = StartHeight - Yresult;
                }
                else
                {
                    (Test.Element as EllipseAnnotation).Y = StartY + Yresult / 2;
                    (Test.Element as EllipseAnnotation).Height = StartHeight + Yresult;
                }
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
        public override void Render(IRenderContext rc, PlotModel model)
        {
            base.Render(rc, model);

            this.screenRectangle = new OxyRect(this.Transform(this.X - (this.Width / 2), this.Y - (this.Height / 2)), this.Transform(this.X + (this.Width / 2), this.Y + (this.Height / 2)));
            this.screenRectangle1 = new OxyRect(this.Transform(this.X - (this.Width / 2), this.Y - (this.Height / 2)), this.Transform(this.X - (this.Width / 2) + 1, this.Y - (this.Height / 2) + 2));
            this.screenRectangle3 = new OxyRect(this.Transform(this.X + (this.Width / 2), this.Y + (this.Height / 2)), this.Transform(this.X + (this.Width / 2) + 1, this.Y + (this.Height / 2) + 2));
            var clippingRectangle = this.GetClippingRect();


            rc.DrawClippedEllipse(
    clippingRectangle,
    this.screenRectangle,
    this.GetSelectableFillColor(this.Fill),
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
                    this.TextHorizontalAlignment,
                    this.TextVerticalAlignment);
            }
        }
    }
    internal class DrawEllipseManipulator : DrawAnnotManipulator
    {
        public DrawEllipseManipulator(IPlotView plotView, AnnotDrawingToolBar drawingToolBar)
            : base(plotView, drawingToolBar)
        {
        }
        public override void Completed(OxyMouseEventArgs args)
        {
            var annot = DrawAnnot as EllipseAnnotationOverride;
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
            var annot = DrawAnnot as EllipseAnnotationOverride;
            if (annot == null)
                return;

            annot.Height = (YAxis.InverseTransform(args.Position.Y) - annot.Y) * 2;
            annot.Width = (XAxis.InverseTransform(args.Position.X) - annot.X) * 2;

            PlotView.InvalidatePlot(false);
        }

        public override void Started(OxyMouseEventArgs args)
        {
            XAxis = PlotView.ActualModel.Axes.First(a => a.IsHorizontal());
            YAxis = PlotView.ActualModel.Axes.First(a => a.IsVertical());

            DrawAnnot = new EllipseAnnotationOverride
            {
                Height = 0,
                Width = 0,
                X = XAxis.InverseTransform(args.Position.X),
                Y = YAxis.InverseTransform(args.Position.Y)
            };



            PlotView.ActualModel.Annotations.Add(DrawAnnot);
            PlotView.InvalidatePlot(false);
        }
    }
}
