using System;
using System.Collections.Generic;

namespace WebMVC.Models;

public partial class MstUser
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string? Pekerjaan { get; set; }

    public virtual MstProfile? MstProfile { get; set; }
}
