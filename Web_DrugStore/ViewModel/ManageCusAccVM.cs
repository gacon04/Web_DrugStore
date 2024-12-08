using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_DrugStore.Identity;

namespace Web_DrugStore.ViewModel
{
    public class ManageCusAccVM
    {
        public List<AppUser> Users { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SearchText { get; set; }
    }
}