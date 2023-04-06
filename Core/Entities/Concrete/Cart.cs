using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class Cart : IEntity
    {
        public int Id { get; set; }
        public short Quantity { get; set; }
        public int UserId { get; set; }
        public int? ProductId { get; set; }
        public int CartId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsOrder { get; set; }

    }
}
