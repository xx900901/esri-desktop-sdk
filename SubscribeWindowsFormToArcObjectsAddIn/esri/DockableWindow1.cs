﻿using ESRI.ArcGIS.esriSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace esri
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class DockableWindow1 : UserControl
    {
        public DockableWindow1(object hook)
        {
            InitializeComponent();
            this.Hook = hook;
        }

        /// <summary>
        /// Host object of the dockable window
        /// </summary>
        private object Hook
        {
            get;
            set;
        }

        /// <summary>
        /// Implementation class of the dockable window add-in. It is responsible for 
        /// creating and disposing the user interface class of the dockable window.
        /// </summary>
        public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
        {
            private DockableWindow1 m_windowUI;

            public AddinImpl()
            {
            }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new DockableWindow1(this.Hook);
                return m_windowUI.Handle;
            }

            protected override void Dispose(bool disposing)
            {
                if (m_windowUI != null)
                    m_windowUI.Dispose(disposing);

                base.Dispose(disposing);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ArcMap.Application.CurrentTool = new selectDockTool() as ESRI.ArcGIS.Framework.ICommandItem;
            ESRI.ArcGIS.Framework.ICommandItem selectTool = ArcMap.Application.Document.CommandBars.Find((new UIDClass { Value = ThisAddIn.IDs.selectDockTool }));
            ArcMap.Application.CurrentTool = selectTool;
            
        }
    }
}
