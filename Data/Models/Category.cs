using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Category
{
    public int Id { get; set; }

    public string CatTitle { get; set; } = null!;

    public int? ParentId { get; set; }
}
