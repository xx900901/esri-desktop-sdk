using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace CleanList
{
    internal class Combo1ViewModel : DockPane
    {
        private const string _dockPaneID = "CleanList_Combo1";
        public static string _SelectList1;
        public static int _SelectIndex;
        private ObservableCollection<string> _List1;
       
        protected Combo1ViewModel() { }

        /// <summary>
        /// Show the DockPane.
        /// </summary>
        /// 

        
        internal static void Show()
        {

            DockPane pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
         
            if (pane == null)
                return;
            
            pane.Activate();
        }

        //protected override void OnShow(bool isVisible)
        //{
        //    SelectSourceIndex = -1;

        //    SelectSource = "";
        //    _SelectList1 = "";

        //}

        protected override void OnHidden()
        {
            SelectSourceIndex = -1;
        }

        /// <summary>
        /// Text shown near the top of the DockPane.
        /// </summary>
        private string _heading = "My DockPane";
        public string Heading
        {
            get { return _heading; }
            set
            {
                SetProperty(ref _heading, value, () => Heading);
            }
        }

        public string SelectSource
        {
            get
            { return _SelectList1; }
            set
            { SetProperty(ref _SelectList1, value, () => SelectSource); }
        }

        public int SelectSourceIndex
        {
            get
            { return _SelectIndex; }
            set
            { SetProperty(ref _SelectIndex, value, () => SelectSourceIndex); }
        }




        public ObservableCollection<string> List1
        {
            get
            {
                if (_List1 == null)
                {
                    _List1 = new ObservableCollection<string>();
                    _List1.Add("one");
                    _List1.Add("two");
                    _List1.Add("three");
                }
                return _List1;
            }
            set
            {
                SetProperty(ref _List1, value, () => _List1);
            }
        }
    }

    /// <summary>
    /// Button implementation to show the DockPane.
    /// </summary>
    internal class Combo1_ShowButton : Button
    {
        protected override void OnClick()
        {
            //Combo1ViewModel.OnShow(false);
            
            Combo1ViewModel.Show();

            
        }

    }
}
