using IST.JMapping.Interface;
using IST.JMapping.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.JMapping
{
    public class JMappingData
    {
        private IDataSource _source;
        public IDataSource Source
        {
            get { return _source; }
            set { _source = value; ExSourceChanged(); }
        }

        private MappingGridsCollection _mappingGrids = null;
        public MappingGridsCollection JMappingGrids { get { return _mappingGrids; } }

        public delegate void EventSourceChanged();
        public event EventSourceChanged SourceChanged;

        public JMappingData()
        {
        }
        public JMappingData(IDataSource source)
        {
            _source = source;
            Init();
        }

        public JMappingData(List<JsonUrlTable> jsons)
        {
            try
            {
                _source = new DataSource(jsons);
                Init();
            }
            catch (Exception ex)
            {
                DevExMessage.MessageBoxWarning(ex.Message);
            }
        }

        public void /*IDataSource*/ CreateDataSource(List<JsonUrlTable> jsons)
        {
            try
            {
                _source = new DataSource(jsons);
                Init();
                //JMemoryDataSource.AddRange(_source.GetData());
            }
            catch (Exception ex)
            {
                DevExMessage.MessageBoxError(ex.Message);
            }
            //try
            //{
            //    _source = new DataSource(jsons);
            //    Init();
            //    return _source;
            //}
            //catch (Exception ex)
            //{
            //    DevExMessage.MessageBoxWarning(ex.Message);
            //    return null;
            //}
        }

        private void Init()
        {
            SourceChanged += new EventSourceChanged(Source_SourceChanged);
            //_mappingControls = new MappingControls(_source);
            _mappingGrids = new MappingGridsCollection(_source);
            //_sqlPerformer = new PerformerSQL();
        }

        void ExSourceChanged()
        {
            if (SourceChanged != null)
                SourceChanged();
        }
        void Source_SourceChanged()
        {
            DevExMessage.MessageBoxInformation("EVENT_SOURCE_CHANGED");
        }
    }
}
