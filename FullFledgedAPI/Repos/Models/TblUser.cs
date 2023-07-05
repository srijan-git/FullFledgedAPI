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
    [Column("id")]
    public int Id { get; set; }
}
