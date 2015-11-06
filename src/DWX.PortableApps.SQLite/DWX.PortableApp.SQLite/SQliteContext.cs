using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DWX.PortableApp.SQLite.Attributes;
using DWX.PortableApp.SQLite.Metadata;
using DWX.PortableApps.Logging;
using SQLite.Net.Async;

namespace DWX.PortableApp.SQLite
{
    public class SQliteContext
    {
        private readonly SQLiteAsyncConnection _connection;
        private List<EntityInformation> _entities;
        private readonly IPortableLogger _log;

        public SQliteContext(SQLiteAsyncConnection connection, Type typeOfEntitiyAssembly, IPortableLogManager logManager)
        {
            _connection = connection;
            _log = logManager.CreateLogger<SQliteContext>();
            InitializeMappings(typeOfEntitiyAssembly);
        }

        public SQLiteAsyncConnection Connection
        {
            get { return _connection; }
        }

        /// <summary>
        /// Initializes the internal mapping table from a base entity Type. Any type in the same assembly as the given type is considered part of the required entities.
        /// </summary>
        /// <param name="typeOfEntitiyAssembly"></param>
        private void InitializeMappings(Type typeOfEntitiyAssembly)
        {
            var types = typeOfEntitiyAssembly.GetTypeInfo().Assembly.DefinedTypes;
            var entities = new List<EntityInformation>();
            foreach (var typeInfo in types)
            {
                var att = typeInfo.GetCustomAttribute<EntityAttribute>();
                if (att != null)
                {
                    var info = new EntityInformation()
                    {
                        EntityType = typeInfo.AsType(),
                    };
                    // find out if any of the properties has a foreignkey attribute
                    entities.Add(info);
                }
            }
            _entities = entities;
        }

        /// <summary>
        /// creates the database schema 
        /// </summary>
        /// <returns></returns>
        public async Task CreateDatabaseSchemaAsync()
        {
            await _connection.CreateTablesAsync(_entities.Select(t => t.EntityType).ToArray());
        }

        /// <summary>
        /// drops the database scheme. This should only be done if you want to reset the database. Be careful with this.
        /// </summary>
        /// <returns></returns>
        public async Task DropDatabaseSchemaAsync()
        {
            foreach (EntityInformation entityType in _entities)
            {
                try
                {
                    var t = entityType.EntityType;
                    await _connection.DropTableAsync(t);
                }
                catch (Exception)
                {
                    _log.Error($"Could not drop table {entityType.EntityType.Name}");
                }
            }
        }


    }
}