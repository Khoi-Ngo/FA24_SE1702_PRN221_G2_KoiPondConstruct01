using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Service.DTOs
{
    public class CustomerRequestListDTO
    {
        //CUSTOMER REQUEST FIELDS
        public long Id { get; set; }

        public long UserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        //CUSTOMER || USER FIELDS
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarImg { get; set; }
        public string Address { get; set; }

    }

}
