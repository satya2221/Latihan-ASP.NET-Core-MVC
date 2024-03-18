using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatihanMVC_DAL.Models.Dto.Res
{
    public class ResUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? Pekerjaan { get; set; }
    }
}
