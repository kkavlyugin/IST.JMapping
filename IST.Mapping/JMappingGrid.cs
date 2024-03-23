using DevExpress.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using IST.JMapping.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IST.JMapping
{
    public enum TypeRepositoryColumn
    {
        Price,
        RelationTable,
        RelationTableViewFilter,
        RelationTableLookUp,
        RelationTableCheckedComboBoxEdit,
        RelationTableFilter,
        Numeric,
        Int,
        String,
        Percent,
        Date,
        DateTime,
        Time,
        Image,
        TextEditMask,
        MemoEdit,
        RelationTableVisible,
        CheckedComboBoxEdit,
        CheckedTreeListLookUpEdit,
        CheckedExtend,
        Checked,
        PictureEdit,
        CalcEdit
    }

    public class JMappingGrid : DataEntity<IDataSource>, IEntity
    {
        protected struct RelationTable
        {
            internal int columnIndex;
            internal RelationReference reference;
        }

        //protected DataTableCollection DTSourceCollection { get; set; }
        protected List<ExtendGridColumn> JGridColumns = new List<ExtendGridColumn>();
        protected List<RepositoryItem> Repositories = new List<RepositoryItem>();
        protected List<RelationTable> RelationsTables = new List<RelationTable>();
        public string Name { get; set; }
        public string TableName { get { return _tableName; } set { _tableName = value; } }
        public bool SaveColumnsSettings { get; set; }

        public JMappingGrid(string tableName)
        {            
            _tableName = tableName;
            Name = tableName;
            base.DTSourceChanged +=
                new DataEntity<IDataSource>.EventDTSourceChanged(MappingGrid_DTSourceChanged);
        }

        public JMappingGrid(string name, string tableName) :
            this(tableName)
        {
            Name = name;
        }

        public JMappingGrid(IDataSource source, string tableName)
            : base(source, tableName)
        {
            Name = tableName;
            InitJGridColumns();
        }

        public JMappingGrid(string tableName, DataTable dtSource) :
            this(tableName)
        {
            Name = tableName;
            if (dtSource == null)
                return;
            DTSource = dtSource;
        }

        void MappingGrid_DTSourceChanged()
        {
            DTSource.TableName = _tableName;
            if (!SaveColumnsSettings)
                InitJGridColumns();
            else
            {
                if (JGridColumns.Count == 0)
                    InitJGridColumns();
            }
        }
        //public void InitDataSource(DataTable dataTable)
        //{
        //    DTSourceCollection = dtCollection;
        //}

        void AddRelationTableColumnFilter(string tableName, int columnIndex,
            string filterFieldName)
        {
            RelationTable relTable = new RelationTable();
            string[] fields = new string[3];
            fields[0] = "id";
            fields[1] = "title";
            fields[2] = filterFieldName;
            relTable.reference = new RelationReference(tableName, fields);
            relTable.columnIndex = columnIndex;
            RelationsTables.Add(relTable);
        }

        private void AddRelationTableColumn(string tableName, int columnIndex)
        {
            RelationTable relTable = new RelationTable();
            if (DSource is null)
                relTable.reference = new RelationReference(tableName); 
            else
                relTable.reference = new RelationReference(DSource, tableName);
            
            relTable.columnIndex = columnIndex;
            RelationsTables.Add(relTable);
        }

        public void SetColumnSettings(int columnIndex, bool visible)
        {
            if (JGridColumns.Count <= columnIndex)
                throw new NotImplementedException("По такому индексу столбца не существует.");
            JGridColumns[columnIndex].Visible = visible;
        }
        public void SetColumnSettings(int columnIndex, int visibleIndex)
        {
            if (JGridColumns.Count <= columnIndex)
                throw new NotImplementedException("По такому индексу столбца не существует.");
            JGridColumns[columnIndex].VisibleIndex = visibleIndex;
        }
        public void SetColumnSettings(int columnIndex, string caption,
            bool summaryCount, bool readOnly)
        {
            if (JGridColumns.Count <= columnIndex)
                throw new NotImplementedException("По такому индексу столбца не существует.");
            JGridColumns[columnIndex].Caption = caption;
            JGridColumns[columnIndex].SummaryCount = summaryCount;
            JGridColumns[columnIndex].OptionsColumn.ReadOnly = readOnly;
        }
        public void SetColumnSettings(int columnIndex, string caption,
            TypeRepositoryColumn typeColumn)
        {
            if (JGridColumns.Count <= columnIndex)
                throw new NotImplementedException("По такому индексу столбца не существует.");
            if (typeColumn == TypeRepositoryColumn.RelationTable ||
                typeColumn == TypeRepositoryColumn.RelationTableLookUp ||
                typeColumn == TypeRepositoryColumn.RelationTableCheckedComboBoxEdit)
                throw new NotImplementedException("В данной версии функции\n" +
                    "тип столбца не может быть установлен в RelationTable.\n" +
                    "Выберите другую версию функции.");
            JGridColumns[columnIndex].Caption = caption;
            JGridColumns[columnIndex].ColumnRepositoryType = typeColumn;
        }
        /// <summary>
        /// Используется для отображения изображений
        /// </summary>
        /// <param name="columnIndex">Индекс столбца</param>
        /// <param name="caption">Отображаемое имя</param>
        /// <param name="typeColumn">Тип столбца (TypeRepositoryColumn.PictureEdit)</param>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <param name="readOnly">Чтения/Изменения</param>
        public void SetColumnSettings(int columnIndex, string caption, TypeRepositoryColumn typeColumn,
                                      int width, int height, bool readOnly)
        {
            if (JGridColumns.Count <= columnIndex)
                throw new NotImplementedException("По такому индексу столбца не существует.");
            JGridColumns[columnIndex].Caption = caption;
            JGridColumns[columnIndex].ColumnRepositoryType = typeColumn;
            JGridColumns[columnIndex].ImgWidth = width;
            JGridColumns[columnIndex].ImgHeight = height;
            JGridColumns[columnIndex].OptionsColumn.ReadOnly = readOnly;
        }
        public void SetColumnSettings(int columnIndex, TypeRepositoryColumn typeColumn)
        {
            if (JGridColumns.Count <= columnIndex)
                throw new NotImplementedException("По такому индексу столбца не существует.");
            if (typeColumn == TypeRepositoryColumn.RelationTable ||
                typeColumn == TypeRepositoryColumn.RelationTableLookUp ||
                typeColumn == TypeRepositoryColumn.RelationTableCheckedComboBoxEdit)
                throw new NotImplementedException("В данной версии функции\n" +
                    "тип столбца не может быть установлен в RelationTable.\n" +
                    "Выберите другую версию функции.");
            JGridColumns[columnIndex].ColumnRepositoryType = typeColumn;
        }
        /// <summary>
        /// Устанавливает отображаемое в сетке имя столбца
        /// </summary>
        /// <param name="columnIndex">Индекс столбца</param>
        /// <param name="caption">Отображаемое имя</param>
        /// <param name="readOnly">Для чтения только</param>
        public void SetColumnSettings(int columnIndex, string caption,
            bool readOnly)
        {
            if (JGridColumns.Count <= columnIndex)
                throw new NotImplementedException("По такому индексу столбца не существует.");
            JGridColumns[columnIndex].Caption = caption;
            JGridColumns[columnIndex].OptionsColumn.ReadOnly = readOnly;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="caption"></param>
        /// <param name="typeColumn"></param>
        /// <param name="summaryCount"></param>
        /// <param name="readOnly"></param>
        public void SetColumnSettings(int columnIndex, string caption, TypeRepositoryColumn typeColumn,
            bool summaryCount, bool readOnly)
        {
            if (JGridColumns.Count <= columnIndex)
                throw new NotImplementedException("По такому индексу столбца не существует.");
            if (typeColumn == TypeRepositoryColumn.RelationTable ||
                typeColumn == TypeRepositoryColumn.RelationTableLookUp ||
                typeColumn == TypeRepositoryColumn.RelationTableCheckedComboBoxEdit)
                throw new NotImplementedException("В данной версии функции\n" +
                    "тип столбца не может быть установлен в RelationTable.\n" +
                    "Выберите другую версию функции.");
            JGridColumns[columnIndex].ColumnRepositoryType = typeColumn;
            JGridColumns[columnIndex].Caption = caption;
            JGridColumns[columnIndex].SummaryCount = summaryCount;
            JGridColumns[columnIndex].OptionsColumn.ReadOnly = readOnly;
        }
        /// <summary>
        /// Привязывает к столбцу связываемую таблицу с двумя столбцами по умолчанию id и title
        /// </summary>
        /// <param name="columnIndex">Индекс столбца</param>
        /// <param name="caption">Наименование столбца</param>
        /// <param name="typeColumn">Тип столбца (TypeRepositoryColumn.RelationTable)</param>
        /// <param name="dbRelationTableName">Имя связываемой таблицы</param>
        /// /// <param name="visible">Видимость столбца</param>
        public void SetColumnSettings(int columnIndex, string caption,
            TypeRepositoryColumn typeColumn, string tableName, bool visible = true)
        {
            if (JGridColumns.Count <= columnIndex)
                throw new NotImplementedException("По такому индексу столбца не существует.");
            JGridColumns[columnIndex].Caption = caption;
            JGridColumns[columnIndex].ColumnRepositoryType = typeColumn;
            JGridColumns[columnIndex].FilterMode = ColumnFilterMode.DisplayText;
            JGridColumns[columnIndex].Visible = visible;

            if (typeColumn == TypeRepositoryColumn.RelationTable ||
                typeColumn == TypeRepositoryColumn.RelationTableVisible ||
                typeColumn == TypeRepositoryColumn.RelationTableLookUp ||
                typeColumn == TypeRepositoryColumn.RelationTableCheckedComboBoxEdit)
                AddRelationTableColumn(tableName, columnIndex);
        }
        /*
        /// <summary>
        /// Привязывает к столбцу связываемую таблицу с двумя столбцами по умолчанию id и title
        /// </summary>
        /// <param name="columnIndex">Индекс столбца</param>
        /// <param name="caption">Наименование столбца</param>
        /// <param name="typeColumn">Тип столбца (TypeRepositoryColumn.RelationTable)</param>
        /// <param name="dbRelationTableName">Имя связываемой таблицы</param>
        /// <param name="visible">Видимость столбца</param>
        public void SetColumnSettings(int columnIndex, string caption,
            TypeRepositoryColumn typeColumn, string tableName,bool visible)
        {
            if (JGridColumns.Count <= columnIndex)
                throw new NotImplementedException("По такому индексу столбца не существует.");
            JGridColumns[columnIndex].Caption = caption;
            JGridColumns[columnIndex].ColumnRepositoryType = typeColumn;
            JGridColumns[columnIndex].FilterMode = ColumnFilterMode.DisplayText;
            JGridColumns[columnIndex].Visible = visible;

            if (typeColumn == TypeRepositoryColumn.RelationTable ||
                typeColumn == TypeRepositoryColumn.RelationTableVisible ||
                typeColumn == TypeRepositoryColumn.RelationTableLookUp ||
                typeColumn == TypeRepositoryColumn.RelationTableCheckedComboBoxEdit)
                AddRelationTableColumn(tableName, columnIndex);
        }
        */
        public void SetColumnSettings(int columnIndex, string caption,
            TypeRepositoryColumn typeColumn, string tableName, int width)
        {
            if (JGridColumns.Count <= columnIndex)
                throw new NotImplementedException("По такому индексу столбца не существует.");
            JGridColumns[columnIndex].Caption = caption;
            JGridColumns[columnIndex].ColumnRepositoryType = typeColumn;
            JGridColumns[columnIndex].ColWidth = width;
            JGridColumns[columnIndex].FilterMode = ColumnFilterMode.DisplayText;

            if (typeColumn == TypeRepositoryColumn.RelationTable ||
                typeColumn == TypeRepositoryColumn.RelationTableLookUp ||
                typeColumn == TypeRepositoryColumn.RelationTableCheckedComboBoxEdit)
                AddRelationTableColumn(tableName, columnIndex);
        }


        public void SetColumnTableViewFilter(int columnIndex, string caption, string tableName, string filter)
        {
            if (JGridColumns.Count <= columnIndex)
                throw new NotImplementedException("По такому индексу столбца не существует.");
            JGridColumns[columnIndex].Caption = caption;
            JGridColumns[columnIndex].ColumnRepositoryType = TypeRepositoryColumn.RelationTableViewFilter;
            JGridColumns[columnIndex].FilterMode = ColumnFilterMode.DisplayText;
            JGridColumns[columnIndex].TextFilter = filter;

            AddRelationTableColumn(tableName, columnIndex);
        }

        /// <summary>
        /// Привязывает к столбцу связываемую таблицу с двумя столбцами для RelationTableFilter
        /// </summary>
        /// <param name="columnIndex">Индекс столбца</param>
        /// <param name="caption">Наименование столбца</param>
        /// <param name="typeColumn">Тип столбца (TypeRepositoryColumn.RelationTable)</param>
        /// <param name="dbRelationTableName">Имя связываемой таблицы</param>
        public void SetColumnSettings(int columnIndex, string caption,
            TypeRepositoryColumn typeColumn, string tableName, string filterFieldName)
        {
            if (JGridColumns.Count <= columnIndex)
                throw new NotImplementedException("По такому индексу столбца не существует.");
            JGridColumns[columnIndex].Caption = caption;
            JGridColumns[columnIndex].ColumnRepositoryType = typeColumn;
            if (typeColumn == TypeRepositoryColumn.RelationTableFilter)
                AddRelationTableColumnFilter(tableName, columnIndex, filterFieldName);
        }

        /// <summary>
        /// Устанавливает в грид поле с иконкой в указанной позиции, \n в gridView необходимо добавить imageList
        /// </summary>
        /// <param name="colIndex">индекс столбца в gridView</param>
        /// <param name="colImageIndex">индекс иконки в imageList для столбца</param>
        /// <param name="cellImageIndex">индекс иконки в imageList для ячейки</param>
        /// <param name="imageList">хранилище иконок imageList</param>
        public void AddGridImageColumn(int colIndex, int colImageIndex,
            int imageIndex, ImageList imageList)
        {
            ExtendGridColumn colImage = new ExtendGridColumn();
            RepositoryItem repository = colImage.InitItemImageComboBox(imageIndex, imageList);
            colImage.ColumnEdit = repository;
            colImage.CustomizationCaption = "Image";
            colImage.ImageAlignment = System.Drawing.StringAlignment.Center;
            colImage.ImageIndex = colImageIndex;
            colImage.Name = "colImage";
            colImage.OptionsColumn.AllowEdit = false;
            colImage.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            colImage.OptionsColumn.AllowMove = false;
            colImage.OptionsColumn.AllowSize = false;
            colImage.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colImage.OptionsColumn.FixedWidth = true;
            colImage.OptionsFilter.AllowFilter = false;
            colImage.Visible = true;
            colImage.VisibleIndex = 0;
            colImage.Width = 30;
            colImage.ColumnRepositoryType = TypeRepositoryColumn.Image;

            JGridColumns.Insert(colIndex, colImage);
            for (int i = 0; i < JGridColumns.Count; i++)
            {
                JGridColumns.ElementAt<ExtendGridColumn>(i).VisibleIndex = i;
            }
            Repositories.Add(repository);
        }        

        public void SetMappingGrid(DevExpress.XtraGrid.Views.Grid.GridView gridView, bool showFooter)
        {
            gridView.GridControl.RepositoryItems.Clear();
            gridView.GroupSummary.Clear();
            gridView.Columns.Clear();
            gridView.GridControl.DataSource = null;
            InitRepositories(null);
            gridView.Columns.Capacity = 0;
            gridView.Columns.AddRange(JGridColumns.ToArray());
            gridView.GridControl.RepositoryItems.AddRange(Repositories.ToArray());
            GridGroupSummaryItem item = new GridGroupSummaryItem();
            item.SummaryType = DevExpress.Data.SummaryItemType.Count;
            item.DisplayFormat = " кол-во = {0}";
            gridView.GroupSummary.Add(item);
            gridView.OptionsView.ShowFooter = showFooter;
            //ClearLists();
            gridView.GridControl.DataSource = this.DTSource;
            gridView.BestFitColumns();
        }
        /*gridView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
           new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "код",, ""),
           new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "кол-во мест", null, "(SUM by Unit Price={0:c})")});/*,
           new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Average, "UnitsInStock", this.gridColumn4, "AVG={0:#.##}"),
           new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "UnitsOnOrder", this.gridColumn5, "SUM={0}")});*/
        public void SetMappingGrid(DevExpress.XtraGrid.Views.Grid.GridView gridView, DataSet source)
        {
            gridView.GridControl.RepositoryItems.Clear();
            gridView.GroupSummary.Clear();
            gridView.Columns.Clear();
            gridView.GridControl.DataSource = null;
            
            InitRepositories(source);
            gridView.Columns.Capacity = 0;
            gridView.Columns.Clear();
            gridView.Columns.AddRange(JGridColumns.ToArray());

            gridView.GridControl.RepositoryItems.AddRange(Repositories.ToArray());

            GridGroupSummaryItem item = new GridGroupSummaryItem();
            item.SummaryType = DevExpress.Data.SummaryItemType.Count;
            item.DisplayFormat = " кол-во = {0}";
            gridView.GroupSummary.Add(item);

            gridView.OptionsView.ShowFooter = true;

            gridView.GridControl.DataSource = this.DTSource;
            gridView.BestFitColumns();
        }

        public void SetMappingGrid(DevExpress.XtraGrid.Views.Tile.TileView tileView, DataSet source)
        {
            //tileView.GridControl.RepositoryItems.Clear();
            //tileView.GroupSummary.Clear();
            //tileView.Columns.Clear();
            tileView.GridControl.DataSource = null;
            InitRepositories(source);
            //tileView.Columns.Capacity = 0;
            tileView.Columns.AddRange(JGridColumns.ToArray());
            tileView.Columns[1].ColumnEditName = tileView.Columns[12].ColumnEditName;
            tileView.GridControl.RepositoryItems.AddRange(Repositories.ToArray());
            //GridGroupSummaryItem item = new GridGroupSummaryItem();
            //item.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //item.DisplayFormat = " кол-во = {0}";
            ////tileView.GroupSummary.Add(item);
            ////tileView.OptionsView.ShowFooter = showFooter;
            ////ClearLists();
            tileView.GridControl.DataSource = this.DTSource;
            //tileView.BestFitColumns();
        }

        public void SetMappingGrid(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            gridView.GridControl.RepositoryItems.Clear();
            gridView.GroupSummary.Clear();
            gridView.Columns.Clear();
            gridView.GridControl.DataSource = null;

            InitRepositories();
            gridView.Columns.Capacity = 0;
            gridView.Columns.Clear();
            gridView.Columns.AddRange(JGridColumns.ToArray());

            gridView.GridControl.RepositoryItems.AddRange(Repositories.ToArray());

            GridGroupSummaryItem item = new GridGroupSummaryItem();
            item.SummaryType = DevExpress.Data.SummaryItemType.Count;
            item.DisplayFormat = " кол-во = {0}";
            gridView.GroupSummary.Add(item);

            gridView.OptionsView.ShowFooter = true;

            gridView.GridControl.DataSource = this.DTSource;
            gridView.BestFitColumns();
        }

        private Predicate<JMappingGrid> ByName(string name)
        {
            return delegate (JMappingGrid item)
            {
                return item.Name.CompareTo(name) == 0;
            };
        }

        private void InitRepositories(DataSet source = null)
        {            
            RelationReference reference;
            RepositoryItem repository;
            foreach (ExtendGridColumn col in JGridColumns)
            {
                if (col.SummaryCount)
                    col.SummaryItem.SetSummary(SummaryItemType.Count, "Записей: {0}");

                switch (col.ColumnRepositoryType)
                {
                    case TypeRepositoryColumn.RelationTable:
                        reference = RelationsTables.Find(RelTableByColumnIndex((int)col.Tag)).reference;
                        if (source != null)
                        {
                            if (reference.DTSource is null)
                                reference.DTSource = source.Tables[source.Tables.IndexOf(reference._tableName)]; //JMemoryDataSource.GetDataTable(reference._tableName);
                        }
                        repository = col.InitItemLookUpEdit(col.Caption, reference.DTSource);
                        if (repository == null) return;
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.RelationTableViewFilter:
                        reference = RelationsTables.Find(RelTableByColumnIndex((int)col.Tag)).reference;
                        repository = col.InitItemLookUpEditViewFilter(col.Caption, reference.DTSource, col.TextFilter);
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.RelationTableLookUp:
                        reference = RelationsTables.Find(RelTableByColumnIndex((int)col.Tag)).reference;
                        repository = col.InitItemLookUpEdit(col.Caption, reference.DTSource, true);
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.RelationTableCheckedComboBoxEdit:
                        reference = RelationsTables.Find(RelTableByColumnIndex((int)col.Tag)).reference;
                        repository = col.InitItemCheckedComboBoxEdit(col.Caption, reference.DTSource, true);
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.RelationTableFilter:
                        reference = RelationsTables.Find(RelTableByColumnIndex((int)col.Tag)).reference;
                        repository = col.InitItemLookUpEditFilter(col.Caption, reference.DTSource, col.FieldName);
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.RelationTableVisible:
                        reference = RelationsTables.Find(RelTableByColumnIndex((int)col.Tag)).reference;
                        repository = col.InitItemLookUpEditVisible(col.Caption, reference.DTSource);
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.Price:
                        repository = col.InitItemCalcEdit();
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);

                        if (col.SpecificCulture != null)
                            System.Threading.Thread.CurrentThread.CurrentCulture =
                            System.Globalization.CultureInfo.CreateSpecificCulture(col.SpecificCulture);
                        else
                            System.Threading.Thread.CurrentThread.CurrentCulture =
                                System.Globalization.CultureInfo.CreateSpecificCulture("uk");//
                        System.Threading.Thread.CurrentThread.CurrentUICulture =
                            System.Threading.Thread.CurrentThread.CurrentCulture;

                        col.DisplayFormat.FormatString = "c";
                        col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        col.SummaryItem.SetSummary(SummaryItemType.Sum, "Всего={0}");

                        System.Threading.Thread.CurrentThread.CurrentCulture =
                            System.Globalization.CultureInfo.CreateSpecificCulture("ru");
                        System.Threading.Thread.CurrentThread.CurrentUICulture =
                            System.Threading.Thread.CurrentThread.CurrentCulture;
                        continue;
                    case TypeRepositoryColumn.Percent:
                        repository = col.InitItemPercentEdit();
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.Int:
                        repository = col.InitItemSpinIntEdit();
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.Numeric:
                        repository = col.InitItemSpinNumericEdit();
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.DateTime:
                        repository = col.InitItemDateTimeEdit(col.NullText);
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.Date:
                        repository = col.InitItemDateEdit();
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.String:
                        repository = col.InitItemTextEdit(col.TextMaxLength);
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.Image:
                        continue;
                    case TypeRepositoryColumn.TextEditMask:
                        repository = col.InitItemTextEditMask(col.TextEditMask);
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.Time:
                        repository = col.InitItemTimeEdit();
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.MemoEdit:
                        repository = col.InitItemMemoEdit();
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.CheckedComboBoxEdit:
                        repository = col.InitItemCheckedComboBoxEdit(col.CountItemCheckedCombo);
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.CheckedExtend:
                        repository = col.InitItemCheckEditExtend();
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.Checked:
                        repository = col.InitItemCheckEdit();
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.PictureEdit:
                        repository = col.InitItemPictureEdit();
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    case TypeRepositoryColumn.CalcEdit:
                        repository = col.InitItemCalcEdit();
                        col.ColumnEdit = repository;
                        Repositories.Add(repository);
                        continue;
                    default:
                        throw new NotImplementedException("Не определен передаваемый тип данных столбца сетки.");
                }
            }
        }
        void InitJGridColumns()
        {
            JGridColumns.Clear();
            int i = 0;
            foreach (DataColumn col in DTSource.Columns)
            {
                ExtendGridColumn gridColumn = new ExtendGridColumn();
                gridColumn.Caption = col.Caption;
                gridColumn.FieldName = col.ColumnName;
                gridColumn.Name = "gridColumn" + i.ToString();
                gridColumn.Visible = true;
                gridColumn.VisibleIndex = i;
                gridColumn.Tag = i;
                gridColumn.ColumnRepositoryType = TypeRepositoryColumn.String;
                JGridColumns.Add(gridColumn);
                i++;
            }
        }
        protected Predicate<RelationTable> RelTableByColumnIndex(int index)
        {
            return delegate (RelationTable relationTable)
            {
                return relationTable.columnIndex == index;
            };
        }
        private Predicate<ExtendGridColumn> ExGridColumnByColumnName(string name)
        {
            return delegate (ExtendGridColumn col)
            {
                return col.FieldName.ToLower() == name.ToLower();
            };
        }

    }

}
