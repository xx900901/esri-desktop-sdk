using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using ESG.Internal.ArcGISTest.Model;
using System.Linq;

namespace ESG.Internal.ArcGISTest
{
    /// <summary>
    /// Provides map data to an application
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindowViewModel()
        {
            this._map = new Map(new Basemap());
            this._LayerFileSources = new ObservableCollection<LayerFileSource>();
            this._map.LoadStatusChanged += Map_LoadStatusChanged;
            this._map.PropertyChanged += Map_PropertyChanged;
        }

        /// <summary>
        /// The one and only map used by the application.
        /// </summary>
        private Map _map = null;
        public Map Map
        {
            get
            {
                return this._map;
            }

            set
            {
                this._map = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// The names of the basemaps from which the user can choose.
        /// </summary>
        public string[] BasemapNames
        {
            get
            {
                string[] basemapNames = { "Generic", "Streets", "Streets (Vector)", "Streets Relief (Vector)", "Topographic", "Topographic (Vector)", "Imagery", "Imagery with Labels", "Imagery with Labels (Vector)", "Ocean", "Dark Gray (Vector)",  "Light Gray", "Light Gray (Vector)", "National Geographic", "Navigation", "Streets Night (Vector)", "Terrain", "Terrain (Vector)"};

                return basemapNames;
            }
        }

        /// <summary>
        /// Logging text.
        /// </summary>
        private string _LoggingText = "";
        public string LoggingText
        {
            get
            {
                return this._LoggingText;
            }

            set
            {
                if (string.IsNullOrEmpty(this._LoggingText))
                    this._LoggingText = value;
                else
                    this._LoggingText = this._LoggingText + "\n" + value;

                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the basemap, by name.
        /// </summary>
        private string _SelectedBasemapName = null;
        public string SelectedBasemapName
        {
            get
            {
                string name = this._SelectedBasemapName;

                if (name == null)
                    name = this.BasemapNames[0];

                return name;
            }

            set
            {
                this._SelectedBasemapName = value;

                switch (value)
                {
                    case "Generic":
                        this.Map.Basemap = new Basemap();
                        break;
                    case "Topographic":
                        this.Map.Basemap = Basemap.CreateTopographic();
                        break;
                    case "Streets":
                        this.Map.Basemap = Basemap.CreateStreets();
                        break;
                    case "Imagery":
                        this.Map.Basemap = Basemap.CreateImagery();
                        break;
                    case "Ocean":
                        this.Map.Basemap = Basemap.CreateOceans();
                        break;
                    case "Streets (Vector)":
                        this.Map.Basemap = Basemap.CreateStreetsVector();
                        break;
                    case "Streets Relief (Vector)":
                        this.Map.Basemap = Basemap.CreateStreetsWithReliefVector();
                        break;
                    case "Topographic (Vector)":
                        this.Map.Basemap = Basemap.CreateTopographicVector();
                        break;
                    case "Imagery with Labels":
                        this.Map.Basemap = Basemap.CreateImageryWithLabels();
                        break;
                    case "Imagery with Labels (Vector)":
                        this.Map.Basemap = Basemap.CreateImageryWithLabelsVector();
                        break;
                    case "Dark Gray (Vector)":
                        this.Map.Basemap = Basemap.CreateDarkGrayCanvasVector();
                        break;
                    case "Light Gray":
                        this.Map.Basemap = Basemap.CreateLightGrayCanvas();
                        break;
                    case "Light Gray (Vector)":
                        this.Map.Basemap = Basemap.CreateLightGrayCanvasVector();
                        break;
                    case "National Geographic":
                        this.Map.Basemap = Basemap.CreateNationalGeographic();
                        break;
                    case "Navigation":
                        this.Map.Basemap = Basemap.CreateNavigationVector();
                        break;
                    case "Streets Night (Vector)":
                        this.Map.Basemap = Basemap.CreateStreetsNightVector();
                        break;
                    case "Terrain":
                        this.Map.Basemap = Basemap.CreateTerrainWithLabels();
                        break;
                    case "Terrain (Vector)":
                        this.Map.Basemap = Basemap.CreateTerrainWithLabelsVector();
                        break;
                    default:
                        break;
                }

                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// The layer file sources displayed in the map.
        /// </summary>
        private ObservableCollection<LayerFileSource> _LayerFileSources = null;
        public ObservableCollection<LayerFileSource> LayerFileSources
        {
            get
            {
                return this._LayerFileSources;
            }
        }

        /// <summary>
        /// Returns the combined bounds of all layers.
        /// </summary>
        public Envelope AllLayerBounds
        {
            get
            {
                Envelope allBounds = null;
                Envelope someBounds = null;

                foreach (LayerFileSource lfs in this.LayerFileSources)
                {
                    foreach (TesterLayer tl in lfs.Children)
                    {
                        if ((tl.FeatureLayer != null) && (tl.FeatureLayer.FullExtent != null))
                        {
                            someBounds = tl.FeatureLayer.FullExtent;

                            if (!double.IsNaN(someBounds.XMin) && !double.IsNaN(someBounds.YMax) && !double.IsNaN(someBounds.XMax) && !double.IsNaN(someBounds.YMax))
                            {
                                if (allBounds == null)
                                    allBounds = someBounds;
                                else
                                    allBounds = this.CombineEnvelopes(allBounds, someBounds);
                            }
                        }
                    }
                }

                return allBounds;
            }
        }

        /// <summary>
        /// Raises the <see cref="MainWindowViewModel.PropertyChanged" /> event
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var propertyChangedHandler = PropertyChanged;
            if (propertyChangedHandler != null)
                propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// The property change event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// This event is broadcast once all layers have loaded in the map, or failed trying.
        /// </summary>
        public event EventHandler AllLayersLoaded;

        /// <summary>
        /// Am I already displaying the passed layer file source?
        /// </summary>
        /// <param name="path">the path to the file</param>
        /// <returns>do I already have this?</returns>
        private bool HasLayerFileSource(string path)
        {
            bool hasSource = false;
            Uri incoming = new Uri(path);
            Uri current = null;

            foreach (LayerFileSource lfs in this.LayerFileSources)
            {
                current = new Uri(lfs.SourceFile);

                if (current.Equals(incoming))
                    hasSource = true;
            }

            return hasSource;
        }

        /// <summary>
        /// Adds a SQLite geodatabase to the list of layers.
        /// </summary>
        /// <param name="pathToGeodatabase">the file to be loaded</param>
        /// <returns>simply a marker indicating that things completed (true all good, false not all good)</returns>
        public async Task<bool> AddSQLiteGeodatabaseAsync(string pathToGeodatabase)
        {
            bool allGood = true;

            if (!this.HasLayerFileSource(pathToGeodatabase))
            {
                LayerFileSource lfs = new LayerFileSource();
                lfs.SourceFile = pathToGeodatabase;
                this.LayerFileSources.Add(lfs);
                Geodatabase gdb = await Geodatabase.OpenAsync(pathToGeodatabase);

                //
                // Store the test layers.
                //
                foreach (GeodatabaseFeatureTable gft in gdb.GeodatabaseFeatureTables)
                {
                    TesterLayer testLayer = new TesterLayer();
                    testLayer.FeatureTable = gft;
                    gft.PropertyChanged += GeodbFeatTab_PropertyChanged;
                    lfs.Children.Add(testLayer);
                }

                //
                // Now load them all.
                //
                foreach (TesterLayer tl in lfs.Children)
                {
                    GeodatabaseFeatureTable gtab = tl.FeatureTable;

                    try
                    {
                        await gtab.LoadAsync();
                        FeatureLayer fLayer = new FeatureLayer(gtab);
                        fLayer.PropertyChanged += FeatureLayer_PropertyChanged;
                        await fLayer.LoadAsync();
                        fLayer.LabelsEnabled = true;
                        this.Map.OperationalLayers.Add(fLayer);
                        tl.FeatureLayer = fLayer;
                    }
                    catch (Exception exc)
                    {
                        tl.LayerLoadException = exc;
                        allGood = false;
                        tl.LoadDone = true;
                    }
                }
            }

            return allGood;
        }

        /// <summary>
        /// Adds a MMPK map source.
        /// </summary>
        /// <param name="fileName">the MMPK file</param>
        /// <returns>the opened MMPK</returns>
        public async Task<MobileMapPackage> AddMMPKAsync(string fileName)
        {
            List<Map> maps = new List<Map>();
            MobileMapPackage mmpk = null;

            if (!this.HasLayerFileSource(fileName))
            {
                LayerFileSource lfs = new LayerFileSource();
                lfs.SourceFile = fileName;
                this.LayerFileSources.Add(lfs);
                mmpk = await MobileMapPackage.OpenAsync(fileName);
                await mmpk.LoadAsync();

                foreach (Map aMap in mmpk.Maps)
                {
                    maps.Add(aMap);
                    var layerCollection = aMap.OperationalLayers.Reverse();
                    await aMap.LoadAsync();
                    Trace.WriteLine("Map = " + aMap.ToString());
                    foreach (var layer in layerCollection)
                    {
                        Layer lyr = layer.Clone();
                        lyr.IsVisible = layer.IsVisible;
                        //if (layer.Opacity == 1)
                        //    layer.Opacity = .55;
                        lyr.Opacity = layer.Opacity;
                        await lyr.LoadAsync();

                        if (lyr is FeatureLayer)
                        {
                            TesterLayer testLayer = new TesterLayer();                            
                            FeatureLayer featLyr = lyr as FeatureLayer;
                            GeodatabaseFeatureTable featureTable = featLyr.FeatureTable as GeodatabaseFeatureTable;
                            testLayer.FeatureTable = featureTable;
                            testLayer.FeatureLayer = featLyr;
                            lfs.Children.Add(testLayer);
                            ((FeatureLayer)lyr).LabelsEnabled = true;
                            ((FeatureLayer)lyr).DefinitionExpression = ((FeatureLayer)layer).DefinitionExpression;
                        }
                        else if (lyr is GroupLayer)
                        {
                            SetupLayersUnderGroupLayer((GroupLayer)lyr, (GroupLayer)layer, lfs);
                        }

                        this.Map.OperationalLayers.Add(lyr);

                    }
                }
            }

            return mmpk;
        }


        /// <summary>
        /// Copies the definition expressions from any feature layers under the source layer to the matching feature layers under the target layer.
        /// </summary>
        /// <param name="targetLayer">the target group layer</param>
        /// <param name="sourceLayer">the source group layer</param>
        private static void SetupLayersUnderGroupLayer(GroupLayer targetLayer, GroupLayer sourceLayer, LayerFileSource lfs)
        {
            Dictionary<string, string> defExprs = new Dictionary<string, string>();
            
            //
            // Collect all definition expressions.
            //
            foreach (Layer srcChildLayer in sourceLayer.Layers)
            {
                if (srcChildLayer is FeatureLayer)
                {
                    TesterLayer testLayer = new TesterLayer();
                    testLayer.FeatureLayer = srcChildLayer as FeatureLayer;
                    GeodatabaseFeatureTable featureTable = testLayer.FeatureLayer.FeatureTable as GeodatabaseFeatureTable;
                    testLayer.FeatureTable = featureTable;
                    lfs.Children.Add(testLayer);
                    FeatureLayer srcLayer = (FeatureLayer)srcChildLayer;
                    string defExpr = srcLayer.DefinitionExpression;

                    if (!string.IsNullOrEmpty(defExpr))
                        defExprs[srcLayer.Name] = defExpr;
                }
            }

            //
            // Apply all definition expressions.  Cache them as derived.
            //
            foreach (Layer tgtChildLayer in targetLayer.Layers)
            {
                if (tgtChildLayer is FeatureLayer)
                {
                    TesterLayer testLayer = new TesterLayer();
                    testLayer.FeatureLayer = tgtChildLayer as FeatureLayer;
                    GeodatabaseFeatureTable featureTable = testLayer.FeatureLayer.FeatureTable as GeodatabaseFeatureTable;
                    testLayer.FeatureTable = featureTable;
                    lfs.Children.Add(testLayer);
                    FeatureLayer tgtLayer = (FeatureLayer)tgtChildLayer;
                    tgtLayer.LabelsEnabled = true;

                    // This will apply the mapservice name to the feature layer under the group layers so that 
                    // the results from this layer will be under the same map service or else it will be in a different set
                    // under a system generated hashcode for this layer.
                    tgtLayer.Id = targetLayer.Id;

                    if (defExprs.ContainsKey(tgtLayer.Name))
                    {
                        tgtLayer.DefinitionExpression = defExprs[tgtLayer.Name];
                    }
                }
            }
        }


        /// <summary>
        /// Temporary method...throws the exception information into the log.
        /// </summary>
        /// <param name="layer"></param>
        private void LogLayerLoadException(TesterLayer layer)
        {
            string excString = "=====================\n";
            excString = excString + "Error loading layer: " + layer.Name + "\n";
            excString = excString + layer.LayerLoadException.Message + "\n";
            excString = excString + layer.LayerLoadException.StackTrace + "\n";
            this.LoggingText = excString;
        }

        /// <summary>
        /// Once the layer's full extent is there, and all layers have been determined to have loaded or failed trying,
        /// raises the AllLayersLoaded event.
        /// </summary>
        /// <param name="sender">the feature layer</param>
        /// <param name="e">information about the property that has changed</param>
        private void FeatureLayer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            FeatureLayer featLayer = (FeatureLayer)sender;
            Trace.WriteLine("Feature layer property changed = " + e.PropertyName + " against " + featLayer.Name);

            if ("FullExtent".Equals(e.PropertyName))
            {
                this.MarkLayerLoaded(featLayer);
                this.RaiseEventIfAllLoaded();
            }
        }

        /// <summary>
        /// If all layers are loaded, raise the event indicating such.
        /// </summary>
        private void RaiseEventIfAllLoaded()
        {
            bool allLoaded = true;

            foreach (LayerFileSource lfs in this.LayerFileSources)
            {
                foreach (TesterLayer tl in lfs.Children)
                {
                    if (!tl.LoadDone)
                    {
                        allLoaded = false;
                        break;
                    }
                }
            }

            if ((allLoaded) && (this.AllLayersLoaded != null))
                this.AllLayersLoaded(this, new EventArgs());
        }

        /// <summary>
        /// Adds the specified vector tile package as a base layer.
        /// </summary>
        /// <param name="vtpkFile">the vector tile package</param>
        /// <returns>a boolean indicating whether or not things went well</returns>
        public async Task<bool> AddVectorTileLayerAsync(string vtpkFile)
        {
            bool allGood = true;

            if (!this.HasLayerFileSource(vtpkFile))
            {
                LayerFileSource lfs = new LayerFileSource();
                lfs.SourceFile = vtpkFile;
                this.LayerFileSources.Add(lfs);
                TesterLayer testLayer = new TesterLayer();

                try
                {
                    ArcGISVectorTiledLayer vtpk = new ArcGISVectorTiledLayer(new Uri(vtpkFile));
                    testLayer.VTPK = vtpk;
                    lfs.Children.Add(testLayer);
                    vtpk.PropertyChanged += Vtpk_PropertyChanged;
                    await vtpk.LoadAsync();
                    this.Map.Basemap.BaseLayers.Add(vtpk);
                }
                catch (Exception e)
                {
                    allGood = false;
                    testLayer.LayerLoadException = e;
                    this.LogLayerLoadException(testLayer);
                }
            }

            return allGood;
        }

        /// <summary>
        /// Once all is loaded, sends the map bounds message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vtpk_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Trace.WriteLine("VTPK property changed = " + e.PropertyName + " against " + sender);
            ArcGISVectorTiledLayer vtpk = (ArcGISVectorTiledLayer)sender;

            if ("FullExtent".Equals(e.PropertyName))
            {
                this.MarkLayerLoaded(vtpk);
                this.RaiseEventIfAllLoaded();
            }
        }

        private void GeodbFeatTab_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GeodatabaseFeatureTable featTab = (GeodatabaseFeatureTable)sender;
            Trace.WriteLine("Geodatabase property changed = " + e.PropertyName + " against " + featTab.TableName);
        }

        private void Map_LoadStatusChanged(object sender, Esri.ArcGISRuntime.LoadStatusEventArgs e)
        {
            Trace.WriteLine("Map load status = " + e.Status);
        }

        private void Map_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Trace.WriteLine("Map property changed = " + e.PropertyName);
        }

        /// <summary>
        /// Sets the LoadDone property on the corresponding TesterLayer to true.
        /// </summary>
        /// <param name="aLayer">for which to mark the corresponding TesterLayer as loaded</param>
        private void MarkLayerLoaded(FeatureLayer aLayer)
        {
            foreach (LayerFileSource lfs in this.LayerFileSources)
            {
                foreach (TesterLayer tl in lfs.Children)
                {
                    if ((tl.FeatureLayer != null) && (tl.FeatureLayer == aLayer))
                    {
                        tl.LoadDone = true;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the LoadDone property on the corresponding TesterLayer to true.
        /// </summary>
        /// <param name="aLayer">for which to mark the corresponding TesterLayer as loaded</param>
        private void MarkLayerLoaded(ArcGISVectorTiledLayer aLayer)
        {
            foreach (LayerFileSource lfs in this.LayerFileSources)
            {
                foreach (TesterLayer tl in lfs.Children)
                {
                    if ((tl.VTPK != null) && (tl.VTPK == aLayer))
                    {
                        tl.LoadDone = true;
                    }
                }
            }
        }

        /// <summary>
        /// Combines the 2 passed envelopes into 1.
        /// </summary>
        /// <param name="envelope1">first envelope</param>
        /// <param name="envelope2">second envelope</param>
        /// <returns>combined envelope</returns>
        private Envelope CombineEnvelopes(Envelope envelope1, Envelope envelope2)
        {
            SpatialReference spaRef = envelope1.SpatialReference;

            if (spaRef == null)
                spaRef = envelope2.SpatialReference;

            double xmin = Math.Min(envelope1.XMin, envelope2.XMin);
            double xmax = Math.Max(envelope1.XMax, envelope2.XMax);
            double ymin = Math.Min(envelope1.YMin, envelope2.YMin);
            double ymax = Math.Max(envelope1.YMax, envelope2.YMax);

            return new Envelope(xmin, ymin, xmax, ymax, spaRef);
        }

        /// <summary>
        /// Temporary method.  Just displays attributes and values from a single record in the passed table.
        /// </summary>
        /// <param name="testLayer">from which to get the single record</param>
        /// <returns>marker indicating the task is complete</returns>
        public async Task<bool> ShowSingleRecordAsync(TesterLayer testLayer)
        {
            if (testLayer.LayerLoadException != null)
                this.LogLayerLoadException(testLayer);
            else if (testLayer.FeatureTable != null)
            {
                FeatureTable aTable = testLayer.FeatureTable;
                QueryParameters queryParams = new QueryParameters();
                queryParams.MaxFeatures = 1;
                queryParams.ReturnGeometry = true;
                queryParams.WhereClause = "1=1";
                FeatureQueryResult fqr = await aTable.QueryFeaturesAsync(queryParams);
                IEnumerator<Feature> features = fqr.GetEnumerator();
                this.LoggingText = "=============";
                this.LoggingText = "TABLE: " + aTable.TableName;
                Feature aFeature = null;

                while (features.MoveNext())
                {
                    aFeature = features.Current;
                    this.LoggingText = "\tShape = " + aFeature.Geometry;

                    foreach (string attName in aFeature.Attributes.Keys)
                    {
                        this.LoggingText = "\t" + attName + " = " + aFeature.Attributes[attName];
                    }
                }

                this.LoggingText = "=============";
            }

            return true;
        }

        /// <summary>
        /// Creates a frequency analysis file for the specified table.
        /// </summary>
        /// <param name="outputFile">the file to which to write the analysis</param>
        /// <param name="table">the table to analyse</param>
        /// <returns>did things go well?</returns>
        public async Task<bool> RunFrequencyAnalysis(string outputFile, GeodatabaseFeatureTable table)
        {
            bool allGood = true;
            long totalRecs = table.NumberOfFeatures;
            Dictionary<string, Dictionary<string, uint>> freqs = new Dictionary<string, Dictionary<string, uint>>();
            string whereClause = "1=1";
            string orderingName = null;
            long lastOID = 0;
            int lastBatchCount = -1;
            long totalRecordsProcessed = 0;
            int recordsPerBatch = 10000;

            foreach (Field aField in table.Fields)
            {
                if (aField.FieldType == FieldType.OID)
                {
                    orderingName = aField.Name;
                    whereClause = aField.Name + " > " + lastOID + " order by " + aField.Name;
                    break;
                }
            }

            try
            {
                while (lastBatchCount != 0)
                {
                    QueryParameters queryParams = new QueryParameters();
                    queryParams.ReturnGeometry = true;
                    lastBatchCount = 0;

                    if (orderingName == null)
                        queryParams.WhereClause = "1=1";
                    else
                        queryParams.WhereClause = orderingName + " > " + lastOID + " order by " + orderingName;

                    FeatureQueryResult result = await table.QueryFeaturesAsync(queryParams);
                    IEnumerator<Feature> features = result.GetEnumerator();
                    object attValue = null;
                    string attStringValue = null;

                    //
                    // Collect the frequencies.
                    //
                    while (features.MoveNext())
                    {
                        Feature aFeature = features.Current;
                        totalRecordsProcessed++;
                        lastBatchCount++;

                        foreach (string attName in aFeature.Attributes.Keys)
                        {
                            attValue = aFeature.Attributes[attName];

                            if (attValue == null)
                                attStringValue = "";
                            else
                                attStringValue = attValue.ToString();

                            this.StoreFrequency(attName, attStringValue, freqs);

                            if (orderingName.Equals(attName))
                                lastOID = (long)attValue;
                        }

                        if (totalRecordsProcessed % 2500 == 0)
                            Trace.WriteLine("Processed " + totalRecordsProcessed + " out of " + table.NumberOfFeatures);

                        if (lastBatchCount == recordsPerBatch)
                        {
                            features.Dispose();
                            break;
                        }
                    }
                }

                //
                // Write out the frequencies.
                //
                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    foreach (string attributeName in freqs.Keys)
                    {
                        Dictionary<string, uint> attributeFreqs = freqs[attributeName];

                        if (attributeFreqs.Count > 200)
                        {
                            writer.Write("\"");
                            writer.Write(attributeName);
                            writer.Write("\"");
                            writer.Write(",");
                            writer.Write("\"");
                            writer.Write("<MANY VALUES>");
                            writer.Write("\"");
                            writer.Write(",");
                            writer.Write(table.NumberOfFeatures);
                            writer.Write("\n");
                        }
                        else
                        {
                            foreach (string attributeValue in attributeFreqs.Keys)
                            {
                                uint numVals = attributeFreqs[attributeValue];
                                writer.Write("\"");
                                writer.Write(attributeName);
                                writer.Write("\"");
                                writer.Write(",");
                                writer.Write("\"");
                                writer.Write(attributeValue);
                                writer.Write("\"");
                                writer.Write(",");
                                writer.Write(numVals);
                                writer.Write("\n");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                allGood = false;
                string excString = "=====================\n";
                excString = excString + e.Message + "\n";
                excString = excString + e.StackTrace + "\n";
                this.LoggingText = excString;
            }

            return allGood;
        }

        /// <summary>
        /// Caches the frequency information.
        /// </summary>
        /// <param name="attributeName">the name of the attribute</param>
        /// <param name="attributeValue">the value for the attribute</param>
        /// <param name="freqs">the current frequency set</param>
        private void StoreFrequency(string attributeName, string attributeValue, Dictionary<string, Dictionary<string, uint>> freqs)
        {
            if (!freqs.ContainsKey(attributeName))
                freqs.Add(attributeName, new Dictionary<string, uint>());

            Dictionary<string, uint> valueFreqs = freqs[attributeName];

            if (valueFreqs.Count < 201)
            {
                if (valueFreqs.ContainsKey(attributeValue))
                    valueFreqs[attributeValue] = valueFreqs[attributeValue] + 1;
                else
                    valueFreqs[attributeValue] = 1;
            }
        }
    }
}