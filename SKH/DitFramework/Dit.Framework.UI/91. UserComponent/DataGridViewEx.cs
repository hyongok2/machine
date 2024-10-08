using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dit.Framework.UI.UserComponent
{
    /// <summary>
    /// 깜빡거림 최소화
    /// </summary>
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DataGridViewEx : DataGridView
    {
        public List<int> EnableSpanColumn = new List<int>();

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;
            if (EnableSpanColumn.Contains(e.ColumnIndex) && IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = this.AdvancedCellBorderStyle.Top;
            }  
        }
        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
                return;
            if (EnableSpanColumn.Contains(e.ColumnIndex) && IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }
        private bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = this[column, row];
            DataGridViewCell cell2 = this[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }
    }
}
