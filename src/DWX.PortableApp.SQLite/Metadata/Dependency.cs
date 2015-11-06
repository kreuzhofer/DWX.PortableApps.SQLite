using System;

namespace DWX.PortableApps.SQLite.Metadata
{
    /// <summary>
    /// Structure for representing a dependency between to entities in a parent-child relationship
    /// </summary>
    public struct Dependency
    {
        public Type SourceType { get; set; }
        public string NavigationProperty { get; set; }
        public Type DependentOn { get; set; }
        public string ForeignKey { get; set; }
        public bool CascadeOnDelete { get; set; }
    }
}