using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Specfication.ProductSpec
{
    public class ProductSpecPrams
    {
        public string? sort { get; set; }
        public string? Product { get; set; }
        public string? Category { get; set; }
        private int PageSize = 30;

        public int pagesize
        {
            get { return PageSize; }
            set { PageSize = value > 30 ? 30 : value; }
        }

        public int PageIndex { get; set; } = 1;



    }
}
