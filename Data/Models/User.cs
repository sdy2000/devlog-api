using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string UserAvatar { get; set; } = null!;

    public DateTime RegisterDate { get; set; }

    public virtual Comment? Comment { get; set; }
}
