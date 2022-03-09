using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library.entities
{
  public class Members
  {
    [Key]
    [Column(Order=1)]
    public int MemberId { get; set; }

    [Column(Order=2)]
    public Guid MemberMsk { get; set; }

    [Column(Order=3)]
    public string Name { get; set; }
  }
}