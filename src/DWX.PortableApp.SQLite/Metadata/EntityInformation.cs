using System;

namespace DWX.PortableApps.SQLite.Metadata
{
    /// <summary>
    /// Structure for representing information about an entity type for creating the corresponding tables in the database
    /// </summary>
    public struct EntityInformation
    {
        public Type EntityType { get; set; }
    }
}