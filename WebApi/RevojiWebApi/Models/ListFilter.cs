using System;
using System.ComponentModel;

namespace RevojiWebApi.Models
{
    public class ListFilter
    {
        [DefaultValue("DESC")]
        public string order { get; set; }

        [DefaultValue(0)]
        public int pageStart { get; set; }

        [DefaultValue(20)]
        public int pageLimit { get; set; }

        public ListFilter() {}
    }
}
