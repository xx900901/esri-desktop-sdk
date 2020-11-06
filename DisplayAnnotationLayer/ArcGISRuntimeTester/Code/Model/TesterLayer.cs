using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Mapping;

namespace ESG.Internal.ArcGISTest.Model
{
    public class TesterLayer : INotifyPropertyChanged
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public TesterLayer() : base()
        {
            this._Children = new ObservableCollection<TesterLayer>();
        }

        /// <summary>
        /// The feature table from a layer.
        /// </summary>
        private GeodatabaseFeatureTable _FeatureTable = null;
        public GeodatabaseFeatureTable FeatureTable
        {
            get
            {
                return this._FeatureTable;
            }

            set
            {
                this._FeatureTable = value;
            }
        }

        /// <summary>
        /// The feature layer for the table.
        /// </summary>
        private FeatureLayer _FeatureLayer = null;
        public FeatureLayer FeatureLayer
        {
            get
            {
                return this._FeatureLayer;
            }

            set
            {
                this._FeatureLayer = value;
            }
        }

        /// <summary>
        /// A vecgtor tiled layer, if that is what this represents.
        /// </summary>
        private ArcGISVectorTiledLayer _vtpk = null;
        public ArcGISVectorTiledLayer VTPK
        {
            get
            {
                return this._vtpk;
            }

            set
            {
                this._vtpk = value;
            }
        }

        /// <summary>
        /// Returns the name of the table.
        /// </summary>
        public string Name
        {
            get
            {
                string name = null;

                if (this.FeatureTable != null)
                    name = this.FeatureTable.TableName + " (" + this.FeatureTable.NumberOfFeatures + ")";
                else if (this.VTPK != null)
                    name = this.VTPK.Name;

                if (this.LayerLoadException != null)
                    name = "*** " + name;

                return name;
            }
        }

        /// <summary>
        /// Any exception raised during loading of the layer.
        /// </summary>
        private Exception _LayerLoadException = null;
        public Exception LayerLoadException
        {
            get
            {
                return this._LayerLoadException;
            }

            set
            {
                this._LayerLoadException = value;
                this.FirePropertyChangedEvent("Name");
            }
        }

        /// <summary>
        /// Will never return anything.  Prevents binding errors.
        /// </summary>
        private ObservableCollection<TesterLayer> _Children = null;
        public ObservableCollection<TesterLayer> Children
        {
            get
            {
                return this._Children;
            }
        }

        /// <summary>
        /// Indicates whether or not the load has completed for the stored layer.
        /// </summary>
        private bool _LoadDone = false;
        public bool LoadDone
        {
            get
            {
                return this._LoadDone;
            }

            set
            {
                this._LoadDone = value;
                this.FirePropertyChangedEvent("Name");
            }
        }

        /// <summary>
        /// Event fires when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Used to raise the property changed event.
        /// </summary>
        /// <param name="propertyName">name of the property that changed</param>
        private void FirePropertyChangedEvent(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
