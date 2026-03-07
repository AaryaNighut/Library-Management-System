using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library_Management.Models
{
    public class dropdownstudent
    {
        public int SelectedItemId { get; set; } // To store the selected item id

        public List<SelectListItem> Items { get; set; } // Dropdown items
    }
}
