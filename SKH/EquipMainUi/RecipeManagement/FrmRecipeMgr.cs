using EquipMainUi.Struct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EquipMainUi.ConvenienceClass;

namespace EquipMainUi.RecipeManagement
{
    public partial class FrmRecipeMgr : Form
    {
        public FrmRecipeMgr()
        {
            InitializeComponent();

            InitList();

            ExtensionUI.AddClickEventLog(this);
            ChangeChinaLanguage();
            pGridSelectedRcp.SelectedObject = new Recipe();
        }
        private void ChangeChinaLanguage()
        {
            if (GG.boChinaLanguage)
            {
                label131.Text = "■ 当前Recipe";		        // ■ 현재 레시피
                label1.Text = "■ 选择的Recipe";	            // ■ 선택한 레시피
                btnSetRecipe1.Text = "LPM1 SET";		    // LPM1 세팅
                btnSetRecipe2.Text = "LPM2 SET";		    // LPM2 세팅
                btnInsert.Text = "生成";		            // 생성
                btnUpdate.Text = "修改";	                // 수정
                btnDelete.Text = "删除";		                // 삭제
            }
        }

        private void InitList()
        {
            lstRcps.Clear();
            lstRcps.Columns.Add(new ColumnHeader() { Text = "Name", Width = 240 });
            lstRcps.Columns.Add(new ColumnHeader() { Text = "Model", Width = 60 });
            lstRcps.Columns.Add(new ColumnHeader() { Text = "Description", Width = 1000 });
            UpdateRecipeInfo();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if(GG.Equip.EquipRunMode == Struct.EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<Recipe 变更>" : "인터락<레시피 변경>", GG.boChinaLanguage ? "Auto Run 时，控制管理的 Recipe 无法 生成/修改/删除 " : "오토런 중에는 제어에서 관리하는 레시피 생성/수정/삭제 가 불가능합니다");
                return;
            }
            Recipe r = (Recipe)pGridSelectedRcp.SelectedObject;
            
            if (RecipeDataMgr.IsExist(r.Name) == true)
            {
                MessageBox.Show(GG.boChinaLanguage ? "已经存在的 Recipe." : "이미 존재하는 레시피입니다");
                return;
            }

            RecipeDataMgr.Insert((Recipe)r.Clone());
            UpdateRecipeInfo();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (GG.Equip.EquipRunMode == Struct.EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<Recipe 变更>" : "인터락<레시피 변경>", GG.boChinaLanguage ? "Auto Run 时，控制管理的 Recipe 无法 生成/修改/删除 " : "오토런 중에는 제어에서 관리하는 레시피 생성/수정/삭제 가 불가능합니다");
                return;
            }
            string sel = lblSelectedRecipe.Text;            

            if (sel == string.Empty || RecipeDataMgr.IsExist(sel) == false)
            {
                MessageBox.Show(GG.boChinaLanguage ? "请重新选择Recipe." : "레시피를 다시 선택해주세요");
                return;
            }

            Recipe src = (Recipe)pGridSelectedRcp.SelectedObject;
            MessageBox.Show(src.Update() ? GG.boChinaLanguage ? "成功" : "성공" : GG.boChinaLanguage ? "失败" : "실패");

            UpdateRecipeInfo();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (GG.Equip.EquipRunMode == Struct.EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<Recipe 变更>" : "인터락<레시피 변경>", GG.boChinaLanguage ? "Auto Run 时，控制管理的 Recipe 无法 生成/修改/删除 " : "오토런 중에는 제어에서 관리하는 레시피 생성/수정/삭제 가 불가능합니다");
                return;
            }
            Recipe src = (Recipe)pGridSelectedRcp.SelectedObject;

            if (RecipeDataMgr.IsExist(src.Name) == false)
            {
                MessageBox.Show(GG.boChinaLanguage ? "请重新选择Recipe." : "레시피를 다시 선택해주세요");
                return;
            }

            MessageBox.Show(RecipeDataMgr.Delete(src.Name) ? GG.boChinaLanguage ? "成功" : "성공" : GG.boChinaLanguage ? "失败" : "실패");

            UpdateRecipeInfo();
        }

