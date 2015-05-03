using System.Linq;

using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.WindowsForms;


namespace OxyPlotDrawingToolbar.AnnotDrawingManipulators
{
    internal class DrawPolygonManipulator : DrawAnnotManipulator
    {
        public DrawPolygonManipulator(IPlotView plotView, AnnotDrawingToolBar drawingToolBar)
            : base(plotView, drawingToolBar)
        {
        }

        public override void Completed(OxyMouseEventArgs args)
        {
            var polyAnnot = new PolygonAnnotation
            {
                Fill = OxyColor.FromAColor(DrawingToolBar.DrawFillAlpha,
                                           DrawingToolBar.DrawFillColor.ToOxyColor()),
                Layer = DrawingToolBar.DrawLayer,
                Text = DrawingToolBar.DrawText
            };
            polyAnnot.Points.AddRange(((PolylineAnnotation) DrawAnnot).Points);

            PlotView.ActualModel.Annotations.Remove(DrawAnnot);
            PlotView.ActualModel.Annotations.Add(polyAnnot);

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
                StrokeThickness = HoverLineThickness
            };

            ((PolylineAnnotation) DrawAnnot).Points.Add(
                XAxis.InverseTransform(args.Position.X, args.Position.Y, YAxis));

            PlotView.ActualModel.Annotations.Add(DrawAnnot);
            PlotView.InvalidatePlot(false);
        }
    }
}
