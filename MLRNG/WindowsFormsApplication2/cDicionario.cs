namespace cDicionario
{
    using System.Collections.Generic;
    using System.Configuration;

    /// <summary>
    /// Persistent store for my parameters.
    /// </summary>
    public class Settings : ApplicationSettingsBase
    {
        /// <summary>
        /// The instance lock.
        /// </summary>
        private static readonly object InstanceLock = new object();

        /// <summary>
        /// The instance.
        /// </summary>
        private static Settings instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="MySettings"/> class 
        /// from being created.
        /// </summary>
        private Settings()
        {
            // don't need to do anything
        }

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static Settings Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        instance = new Settings();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        [UserScopedSetting]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        public Dictionary<string, string> Parameters
        {
            get
            {
                return (Dictionary<string, string>)this["Parameters"];
            }

            set
            {
                this["Parameters"] = value;
            }
        }
    }
}