using System.Linq;

using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.WindowsForms;
using System.Windows.Forms;


namespace OxyPlotDrawingToolbar.AnnotDrawingManipulators
{
    internal class DrawLineManipulator : DrawAnnotManipulator
    {
        public DrawLineManipulator(IPlotView plotView, AnnotDrawingToolBar drawingToolBar)
            : base(plotView, drawingToolBar)
        {
        }
        public override void Completed(OxyMouseEventArgs args)
        {
            var annot = DrawAnnot as LineAnnotation;
            if (annot == null)
                return;

            //annot.Color = OxyColor.FromAColor(DrawingToolBar.DrawLineAlpha,
            //                                  DrawingToolBar.DrawLineColor.ToOxyColor());
            annot.Layer = DrawingToolBar.DrawLayer;
            annot.LineStyle = DrawingToolBar.DrawLineStyle;
            annot.StrokeThickness = DrawingToolBar.DrawLineThickness;
            annot.Text = DrawingToolBar.DrawText;
            DrawAnnot = null;
            PlotView.InvalidatePlot(false);
        }
        public override void Delta(OxyMouseEventArgs args)
        {
            var annot = DrawAnnot as LineAnnotation;
            if (annot == null)
                return;

            switch (DrawingToolBar.DrawLimitType)
            {
                case AnnotDrawingToolBar.LimitType.None:
                    double x1 = annot.X;
                    double x2 = XAxis.InverseTransform(args.Position.X);
                    double y1 = annot.Y;
                    double y2 = YAxis.InverseTransform(args.Position.Y);
                    double slope = (y2 - y1) / (x2 - x1);

                    annot.Intercept = y2 - slope * x2;
                    annot.Slope = slope;
                    break;

                case AnnotDrawingToolBar.LimitType.Vertical:
                    annot.X = XAxis.InverseTransform(args.Position.X);
                    break;

                case AnnotDrawingToolBar.LimitType.Horizontal:
                    annot.Y = YAxis.InverseTransform(args.Position.Y);
                    break;
            }

            PlotView.InvalidatePlot(false);
        }

        public override void Started(OxyMouseEventArgs args)
        {
            XAxis = PlotView.ActualModel.Axes.First(a => a.IsHorizontal());
            YAxis = PlotView.ActualModel.Axes.First(a => a.IsVertical());

            DrawAnnot = new LineAnnotation
            {
                Color = HoverColor,
                Layer = HoverLayer,
                LineStyle = HoverLineStyle,
                StrokeThickness = HoverLineThickness
            };

            var annot = DrawAnnot as LineAnnotation;
            switch (DrawingToolBar.DrawLimitType)
            {
                case AnnotDrawingToolBar.LimitType.None:
                    annot.X = XAxis.InverseTransform(args.Position.X);
                    annot.Y = YAxis.InverseTransform(args.Position.Y);
                    annot.Intercept = YAxis.InverseTransform(args.Position.Y);
                    annot.Slope = 0;
                    break;

                case AnnotDrawingToolBar.LimitType.Vertical:
                    annot.Type = LineAnnotationType.Vertical;
                    annot.X = XAxis.InverseTransform(args.Position.X);
                    break;

                case AnnotDrawingToolBar.LimitType.Horizontal:
                    annot.Type = LineAnnotationType.Horizontal;
                    annot.Y = YAxis.InverseTransform(args.Position.Y);
                    break;
            }

            PlotView.ActualModel.Annotations.Add(DrawAnnot);

            PlotView.InvalidatePlot(false);
        }
    }
}
