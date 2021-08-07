using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model
{
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime DateEnd { get; set; }
        
        public bool IsExpired() => DateTime.UtcNow >= DateEnd;
    }
}