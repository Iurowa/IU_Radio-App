using System;
using System.Collections.Generic;
using Iubh.RadioApp.Data.Database;
using MvvmCross;

namespace Iubh.RadioApp.Data
{
    public class Config
    {
        public sealed class Static
        {
            public static string DatabaseFileName = "iubh-radioapp.db";

            public static Guid OnboardingId = new Guid("f157bad7-37ba-4daa-9e51-5f3720eff29d");

            public static Guid IsLoginId = new Guid("d9af772b-2634-469d-a9f5-235064beac0f");
        }

        public sealed class ConfigEntry
        {
            public string Key { get; set; }

            public string Value { get; set; }
        }

        private static volatile Config instance;

        private static readonly object synchronizationLock = new object();



        /// <summary>
        /// Gibt den Singleton der Config zurück.
        /// </summary>
        public static Config Database
        {
            get
            {
                if (instance == null)
                {
                    lock (synchronizationLock)
                    {
                        if (instance == null)
                        {
                            instance = new Config();
                        }
                    }
                }

                return instance;
            }
        }

        private Config()
        {
            
        }
    }
}
