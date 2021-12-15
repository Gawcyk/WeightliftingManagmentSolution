using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightliftingManagment.Domain.Model
{
    public class Category
    {
        public Category()
        {
            Name = string.Empty;
        }
        public string Name { get; set; }
        public int MaxWeight { get; set; }
        public int MinWeight { get; set; }
        public bool IsInTheCategory(double weight) => weight > MinWeight && weight <= MaxWeight;

        public override string ToString() => Name;
    }
}
