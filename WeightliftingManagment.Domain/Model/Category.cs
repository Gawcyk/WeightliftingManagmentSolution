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

        public Category(string name, double maxWeight, double minWeight)
        {
            Name = name;
            MaxWeight = maxWeight;
            MinWeight = minWeight;
        }

        public string Name { get; set; }
        public double MaxWeight { get; set; }
        public double MinWeight { get; set; }
        public bool IsInTheCategory(double weight) => weight > MinWeight && weight <= MaxWeight;

        public override string ToString() => Name;
    }
}
