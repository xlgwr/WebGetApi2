using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPanamaDB.models
{
    [Table("CountryItems")]
    public class CountryItems : modelTID
    {
        /// <summary>
        /// 国家
        /// </summary>
        [Index(Order = 0)]
        [StringLength(128)]
        public string Countries { get; set; }
        public string CountriesDesc { get; set; }

        [Index]
        public int CountriesIndex { get; set; }

        [Index(Order = 1)]
        [StringLength(32)]
        public string ttype { get; set; }

        [Index(Order = 2)]
        [StringLength(128)]
        public string name { get; set; }

        public string nameDesc { get; set; }

        public string nameURL { get; set; }
    }
}
