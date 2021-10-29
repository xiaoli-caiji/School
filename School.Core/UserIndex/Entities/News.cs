using EntityConfigurationBase;
using System;

namespace School.Core.UserIndex.Entities
{
    public class News : EntityBase<int>
    {
        public string NewsName { get; set; }
        public string NewsWriter { get; set; }
        public string NewsContentAddress { get; set; }
        public string NewsImgsAddress { get; set; }
        public string NewsFileAddress { get; set; }
        public string NewsCoverType { get; set; }
        public string NewsCoverAddressOrTitle { get; set; }
        public DateTime? NewsShowStartTime { get; set; }
        public DateTime? NewsShowEndTime { get; set; }
        public DateTime? NewsWriteTime { get; set; }
        public virtual NewsType NewsType { get; set; }
    }
}
