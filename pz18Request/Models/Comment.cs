using System;
using System.Collections.Generic;

namespace pz18Request.Model;

public partial class Comment
{
    public int CommentId { get; set; }

    public int RequestId { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime CommentDate { get; set; }

    public virtual Request Request { get; set; } = null!;
}
