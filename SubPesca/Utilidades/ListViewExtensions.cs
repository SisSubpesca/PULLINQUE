using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SubPesca.Utilidades
{
    public static class ListViewExtensions
    {

        public static List<ListViewDataItem> GetSelectedDataKeys2(this ListView control, string checkBoxId)
        {
            return control.Items.Where(x => IsCheckedAndEnabled(x, checkBoxId)).ToList();
        }

        private static bool IsCheckedAndEnabled(ListViewDataItem item, string checkBoxId)
        {
            var control = item.FindControl(checkBoxId) as CheckBox;

            if (control == null || control.Enabled == false)
            {
                return false;
            }
            return control.Checked;
        }


       
    }     
}