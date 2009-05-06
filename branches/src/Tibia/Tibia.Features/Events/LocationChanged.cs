﻿using System;
using Tibia.Features.Structures;
namespace Tibia.Features.Events {
    public class LocationEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationEventArgs"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        public LocationEventArgs(Location location) {
            Location = location;
        }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        public Location Location { get; set; }
    }
}
