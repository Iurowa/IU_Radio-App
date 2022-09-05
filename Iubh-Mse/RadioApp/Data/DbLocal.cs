using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Iubh.RadioApp.Common.Services;
using Iubh.RadioApp.Data.Database;
using Iubh.RadioApp.Data.Database.Commands;
using Iubh.RadioApp.Data.Models;
using MvvmCross;
using MvvmCross.Plugin.File;
using SQLite;

namespace Iubh.RadioApp.Data
{
    public class DbLocal : IDbLocal
    {
        private readonly IMvxFileStore fileStore;

        private readonly string environment;
        private readonly Assembly assembly;
        private readonly string resourceNamespace;
        private readonly string[] resourceNames;
        private readonly IDeviceService deviceService;

        protected List<ISqlCommand> SqlCommands { get; }

        public DbLocal(IMvxFileStore fileStore)
        {
            this.fileStore = fileStore;

            this.environment = "Debug";
            this.deviceService = Mvx.IoCProvider.Resolve<IDeviceService>();

            this.assembly = this.GetType().GetTypeInfo().Assembly;
            this.resourceNamespace = "Iubh.RadioApp.Data.Database.Scripts";
            this.resourceNames = this.assembly.GetManifestResourceNames();

            this.SqlCommands = new List<ISqlCommand>();
        }

