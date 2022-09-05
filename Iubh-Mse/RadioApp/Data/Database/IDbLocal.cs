using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iubh.RadioApp.Data.Models;

namespace Iubh.RadioApp.Data.Database
{
    public interface IDbLocal
    {
        void Init();

        void Update();

        IEnumerable<T> ExecuteQuery<T>(ISqlQuery<T> query) where T : new();

        Task<IEnumerable<T>> ExecuteQueryAsync<T>(ISqlQuery<T> query) where T : new();

        T ExecuteSingle<T>(ISqlQuery<T> query) where T : new();

        Task<T> ExecuteSingleAsync<T>(ISqlQuery<T> query) where T : new();

        T ExecuteSingleOrDefault<T>(ISqlQuery<T> query) where T : new();

        Task<T> ExecuteSingleOrDefaultAsync<T>(ISqlQuery<T> query) where T : new();

        T ExecuteFirstOrDefault<T>(ISqlQuery<T> query) where T : new();

        Task<T> ExecuteFirstOrDefaultAsync<T>(ISqlQuery<T> query) where T : new();

        void AddCommand(ISqlCommand command);

        void AddCommands(IEnumerable<ISqlCommand> commands);

        void Commit();

        Task CommitAsync();

        void Rollback();
        
        void AddConfigValue(Guid id, string value);

        string GetConfigValue(Guid id);

        bool ExistConfigValue(Guid id);

        void RemoveConfigValue(Guid id);
    }
}
