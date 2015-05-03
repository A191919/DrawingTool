using System;
using System.Linq;
using System.Windows.Forms;

using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.WindowsForms;

using OxyPlotDrawingToolbar.AnnotDrawingManipulators;


namespace OxyPlotDrawingToolbar
{
    sealed partial class AnnotDrawingToolBar
    {
        /* Temporary command used to save the command added to the PlotController to remove later */
        private IViewCommand<OxyMouseDownEventArgs> _tempCommand;

        private void DeleteAnnot_OnMouseDown(object sender, OxyMouseDownEventArgs args)
        {
            if (args.ChangedButton != OxyMouseButton.Left)
                return;

            PlotModel.Annotations.Remove((Annotation) sender);
            PlotModel.InvalidatePlot(false);
            args.Handled = true;
        }

        private void SelectAnnot_OnMouseDown(object sender, OxyMouseDownEventArgs args)
        {
            if (((Annotation) sender).IsSelected())
            { }
                //((Annotation) sender).Unselect();
            else
                ((Annotation) sender).Select();

            PlotModel.InvalidatePlot(false);
            args.Handled = true;
        }

        private void uiDeleteAllAnnotsButton_OnClick(object sender, EventArgs args)
        {
            PlotModel.Annotations.Clear();
            PlotModel.InvalidatePlot(false);
        }

        private void uiDeleteAnnotButton_OnCheckedChanged(object sender, EventArgs args)
        {
            RestoreDefaultToolbarSetup();

            if (((ToolStripButton) sender).Checked)
            {
                ResetItemsToggle((ToolStripButton) sender);
                foreach (Annotation annot in PlotModel.Annotations)
                    annot.MouseDown += DeleteAnnot_OnMouseDown;
            }
            else
            {
                foreach (Annotation annot in PlotModel.Annotations)
                    annot.MouseDown -= DeleteAnnot_OnMouseDown;
            }
        }

        private void uiDrawArrowButton_OnCheckedChanged(object sender, EventArgs args)
        {
            RestoreDefaultToolbarSetup();

            if (((ToolStripButton) sender).Checked)
            {
                ResetItemsToggle((ToolStripButton) sender);
                ShowPolylineEditingToolbarItems();

                var arrowManipulator = new DrawArrowManipulator(PlotModel.PlotView, this);
                _tempCommand =
                    new DelegatePlotCommand<OxyMouseDownEventArgs>(
                        (view, controller, cargs) =>
                            controller.AddMouseManipulator(view, arrowManipulator, cargs));
                PlotController.BindMouseDown(OxyMouseButton.Left,
                                             OxyModifierKeys.None,
                                             _tempCommand);
            }
            else
            {
                if (_tempCommand == null)
                    return;

                PlotController.Unbind(_tempCommand);
                _tempCommand = null;
            }
        }

        private void uiDrawEllipseButton_OnCheckedChanged(object sender, EventArgs args)
        {
            RestoreDefaultToolbarSetup();

            if (((ToolStripButton) sender).Checked)
            {
                ResetItemsToggle((ToolStripButton) sender);
                ShowShapeEditingToolbarItems();

                var ellipseManipulator = new DrawEllipseManipulator(PlotModel.PlotView, this);
                _tempCommand =new DelegatePlotCommand<OxyMouseDownEventArgs>((view, controller, cargs) => controller.AddMouseManipulator(view, ellipseManipulator, cargs));
                PlotController.BindMouseDown(OxyMouseButton.Left,OxyModifierKeys.None,_tempCommand);
            }
            else
            {
                if (_tempCommand == null)
                    return;

                PlotController.Unbind(_tempCommand);
                _tempCommand = null;
            }
        }

        private void uiDrawLineButton_OnCheckedChanged(object sender, EventArgs args)
        {
            RestoreDefaultToolbarSetup();

            if (((ToolStripButton) sender).Checked)
            {
                ResetItemsToggle((ToolStripButton) sender);
                ShowLineEditingToolbarItems();

                var lineManipulator = new DrawLineManipulator(PlotModel.PlotView, this);
                _tempCommand =
                    new DelegatePlotCommand<OxyMouseDownEventArgs>(
                        (view, controller, cargs) =>
                            controller.AddMouseManipulator(view, lineManipulator, cargs));
                PlotController.BindMouseDown(OxyMouseButton.Left,
                                             OxyModifierKeys.None,
                                             _tempCommand);
            }
            else
            {
                if (_tempCommand == null)
                    return;

                PlotController.Unbind(_tempCommand);
                _tempCommand = null;
            }
        }

