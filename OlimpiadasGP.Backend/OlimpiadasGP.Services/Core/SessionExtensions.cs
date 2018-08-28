using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace OlimpiadasGP.Services.Core
{
    public static class SessionExtensions
    {
        private static IDictionary<ISession, Stack<string>> _savePointsStack = new Dictionary<ISession, Stack<string>>();

        public static int CreateSavepoint(this ISession session, string name)
        {
            var savepointsStack = GetSavepointsStack(session);
            savepointsStack.Push(name);

            if (!session.Transaction.IsActive)
            {
                session.BeginTransaction();
            }

            const string commandString = "save transaction :savepointName";
            session.CreateSQLQuery(commandString).SetParameter("savepointName", name).ExecuteUpdate();

            return savepointsStack.Count;
        }

        public static int RollbackToSavepoint(this ISession session, string name)
        {
            var savepointsStack = GetSavepointsStack(session);
            while (savepointsStack.Count > 0)
            {
                if (savepointsStack.Pop() == name)
                {
                    const string commandString = "rollback transaction :savepointName";
                    session.CreateSQLQuery(commandString).SetParameter("savepointName", name).ExecuteUpdate();

                    break;
                }
            }

            if (savepointsStack.Count == 0 && session.Transaction.IsActive)
            {
                session.Transaction.Rollback();
            }

            DeleteEmptySavepointsStacks();

            return savepointsStack.Count;
        }       

        private static Stack<string> GetSavepointsStack(ISession session)
        {
            if (!_savePointsStack.ContainsKey(session))
            {
                _savePointsStack.Add(new KeyValuePair<ISession, Stack<string>>(session, new Stack<string>()));
            }
            return _savePointsStack[session];
        }

        private static void DeleteEmptySavepointsStacks()
        {
            var emptyEntryKeys = _savePointsStack.Where(pair => pair.Value.Count == 0).Select(pair => pair.Key).ToList();

            foreach (var key in emptyEntryKeys)
            {
                _savePointsStack.Remove(key);
            }
        }
    }
}
