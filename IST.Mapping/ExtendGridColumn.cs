using System.IO;
using System.Drawing;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using System;

namespace IST.JMapping
{
    public class ExtendGridColumn : GridColumn
    {
        public TypeRepositoryColumn ColumnRepositoryType { get; set; }
        public string TextEditMask { get; set; }

        public string TextFilter { get; set; }
        public bool SummaryCount { get; set; }
        public string SpecificCulture { get; set; }
        public string NullText { get; set; }
        public int CountItemCheckedCombo { get; set; }
        public int TextMaxLength { get; set; }
        public int ImgWidth { get; set; }
        public int ColWidth { get; set; }
        public int ImgHeight { get; set; }
        public bool visibleID { get; set; }

        public RepositoryItemLookUpEdit InitItemLookUpEdit(string caption, DataTable dtSource)
        {
            if (dtSource != null)
            {
                if (dtSource.Columns[0].ColumnName == "id")
                    dtSource.Columns[0].ColumnName = "код";
                if (dtSource.Columns[1].ColumnName == "title")
                    dtSource.Columns[1].ColumnName = "Найменування";
            }
            else
            {
                return null;
            }
            string valMember = "код", displayMember = "Найменування";
            RepositoryItemLookUpEdit repositoryItemLookUpEdit = new RepositoryItemLookUpEdit();
            repositoryItemLookUpEdit.DataSource = dtSource;
            repositoryItemLookUpEdit.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
            repositoryItemLookUpEdit.DisplayMember = displayMember;
            repositoryItemLookUpEdit.DropDownRows = 15;
            repositoryItemLookUpEdit.ValueMember = valMember;
            repositoryItemLookUpEdit.Name = caption;
            repositoryItemLookUpEdit.PopupWidthMode = DevExpress.XtraEditors.PopupWidthMode.UseEditorWidth;
            LookUpColumnInfoCollection coll = repositoryItemLookUpEdit.Columns;

            foreach (DataColumn col in dtSource.Columns)
            {
                int index = coll.Add(new LookUpColumnInfo(col.ColumnName, col.ColumnName));
                if (dtSource.Columns[index].ColumnName == "наименование" ||
                    dtSource.Columns[index].ColumnName == "title" ||
                    dtSource.Columns[index].ColumnName == "Найменування" ||
                    dtSource.Columns[index].ColumnName == "додатково")
                {
                    coll[index].Visible = true;
                }
                else
                {
                    coll[index].Visible = false;
                }
            }

            repositoryItemLookUpEdit.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            repositoryItemLookUpEdit.EditValueChangedFiringMode = EditValueChangedFiringMode.Buffered;
            repositoryItemLookUpEdit.SearchMode = SearchMode.AutoFilter;
            repositoryItemLookUpEdit.AutoSearchColumnIndex = 1;
            repositoryItemLookUpEdit.NullText = String.Empty;
            repositoryItemLookUpEdit.AppearanceDropDown.Font =
                new System.Drawing.Font("Microsoft San Serif", 10);
            repositoryItemLookUpEdit.PopupSizeable = true;
            if (ColWidth == 0)
                repositoryItemLookUpEdit.PopupWidth = 500;
            else
                repositoryItemLookUpEdit.PopupWidth = ColWidth;

            //repositoryItemLookUpEdit.QueryPopUp += RepositoryItemLookUpEdit_QueryPopUp;
            //repositoryItemLookUpEdit.Popup += RepositoryItemLookUpEdit_Popup;
            repositoryItemLookUpEdit.Mask.BeepOnError = true;
            //repositoryItemLookUpEdit.EditValueChanged += new EventHandler(repositoryItemLookUpEdit_EditValueChanged);
            return repositoryItemLookUpEdit;
        }

