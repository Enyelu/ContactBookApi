using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Pagenator
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        public Pagenator()
        {
            PageSize = 2;
            CurrentPage = 1;
        }

        public Pagenator(int pageSize, int currentPage)
        {
            PageSize = pageSize > 5 ? 5:pageSize;
            CurrentPage = currentPage < 1 ? 1:currentPage;
        }
    }
}
