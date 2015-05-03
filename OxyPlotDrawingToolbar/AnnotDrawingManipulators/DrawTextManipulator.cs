using System;
using System.Linq;
using System.Windows.Forms;

using OxyPlot;
using OxyPlot.Annotations;


namespace OxyPlotDrawingToolbar.AnnotDrawingManipulators
{
    internal class DrawTextManipulator : DrawAnnotManipulator
    {
        public DrawTextManipulator(IPlotView plotView, AnnotDrawingToolBar drawingToolBar)
            : base(plotView, drawingToolBar)
        {
        }
        public override void Completed(OxyMouseEventArgs args)
        {
            var annot = DrawAnnot as TextAnnotation;
            if (annot == null)
                return;

            annot.Background = OxyColors.Undefined;

            if (string.IsNullOrEmpty(DrawingToolBar.DrawText))
                using (var d = new InputTextDialog())
                    if (d.ShowDialog() == DialogResult.OK)
                        annot.Text = d.DisplayText;
                    else
                        PlotView.ActualModel.Annotations.Remove(DrawAnnot);
            else
                annot.Text = DrawingToolBar.DrawText;

            DrawAnnot = null;
            PlotView.InvalidatePlot(false);
        }

        public override void Delta(OxyMouseEventArgs args)
        {
            var annot = DrawAnnot as TextAnnotation;
            if (annot == null)
                return;

            double x = annot.TextPosition.X;
            double y = annot.TextPosition.Y;

            annot.TextRotation = Math.Atan2(args.Position.Y - YAxis.Transform(y),
                                            args.Position.X - XAxis.Transform(x)) * 180 / Math.PI;

            PlotView.InvalidatePlot(false);
        }

        public override void Started(OxyMouseEventArgs args)
        {
            XAxis = PlotView.ActualModel.Axes.First(a => a.IsHorizontal());
            YAxis = PlotView.ActualModel.Axes.First(a => a.IsVertical());

            DrawAnnot = new TextAnnotation
            {
                Background = HoverColor,
                Stroke = OxyColors.Undefined,
                Text = "Move mouse to rotate.",
                TextPosition = XAxis.InverseTransform(args.Position.X, args.Position.Y, YAxis)
            };

            PlotView.ActualModel.Annotations.Add(DrawAnnot);
            PlotView.InvalidatePlot(false);
        }
    }
}