        private void RepositoryItemLookUpEdit_Popup(object sender, EventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo info = gridView1.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
            DevExpress.Utils.Win.IPopupControl popup = sender as DevExpress.Utils.Win.IPopupControl;
            var edit = sender as DevExpress.XtraEditors.LookUpEdit;
            var gridView = (edit.Parent as DevExpress.XtraGrid.GridControl);
            //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo info = gridView.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
            //for (int i = 0; i < parent.Controls.Count; i++)
            //{
            //    MessageBox.Show(parent.Controls[i].Name);
            //}
            DevExpress.XtraEditors.Popup.PopupLookUpEditForm popupForm = popup.PopupWindow as DevExpress.XtraEditors.Popup.PopupLookUpEditForm;
            //popupForm.Size = new Size(info.ColumnsInfo[1].Bounds.Width, 500);

        }

        private void RepositoryItemLookUpEdit_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //var edit = sender as DevExpress.XtraEditors.LookUpEdit;
            //if (edit == null) return;
            //ApplySize(edit.Width, edit);
        }
        private void ApplySize(int width, RepositoryItemLookUpEdit properties)
        {
            properties.PopupFormMinSize = new Size(width, 0);
            properties.PopupFormSize = properties.PopupFormMinSize;
        }

        public RepositoryItemLookUpEdit InitItemLookUpEdit(string caption, DataTable dtSource, bool inVisible)
        {
            string valMember = "код", displayMember = "наименование";
            RepositoryItemLookUpEdit repositoryItemLookUpEdit = new RepositoryItemLookUpEdit();
            repositoryItemLookUpEdit.DataSource = dtSource;
            repositoryItemLookUpEdit.TextEditStyle = TextEditStyles.Standard;
            repositoryItemLookUpEdit.DisplayMember = displayMember;
            repositoryItemLookUpEdit.DropDownRows = 15;
            repositoryItemLookUpEdit.ValueMember = valMember;
            repositoryItemLookUpEdit.Name = caption;
            repositoryItemLookUpEdit.BestFit();
            LookUpColumnInfoCollection coll = repositoryItemLookUpEdit.Columns;
            coll.Add(new LookUpColumnInfo(valMember, valMember));
            coll.Add(new LookUpColumnInfo(displayMember, displayMember));
            for (int i = 2; i < dtSource.Columns.Count; i++)
            {
                int index = coll.Add(new LookUpColumnInfo(dtSource.Columns[i].ColumnName,
                    dtSource.Columns[i].ColumnName));
                coll[index].Visible = inVisible;
                if (dtSource.Columns[i].ColumnName == "title2")
                    coll[index].Visible = true;
            }
            coll[0].Visible = false;
            repositoryItemLookUpEdit.SearchMode = SearchMode.AutoComplete;
            if (dtSource.Columns.Count >= 3)
                repositoryItemLookUpEdit.AutoSearchColumnIndex = 2;
            else
                repositoryItemLookUpEdit.AutoSearchColumnIndex = 1;
            repositoryItemLookUpEdit.NullText = String.Empty;
            repositoryItemLookUpEdit.AppearanceDropDown.Font =
                new System.Drawing.Font("Microsoft San Serif", 10);
            repositoryItemLookUpEdit.PopupSizeable = true;
            repositoryItemLookUpEdit.PopupWidth = 300;
            repositoryItemLookUpEdit.Mask.BeepOnError = true;
            //repositoryItemLookUpEdit.EditValueChanged += new EventHandler(repositoryItemLookUpEdit_EditValueChanged);
            return repositoryItemLookUpEdit;
        }
        public RepositoryItemCheckedComboBoxEdit InitItemCheckedComboBoxEdit(string caption, DataTable dtSource, bool inVisible)
        {
            //string valMember = "код", displayMember = "наименование";
            RepositoryItemCheckedComboBoxEdit repositoryItemLookUpEdit = new RepositoryItemCheckedComboBoxEdit();
            repositoryItemLookUpEdit.DataSource = dtSource;
            repositoryItemLookUpEdit.DisplayMember = dtSource.Columns[1].ColumnName;
            repositoryItemLookUpEdit.ValueMember = dtSource.Columns[0].ColumnName;
            repositoryItemLookUpEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
            repositoryItemLookUpEdit.SeparatorChar = ',';
            repositoryItemLookUpEdit.DropDownRows = 10;

            repositoryItemLookUpEdit.Name = caption;

            repositoryItemLookUpEdit.NullText = String.Empty;
            repositoryItemLookUpEdit.AppearanceDropDown.Font =
                new System.Drawing.Font("Microsoft San Serif", 10);
            repositoryItemLookUpEdit.PopupSizeable = true;
            repositoryItemLookUpEdit.Mask.BeepOnError = true;
            //repositoryItemLookUpEdit.EditValueChanged += new EventHandler(repositoryItemLookUpEdit_EditValueChanged);
            return repositoryItemLookUpEdit;
        }
        public RepositoryItemLookUpEdit InitItemLookUpEditFilter(string caption, DataTable dtSource,
            string filterFieldName)
        {
            string valMember = "код", displayMember = "наименование";
            RepositoryItemLookUpEdit repositoryItemLookUpEdit = new RepositoryItemLookUpEdit();
            repositoryItemLookUpEdit.DataSource = dtSource;
            repositoryItemLookUpEdit.TextEditStyle = TextEditStyles.Standard;
            repositoryItemLookUpEdit.DisplayMember = displayMember;
            repositoryItemLookUpEdit.DropDownRows = 15;
            repositoryItemLookUpEdit.ValueMember = valMember;
            repositoryItemLookUpEdit.Name = caption;
            LookUpColumnInfoCollection coll = repositoryItemLookUpEdit.Columns;
            coll.Add(new LookUpColumnInfo(valMember, valMember));
            coll.Add(new LookUpColumnInfo(displayMember, displayMember));
            coll.Add(new LookUpColumnInfo(filterFieldName, filterFieldName));
            coll[0].Visible = false;
            coll[2].Visible = false;
            repositoryItemLookUpEdit.SearchMode = SearchMode.AutoComplete;
            repositoryItemLookUpEdit.AutoSearchColumnIndex = 1;
            repositoryItemLookUpEdit.NullText = "";
            repositoryItemLookUpEdit.AppearanceDropDown.Font =
                new System.Drawing.Font("Microsoft San Serif", 10);
            repositoryItemLookUpEdit.PopupSizeable = true;
            repositoryItemLookUpEdit.PopupWidth = 400;
            repositoryItemLookUpEdit.Mask.BeepOnError = true;
            return repositoryItemLookUpEdit;
        }

