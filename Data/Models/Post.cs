using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Post
{
    public int Id { get; set; }

    public int CatId { get; set; }

    public int AuthorId { get; set; }

    public string PostTitle { get; set; } = null!;

    public string PostImage { get; set; } = null!;

    public string PostShortDiscription { get; set; } = null!;

    public string PostDiscription { get; set; } = null!;

    public string Tags { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public bool IsDelete { get; set; }

    public virtual Post Author { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Post> InverseAuthor { get; set; } = new List<Post>();
}
