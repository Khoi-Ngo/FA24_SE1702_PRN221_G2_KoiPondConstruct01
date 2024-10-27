using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Service.ViewModels
{
    public class DesignModel
    {
        public long Id { get; set; }

        public long RequestDetailId { get; set; }

        //public DateTime CreatedTime { get; set; }

        //public DateTime UpdatedTime { get; set; }

        public string CreatedBy { get; set; }

        public string File { get; set; }

        public string Status { get; set; }

        public string Note { get; set; }

        public string ApprovedBy { get; set; }

        //public DateTime ApprovedTime { get; set; }

        public string ContentText { get; set; }

        public bool IsDeleted { get; set; }


    }
}
