using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPanamaDB.models.modelviews
{
    public class EntityConnect
    {
        public EntityConnect()
        {
            this.entitysAll = new EntitysAll();
            this.connections = new List<Connections>();
        }
        public EntitysAll entitysAll { get; set; }
        public ICollection<Connections> connections { get; set; }
    }
}
