using System;
using System.Linq;
using System.Collections.Generic;

namespace Tibia.Features.Model.Graphics {
    public class SqmCollection : List<Sqm> {

        /// <summary>
        /// Initializes a new instance of the <see cref="SQMCollection"/> class.
        /// </summary>
        public SqmCollection() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQMCollection"/> class.
        /// </summary>
        /// <param name="list">The list.</param>
        public SqmCollection(List<Sqm> list) {
            this.AddRange(list);
        }

        /// <summary>
        /// Containerses this instance.
        /// </summary>
        /// <returns></returns>
        public SqmCollection Containers() {
            return new SqmCollection(
                (from sqm in this where sqm.Tiles.Count(t => t.IsContainer == true && t.IsDepot == false) > 0 select sqm)
                .ToList());
        }

        /// <summary>
        /// Depotses this instance.
        /// </summary>
        /// <returns></returns>
        public SqmCollection Depots() {
            return new SqmCollection(
                (from sqm in this where sqm.Tiles.Count(t => t.IsDepot == true) > 0 select sqm)
                .ToList());
        }

        /// <summary>
        /// Playerses this instance.
        /// </summary>
        /// <returns></returns>
        public SqmCollection Players() {
            return new SqmCollection(
                (from sqm in this where sqm.Tiles.Count(t => t.TileId == 99) > 0 select sqm)
                .ToList());
        }
    }
}
