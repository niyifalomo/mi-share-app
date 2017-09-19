using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mi_Share.Code.Helpers
{
    public class CustomHelpers
    {
        public static SelectList SelectListForItemStatus(object selectedValue = null)
        {
            SelectListItem[] selectListItems = new SelectListItem[2];

            var itemTrue = new SelectListItem();
            itemTrue.Value = "true";
            itemTrue.Text = "Borrowed";
            selectListItems[0] = itemTrue;

            var itemFalse = new SelectListItem();
            itemFalse.Value = "false";
            itemFalse.Text = "Available";
            selectListItems[1] = itemFalse;

            var selectList = new SelectList(selectListItems, "Value", "Text", selectedValue);

            return selectList;
        }
    }
}