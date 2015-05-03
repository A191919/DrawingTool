using System.Linq;

using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.WindowsForms;


namespace OxyPlotDrawingToolbar.AnnotDrawingManipulators
{
    public class DrawArrowManipulator : DrawAnnotManipulator
    {
        public DrawArrowManipulator(IPlotView plotView, AnnotDrawingToolBar drawingToolBar)
            : base(plotView, drawingToolBar)
        {
        }

        public override void Completed(OxyMouseEventArgs args)
        {
            var annot = DrawAnnot as ArrowAnnotation;
            if (annot == null)
                return;

            annot.Color = OxyColor.FromAColor(DrawingToolBar.DrawLineAlpha,
                                              DrawingToolBar.DrawLineColor.ToOxyColor());
            annot.Layer = DrawingToolBar.DrawLayer;
            annot.LineStyle = DrawingToolBar.DrawLineStyle;
            annot.StrokeThickness = DrawingToolBar.DrawLineThickness;
            annot.Text = DrawingToolBar.DrawText;

            PlotView.InvalidatePlot(false);
        }

        public override void Delta(OxyMouseEventArgs args)
        {
            ((ArrowAnnotation) DrawAnnot).EndPoint =
                XAxis.InverseTransform(args.Position.X, args.Position.Y, YAxis);
            PlotView.InvalidatePlot(false);
        }

        public override void Started(OxyMouseEventArgs args)
        {
            XAxis = PlotView.ActualModel.Axes.First(a => a.IsHorizontal());
            YAxis = PlotView.ActualModel.Axes.First(a => a.IsVertical());

            DrawAnnot = new ArrowAnnotation
            {
                Color = HoverColor,
                Layer = HoverLayer,
                EndPoint = XAxis.InverseTransform(args.Position.X, args.Position.Y, YAxis),
                StartPoint = XAxis.InverseTransform(args.Position.X, args.Position.Y, YAxis)
            };

            PlotView.ActualModel.Annotations.Add(DrawAnnot);
            PlotView.InvalidatePlot(false);
        }
    }
}
