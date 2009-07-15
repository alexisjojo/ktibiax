using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.SourceCode;

namespace Keyrox.Scripting.Util {
    public static class Extensions {

        /// <summary>
        /// Toes the list.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public static List<Word> ToList(this Row row) {
            return row.Cast<Word>().ToList();
        }

    }
}
