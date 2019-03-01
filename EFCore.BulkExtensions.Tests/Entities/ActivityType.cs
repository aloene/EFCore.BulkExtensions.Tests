using System;
using System.Collections.Generic;

namespace EFCore.BulkExtensions.Tests.Entities
{
    public partial class ActivityType
    {
        public ActivityType()
        {
            Activities = new HashSet<Activity>();
        }

        public ActivityTypeEnum Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
