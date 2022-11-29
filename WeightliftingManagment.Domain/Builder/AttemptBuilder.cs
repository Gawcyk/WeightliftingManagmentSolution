using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightliftingManagment.Domain.Model;

namespace WeightliftingManagment.Domain.Builder
{
    public class AttemptBuilder
    {
        private int? _value = null;
        private AttemptStatus? _status = null;

        public Attempt Build()
        {
            var model = new Attempt();
            if (_value is not null)
                model.Value = _value.Value;
            if(_status is not null)
                model.Status = _status.Value;
            return model;
        }
        public AttemptBuilder WithValue(int value)
        {
            _value = value;
            return this;
        }

        public AttemptBuilder WithStatus(AttemptStatus status)
        {
            _status = status;
            return this;
        }

    }
}
