﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class UserCart:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsOrder { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
