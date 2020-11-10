using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChartDoc.Models
{
    public class ClsMenu
    {
        #region Public Properties******************************************************************************************************************************
        public string menuId { get; set; }
        public string menuName { get; set; }
        public string className { get; set; }
        #endregion
    }

    public class ClsSubMenu
    {
        #region Public Properties******************************************************************************************************************************
        public string menuId { get; set; }
        public string submenu { get; set; }
        public string parentmenuId { get; set; }
        public string submenuRoute { get; set; }
        public string events { get; set; }

        public string queryParamsvalue { get; set; }
        #endregion
    }

    public class ClsMeuList
    {
        public List<ClsMenu> clsMenu { get; set; }
        public List<ClsSubMenu> clssubMenu { get; set; }
    }
}
