using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    public readonly record struct Percentage(decimal Value)
    {
        public Money of(Money m) => new(m.Value * Value);
    }
}