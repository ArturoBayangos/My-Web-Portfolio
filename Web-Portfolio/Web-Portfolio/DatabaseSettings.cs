using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Portfolio
{
    public class DatabaseSettings
    {
        public class DatabaseSetting : IDatabaseSetting
        {
            public string CollectionName { get; set; }
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
        }

        public interface IDatabaseSetting
        {
            string CollectionName { get; set; }
            string ConnectionString { get; set; }
            string DatabaseName { get; set; }
        }
    }
}
