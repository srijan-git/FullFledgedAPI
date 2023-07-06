using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FullFledgedAPI.Repos.Models;

[Keyless]
[Table("tbl_user")]
public partial class TblUser
{
    [Column("code")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Code { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    [StringLength(10)]
    public string? Phone { get; set; }

    [StringLength(50)]
    public string? Password { get; set; }

    [Column("isactive")]
    public bool? Isactive { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Role { get; set; }
}
