using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class PropertyType
{
    public int PropertyTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

    public virtual ICollection<PropertySubType> PropertySubTypes { get; set; } = new List<PropertySubType>();
}
