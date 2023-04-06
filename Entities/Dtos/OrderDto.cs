using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class OrderDto:IDto
    {
        public int OrderId { get; set; }
        public string Description { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Freight { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public List<CartProductDto> ProductInfo { get; set; }
        public UserDto UserInfo { get; set; }


    }
}
