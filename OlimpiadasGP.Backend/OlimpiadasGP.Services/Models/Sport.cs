using System;
using System.Collections.Generic;
using System.Text;

namespace OlimpiadasGP.Services.Models
{
    public class Sport
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual SportType Type { get; set; }
        public virtual string FirstRoundClashes { get; set; }
    }

    public enum SportType
    {
        PlayOff,        // The contenders compete 2 by 2. The one that looses is out. 
                        // The other, competes with the winner of another pair.

        Run,            // All contenders run at the same time

        RunWithPlayOff  // The contenders are divided in 2 groups and the best 2 of each group 
                        // run again in the final run.
    }
}
