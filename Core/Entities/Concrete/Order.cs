using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public decimal Freight { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
