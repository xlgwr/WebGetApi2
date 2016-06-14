using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetPanamaDB.models.modelviews
{
    public class EntityConnect
    {
        public EntityConnect()
        {
            this.entitysAll = new EntitysAll();
            this.connections = new List<Connections>();
        }

        [Required]
        public long Tid { get; set; }
        public EntitysAll entitysAll { get; set; }
        public ICollection<Connections> connections { get; set; }
    }
}
