using DTO.BusinessObjects;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haldan.Wpf.Events
{
    public class CustomEvent
    {
        public class DisableEditingEvent : PubSubEvent<PublishedEvent<object>>
        {
        }
    }
}
