using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class Multireddit
    {
        public Guid userID { get; set; }
        public List<string> subreddits { get; set; }
        
    }
}
