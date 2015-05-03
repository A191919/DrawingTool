using System.Linq;

using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.WindowsForms;


namespace OxyPlotDrawingToolbar.AnnotDrawingManipulators
{
    internal class DrawPolylineManipulator : DrawAnnotManipulator
    {
        public DrawPolylineManipulator(IPlotView plotView, AnnotDrawingToolBar drawingToolBar)
            : base(plotView, drawingToolBar)
        {
        }

        public override void Completed(OxyMouseEventArgs args)
        {
            var annot = DrawAnnot as PolylineAnnotation;
            if (annot == null)
                return;

            annot.Color = OxyColor.FromAColor(DrawingToolBar.DrawLineAlpha,
                                              DrawingToolBar.DrawLineColor.ToOxyColor());
            annot.Layer = DrawingToolBar.DrawLayer;
            annot.LineStyle = DrawingToolBar.DrawLineStyle;
            annot.StrokeThickness = DrawingToolBar.DrawLineThickness;
            annot.Text = DrawingToolBar.DrawText;

            DrawAnnot = null;
            PlotView.InvalidatePlot(false);
        }

        public override void Delta(OxyMouseEventArgs args)
        {
            ((PolylineAnnotation) DrawAnnot).Points.Add(
                XAxis.InverseTransform(args.Position.X, args.Position.Y, YAxis));

            PlotView.InvalidatePlot(false);
        }

        public override void Started(OxyMouseEventArgs args)
        {
            XAxis = PlotView.ActualModel.Axes.First(a => a.IsHorizontal());
            YAxis = PlotView.ActualModel.Axes.First(a => a.IsVertical());

            DrawAnnot = new PolylineAnnotation
            {
                Color = HoverColor,
                Layer = HoverLayer,
                LineStyle = HoverLineStyle,
                StrokeThickness = HoverLineThickness,
            };

            ((PolylineAnnotation) DrawAnnot).Points.Add(
                XAxis.InverseTransform(args.Position.X, args.Position.Y, YAxis));

            PlotView.ActualModel.Annotations.Add(DrawAnnot);
            PlotView.InvalidatePlot(false);
        }
    }
}