        public RepositoryItemLookUpEdit InitItemLookUpEditViewFilter(string caption, DataTable dtSource,
            string filterFieldName)
        {
            string valMember = "код", displayMember = "наименование";
            RepositoryItemLookUpEdit repositoryItemLookUpEdit = new RepositoryItemLookUpEdit();
            repositoryItemLookUpEdit.DataSource = dtSource;
            repositoryItemLookUpEdit.TextEditStyle = TextEditStyles.Standard;
            repositoryItemLookUpEdit.DisplayMember = displayMember;
            repositoryItemLookUpEdit.DropDownRows = 15;
            repositoryItemLookUpEdit.ValueMember = valMember;
            repositoryItemLookUpEdit.Name = caption;
            LookUpColumnInfoCollection coll = repositoryItemLookUpEdit.Columns;
            coll.Add(new LookUpColumnInfo(valMember, valMember));
            coll.Add(new LookUpColumnInfo(displayMember, displayMember));
            coll.Add(new LookUpColumnInfo("filter", "filter"));
            coll[0].Visible = false;
            coll[2].Visible = false;
            //repositoryItemLookUpEdit.Popup += RepositoryItemLookUpEdit_Popup;
            repositoryItemLookUpEdit.SearchMode = SearchMode.AutoComplete;
            repositoryItemLookUpEdit.AutoSearchColumnIndex = 1;
            repositoryItemLookUpEdit.NullText = "";
            repositoryItemLookUpEdit.AppearanceDropDown.Font =
                new System.Drawing.Font("Microsoft San Serif", 10);
            repositoryItemLookUpEdit.PopupSizeable = true;
            repositoryItemLookUpEdit.PopupWidth = 400;
            repositoryItemLookUpEdit.Mask.BeepOnError = true;
            return repositoryItemLookUpEdit;
        }

