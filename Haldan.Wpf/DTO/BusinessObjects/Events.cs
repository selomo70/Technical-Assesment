using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.BusinessObjects
{
    public class PublishedEvent<T>
    {
        public Guid EventId { get; set; }
        public T EventParameter { get; set; }
        public PublishedEvent(Guid eventId, T parameter)
        {
            this.EventId = eventId;
            this.EventParameter = parameter;
        }
    }
}
