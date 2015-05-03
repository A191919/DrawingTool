﻿using System.Windows.Forms;


namespace OxyPlotDrawingToolbar
{
    public partial class InputTextDialog : Form
    {
        public InputTextDialog()
        {
            InitializeComponent();
        }

        public string DisplayText
        {
            get { return valueTextBox.Text; }
            set { valueTextBox.Text = value; }
        }
    }
}
