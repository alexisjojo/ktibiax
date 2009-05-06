using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace Tibia.Features.Structures {
    /// <summary>
    /// Structure of Skill Value.
    /// </summary>
    public struct PointsLeft {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointsLeft"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="percent">The percent.</param>
        public PointsLeft(uint value, uint percent) {
            this.Value = value;
            this.Left = Convert.ToUInt32(100 - percent);
        }

        #region "[rgn] Public Properties   "
        public uint Value, Left;
        #endregion

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString() {
            return Value.ToString();
        }

    }
}
