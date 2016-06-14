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
    /// Intermediary(中介):获取所有
    /// Entity:获取所有 Officer（Shareholder of）记录
    /// Address: 获取所有
    /// </summary>
    [Table("Connections")]
    public class Connections : modelTID
    {
        /// <summary>
        /// 中介/人员
        /// Intermediary/Officer
        /// </summary>
        [Index(Order = 0)]
        [StringLength(128)]
        public string nameFrom { get; set; }
        public string nameFromDesc { get; set; }

        [Index(Order = 2)]
        [StringLength(256)]
        public string nameType { get; set; }

        /// <summary>
        /// 公司/地址
        /// Entity/Address
        /// </summary>
        [Index(Order = 2)]
        [StringLength(128)]
        public string nameTo { get; set; }
        public string nameToDesc { get; set; }

    }
}
