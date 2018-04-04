using System.Collections.Generic;

namespace Timesheet.ViewModels.DataTable
{
    public class DataTableObject<T>
    {
        private int _iTotalRecords;
        private int _iDisplayCount;
        private List<T> _aaData;

        public DataTableObject()
        {
            _iTotalRecords = 0;
            _iDisplayCount = 0;
            _aaData = new List<T>();
        }

        public List<T> aaData
        {
            get { return _aaData; }
            set { _aaData = value; }
        }

        public int iTotalRecords
        {
            get { return _iTotalRecords; }
            set { _iTotalRecords = value; }
        }

        public int iDisplayCount
        {
            get { return _iDisplayCount; }
            set { _iDisplayCount = value; }
        }
    }
}
