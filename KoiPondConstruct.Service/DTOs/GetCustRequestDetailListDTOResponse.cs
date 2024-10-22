using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Service.DTOs
{
    public class GetCustRequestDetailListDTOResponse
    {
        public long Id { get; set; }
        public int UserId { get; set; }

        public long RequestId { get; set; }

        public long SampleDesignId { get; set; }

        public string HomeownerFirstName { get; set; }

        public string HomeownerLastName { get; set; }

        public string HomeownerPhone { get; set; }

        public DateOnly HomeownerDateOfBirth { get; set; }

        public long Height { get; set; }

        public long Width { get; set; }

        public long Length { get; set; }

        public string Shape { get; set; }

        public long Budget { get; set; }

        public string Type { get; set; }

        public string Address { get; set; }

        public string Note { get; set; }

        public bool IsDeleted { get; set; }
    }
}
