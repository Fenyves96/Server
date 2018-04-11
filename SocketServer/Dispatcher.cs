
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Dispatcher:User {

    /**
     * 
     */
    public Dispatcher(string Name):base(Name){
    }



    public void ListDailyTasks() {
        // TODO implement here
    }

    public override string ToString()
    {
        return "Dispatcher";
    }

   

    internal void ManageDailyTasks()
    {
        throw new NotImplementedException();
    }
}