using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMainUi.Common.Convenience
{
    public class PropertyGridEx : PropertyGrid
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            var grid = this.Controls[2];
            grid.MouseClick += grid_MouseClick;
        }
        void grid_MouseClick(object sender, MouseEventArgs e)
        {
            var grid = this.Controls[2];
            var flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var invalidPoint = new Point(-2147483648, -2147483648);
            var FindPosition = grid.GetType().GetMethod("FindPosition", flags);
            var p = (Point)FindPosition.Invoke(grid, new object[] { e.X, e.Y });
            GridItem entry = null;
            if (p != invalidPoint)
            {
                var GetGridEntryFromRow = grid.GetType()
                                              .GetMethod("GetGridEntryFromRow", flags);
                entry = (GridItem)GetGridEntryFromRow.Invoke(grid, new object[] { p.Y });
            }
            if (entry != null && entry.Value != null)
            {
                object parent;
                if (entry.Parent != null && entry.Parent.Value != null)
                    parent = entry.Parent.Value;
                else
                    parent = this.SelectedObject;
                if (entry.Value != null && entry.Value is bool)
                {
                    entry.PropertyDescriptor.SetValue(parent, false);
                    this.Refresh();
                }
            }
        }
        public class PGridBoolEditor : UITypeEditor
        {
            public override bool GetPaintValueSupported
                (System.ComponentModel.ITypeDescriptorContext context)
            { return true; }
            public override void PaintValue(PaintValueEventArgs e)
            {
                var rect = e.Bounds;
                rect.Inflate(1, 1);
                ControlPaint.DrawCheckBox(e.Graphics, rect, ButtonState.Flat |
                    (((bool)e.Value) ? ButtonState.Checked : ButtonState.Normal));
            }
        }
    }
}
