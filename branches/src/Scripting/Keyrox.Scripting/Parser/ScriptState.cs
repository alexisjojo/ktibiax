using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Keyrox.Scripting.Parser {
    public enum ScriptState { 
        
        [Description("Not Verified")]
        NotCompiled = 0,
                
        [Description("Build Succeded")]
        ValidScript = 1,

        [Description("Has Errors")]
        HasErrors = 2
   
    }
}
