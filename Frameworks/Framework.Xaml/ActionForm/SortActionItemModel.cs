using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Xaml.ActionForm
{
    public class SortActionItemModel : ActionItemModel
    {
        private Framework.Queries.QueryOrderBySetting m_QueryOrderBySetting;
        public Framework.Queries.QueryOrderBySetting QueryOrderBySetting
        {
            get { return m_QueryOrderBySetting; }
            set
            {
                Set(nameof(QueryOrderBySetting), ref m_QueryOrderBySetting, value);
            }
        }

        //private bool m_IsSelected;
        //public bool IsSelected
        //{
        //    get { return m_IsSelected; }
        //    set
        //    {
        //        Set(nameof(IsSelected), ref m_IsSelected, value);
        //    }
        //}

        //private Framework.Queries.QueryOrderDirections m_Direction;
        //public Framework.Queries.QueryOrderDirections Direction
        //{
        //    get { return m_Direction; }
        //    set
        //    {
        //        Set(nameof(Direction), ref m_Direction, value);
        //    }
        //}
    }
}

