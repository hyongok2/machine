using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EquipMainUi.Struct
{
    public partial class SolutionDialog : Form
    {
        public SolutionDialog(AlarmSolution solution)
        {
            InitializeComponent();
            lblAlarmName.Text = string.Format("[{0:D4}] {1}", solution.Number, solution.Name);
            txtCause.Text = solution.Cause.TrimEnd('\n').TrimStart('\n').Replace("\n", System.Environment.NewLine);
            txtSolution.Text = solution.Solution.TrimEnd('\n').TrimStart('\n').Replace("\n", System.Environment.NewLine);
        }
    }
}
