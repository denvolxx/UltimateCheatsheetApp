using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.QueryParameters
{
    public class UserParams
    {
        //page can not be bigger than 100
        private const int maxPageSize = 100;

        //default page size
        private int pageSize = 10;

        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}
