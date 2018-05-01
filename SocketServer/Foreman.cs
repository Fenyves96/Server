
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Foreman : User
{

    /**
     * 
     */
    public Foreman(string Name) : base(Name)
    {
    }



    public void ListDailyTasks()
    {
        // TODO implement here
    }

    public override string ToString()
    {
        return "Foreman";
    }



    internal void ManageDailyTasks()
    {
        throw new NotImplementedException();
    }
}
