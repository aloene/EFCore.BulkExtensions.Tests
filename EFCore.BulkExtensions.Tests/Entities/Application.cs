using System;
using System.Collections.Generic;

namespace EFCore.BulkExtensions.Tests.Entities
{
    public partial class Application
    {
        public Application()
        {
            Activities = new HashSet<Activity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
