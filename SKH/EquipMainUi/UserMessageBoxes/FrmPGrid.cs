using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMainUi.UserMessageBoxes
{
    public partial class FrmPGrid : Form
    {
        public FrmPGrid(string title, object obj)
        {
            InitializeComponent();
            this.Text = title;
            TypeDescriptor.AddAttributes(obj, new Attribute[] { new ReadOnlyAttribute(true) });
            propertyGridEx1.SelectedObject = obj;
        }
    }
}
