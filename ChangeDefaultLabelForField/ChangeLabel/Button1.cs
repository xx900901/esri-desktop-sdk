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

namespace ChangeLabel
{
    internal class Button1 : Button
    {
        protected override void OnClick()
        {
            
            QueuedTask.Run(() =>
            {
                
                //var cimMap = MapView.Active.Map.GetDefinition();
                //CIMGeneralPlacementProperties labelEngine = cimMap.GeneralPlacementProperties;

                var layer = MapView.Active.Map.GetLayersAsFlattenedList().OfType<FeatureLayer>().FirstOrDefault();
                var featureLayer = layer as FeatureLayer;

                //Note: call within QueuedTask.Run()
                //Get the layer's definition
                var lyrDefn = featureLayer.GetDefinition() as CIMFeatureLayer;
                //Get the label classes - we need the first one
                var listLabelClasses = lyrDefn.LabelClasses.ToList();
                
                var theLabelClass = listLabelClasses.FirstOrDefault();

                theLabelClass.Expression = "$feature.SYSTEM";           

                lyrDefn.LabelClasses = listLabelClasses.ToArray(); //Set the labelClasses back
                featureLayer.SetDefinition(lyrDefn); //set the layer's definition
                                                     //set the label's visiblity
                featureLayer.SetLabelVisibility(true);
                //cimMap.GeneralPlacementProperties =
                //MapView.Active.Map.SetDefinition(cimMap);
            });
            
        }
    }
}
