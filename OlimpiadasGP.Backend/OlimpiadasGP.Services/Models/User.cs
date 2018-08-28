using System;
using System.Collections.Generic;
using System.Text;

namespace OlimpiadasGP.Services.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string RealName { get; set; }
        public virtual string NickName { get; set; }
        public virtual string Email { get; set; }
    }
}
