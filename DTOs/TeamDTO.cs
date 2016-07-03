using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace DTOs
{
    
    [DataContract(IsReference = true)]
    public class TeamDTO
    {
        public TeamDTO()
        {
            this.Players = new HashSet<PlayerDTO>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Coach { get; set; }
        [DataMember]
        public ICollection<PlayerDTO> Players { get; set; }
    }
}
