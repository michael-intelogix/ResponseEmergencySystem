using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Models.Logs;

namespace ResponseEmergencySystem.Builders
{
    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf>
        where TSubject : new()
    {
        private readonly List<Func<Log, Log>> actions = new List<Func<Log, Log>>();

        public TSelf Do(Action<Log> action) => AddAction(action);

        public Log Build() => actions.Aggregate(new Log(), (l, f) => f(l));

        private TSelf AddAction(Action<Log> action)
        {
            actions.Add(l =>
            {
                action(l);

                if (l.NewValue != l.OldValue)
                { 

                    using (SqlCommand cmd = new SqlCommand {
                        Connection = constants.SIREMConnection,
                        CommandText = $"Update_Logs",
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        if (cmd.Connection.State ==ConnectionState.Open)
                        {
                            cmd.Connection.Close();
                        }

                        cmd.Parameters.AddWithValue("@OldValue", l.OldValue);
                        cmd.Parameters.AddWithValue("@NewValue", l.NewValue);
                        cmd.Parameters.AddWithValue("@Description", l.Change);
                        cmd.Parameters.AddWithValue("@ID_Incident", constants.userID);

                        cmd.Connection.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr == null)
                            {
                                throw new NullReferenceException("No Information Avaible.");
                            }
                            while (sdr.Read())
                            {
                                Debug.WriteLine(sdr["Validacion"]);
                                Debug.WriteLine(sdr["msg"]);
                                Debug.WriteLine(sdr["ID"]);
                            }

                        }

                        cmd.Connection.Close();
                    }

                }
                return l;
            });

            return (TSelf)this;
        }
    }


    public sealed class LogBuilder : FunctionalBuilder<Log, LogBuilder>
    {

        public LogBuilder Create(string change, string oldValue, string newValue, string user) => Do(l => {
            Debug.WriteLine($"the {nameof(oldValue)} is {oldValue} and the {nameof(newValue)} is {newValue}");

            l.SetLog(change, oldValue, newValue, user);             
        });
    }

}
