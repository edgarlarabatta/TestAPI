using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Entities
{
    public class Parameters
    {
        public Nullable< DateTime> StartDate { get; set; }
        public Nullable< DateTime> EndDate { get; set; }

        public string Status { get; set; }
    }
}
