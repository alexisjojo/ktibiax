using System;
using DevExpress.Xpo;

namespace Keyrox.Infra.Transaction {
    public sealed class TransactionScope : IDisposable {

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionScope"/> class.
        /// </summary>
        public TransactionScope() {
            if (UnitOfWork == null) {
                UnitOfWork = new UnitOfWork();
                //UnitOfWork.DefaultSession is null ??
                isTransactionOwner = true;
            }
            SetUpEvents();
        }

        /// <summary>
        /// Sets up events.
        /// </summary>
        private void SetUpEvents() {
            UnitOfWork.AfterConnect += new SessionManipulationEventHandler(UnitOfWork_AfterConnect);
            UnitOfWork.AfterDisconnect += new SessionManipulationEventHandler(UnitOfWork_AfterDisconnect);
            UnitOfWork.AfterBeginTransaction += new SessionManipulationEventHandler(UnitOfWork_AfterBeginTransaction);
            UnitOfWork.AfterCommitTransaction += new SessionManipulationEventHandler(UnitOfWork_AfterCommitTransaction);
            UnitOfWork.AfterRollbackTransaction += new SessionManipulationEventHandler(UnitOfWork_AfterRollbackTransaction);

            UnitOfWork.BeforeBeginTransaction += new SessionManipulationEventHandler(UnitOfWork_BeforeBeginTransaction);
            UnitOfWork.BeforeCommitTransaction += new SessionManipulationEventHandler(UnitOfWork_BeforeCommitTransaction);
            UnitOfWork.BeforeConnect += new SessionManipulationEventHandler(UnitOfWork_BeforeConnect);
            UnitOfWork.BeforeDisconnect += new SessionManipulationEventHandler(UnitOfWork_BeforeDisconnect);
            UnitOfWork.BeforeRollbackTransaction += new SessionManipulationEventHandler(UnitOfWork_BeforeRollbackTransaction);

            UnitOfWork.ObjectDeleted += new ObjectManipulationEventHandler(UnitOfWork_ObjectDeleted);
            UnitOfWork.ObjectDeleting += new ObjectManipulationEventHandler(UnitOfWork_ObjectDeleting);
            UnitOfWork.ObjectSaved += new ObjectManipulationEventHandler(UnitOfWork_ObjectSaved);
            UnitOfWork.ObjectSaving += new ObjectManipulationEventHandler(UnitOfWork_ObjectSaving);
        }

        #region " Internal Properties  "
        public bool isTransactionOwner { get; private set; }
        public bool CommitOnChange { get; set; }
        private UnitOfWork unitOfWork;
        public UnitOfWork UnitOfWork {
            get {
                try {
                    if (unitOfWork.DataLayer == null) {
                        unitOfWork = new UnitOfWork();
                        SetUpEvents();
                    }
                }
                catch (NullReferenceException) { unitOfWork = new UnitOfWork(); SetUpEvents(); }
                catch (ObjectDisposedException) { unitOfWork = new UnitOfWork(); SetUpEvents(); }
                return unitOfWork;
            }
            set { unitOfWork = value; }
        }
        #endregion

        #region " Unit of Work Events  "
        private void UnitOfWork_ObjectSaving(object sender, ObjectManipulationEventArgs e) {
        }

        private void UnitOfWork_ObjectSaved(object sender, ObjectManipulationEventArgs e) {
            //System.Diagnostics.Debugger.Break();
            //TODO: Perform Log Actions with "e.Object"!
            //if (CommitOnChange) UnitOfWork.CommitChanges();
        }

        private void UnitOfWork_ObjectDeleting(object sender, ObjectManipulationEventArgs e) {
        }

        private void UnitOfWork_ObjectDeleted(object sender, ObjectManipulationEventArgs e) {
            //System.Diagnostics.Debugger.Break();
            //TODO: Perform Log Actions with "e.Object"!
            //if (CommitOnChange) UnitOfWork.CommitChanges();
        }

        private void UnitOfWork_BeforeDisconnect(object sender, SessionManipulationEventArgs e) {
        }

        private void UnitOfWork_BeforeConnect(object sender, SessionManipulationEventArgs e) {
        }

        private void UnitOfWork_BeforeCommitTransaction(object sender, SessionManipulationEventArgs e) {
        }

        private void UnitOfWork_BeforeBeginTransaction(object sender, SessionManipulationEventArgs e) {
        }

        private void UnitOfWork_BeforeRollbackTransaction(object sender, SessionManipulationEventArgs e) {
        }

        private void UnitOfWork_AfterRollbackTransaction(object sender, SessionManipulationEventArgs e) {
        }

        private void UnitOfWork_AfterCommitTransaction(object sender, SessionManipulationEventArgs e) {
        }

        private void UnitOfWork_AfterBeginTransaction(object sender, SessionManipulationEventArgs e) {
        }

        private void UnitOfWork_AfterDisconnect(object sender, SessionManipulationEventArgs e) {
        }

        private void UnitOfWork_AfterConnect(object sender, SessionManipulationEventArgs e) {
        }
        #endregion

        #region " Transction Singleton "
        private static TransactionScope current;
        public static TransactionScope Current {
            get { if (current == null) current = new TransactionScope(); return current; }
        }
        public static UnitOfWork DefaultUnitOfWork {
            get { return Current.UnitOfWork; }
        }
        #endregion

        /// <summary>
        /// Commits the changes.
        /// </summary>
        public void CommitChanges() {
            if (UnitOfWork != null) UnitOfWork.CommitChanges();
        }

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public void CommitTransaction() {
            if (UnitOfWork != null) UnitOfWork.CommitTransaction();
        }

        /// <summary>
        /// Votes the roll back.
        /// </summary>
        public void VoteRollBack() {
            if (UnitOfWork != null)
                UnitOfWork.RollbackTransaction();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            if (isTransactionOwner && UnitOfWork != null) {
                UnitOfWork.Disconnect();
                UnitOfWork.Dispose();
            }
        }
    }
}
