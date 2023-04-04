using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class Wallet:IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
