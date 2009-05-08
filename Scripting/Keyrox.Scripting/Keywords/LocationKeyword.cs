using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Scripting.Keywords {
    public class LocationKeyword {

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        /// <value>The X.</value>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        /// <value>The Y.</value>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the Z.
        /// </summary>
        /// <value>The Z.</value>
        public int Z { get; set; }

        /// <summary>
        /// Parses the specified args.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public bool Parse(string args) {

            var items = args.Split(new[] { ',' });
            if (items.Length == 3) {

                var x = 0; int.TryParse(items[0], out x);
                var y = 0; int.TryParse(items[1], out y);
                var z = 0; int.TryParse(items[2], out z);

                if (x > 0 && y > 0 && z > 0) {
                    this.X = x;
                    this.Y = y;
                    this.Z = z;
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Gets the map location.
        /// </summary>
        /// <returns></returns>
        public Tibia.Features.Structures.Location GetTibiaLocation() {
            return new Tibia.Features.Structures.Location((uint)X, (uint)Y, (uint)Z);
        }
    }
}