        public IEnumerable<T> ExecuteQuery<T>(ISqlQuery<T> query) where T : new()
        {
            var connection = this.GetOpenConnection();
            return connection.Query<T>(query.GetCommand(), query.GetParams() ?? new object[] { });
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(ISqlQuery<T> query) where T : new()
        {
            var connection = this.GetOpenConnection();
            return connection.Query<T>(query.GetCommand(), query.GetParams() ?? new object[] { });
        }

        public T ExecuteSingle<T>(ISqlQuery<T> query) where T : new()
        {
            return this.ExecuteQuery(query).Single();
        }

        public async Task<T> ExecuteSingleAsync<T>(ISqlQuery<T> query) where T : new()
        {
            return (await this.ExecuteQueryAsync(query).ConfigureAwait(false)).Single();
        }

        public T ExecuteSingleOrDefault<T>(ISqlQuery<T> query) where T : new()
        {
            return this.ExecuteQuery(query).SingleOrDefault();
        }

        public async Task<T> ExecuteSingleOrDefaultAsync<T>(ISqlQuery<T> query) where T : new()
        {
            return (await this.ExecuteQueryAsync(query).ConfigureAwait(false)).SingleOrDefault();
        }

        public T ExecuteFirstOrDefault<T>(ISqlQuery<T> query) where T : new()
        {
            return this.ExecuteQuery(query).FirstOrDefault();
        }

        public async Task<T> ExecuteFirstOrDefaultAsync<T>(ISqlQuery<T> query) where T : new()
        {
            return (await this.ExecuteQueryAsync(query).ConfigureAwait(false)).FirstOrDefault();
        }

        public void AddCommand(ISqlCommand command)
        {
            this.SqlCommands.Add(command);
        }

        public void AddCommands(IEnumerable<ISqlCommand> commands)
        {
            this.SqlCommands.AddRange(commands);
        }

        public void Commit()
        {
            this.ExecuteCommands();
        }

        public async Task CommitAsync()
        {
            return;
            //await this.ExecuteCommandsAsync().ConfigureAwait(false);
        }

        public void Rollback()
        {
            this.SqlCommands.Clear();
        }

        public void Init()
        {
            this.SqlCommands.AddRange(this.GetSqlCommandsFromScript(this.GetInitScript()));
            //this.Update(0);
            this.ExecuteCommands();

            var config = new Configuration { Id = Config.Static.OnboardingId, Value = "false" };

            var connection = this.GetOpenConnection();
            connection.InsertOrReplace(config);
        }

        public void Update()
        {

            int currentVersion = this.ExecuteSingle(new GetVersionQuery()).Nr;
            this.Update(currentVersion);

        }

        private void Update(int currentVersion)
        {

            foreach (var dbScript in this.GetUpdateScripts().Where(k => k.Key > currentVersion).OrderBy(k => k.Key))
            {
                this.SqlCommands.AddRange(this.GetSqlCommandsFromScript(dbScript.Value));
                this.SqlCommands.Add(new InsertVersionCommand(new Models.Version { Nr = dbScript.Key }));
            }

            this.ExecuteCommands();
        }

        private void ExecuteCommands()
        {
            if (this.SqlCommands.Any() == false)
            {
                return;
            }

            var connection = this.GetOpenConnection();

            try
            {
                var sqlCommands = this.SqlCommands.ToList();
                this.SqlCommands.Clear();
                connection.RunInTransaction(() =>
                {
                    foreach (ISqlCommand command in sqlCommands)
                    {
                        connection.Execute(command.GetCommand(), command.GetParams() ?? new object[] { });
                    }
                });
                connection.Commit();
            }
            catch
            {
                connection.Rollback();
            }
        }

        private string GetInitScript()
        {
            string initScriptResourceName = this.resourceNames.Single(n => n.Equals($"{this.resourceNamespace}.InitScripts.InitSchema.sql"));
            return this.LoadScript(initScriptResourceName);
        }

        private Dictionary<int, string> GetUpdateScripts()
        {
            var dbScriptsDictionary = new Dictionary<int, string>();

            var updateScriptsNamespace = $"{this.resourceNamespace}.UpdateScripts.";
            var updateScripts = this.resourceNames.Where(n => n.StartsWith($"{updateScriptsNamespace}")).ToList();

            foreach (var updateScript in updateScripts)
            {
                var version = int.Parse(updateScript.Replace(updateScriptsNamespace, string.Empty).Replace(".sql", string.Empty));
                var script = this.LoadScript(updateScript);
                if (!string.IsNullOrEmpty(script))
                {
                    dbScriptsDictionary.Add(version, script);
                }
            }

            return dbScriptsDictionary;
        }

        private IEnumerable<string> GetConfigScripts()
        {
            var configScripts = new List<string>();

            string configScriptResourceName = this.resourceNames.Single(n => n.Equals($"{this.resourceNamespace}.ConfigScripts.SetConfig.sql"));
            configScripts.Add(this.LoadScript(configScriptResourceName));
            string environmentConfigScriptResourceName = this.resourceNames.Single(n => n.Equals($"{this.resourceNamespace}.ConfigScripts.SetConfig." + this.environment + ".sql"));
            configScripts.Add(this.LoadScript(environmentConfigScriptResourceName));

            string platformConfigScriptResourceName = null;

            switch (this.deviceService.Platform)
            {
                case Common.Options.DeviceOption.Droid:
                    platformConfigScriptResourceName = this.resourceNames.SingleOrDefault(n => n.Equals($"{this.resourceNamespace}.ConfigScripts.SetConfig." + this.environment + ".Android.sql"));
                    break;
                case Common.Options.DeviceOption.IOS:
                    platformConfigScriptResourceName = this.resourceNames.SingleOrDefault(n => n.Equals($"{this.resourceNamespace}.ConfigScripts.SetConfig." + this.environment + ".iOS.sql"));
                    break;
            }

            if (string.IsNullOrWhiteSpace(platformConfigScriptResourceName) == false)
            {
                configScripts.Add(this.LoadScript(platformConfigScriptResourceName));
            }

            return configScripts;
        }

        private string LoadScript(string resourceName)
        {
            var resourceStream = this.assembly.GetManifestResourceStream(resourceName);
            if (resourceStream == null)
            {
                return string.Empty;
            }

            var reader = new StreamReader(resourceStream);
            var script = reader.ReadToEnd();
            return script;
        }

        private List<ISqlCommand> GetSqlCommandsFromScript(string script)
        {
            var commands = new List<ISqlCommand>();

            var statements = script.Split(new[] { "GO\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var statement in statements)
            {
                commands.Add(new PlainSqlCommand(statement));
            }

            return commands;
        }

        private SQLiteConnection GetOpenConnection()
        {
            return new SQLiteConnection(this.fileStore.NativePath(Config.Static.DatabaseFileName));
        }


        public void AddConfigValue(Guid id, string value)
        {
            var connection = this.GetOpenConnection();
            var config = new Configuration
            {
                Id = id,
                Value = value
            };
            connection.InsertOrReplace(config);
        }

        public void RemoveConfigValue(Guid id)
        {
            var connection = this.GetOpenConnection();
            var config = connection.Table<Configuration>().SingleOrDefault(x => x.Id == id);
            if (config != null)
            {
                connection.Delete(config);
            }
        }

        public string GetConfigValue(Guid id)
        {
            var connection = this.GetOpenConnection();
            return connection.Table<Configuration>().Single(x => x.Id == id).Value;
        }

        public bool ExistConfigValue(Guid id)
        {
            var connection = this.GetOpenConnection();
            return connection.Table<Configuration>().SingleOrDefault(x => x.Id == id) != null;
        }
    }
}
