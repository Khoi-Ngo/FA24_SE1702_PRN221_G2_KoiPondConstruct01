using KoiPondConstruct.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Service.DTOs
{
    public class CustomerRequestDetailDTO
    {
        public long Id { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerUserName { get; set; }
        public long UserId { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public string Priority { get; set; }
        public string Address { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string Status { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<CustomerRequestDetailListDTO> customerRequestDetailListDTOs { get; set; }

    }
}
