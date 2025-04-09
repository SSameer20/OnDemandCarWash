using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.DTO;

namespace Backend.Model;
public class Feedback{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string FeedbackMessage { get; set; } = string.Empty;
    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }
    [ForeignKey("WashOrder")]
    public int WashOrderId { get; set; }
    public User User { get; set; } = null!;
    public WashOrder WashOrder { get; set; } = null!;
}