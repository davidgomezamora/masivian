using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Repository
{
    public class PropertyMappingValue
    {
        public List<string> DestinationProperties { get; private set; }
        public bool Revert { get; private set; }

        public PropertyMappingValue(List<string> destinationProperties, bool revert = false)
        {
            this.DestinationProperties = destinationProperties ?? throw new ArgumentNullException(nameof(destinationProperties));
            this.Revert = revert;
        }
    }
}