        private void uiDrawPolygonButton_OnCheckedChanged(object sender, EventArgs args)
        {
            RestoreDefaultToolbarSetup();

            if (((ToolStripButton) sender).Checked)
            {
                ResetItemsToggle((ToolStripButton) sender);
                ShowShapeEditingToolbarItems();

                var polygonManipulator = new DrawPolygonManipulator(PlotModel.PlotView, this);
                _tempCommand =
                    new DelegatePlotCommand<OxyMouseDownEventArgs>(
                        (view, controller, cargs) =>
                            controller.AddMouseManipulator(view, polygonManipulator, cargs));
                PlotController.BindMouseDown(OxyMouseButton.Left,
                                             OxyModifierKeys.None,
                                             _tempCommand);
            }
            else
            {
                if (_tempCommand == null)
                    return;

                PlotController.Unbind(_tempCommand);
                _tempCommand = null;
            }
        }

        private void uiDrawPolylineButton_OnCheckedChanged(object sender, EventArgs args)
        {
            RestoreDefaultToolbarSetup();

            if (((ToolStripButton) sender).Checked)
            {
                ResetItemsToggle((ToolStripButton) sender);
                ShowPolylineEditingToolbarItems();

                var polylineManipulator = new DrawPolylineManipulator(PlotModel.PlotView, this);
                _tempCommand =
                    new DelegatePlotCommand<OxyMouseDownEventArgs>(
                        (view, controller, cargs) =>
                            controller.AddMouseManipulator(view, polylineManipulator, cargs));
                PlotController.BindMouseDown(OxyMouseButton.Left,
                                             OxyModifierKeys.None,
                                             _tempCommand);
            }
            else
            {
                if (_tempCommand == null)
                    return;

                PlotController.Unbind(_tempCommand);
                _tempCommand = null;
            }
        }

        private void uiDrawRectangleButton_OnCheckedChanged(object sender, EventArgs args)
        {
            RestoreDefaultToolbarSetup();

            if (((ToolStripButton) sender).Checked)
            {
                ResetItemsToggle((ToolStripButton) sender);
                ShowRectangleEditingToolbarItems();

                var rectManipulator = new DrawRectangleManipulator(PlotModel.PlotView, this);
                _tempCommand =
                    new DelegatePlotCommand<OxyMouseDownEventArgs>(
                        (view, controller, cargs) =>
                            controller.AddMouseManipulator(view, rectManipulator, cargs));
                PlotController.BindMouseDown(OxyMouseButton.Left,
                                             OxyModifierKeys.None,
                                             _tempCommand);
            }
            else
            {
                if (_tempCommand == null)
                    return;

                PlotController.Unbind(_tempCommand);
                _tempCommand = null;
            }
        }

        private void uiDrawTextButton_OnCheckedChanged(object sender, EventArgs args)
        {
            RestoreDefaultToolbarSetup();

            if (((ToolStripButton) sender).Checked)
            {
                ResetItemsToggle((ToolStripButton) sender);
                ShowTextEditingToolbarItems();

                var textManipulator = new DrawTextManipulator(PlotModel.PlotView, this);
                _tempCommand =
                    new DelegatePlotCommand<OxyMouseDownEventArgs>(
                        (view, controller, cargs) =>
                            controller.AddMouseManipulator(view, textManipulator, cargs));
                PlotController.BindMouseDown(OxyMouseButton.Left,
                                             OxyModifierKeys.None,
                                             _tempCommand);
            }
            else
            {
                if (_tempCommand == null)
                    return;

                PlotController.Unbind(_tempCommand);
                _tempCommand = null;
            }
        }