        //private void RepositoryItemLookUpEdit_Popup(object sender, EventArgs e)
        //{
        //    DevExpress.XtraEditors.LookUpEdit edit = sender as DevExpress.XtraEditors.LookUpEdit;
        //    DataTable dt = (edit.Properties.DataSource as DataView).ToTable();
        //    //(edit.Properties.DataSource as DataRow)[2] = 3;
        //}

        public RepositoryItemLookUpEdit InitItemLookUpEditVisible(string caption, DataTable dtSource)
        {
            string valMember = "код", displayMember = "наименование";
            RepositoryItemLookUpEdit repositoryItemLookUpEdit = new RepositoryItemLookUpEdit();
            repositoryItemLookUpEdit.DataSource = dtSource;
            repositoryItemLookUpEdit.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            repositoryItemLookUpEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
            repositoryItemLookUpEdit.DisplayMember = displayMember;
            repositoryItemLookUpEdit.DropDownRows = 15;
            repositoryItemLookUpEdit.ValueMember = valMember;
            repositoryItemLookUpEdit.Name = caption;
            repositoryItemLookUpEdit.PopupWidthMode = DevExpress.XtraEditors.PopupWidthMode.UseEditorWidth;
            LookUpColumnInfoCollection coll = repositoryItemLookUpEdit.Columns;

            foreach (DataColumn col in dtSource.Columns)
            {
                int index = coll.Add(new LookUpColumnInfo(col.ColumnName, col.ColumnName));
                coll[index].Visible = true;
                if (dtSource.Columns[index].ColumnName == "код")
                {
                    coll[index].Width = 5;
                }
                //if (dtSource.Columns[index].ColumnName == "наименование" ||
                //    dtSource.Columns[index].ColumnName == "додатково")
                //{
                //    coll[index].Visible = true;
                //}
                //else
                //{
                //    coll[index].Visible = false;
                //}
            }

            repositoryItemLookUpEdit.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            repositoryItemLookUpEdit.Properties.EditValueChangedFiringMode = EditValueChangedFiringMode.Buffered;
            repositoryItemLookUpEdit.SearchMode = SearchMode.AutoFilter;
            repositoryItemLookUpEdit.AutoSearchColumnIndex = 1;
            repositoryItemLookUpEdit.NullText = String.Empty;
            repositoryItemLookUpEdit.AppearanceDropDown.Font =
                new System.Drawing.Font("Microsoft San Serif", 10);
            repositoryItemLookUpEdit.PopupSizeable = true;
            if (ColWidth == 0)
                repositoryItemLookUpEdit.PopupWidth = 500;
            else
                repositoryItemLookUpEdit.PopupWidth = ColWidth;

            //repositoryItemLookUpEdit.QueryPopUp += RepositoryItemLookUpEdit_QueryPopUp;
            //repositoryItemLookUpEdit.Popup += RepositoryItemLookUpEdit_Popup;
            repositoryItemLookUpEdit.Mask.BeepOnError = true;
            //repositoryItemLookUpEdit.EditValueChanged += new EventHandler(repositoryItemLookUpEdit_EditValueChanged);
            return repositoryItemLookUpEdit;
            //string valMember = "код", displayMember = "наименование";
            //RepositoryItemLookUpEdit repositoryItemLookUpEdit = new RepositoryItemLookUpEdit();
            //repositoryItemLookUpEdit.DataSource = dtSource;
            //repositoryItemLookUpEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
            //repositoryItemLookUpEdit.DisplayMember = displayMember;
            //repositoryItemLookUpEdit.DropDownRows = 15;
            //repositoryItemLookUpEdit.ValueMember = valMember;
            //repositoryItemLookUpEdit.Name = caption;
            //LookUpColumnInfoCollection cols = repositoryItemLookUpEdit.Columns;

            //foreach (DataColumn col in dtSource.Columns)
            //{
            //    int index = cols.Add(new LookUpColumnInfo(col.ColumnName, col.ColumnName));
            //    if (col.ColumnName.IndexOf("id") != -1 || col.ColumnName.IndexOf("код") != -1)
            //        cols[index].Visible = false;
            //    if (col.ColumnName == "title2")
            //        cols[index].Visible = true;
            //}

            //repositoryItemLookUpEdit.SearchMode = SearchMode.AutoComplete;
            //repositoryItemLookUpEdit.AutoSearchColumnIndex = 1;
            //repositoryItemLookUpEdit.Properties.NullText = String.Empty;
            //repositoryItemLookUpEdit.AppearanceDropDown.Font =
            //    new System.Drawing.Font("Microsoft San Serif", 10);
            //repositoryItemLookUpEdit.PopupSizeable = true;
            //repositoryItemLookUpEdit.PopupWidth = 700;
            //repositoryItemLookUpEdit.Mask.BeepOnError = true;
            //repositoryItemLookUpEdit.BestFit();
            //return repositoryItemLookUpEdit;
        }
        public RepositoryItemGridLookUpEdit InitItemLookUpEditVisibleGrid(string caption, DataTable dtSource)
        {
            RepositoryItemGridLookUpEdit repositoryItemLookUpEdit = new RepositoryItemGridLookUpEdit();
            repositoryItemLookUpEdit.DataSource = dtSource;
            return repositoryItemLookUpEdit;
        }
        public RepositoryItemTextEdit InitItemTextEditMask(string mask)
        {
            RepositoryItemTextEdit repositoryItemTextEdit = new RepositoryItemTextEdit();
            //repositoryItemTextEdit.Mask.BeepOnError = true;
            repositoryItemTextEdit.Mask.EditMask = mask;
            repositoryItemTextEdit.Name = "repositoryItemTextEditMask";
            repositoryItemTextEdit.AutoHeight = false;
            repositoryItemTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            repositoryItemTextEdit.Mask.PlaceHolder = '*';
            return repositoryItemTextEdit;
        }
        public RepositoryItemTextEdit InitItemTextEdit(int maxLength)
        {
            RepositoryItemTextEdit repositoryItemTextEdit = new RepositoryItemTextEdit();
            //repositoryItemTextEdit.Mask.BeepOnError = true;
            //repositoryItemTextEdit.Mask.EditMask = mask;
            repositoryItemTextEdit.Name = "repositoryItemTextEdit";
            repositoryItemTextEdit.AutoHeight = false;
            if (maxLength > 0)
                repositoryItemTextEdit.MaxLength = maxLength;
            /*repositoryItemTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            repositoryItemTextEdit.Mask.PlaceHolder = '*';*/
            return repositoryItemTextEdit;
        }
        public RepositoryItemCalcEdit InitItemCalcEdit()
        {
            RepositoryItemCalcEdit repositoryItemCalcEdit = new RepositoryItemCalcEdit();
            repositoryItemCalcEdit.AutoHeight = false;
            //repositoryItemCalcEdit.Mask.BeepOnError = true;
            return repositoryItemCalcEdit;
        }
        public RepositoryItemSpinEdit InitItemSpinIntEdit()
        {
            RepositoryItemSpinEdit repositoryItemSpinEdit = new RepositoryItemSpinEdit();
            repositoryItemSpinEdit.AutoHeight = false;
            repositoryItemSpinEdit.Name = "repositoryItemSpinEdit";
            repositoryItemSpinEdit.IsFloatValue = false;
            repositoryItemSpinEdit.Mask.EditMask = "N00";
            //repositoryItemSpinEdit.Mask.BeepOnError = true;
            //repositoryItemSpinEdit.MaxLength = 2;
            return repositoryItemSpinEdit;
        }
        public RepositoryItemSpinEdit InitItemSpinNumericEdit()
        {
            RepositoryItemSpinEdit repositoryItemSpinEdit = new RepositoryItemSpinEdit();
            repositoryItemSpinEdit.AutoHeight = false;
            repositoryItemSpinEdit.Mask.BeepOnError = true;
            repositoryItemSpinEdit.Name = "repositoryItemSpinEdit";
            repositoryItemSpinEdit.Mask.BeepOnError = true;
            return repositoryItemSpinEdit;
        }
        public RepositoryItemCheckEdit InitItemCheckEdit()
        {
            RepositoryItemCheckEdit repositoryItemCheckEdit = new RepositoryItemCheckEdit();
            repositoryItemCheckEdit.CheckStyle = CheckStyles.Standard;
            repositoryItemCheckEdit.AutoHeight = false;
            //repositoryItemCheckEdit.LookAndFeel.UseDefaultLookAndFeel = false;
            repositoryItemCheckEdit.Name = "repositoryItemCheckEdit";
            return repositoryItemCheckEdit;
        }
        public RepositoryItemCheckEdit InitItemCheckEditExtend()
        {
            RepositoryItemCheckEdit repositoryItemCheckEdit = new RepositoryItemCheckEdit();
            repositoryItemCheckEdit.CheckStyle = CheckStyles.Standard;
            repositoryItemCheckEdit.ValueUnchecked = "нет";
            repositoryItemCheckEdit.ValueGrayed = "нет";
            repositoryItemCheckEdit.ValueChecked = "да";
            repositoryItemCheckEdit.LookAndFeel.SkinName = "The Asphalt World";
            repositoryItemCheckEdit.LookAndFeel.UseDefaultLookAndFeel = false;
            repositoryItemCheckEdit.AutoHeight = false;
            repositoryItemCheckEdit.LookAndFeel.UseDefaultLookAndFeel = false;
            repositoryItemCheckEdit.Name = "repositoryItemCheckEdit";
            return repositoryItemCheckEdit;
        }
        public RepositoryItemDateEdit InitItemDateTimeEdit()
        {
            RepositoryItemDateEdit repositoryItemDateEdit = new RepositoryItemDateEdit();
            repositoryItemDateEdit.AutoHeight = false;
            repositoryItemDateEdit.DisplayFormat.FormatString = "g";
            repositoryItemDateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemDateEdit.Mask.EditMask = "g";
            repositoryItemDateEdit.Mask.BeepOnError = true;
            repositoryItemDateEdit.Name = "repositoryItemDateTimeEdit";
            return repositoryItemDateEdit;
        }
        public RepositoryItemDateEdit InitItemDateTimeEdit(string nullText)
        {
            RepositoryItemDateEdit repositoryItemDateEdit = new RepositoryItemDateEdit();
            repositoryItemDateEdit.AutoHeight = false;
            repositoryItemDateEdit.DisplayFormat.FormatString = "g";
            repositoryItemDateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemDateEdit.Mask.EditMask = "g";
            repositoryItemDateEdit.Mask.BeepOnError = true;
            repositoryItemDateEdit.Name = "repositoryItemDateTimeEdit";
            repositoryItemDateEdit.NullText = nullText;
            return repositoryItemDateEdit;
        }
        public RepositoryItemDateEdit InitItemDateEdit()
        {
            RepositoryItemDateEdit repositoryItemDateEdit = new RepositoryItemDateEdit();
            repositoryItemDateEdit.AutoHeight = false;
            repositoryItemDateEdit.DisplayFormat.FormatString = "dd/MM/yyyy";
            repositoryItemDateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemDateEdit.Mask.EditMask = "dd/MM/yyyy";
            repositoryItemDateEdit.Mask.BeepOnError = true;
            repositoryItemDateEdit.TodayDate = DateTime.Today;
            repositoryItemDateEdit.NullDate = DateTime.MinValue;
            repositoryItemDateEdit.NullDate = DBNull.Value;
            repositoryItemDateEdit.NullText = String.Empty;
            repositoryItemDateEdit.Name = "repositoryItemDateEdit";
            return repositoryItemDateEdit;
        }
        public RepositoryItemTimeEdit InitItemTimeEdit()
        {
            RepositoryItemTimeEdit repositoryItemTimeEdit = new RepositoryItemTimeEdit();
            repositoryItemTimeEdit.AutoHeight = false;
            repositoryItemTimeEdit.DisplayFormat.FormatString = "H:mm";
            repositoryItemTimeEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit.EditFormat.FormatString = "H:mm";
            repositoryItemTimeEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit.Mask.EditMask = "H:mm";
            repositoryItemTimeEdit.Mask.BeepOnError = true;
            repositoryItemTimeEdit.Name = "repositoryItemTimeEdit";
            return repositoryItemTimeEdit;
            /*
            RepositoryItemTimeEdit repositoryItemTimeEdit = new RepositoryItemTimeEdit();
            repositoryItemTimeEdit.AutoHeight = false;
            //repositoryItemDateEdit.DisplayFormat.FormatString = "G";"MM/dd/yyyy"
            //repositoryItemTimeEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //repositoryItemDateEdit.Mask.EditMask = "G";
            repositoryItemTimeEdit.Mask.BeepOnError = true;
            repositoryItemTimeEdit.Name = "repositoryItemTimeEdit";
            return repositoryItemTimeEdit;
            */
        }
        public RepositoryItemImageComboBox InitItemImageComboBox(int imageIndex, ImageList imageList)
        {
            RepositoryItemImageComboBox repositoryItemImageComboBox = new RepositoryItemImageComboBox();
            repositoryItemImageComboBox.AutoHeight = true;
            repositoryItemImageComboBox.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            repositoryItemImageComboBox.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", null, imageIndex)});
            repositoryItemImageComboBox.Name = "repositoryItemImageComboBox";
            repositoryItemImageComboBox.SmallImages = imageList;
            return repositoryItemImageComboBox;
        }
        public RepositoryItemSpinEdit InitItemPercentEdit()
        {
            RepositoryItemSpinEdit editor = new RepositoryItemSpinEdit();
            editor.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            editor.Mask.EditMask = "P";
            editor.Mask.UseMaskAsDisplayFormat = true;
            return editor;
        }
        public RepositoryItemMemoEdit InitItemMemoEdit()
        {
            RepositoryItemMemoEdit riMemoEdit = new RepositoryItemMemoEdit();
            riMemoEdit.WordWrap = true;
            return riMemoEdit;
        }

