using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesClass
{
    [Serializable]
    public class GlobalEntities
    {
        public DataSet ListDataset { get; set; }
        public DataTable ListDataTable { get; set; }
    }

    [Serializable]
    public class Reports
    {
        public Dictionary<string, object> spParameter { get; set; }
        public Dictionary<string, string> ReportParameter { get; set; }

        public string ReportID { get; set; }
        public string PageReport { get; set; }

    }
}
