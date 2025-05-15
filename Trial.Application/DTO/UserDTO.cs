using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial.Application.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool? Active { get; set; }

        public DateTime? CreateDate { get; set; }

        public Guid? CreateUser { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
