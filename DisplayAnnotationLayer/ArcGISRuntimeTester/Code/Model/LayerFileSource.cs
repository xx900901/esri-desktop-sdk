using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ESG.Internal.ArcGISTest.Model
{
    public class LayerFileSource : INotifyPropertyChanged
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public LayerFileSource() : base()
        {
            this._Children = new ObservableCollection<TesterLayer>();
            this._Children.CollectionChanged += Children_CollectionChanged;
        }

        /// <summary>
        /// The source file for the layer.
        /// </summary>
        private string _SourceFile = null;
        public string SourceFile
        {
            get
            {
                return this._SourceFile;
            }
            
            set
            {
                this._SourceFile = value;
            }
        }

        /// <summary>
        /// The layers contained within the source file.
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
        /// Self's source file.
        /// </summary>
        public string Name
        {
            get
            {
                return this.SourceFile + " (" + this.Children.Count + " layers)";
            }
        }

        /// <summary>
        /// Used to notify interested parties that a property has changed.
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

        /// <summary>
        /// Simply fires of a property change for self's name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.FirePropertyChangedEvent("Name");
        }
    }
}
