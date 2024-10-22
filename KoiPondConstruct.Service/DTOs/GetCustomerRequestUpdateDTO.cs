using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Service.DTOs
{
    public class GetCustomerRequestUpdateDTO
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public string Priority { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string Status { get; set; }

        public bool IsDeleted { get; set; }
    }
}
