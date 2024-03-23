using IST.JMapping.Interface;
using IST.JMapping.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.JMapping
{
    public class MappingGridsCollection : SupportIEntityCollection<JMappingGrid>
    {
        private IDataSource _source;
        public int Count { get { return base.ItemsCount; } }

        public MappingGridsCollection(IDataSource source)
        {
            _source = source;
        }
        public JMappingGrid this[string name]
        {
            get { return base.FindItem(name); }
        }
        public JMappingGrid this[int index]
        {
            get { return base.ElementAt(index); }
        }
        public JMappingGrid Add(string name, string tableName)
        {
            try
            {
                JMappingGrid mg = new JMappingGrid(_source, tableName);
                mg.Name = name;
                if (base.AddItem(mg))
                    return mg;
                else
                {
                    DevExMessage.MessageBoxWarning("Элемент с таким именем уже существует");
                    return null;
                }
            }
            catch (Exception ex)
            {
                DevExMessage.MessageBoxWarning(ex.Message);
                return null;
            }
        }
        public JMappingGrid Add(string tableName)
        {
            try
            {
                JMappingGrid mg = new JMappingGrid(_source, tableName);
                if (base.AddItem(mg))
                    return mg;
                else
                {
                    DevExMessage.MessageBoxWarning("Элемент с таким именем уже существует");
                    return null;
                }
            }
            catch (Exception ex)
            {
                DevExMessage.MessageBoxWarning(ex.Message);
                return null;
            }
        }
        public void Add(JMappingGrid mappingGrid)
        {
            try
            {
                if (!base.AddItem(mappingGrid))
                    DevExMessage.MessageBoxWarning("Элемент с таким именем уже существует");
            }
            catch (Exception ex)
            {
                DevExMessage.MessageBoxWarning(ex.Message);
            }
        }

        public DataSet Get()
        {
            return _source.GetDataSet(); //base.GetItem();
        }
        public JMappingGrid Find(string name)
        {
            return base.FindItem(name);
        }
        public bool Exists(string name)
        {
            return base.ExistsItem(name);
        }
    }
}