        private void uiSelectAnnotButton_OnCheckedChanged(object sender, EventArgs args)
        {
            RestoreDefaultToolbarSetup();
            
            if (((ToolStripButton) sender).Checked)
            {
                ResetItemsToggle((ToolStripButton) sender);
                ShowAnnotEditingToolbarItems();
                foreach (Annotation a in PlotModel.Annotations)
                {
                    a.MouseDown += SelectAnnot_OnMouseDown;
                }
                foreach (Annotation item in PlotModel.Annotations)
                {
                    RectangleAnnotationOverride Rectangle = item as RectangleAnnotationOverride;
                    if (Rectangle != null)
                    {
                        Rectangle.CanSee = true;
                    }
                    PlotModel.InvalidatePlot(false);
                }
                foreach (Annotation item in PlotModel.Annotations)
                {
                    EllipseAnnotationOverride Rectangle = item as EllipseAnnotationOverride;
                    if (Rectangle != null)
                    {
                        Rectangle.CanSee = true;
                    }
                    PlotModel.InvalidatePlot(false);
                }
            }
            else
            {
                foreach (Annotation a in PlotModel.Annotations)
                    a.MouseDown -= SelectAnnot_OnMouseDown;
                foreach (Annotation item in PlotModel.Annotations)
                {
                    EllipseAnnotationOverride Rectangle = item as EllipseAnnotationOverride;
                    if (Rectangle != null)
                    {
                        Rectangle.CanSee = false;
                    }
                    PlotModel.InvalidatePlot(false);
                }
                foreach (Annotation item in PlotModel.Annotations)
                {
                    RectangleAnnotationOverride Rectangle = item as RectangleAnnotationOverride;
                    if (Rectangle != null)
                    {
                        Rectangle.CanSee = false;
                    }
                    PlotModel.InvalidatePlot(false);
                }

            }
        }

        private void uiSetFillAlphaTextBox_OnKeyDown(object sender, KeyEventArgs args)
        {
            if (args.KeyCode != Keys.Enter)
                return;

            foreach (Annotation a in PlotModel.Annotations.Where(a => a.IsSelected()))
            {
                var ellipseAnnot = a as EllipseAnnotation;
                if (ellipseAnnot != null)
                {
                    ellipseAnnot.Fill =
                        OxyColor.FromAColor(byte.Parse(uiSetFillAlphaTextBox.Text),
                                            ellipseAnnot.Fill);
                }

                var polyAnnot = a as PolygonAnnotation;
                if (polyAnnot != null)
                {
                    polyAnnot.Fill =
                        OxyColor.FromAColor(byte.Parse(uiSetFillAlphaTextBox.Text), polyAnnot.Fill);
                }

                var rectAnnot = a as RectangleAnnotation;
                if (rectAnnot != null)
                {
                    rectAnnot.Fill =
                        OxyColor.FromAColor(byte.Parse(uiSetFillAlphaTextBox.Text), rectAnnot.Fill);
                }
            }

            PlotModel.InvalidatePlot(false);
        }

        private void uiSetFillColorButton_OnClick(object sender, EventArgs args)
        {
            using (var d = new ColorDialog())
            {
                d.Color = uiSetFillColorButton.BackColor;

                if (d.ShowDialog() != DialogResult.OK)
                    return;

                uiSetFillColorButton.BackColor = d.Color;

                if (uiSelectAnnotButton.Checked)
                {
                    foreach (Annotation a in PlotModel.Annotations.Where(a => a.IsSelected()))
                    {
                        var ellipseAnnot = a as EllipseAnnotation;
                        if (ellipseAnnot != null)
                        {
                            ellipseAnnot.Fill =
                                OxyColor.FromAColor(ellipseAnnot.Fill.A, d.Color.ToOxyColor());
                        }

                        var polygonAnnot = a as PolygonAnnotation;
                        if (polygonAnnot != null)
                        {
                            polygonAnnot.Fill =
                                OxyColor.FromAColor(polygonAnnot.Fill.A, d.Color.ToOxyColor());
                        }

                        var rectAnnot = a as RectangleAnnotation;
                        if (rectAnnot != null)
                        {
                            rectAnnot.Fill =
                                OxyColor.FromAColor(rectAnnot.Fill.A, d.Color.ToOxyColor());
                        }
                    }
                }

                PlotModel.InvalidatePlot(false);
            }
        }

        private void uiSetLayerComboBox_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            if (!uiSelectAnnotButton.Checked)
                return;

