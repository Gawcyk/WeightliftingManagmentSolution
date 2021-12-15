using System.Collections.Generic;

namespace WeightliftingManagment.Domain.Model
{
    public class ListOfCategory
    {
        private List<Category> _categories = new();
        public List<Category> Categories
        {
            get=> _categories;
            set=> _categories = value;
        }
    }
}