        public RepositoryItemCheckedComboBoxEdit InitItemCheckedComboBoxEdit(int count)
        {
            RepositoryItemCheckedComboBoxEdit riCheckedComboBoxEdit = new RepositoryItemCheckedComboBoxEdit();
            for (int i = 0; i < count; i++)
            {
                riCheckedComboBoxEdit.Items.Add(i + 1, false);
            }
            riCheckedComboBoxEdit.DropDownRows = count + 1;
            //riMemoEdit.WordWrap = true;
            return riCheckedComboBoxEdit;
        }
        public RepositoryItemPictureEdit InitItemPictureEdit()
        {
            RepositoryItemPictureEdit riPictureEdit = new RepositoryItemPictureEdit();
            riPictureEdit.SizeMode = PictureSizeMode.Squeeze;
            if (ImgWidth != 0 || ImgHeight != 0)
            {
                ImgWidth = ImgWidth;
                ImgHeight = ImgHeight;
                riPictureEdit.EditValueChanging += new ChangingEventHandler(riPictureEdit_EditValueChanging);
            }
            return riPictureEdit;
        }
        void riPictureEdit_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (e.NewValue == null) return;
            byte[] data = (byte[])e.NewValue;
            byte[] _data;
            using (MemoryStream stream = new MemoryStream(data))
            {
                Bitmap Bit = new Bitmap(Image.FromStream(stream), ImgWidth, ImgHeight);

                using (System.IO.MemoryStream _stream = new System.IO.MemoryStream())
                {
                    Bit.Save(_stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    _stream.Position = 0;
                    _data = new byte[_stream.Length];
                    _stream.Read(_data, 0, (int)_stream.Length);
                    _stream.Close();
                }
                stream.Close();
            }
            e.Cancel = false;
            e.NewValue = _data;
        }
    }
}