            foreach (Annotation a in PlotModel.Annotations.Where(a => a.IsSelected()))
                a.Layer = (AnnotationLayer) uiSetLayerComboBox.SelectedIndex;

            PlotModel.InvalidatePlot(false);
        }

        private void uiSetLineColorButton_OnClick(object sender, EventArgs args)
        {
            using (var d = new ColorDialog())
            {
                d.Color = uiSetLineColorButton.BackColor;

                if (d.ShowDialog() != DialogResult.OK)
                    return;

                uiSetLineColorButton.BackColor = d.Color;

                if (uiSelectAnnotButton.Checked)
                {
                    foreach (Annotation a in PlotModel.Annotations.Where(a => a.IsSelected()))
                    {
                        var arrowAnnot = a as ArrowAnnotation;
                        if (arrowAnnot != null)
                        {
                            arrowAnnot.Color =
                                OxyColor.FromAColor(arrowAnnot.Color.A, d.Color.ToOxyColor());
                        }

                        // This will cover both Line and Polyline annotations
                        var pathAnnot = a as PathAnnotation;
                        if (pathAnnot != null)
                        {
                            pathAnnot.Color =
                                OxyColor.FromAColor(pathAnnot.Color.A, d.Color.ToOxyColor());
                        }
                    }
                }

                PlotModel.InvalidatePlot(false);
            }
        }

        private void uiSetLineAlphaTextBox_OnKeyDown(object sender, KeyEventArgs args)
        {
            if (args.KeyCode != Keys.Enter || !uiSelectAnnotButton.Checked)
                return;

            foreach (Annotation a in PlotModel.Annotations.Where(a => a.IsSelected()))
            {
                var arrowAnnot = a as ArrowAnnotation;
                if (arrowAnnot != null)
                {
                    arrowAnnot.Color =
                        OxyColor.FromAColor(byte.Parse(uiSetLineAlphaTextBox.Text),
                                            arrowAnnot.Color);
                }

                var pathAnnot = a as PathAnnotation;
                if (pathAnnot != null)
                {
                    pathAnnot.Color =
                        OxyColor.FromAColor(byte.Parse(uiSetLineAlphaTextBox.Text),
                                            pathAnnot.Color);
                }
            }

            PlotModel.InvalidatePlot(false);
        }

        private void uiSetLineStyleComboBox_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            if (!uiSelectAnnotButton.Checked)
                return;

            foreach (Annotation a in PlotModel.Annotations.Where(a => a.IsSelected()))
            {
                var arrowAnnot = a as ArrowAnnotation;
                if (arrowAnnot != null)
                    arrowAnnot.LineStyle = (LineStyle) uiSetLineStyleComboBox.SelectedItem;

                var pathAnnot = a as PathAnnotation;
                if (pathAnnot != null)
                    pathAnnot.LineStyle = (LineStyle) uiSetLineStyleComboBox.SelectedItem;
            }

            PlotModel.InvalidatePlot(false);
        }

        private void uiSetLineThicknessTextBox_OnKeyDown(object sender, KeyEventArgs args)
        {
            if (args.KeyCode != Keys.Enter || !uiSelectAnnotButton.Checked)
                return;

            foreach (Annotation a in PlotModel.Annotations.Where(a => a.IsSelected()))
            {
                var arrowAnnot = a as ArrowAnnotation;
                if (arrowAnnot != null)
                    arrowAnnot.StrokeThickness = double.Parse(uiSetLineThicknessTextBox.Text);

                var pathAnnot = a as PathAnnotation;
                if (pathAnnot != null)
                    pathAnnot.StrokeThickness = double.Parse(uiSetLineThicknessTextBox.Text);
            }

            PlotModel.InvalidatePlot(false);
        }

        private void uiSetTextTextBox_OnKeyDown(object sender, KeyEventArgs args)
        {
            if (args.KeyCode != Keys.Enter || !uiSelectAnnotButton.Checked)
                return;

            foreach (Annotation a in PlotModel.Annotations.Where(a => a.IsSelected()))
                ((TextualAnnotation) a).Text = uiSetTextTextBox.Text;

            PlotModel.InvalidatePlot(false);
        }
    }
}
