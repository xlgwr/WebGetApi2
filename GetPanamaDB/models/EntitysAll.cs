using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPanamaDB.models
{
    /// <summary>
    /// 公司/地址/中介/人员
    /// </summary>
    [Table("EntitysAll")]
    public class EntitysAll : modelTID
    {

        [Index(IsUnique = true)]
        [StringLength(128)]
        public string name { get; set; }

        public string nameDesc { get; set; }

        /// <summary>
        /// 公司/地址/中介/人员
        /// Entity/Address/Intermediary/Officer
        /// </summary>
        [Index(Order = 1)]
        [StringLength(32)]
        public string ttype { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [Index(Order = 2)]
        [StringLength(128)]
        public string Countries { get; set; }

        /// <summary>
        /// 共有
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 公司 有
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 地址 有
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 公司 有
        /// </summary>
        public string CompanyType { get; set; }
        /// <summary>
        /// 公司 有
        /// </summary>
        public string Jurisdiction { get; set; }


    }
}
