using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kyrsach.Models.InstructionViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Category> categories, int? category, string request)
        {
            categories.Insert(0, new Category { Name = "All", Id = 0 });
            Categories = new SelectList(categories, "Id", "Name", category);
            SelectedCategory = category;
            Request = request;
        }
        public SelectList Categories { get; set; } 
        public int? SelectedCategory { get; set; }
        public string Request { get; set; }
    }
}
