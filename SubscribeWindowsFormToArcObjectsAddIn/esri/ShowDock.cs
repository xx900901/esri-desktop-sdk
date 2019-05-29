using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;

namespace esri
{
    public class ShowDock : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ShowDock()
        {
        }

        protected override void OnClick()
        {
            UID dockableWinUID = new UIDClass();
            dockableWinUID.Value = ThisAddIn.IDs.DockableWindow1;
            IDockableWindow DockableWin = ArcMap.DockableWindowManager.GetDockableWindow(dockableWinUID);
            DockableWin.Show(true);
        }

        protected override void OnUpdate()
        {
        }
    }
}
