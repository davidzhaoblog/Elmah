using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Elmah.AspNetMvcCoreViewModel
{
    /// <summary>
    ///
    /// </summary>
    public class UISharedViewModel
    {
        public List<SelectListItem> SelectListOfPageSize { get; set; }
        public List<SelectListItem> SelectListOfQueryOrderBySettingCollecionInString { get; set; }
        public List<SelectListItem> SelectListOfDataExport { get; set; }
        public List<SelectListItem> PreDefinedDateTimeRangeList { get; private set; }
        public List<SelectListItem> PredefinedBooleanSelectionList { get; private set; }

        public static UISharedViewModel GetUISharedViewModel(
            IList<Framework.Models.NameValuePair> listOfQueryOrderBySettingCollecionInString
            , IList<Framework.Models.NameValuePair> pageSizeSelectionList
            , IList<Framework.Models.NameValuePair> listOfDataExport)
        {
            UISharedViewModel retval = new UISharedViewModel();
            retval.SelectListOfQueryOrderBySettingCollecionInString = BuildListOfSelectListItem(listOfQueryOrderBySettingCollecionInString);

            //retval.SelectListOfPageSize = BuildListOfSelectListItem(pageSizeSelectionList);
            retval.SelectListOfPageSize = new List<SelectListItem>();
            retval.SelectListOfPageSize.Add(new SelectListItem { Text = "10 Items PerPage", Value = "10" });
            retval.SelectListOfPageSize.Add(new SelectListItem { Text = "25 Items PerPage", Value = "25" });
            retval.SelectListOfPageSize.Add(new SelectListItem { Text = "50 Items PerPage", Value = "50" });
            retval.SelectListOfPageSize.Add(new SelectListItem { Text = "100 Items PerPage", Value = "100" });

            retval.SelectListOfDataExport = BuildListOfSelectListItem(listOfDataExport);

            retval.PreDefinedDateTimeRangeList = new List<SelectListItem>();
            var preDefinedDateTimeRangeList = Framework.Queries.QuerySystemDateTimeRangeCriteria.GetList(null);
            foreach (var item in preDefinedDateTimeRangeList)
            {
                retval.PreDefinedDateTimeRangeList.Add(new SelectListItem { Text = item.Name, Value = item.Value });
            }

            retval.PredefinedBooleanSelectionList = new List<SelectListItem>();
            var predefinedBooleanSelectedValueList = Framework.Queries.QuerySystemBooleanEqualsCriteria.GetList(null);
            foreach (var item in predefinedBooleanSelectedValueList)
            {
                retval.PredefinedBooleanSelectionList.Add(new SelectListItem { Text = item.Name, Value = item.Value });
            }
            return retval;
        }

        public static List<SelectListItem> BuildListOfSelectListItem(IList<Framework.Models.NameValuePair> input)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            if (input != null)
            {
                foreach (var nvp in input)
                {
                    selectList.Add(new SelectListItem { Text = nvp.Name, Value = nvp.Value.Trim() });
                }
            }
            return selectList;
        }

        public static SelectList GetSelectList(IList<Framework.Models.NameValuePair> input, string selectedValue)
        {
            if (string.IsNullOrWhiteSpace(selectedValue))
            {
                return new SelectList(input, "Value", "Name", input[0].Value);
            }
            else
            {
                return new SelectList(input, "Value", "Name", selectedValue);
            }
        }
        public static SelectList GetSelectList(IList<Framework.Queries.PagingPageSizeOption> input, int currentPageSize)
        {
            if (currentPageSize != -1)
            {
                return new SelectList(input, "PageSize", "PageSizeName", currentPageSize);
            }
            else
            {
                return new SelectList(input, "PageSize", "PageSizeName", input[0].PageSize);
            }
        }
    }
}

