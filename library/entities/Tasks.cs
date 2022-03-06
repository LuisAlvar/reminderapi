using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library.entities
{
  public class Tasks
  {
    [Key]
    [Column(Order=1)]
    public int TaskId { get; set; }
  }
}