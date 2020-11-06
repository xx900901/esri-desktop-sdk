using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Tasks.Geocoding;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArcGISApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }
        private LocatorTask _locatorTask;
        
        private async void Initialize()
        {


            string singleLineInput = "TOLT AVE / EASEMENT";
            //string singleLineInput = "173 hunter st, atlanta";
            Uri localLocator = new Uri(@"E:\Cases\02518615\GeocodeTest\GeocodeTest\bin\Debug\NORCOM\locator\Composite\v101\Composite.loc");
            //Uri localLocator = new Uri(@"E:\Cases\02518615\New Folder\Untitled\locator\streets_composite\v101\streets_composite.loc");
            //Uri _serviceUri = new Uri("https://supt0007894.esri.com/server/rest/services/Composite/GeocodeServer");
            Uri _serviceUri = new Uri("https://supt0007894.esri.com/server/rest/services/Composite_arcmap/GeocodeServer");
            _locatorTask = await LocatorTask.CreateAsync(_serviceUri);
            var results = await _locatorTask.GeocodeAsync(singleLineInput);
            StringBuilder formattedResults = new StringBuilder();
            foreach (GeocodeResult result in results)
            {
                string latlon = $"{result.DisplayLocation.Y} ~ {result.DisplayLocation.X}";
                formattedResults.AppendLine($"{result.Score.ToString("F")}\t{result.Label}\t{latlon}");
            }
            if (results.Count == 0)
                MessageBox.Show("no candidates returned");
            else
            {
                MessageBox.Show(formattedResults.ToString());

            }
        }
    }
}
       
        

        
