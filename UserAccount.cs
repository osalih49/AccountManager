using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager
{
    public class UserAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }
        public bool IsActive { get; set; }

    }
}
