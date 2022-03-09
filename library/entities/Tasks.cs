using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library.entities
{
  public class Tasks
  {
    [Key]
    [Column(Order=1)]
    public int TaskId { get; set; }

    [Column(Order=2)]
    public Guid TaskMsk { get; set; }

    [Column(Order=3)]
    public string Description { get; set;}
    
    [Column(Order=4)]
    public bool IsReminder { get; set; }
    
    [Column(Order=5)]
    public DateTime ProjectedDate { get; set; }

    [Column(Order=6)]
    public bool IsCompleted { get; set; }

    [Column(Order=7)]
    public bool IsActive { get; set; }
    
    [Column(Order=8)]
    public Members member { get; set; }

    [Column(Order=9)]
    public DateTime CreateDate { get; set; } 
    
    [Column(Order=10)]
    public DateTime LastUpdated { get; set; }
  }
}