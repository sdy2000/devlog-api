using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public string Comment1 { get; set; } = null!;

    public DateTime PostDate { get; set; }

    public bool IsDelete { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
