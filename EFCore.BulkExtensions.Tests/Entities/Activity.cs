using System;
using System.Collections.Generic;

namespace EFCore.BulkExtensions.Tests.Entities
{
    public partial class Activity
    {
        public Activity()
        {
            Children = new HashSet<Activity>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? ApplicationId { get; set; }
        public string PublicId { get; set; }
        public string Name { get; set; }
        public ActivityTypeEnum ActivityTypeId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime? ClosedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ActivityType ActivityType { get; set; }
        public virtual Application Application { get; set; }
        public virtual Activity Parent { get; set; }
        public virtual ICollection<Activity> Children { get; set; }
    }
}
