using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocoOrm.Core.Contract.Command;

namespace PocoOrm.Core.Helpers
{
    public class ParameterCounter : IParameterCounter
    {
        private int _counter;
        public string ParameterName => $"@parameter{++_counter}";
    }
}
