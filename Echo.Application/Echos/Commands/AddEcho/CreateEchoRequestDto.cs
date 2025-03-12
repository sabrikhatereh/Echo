using System;
using System.ComponentModel.DataAnnotations;

namespace Echo.Application.Echos.Commands.AddEcho
{
    public class CreateEchoRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [StringLength(400, ErrorMessage = "Message must not exceed 400 characters.")]
        public string Message { get; set; }
    }
}
