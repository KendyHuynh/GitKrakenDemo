using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCElectronicStore.Models
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public List<Product> SearchResults { get; set; }
        public List<Product> LstProduct { get; set; }
        public List<Category> LstCategogy { get; set; }
        public List<Brand> LstBard { get; set; }
    }
}
