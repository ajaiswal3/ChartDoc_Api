using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

/// <summary>
/// Summary description for ConnectionSpy
/// </summary>
public class ConnectionSpy
{
        SqlConnection con;
        StackTrace st;
        StateChangeEventHandler handler;

        public ConnectionSpy(SqlConnection  con, StackTrace st)
        {
            this.st = st;
            //latch on to the connection
            this.con = con;
            handler = new StateChangeEventHandler(StateChange);
            con.StateChange += handler;
            //substitute the spy's finalizer for the
            //connection's
            GC.SuppressFinalize(con);
        }
        public void StateChange(Object sender, System.Data.StateChangeEventArgs args)
        {
            if (args.CurrentState == ConnectionState.Closed)
            {
                //detach the spy object and let it float away into space
                //if the connection and the spy are already in the FReachable queue
                //GC.SuppressFinalize doesn't do anyting.
                GC.SuppressFinalize(this);
                con.StateChange -= handler;
                con = null;
                st = null;
            }
        }
        ~ConnectionSpy()
        {
            //if we got here then the connection was not closed.
            Trace.WriteLine("WARNING: Open Connection is being Garbage Collected");
            Trace.WriteLine("The connection was initially opened " + st.ToString());
            con.StateChange -= handler;
            //clean up the connection
            con.Dispose();
        }
  

}
