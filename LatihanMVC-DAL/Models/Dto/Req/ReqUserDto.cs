using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatihanMVC_DAL.Models.Dto.Req
{
    public class ReqUserDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(5)]
        public string UserName { get; set; } = null!;

        public string? Pekerjaan { get; set; }
    }
}
