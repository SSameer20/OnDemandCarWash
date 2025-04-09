using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.DTO;
using backend.helper;
using Microsoft.EntityFrameworkCore;

namespace Backend.Model;

public class WashOrder
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    [ForeignKey("Washer")]   
    public int WasherId { get; set; }

    [Required]
    [ForeignKey("Car")]
    public int CarId { get; set; }

    public string Status { get; set; } = "pending";
    public DateTime OrderDate { get; set; }
    
    public Car Car { get; set; } = null!;
    public User User { get; set; } = null!;
    public User Washer { get; set; } = null!;


   
}
