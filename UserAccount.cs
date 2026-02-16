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

        public bool AdminUser { get; set; } = false;
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public List<string> Transactions { get; set; } = new List<string>();

    }
}
