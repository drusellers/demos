using System;

namespace demos.transactions
{
    using System.Transactions;
    using NUnit.Framework;

    public class Class1
    {
        [Test]
        public void CreateAResource()
        {
            using (var t = new CommittableTransaction())
            {
                //MT: Would be volitile because its just memory
                t.EnlistVolatile(new AlwaysOk(), EnlistmentOptions.EnlistDuringPrepareRequired);

                t.Commit();
            }    
        }

        public class AlwaysOk : IEnlistmentNotification
        {
            public void Prepare(PreparingEnlistment preparingEnlistment)
            {
                Console.WriteLine("Prepare");
                
                preparingEnlistment.Prepared();
            }

            public void Commit(Enlistment enlistment)
            {
                Console.WriteLine("Commit");

                enlistment.Done();
            }

            public void Rollback(Enlistment enlistment)
            {
                Console.WriteLine("Rollback");

                enlistment.Done();
            }

            public void InDoubt(Enlistment enlistment)
            {
                Console.WriteLine("InDoubt");

                enlistment.Done();
            }
        }

        [Test]
        public void WorkWithCurrent()
        {
            using (var t = new CommittableTransaction())
            {
                //MT: Would be volitile because its just memory
                t.EnlistVolatile(new AlwaysOk(), EnlistmentOptions.None);

                //We can enlist in a current trans
                Transaction.Current.EnlistVolatile(new AlwaysOk(), EnlistmentOptions.None);

                t.Commit();
            }
        }

        [Test]
        public void InitiallyNull()
        {
            var trx = Transaction.Current;
            Assert.AreEqual(null, trx);
        }

        [Test]
        public void Scope()
        {
            using (var t = new TransactionScope())
            {
                var plainTransaction = Transaction.Current;
                
                //Can't commit from the static
                //trx.Commit();

                t.Complete();

                Assert.AreNotEqual(null, plainTransaction);
            }
        }



        [Test]
        public void CanCommit()
        {
            var trx = new CommittableTransaction();
            BindLogging(trx);

            //NOTE: Transaction.Current == null
            Assert.AreEqual(null, Transaction.Current);

            trx.Commit();
            
            trx.Dispose();
        }

        [Test]
        public void CanNotCommit()
        {
            Transaction trx = new CommittableTransaction();
            BindLogging(trx);

            //trx.Commit();

            trx.Dispose();
        }

        void BindLogging(Transaction trx)
        {
            trx.TransactionCompleted += (sender, args) =>
            {
                Console.WriteLine("Completed: {0}", args.Transaction.TransactionInformation.Status);
                Console.WriteLine("DTC ID: {0}", args.Transaction.TransactionInformation.DistributedIdentifier);
            };

        }
    }

}
