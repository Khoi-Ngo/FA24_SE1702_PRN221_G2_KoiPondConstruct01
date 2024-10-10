using KoiPondConstruct.Data.Base;
using KoiPondConstruct.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiPondConstruct.Data.Repository.Impl
{
    public class SuggestionRepository : BaseRepository<TblSuggestionDoc>
    {
        public SuggestionRepository() { }

        public SuggestionRepository(FA24_SE1702_PRN221_G2_KoiPondConstructContext context) : base(context)
        {
            _context = context;
        }
    }
}
