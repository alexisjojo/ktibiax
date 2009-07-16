using System.Collections.Generic;
using DevExpress.Data.Filtering;

namespace Keyrox.Infra.Repository {
    public interface IRepositoryBase<T> : IRepositoryBase
        where T : DevExpress.Xpo.XPBaseObject {

        T SaveNew(T obj);

        void Update(T obj);

        void Delete(T obj);

        void DeleteAll();

        void DeleteAll(CriteriaOperator criteria);


        T LoadById(object id);

        T LoadById(object id, bool forceGetFromDB);


        IList<T> LoadByProperty(string property, object value);

        IList<T> LoadAll();

        IList<T> LoadAll(CriteriaOperator criteria);

        bool NotCommitAfterChange { get; set; }

        void CommitChanges();

        int Count();

        int Count(CriteriaOperator criteria);
    }
    public interface IRepositoryBase {
    }
}
