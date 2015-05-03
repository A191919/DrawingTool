using OxyPlot;
using OxyPlot.Annotations;


namespace OxyPlotDrawingToolbar.AnnotDrawingManipulators
{
    public class DrawAnnotManipulator : MouseManipulator
    {
        protected const int HoverAlpha = 120;
        protected const AnnotationLayer HoverLayer = AnnotationLayer.AboveSeries;
        protected const LineStyle HoverLineStyle = LineStyle.Dash;
        protected const int HoverLineThickness = 3;
        protected readonly OxyColor HoverColor = OxyColor.FromAColor(HoverAlpha, OxyColors.DarkGreen);
        protected Annotation DrawAnnot;
        protected readonly AnnotDrawingToolBar DrawingToolBar;

        public DrawAnnotManipulator(IPlotView plotView, AnnotDrawingToolBar drawingToolBar)
            : base(plotView)
        {
            DrawingToolBar = drawingToolBar;
        }
    }
}
