using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;

namespace Ring
{
    internal class Button1 : Button
    {
        protected async Task OnClickAsync()
        {
            await QueuedTask.Run(() => MainMethodCode());
        }
   
        public void MainMethodCode()
        {
            // Opens a file geodatabase. This will open the geodatabase if the folder exists and contains a valid geodatabase.
           
            using (Geodatabase geodatabase = new Geodatabase(new FileGeodatabaseConnectionPath(new Uri(@"E:\orientationtest\test.gdb"))))
            {
                // Use the geodatabase.
                
                FeatureClassDefinition featureClassDefinition = geodatabase.GetDefinition<FeatureClassDefinition>("RingLine");

            }

    
        }
    }
}
