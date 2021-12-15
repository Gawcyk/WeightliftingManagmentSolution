using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightliftingManagment.Core.FlyoutManager
{
    public class FlyoutAction
    {
        private const string OPENING = "Opening";
        private const string CLOSING = "Closing";

        protected FlyoutAction(string action)
        {
            Action = action;
        }

        public static FlyoutAction Opening => new(OPENING);

        public static FlyoutAction Closing => new(CLOSING);

        public string Action { get; }

        public override bool Equals(object obj)
        {
            if (obj is not FlyoutAction other)
            {
                return false;
            }

            return Action == other.Action;
        }

        public override int GetHashCode() => Action.GetHashCode();

        public override string ToString() => Action;
    }
}
