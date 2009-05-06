using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Keyrox.Infra.Transaction;

namespace Keyrox.Infra.Repository {
    public class RepositoryBase<T> : IRepositoryBase, IRepositoryBase<T> where T : XPBaseObject {

        public virtual T SaveNew(T obj) {
            TransactionScope.DefaultUnitOfWork.Save(obj);
            if (!NotCommitAfterChange) CommitChanges();
            obj.Reload();
            return obj;
        }

        public virtual void Update(T obj) {
            TransactionScope.DefaultUnitOfWork.Save(obj);
            if (!NotCommitAfterChange) CommitChanges();
        }

        public virtual void Delete(T obj) {
            TransactionScope.DefaultUnitOfWork.Delete(obj);
            TransactionScope.DefaultUnitOfWork.Save(obj);
            if (!NotCommitAfterChange) CommitChanges();
        }

        public virtual void DeleteAll() {
            var list = new XPCollection<T>(PersistentCriteriaEvaluationBehavior.InTransaction, TransactionScope.DefaultUnitOfWork, null);
            TransactionScope.DefaultUnitOfWork.Delete(list);
            TransactionScope.DefaultUnitOfWork.Save(list);
            if (!NotCommitAfterChange) CommitChanges();
        }

        public virtual void DeleteAll(CriteriaOperator criteria) {
            var list = new XPCollection<T>(PersistentCriteriaEvaluationBehavior.InTransaction, TransactionScope.DefaultUnitOfWork, criteria);
            TransactionScope.DefaultUnitOfWork.Delete(list);
            TransactionScope.DefaultUnitOfWork.Save(list);
        }

        public T LoadById(object id) {
            return TransactionScope.DefaultUnitOfWork.GetObjectByKey<T>(id);
        }

        public T LoadById(object id, bool forceGetFromDB) {
            return TransactionScope.DefaultUnitOfWork.GetObjectByKey<T>(id, forceGetFromDB);
        }

        public IList<T> LoadByProperty(string property, object value) {
            return new XPCollection<T>(
                PersistentCriteriaEvaluationBehavior.InTransaction,
                TransactionScope.DefaultUnitOfWork,
                new BinaryOperator(property, value)).ToList<T>();
        }

        public IList<T> LoadAll() {
            return new XPCollection<T>(
                PersistentCriteriaEvaluationBehavior.InTransaction,
                TransactionScope.DefaultUnitOfWork, null).ToList<T>();
        }

        public IList<T> LoadAll(CriteriaOperator criteria) {
            return new XPCollection<T>(
                PersistentCriteriaEvaluationBehavior.InTransaction,
                TransactionScope.DefaultUnitOfWork, criteria).ToList<T>();
        }

        public int Count() {
            return (int)TransactionScope.DefaultUnitOfWork.Evaluate(typeof(T), CriteriaOperator.Parse("Count()"), null);
        }

        public int Count(CriteriaOperator criteria) {
            return (int)TransactionScope.DefaultUnitOfWork.Evaluate(typeof(T), CriteriaOperator.Parse("Count()"), criteria);
        }

        public bool NotCommitAfterChange { get; set; }

        public void CommitChanges() {
            TransactionScope.DefaultUnitOfWork.CommitChanges();
        }
    }
}
