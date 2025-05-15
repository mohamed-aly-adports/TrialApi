using System;
using System.Collections.Generic;

namespace Trial.Domain.Entities;

public partial class TblUser
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? Active { get; set; }

    public DateTime? CreateDate { get; set; }

    public Guid? CreateUser { get; set; }

    public DateTime? UpdateDate { get; set; }
}