        private void btnSetRecipe_Click(object sender, EventArgs e)
        {
            if (GG.Equip.EquipRunMode == Struct.EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<Recipe 设置>" : "인터락<레시피 설정>", GG.boChinaLanguage ? "Auto Run 时， 控制管理的 Recipe 无法设置 " : "오토런 중에는 제어에서 관리하는 레시피 설정이 불가능합니다");
                return;
            }

            Button btn = sender as Button;
            Recipe src = (Recipe)pGridSelectedRcp.SelectedObject;

            if (src == null || RecipeDataMgr.IsExist(src.Name) == false)
            {
                MessageBox.Show(GG.boChinaLanguage ? "请重新选择Recipe." : "레시피를 다시 선택해주세요");
                return;
            }

            Recipe cur = btn == btnSetRecipe1 ? RecipeDataMgr.GetRecipe(RecipeDataMgr.CurLPM1Recipe) : RecipeDataMgr.GetRecipe(RecipeDataMgr.CurLPM2Recipe);            
            cur.Desc = src.Name;
            cur.Update();
            UpdateRecipeInfo();
            
            if(btn == btnSetRecipe1)
            {
                GG.Equip.ChangeRecipe(1, RecipeDataMgr.GetCurRecipeName(0));
            }
            else
            {
                GG.Equip.ChangeRecipe(2, RecipeDataMgr.GetCurRecipeName(1));
            }
        }

        private void UpdateRecipeInfo()
        {
            lstRcps.Items.Clear();

            foreach (var r in RecipeDataMgr.GetRecipes())
            {
                if (r.Name == RecipeDataMgr.CurLPM1Recipe || r.Name == RecipeDataMgr.CurLPM2Recipe)
                    continue;

                string[] row = { r.Name, r.Model, r.Desc };
                ListViewItem newitem = new ListViewItem(row);
                lstRcps.Items.Add(newitem);
            }

            Recipe cur = RecipeDataMgr.GetRecipe(RecipeDataMgr.CurLPM1Recipe);
            lblCurRecipe.Text = cur == null ? "" : cur.Desc;
            cur = RecipeDataMgr.GetRecipe(RecipeDataMgr.CurLPM2Recipe);
            lblCurRecipe2.Text = cur == null ? "" : cur.Desc;
            pGridSelectedRcp.SelectedObject = new Recipe();

            lstRcps_ColumnClick(null, new ColumnClickEventArgs(0));
            lstRcps_ColumnClick(null, new ColumnClickEventArgs(0));
        }

        private void lstRcps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRcps.SelectedIndices.Count > 0)
            {
                string selected = lstRcps.SelectedItems[0].SubItems[0].Text;

                if (RecipeDataMgr.IsExist(selected) == true)
                {
                    Recipe cur = RecipeDataMgr.GetRecipe(selected);
                    pGridSelectedRcp.SelectedObject = (Recipe)cur;
                    lblSelectedRecipe.Text = cur.Name;
                }
            }
        }
        
        private void lstRcps_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if(e.Column != 0)
            {
                return;
            }
            if(this.lstRcps.Sorting == SortOrder.Ascending || this.lstRcps.Sorting == SortOrder.None)
            {
                this.lstRcps.ListViewItemSorter = new ListviewItemComparer(e.Column, "desc");
                this.lstRcps.Sorting = SortOrder.Descending;
            }
            else if (this.lstRcps.Sorting == SortOrder.Descending || this.lstRcps.Sorting == SortOrder.None)
            {
                this.lstRcps.ListViewItemSorter = new ListviewItemComparer(e.Column, "acs");
                this.lstRcps.Sorting = SortOrder.Ascending;
            }

            lstRcps.Sort();
        }

        private void btnUpDown_Click(object sender, EventArgs e)
        {
            if (lstRcps.SelectedItems == null)
                return;

            Button btn = sender as Button;
            int idx = lstRcps.SelectedItems[0].Index;

            var lstRecipe = RecipeDataMgr.GetRecipes();


            if(btn == btnUp)
            {
                
            }
            else if(btn == btnDown)
            {

            }
        }
    }

    class ListviewItemComparer : IComparer
    {
        private int col;
        public string sort = "acs";
        public ListviewItemComparer()
        {
            col = 0;
        }

        public ListviewItemComparer(int column, string sort)
        {
            col = column;
            this.sort = sort;
        }

        public int Compare(object x, object y)
        {
            if(sort == "acs")
            {
                return string.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
            else
            {
                return string.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
            }
        }
    }
}
