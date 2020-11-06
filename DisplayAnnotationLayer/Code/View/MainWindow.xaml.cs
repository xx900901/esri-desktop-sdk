using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using Microsoft.Win32;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.Symbology;
using ESG.Internal.ArcGISTest.Model;
using System.Linq;

namespace ESG.Internal.ArcGISTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The dialog for adding a layer.
        /// </summary>
        private OpenFileDialog LayerOpenDialog = null;

        /// <summary>
        /// The dialog for choosing a folder to which to save a frequency analysis.
        /// </summary>
        private SaveFileDialog FreqAnalysisDialog = null;

        /// <summary>
        /// The graphics overlay used for testing.
        /// </summary>
        private GraphicsOverlay Overlay = null;

        /// <summary>
        /// Symbol used by point graphics.
        /// </summary>
        private SimpleMarkerSymbol PointSymbol = null;

        /// <summary>
        /// Symbol used by line graphics.
        /// </summary>
        private SimpleLineSymbol LineSymbol = null;

        /// <summary>
        /// Symbol used by polygon graphics.
        /// </summary>
        private SimpleFillSymbol PolygonSymbol = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel viewModel = (MainWindowViewModel)this.FindResource("MainWindowViewModel");
            viewModel.AllLayersLoaded += ViewModel_AllLayersLoaded;
            this.InitOverlay();
            this.mapView.GraphicsOverlays.Add(this.Overlay);
        }

        /// <summary>
        /// Sets up the demo graphics overlay.
        /// </summary>
        private void InitOverlay()
        {
            this.Overlay = new GraphicsOverlay();
            this.PointSymbol = new SimpleMarkerSymbol()
            {
                Color = System.Drawing.Color.Yellow,
                Size = 30,
                Style = SimpleMarkerSymbolStyle.Square,
            };
            this.LineSymbol = new SimpleLineSymbol()
            {
                Color = System.Drawing.Color.Red,
                Style = SimpleLineSymbolStyle.Solid,
                Width = 5d
            };
            this.PolygonSymbol = new SimpleFillSymbol()
            {
                Color = System.Drawing.Color.Red,
                Style = SimpleFillSymbolStyle.Solid,
            };
        }

        /// <summary>
        /// Once all the layers are loaded, asks the view model for the combined extent of all layers
        /// and zooms the map there.
        /// </summary>
        /// <param name="sender">the MainWindowViewMdoel</param>
        /// <param name="e">empty event arguments</param>
        private void ViewModel_AllLayersLoaded(object sender, EventArgs e)
        {
            MainWindowViewModel viewModel = (MainWindowViewModel)this.FindResource("MainWindowViewModel");
            Envelope bounds = viewModel.AllLayerBounds;

            if (bounds != null)
                this.mapView.SetViewpointGeometryAsync(bounds);
        }

        /// <summary>
        /// Pops up a file dialog to allow the user to select a MMPK, geodatabase, or VTPK file to add to the map.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button_Click(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel viewModel = null;
            string fileExtension = null;

            if (this.LayerOpenDialog == null)
            {
                this.LayerOpenDialog = new OpenFileDialog();
                this.LayerOpenDialog.Filter = "SQLite Geodatabase(*.geodatabase)|*.geodatabase|Mobile Map Package(*.mmpk)|*.mmpk|Vector Tile Package(*.vtpk)|*.vtpk";
                this.LayerOpenDialog.Multiselect = false;
                this.LayerOpenDialog.Title = "Add Map Content...";
            }

            bool? result = this.LayerOpenDialog.ShowDialog();
            string fileName = null;

            if (result == true)
            {
                fileName = this.LayerOpenDialog.FileName;
                fileExtension = this.GetFileExtension(fileName);
                viewModel = (MainWindowViewModel)this.FindResource("MainWindowViewModel");

                if (!string.IsNullOrEmpty(fileExtension))
                {
                    if (".geodatabase".Equals(fileExtension))
                    {
                        await viewModel.AddSQLiteGeodatabaseAsync(fileName);
                    }
                    else if (".mmpk".Equals(fileExtension))
                    {
                        await viewModel.AddMMPKAsync(fileName);
                    }
                    else if (".vtpk".Equals(fileExtension))
                    {
                        await viewModel.AddVectorTileLayerAsync(fileName);
                    }

                    this.mapView.Map.MinScale = 0;
                    this.mapView.Map.MaxScale = 0;
                }
            }
        }





        /// <summary>
        /// Assuming there's a selected SQLite layer, prompts the user for a CSV filename and spits out a frequency
        /// analysis of the values in the specified table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void freqAnalysisButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel viewModel = (MainWindowViewModel)this.FindResource("MainWindowViewModel");
            TesterLayer selectedLayer = null;

            try
            {
                selectedLayer = (TesterLayer)this.treeView.SelectedItem;
            }
            catch (InvalidCastException)
            { }

            if ((selectedLayer != null) && (selectedLayer.FeatureTable != null))
            {
                if (this.FreqAnalysisDialog == null)
                {
                    this.FreqAnalysisDialog = new SaveFileDialog();
                    this.FreqAnalysisDialog.AddExtension = true;
                    this.FreqAnalysisDialog.Filter = "Comma-Delimited(*.csv)|*.csv";
                    this.FreqAnalysisDialog.OverwritePrompt = true;
                    this.FreqAnalysisDialog.Title = "Save Frequency Analysis...";
                }

                this.FreqAnalysisDialog.FileName = Path.Combine(this.FreqAnalysisDialog.InitialDirectory, selectedLayer.FeatureTable.TableName + ".csv");
                bool? result = this.FreqAnalysisDialog.ShowDialog();
                string fileName = null;

                if (result == true)
                {
                    fileName = this.FreqAnalysisDialog.FileName;
                    result = await viewModel.RunFrequencyAnalysis(fileName, selectedLayer.FeatureTable);

                    if (result == true)
                        MessageBox.Show("Frequency analysis for " + selectedLayer.FeatureTable.TableName + " is complete.");
                    else
                        MessageBox.Show("Frequency analysis for " + selectedLayer.FeatureTable.TableName + " FAILED.  See error log for details.");
                }
            }
        }

        /// <summary>
        /// Returns the file extension for the passed file, lowercase.
        /// </summary>
        /// <param name="fileName">the file from which to pull the extension</param>
        /// <returns>the extension, lowercase</returns>
        private string GetFileExtension(string fileName)
        {
            string ext = System.IO.Path.GetExtension(fileName);

            if (!string.IsNullOrEmpty(ext))
                ext = ext.ToLower();

            return ext;
        }

        /// <summary>
        /// Set the basemap based on the user selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBasemapChooserSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindowViewModel viewModel = (MainWindowViewModel)this.FindResource("MainWindowViewModel");
            viewModel.SelectedBasemapName = e.AddedItems[0].ToString();
        }

        /// <summary>
        /// Used to toss a view of records from a selected table into the log.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            MainWindowViewModel viewModel = (MainWindowViewModel)this.FindResource("MainWindowViewModel");
            TesterLayer selectedLayer = null;

            try
            {
                selectedLayer = (TesterLayer)this.treeView.SelectedItem;
            }
            catch (InvalidCastException)
            { }

            if (selectedLayer != null)
                await viewModel.ShowSingleRecordAsync(selectedLayer);
        }

        /// <summary>
        /// Throws down some generic test graphics on the map to test the graphics capabilities.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void graphicsButton_Click(object sender, RoutedEventArgs e)
        {
            Polygon withinBounds = this.mapView.VisibleArea;
            Envelope bounds = withinBounds.Extent;
            SpatialReference spaRef = withinBounds.SpatialReference;

            //
            // Point.
            //
            double x = (bounds.XMax + bounds.XMin) / 2.0;
            double y = (bounds.YMax + bounds.YMin) / 2.0;
            MapPoint point = new MapPoint(x, y, spaRef);
            Graphic pointGraphic = new Graphic(point, this.PointSymbol);
            this.Overlay.Graphics.Add(pointGraphic);

            //
            // Polyline.
            //
            MapPoint point1 = new MapPoint(bounds.XMax, bounds.YMax, withinBounds.SpatialReference);
            MapPoint point2 = new MapPoint(bounds.XMin, bounds.YMin, withinBounds.SpatialReference);
            List<MapPoint> points = new List<MapPoint>();
            points.Add(point1);
            points.Add(point2);
            Polyline polyline = new Polyline(points, spaRef);
            Graphic lineGraphic = new Graphic(polyline, this.LineSymbol);
            this.Overlay.Graphics.Add(lineGraphic);

            //
            // Polygon.
            //
            double length = (bounds.XMax - bounds.XMin) / 25.0;
            double height = (bounds.YMax - bounds.YMin) / 25.0;
            x = bounds.XMax;
            y = bounds.YMax;
            points = new List<MapPoint>();
            points.Add(new MapPoint(x - length, y - height, spaRef));
            points.Add(new MapPoint(x - length, y + height, spaRef));
            points.Add(new MapPoint(x + length, y + height, spaRef));
            points.Add(new MapPoint(x + length, y - height, spaRef));
            Polygon polygon = new Polygon(points, spaRef);
            Graphic polygonGraphic = new Graphic(polygon, this.PolygonSymbol);
            this.Overlay.Graphics.Add(polygonGraphic);
        }

        /// <summary>
        /// Removes all graphics from the map.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearGraphicsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Overlay.Graphics.Clear();
        }
    }
}
