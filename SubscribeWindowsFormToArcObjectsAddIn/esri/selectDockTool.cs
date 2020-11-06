using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace esri
{
    public class selectDockTool : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public selectDockTool()
        {
            //DockableWindow1.AddinImpl winImpl = AddIn.FromID<DockableWindow1.AddinImpl>(ThisAddIn.IDs.DockableWindow1);
            //DockableWindow1 dockWin = winImpl.UI;
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
        protected override void OnMouseDown(MouseEventArgs arg)
        {
            IMxDocument pMxDoc = ArcMap.Document;
            IMap map = pMxDoc.FocusMap;
            IPoint pPoint = pMxDoc.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y);

            IActiveView activeView = (IActiveView)map;
            IRubberBand rubberEnv = new RubberEnvelopeClass();
            IGeometry geom = rubberEnv.TrackNew(activeView.ScreenDisplay, null);
            IArea area = (IArea)geom;

            //Extra logic to cater for the situation where the user simply clicks a point on the map 
            //or where envelope is so small area is zero 
            if ((geom.IsEmpty == true) || (area.Area == 0))
            {

                //create a new envelope 
                IEnvelope tempEnv = new EnvelopeClass();

                //create a small rectangle 
                ESRI.ArcGIS.esriSystem.tagRECT RECT = new tagRECT();
                RECT.bottom = 0;
                RECT.left = 0;
                RECT.right = 5;
                RECT.top = 5;

                //transform rectangle into map units and apply to the tempEnv envelope 
                IDisplayTransformation dispTrans = activeView.ScreenDisplay.DisplayTransformation;
                dispTrans.TransformRect(tempEnv, ref RECT, 4); //4 = esriDisplayTransformationEnum.esriTransformToMap)
                tempEnv.CenterAt(pPoint);
                geom = (IGeometry)tempEnv;
            }

            //Set the spatial reference of the search geometry to that of the Map 
            ISpatialReference spatialReference = map.SpatialReference;
            geom.SpatialReference = spatialReference;

            map.SelectByShape(geom, null, false);
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, activeView.Extent);

        }
    }

}
