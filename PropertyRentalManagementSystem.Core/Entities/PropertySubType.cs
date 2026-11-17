using System;
using System.Collections.Generic;

namespace PropertyRentalManagementSystem.Domain.Entities;

public partial class PropertySubType
{
    public int PropertySubTypeId { get; set; }

    public int PropertyTypeId { get; set; }

    public string SubTypeName { get; set; } = null!;

    public virtual PropertyType PropertyType { get; set; } = null!;
}
