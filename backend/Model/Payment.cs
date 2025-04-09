using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend.Model;
public class Payment{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public double Amount { get; set; }
    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }
    [ForeignKey("WashOrder")]
    public int WashOrderId { get; set; }

    public bool IsPaid { get; set; } = false;
    public User User { get; set; } = null!;

    public WashOrder WashOrder { get; set; } = null!;
}