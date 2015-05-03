using System;
using System.Windows.Forms;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;


namespace OxyPlotDrawingToolbar
{
    public partial class ExampleForm : Form
    {
        /* Number of series to display. */
        private const int NumSeries = 10;
        /* Number of points to generate per series. */
        private const int NumPoints = 25;
        /* Minimum x value for function series. */
        private const int MinimumX = 0;
        /* Maximum x value for function series. */
        private const int MaximumX = 50;

        public ExampleForm()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            // Create the plot model
            var model = new PlotModel
            {
                DefaultColors = OxyPalettes.HueDistinct(NumSeries).Colors,
                IsLegendVisible = true,
                Title = "Draw on me!"
            };

            // Add a basic xAxis
            model.Axes.Add(new LinearAxis
            {
                IsPanEnabled = true,
                IsZoomEnabled = true,
                Key = "xAxis",
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = "X"
            });

            // And a basic yAxis
            model.Axes.Add(new LinearAxis
            {
                IsPanEnabled = true,
                IsZoomEnabled = true,
                Key = "yAxis",
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = "Y"
            });

            // And generate some interesting data on the chart to play with
            //for (int s = 0; s < NumSeries; s++)
            //{
            //    switch (s % 3)
            //    {
            //        case 0:
            //            model.Series.Add(new FunctionSeries(x => Math.Sin(x + s),
            //                                                MinimumX,
            //                                                MaximumX,
            //                                                NumPoints) {YAxisKey = "yAxis"});
            //            break;

            //        case 1:
            //            model.Series.Add(new FunctionSeries(x => Math.Sin((x + s) / 4)
            //                                                     * Math.Acos(Math.Sin(x + s)),
            //                                                MinimumX,
            //                                                MaximumX,
            //                                                NumPoints) {YAxisKey = "yAxis"});
            //            break;

            //        case 2:
            //            model.Series.Add(new FunctionSeries(
            //                                 x => Math.Sin(2 * Math.Cos(2
            //                                      * Math.Sin(2 * Math.Cos(x + s)))),
            //                                 MinimumX,
            //                                 MaximumX,
            //                                 NumPoints) { YAxisKey = "yAxis" });
            //            break;
            //    }
            //}

            var controller = new PlotController();

            // For fun, make it so you can copy the chart with Ctrl+C
            controller.BindKeyDown(OxyKey.C,
                                   OxyModifierKeys.Control,
                                   new DelegatePlotCommand<OxyKeyEventArgs>(CopyChart_OnKeyDown));

            uiPlotView.Controller = controller;
            uiPlotView.Model = model;
            uiDrawingToolbar.PlotController = controller;
            uiDrawingToolbar.PlotModel = model;

            // Add saving and printing events to the drawing toolbar
            uiDrawingToolbar.uiSaveButton.Click += uiSaveButton_OnClick;
            uiDrawingToolbar.uiPrintButton.Click += uiPrintButton_OnClick;
        }
    }
}
