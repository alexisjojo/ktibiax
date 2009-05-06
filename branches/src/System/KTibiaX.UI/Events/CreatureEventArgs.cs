using Tibia.Features.Model;
using System;

namespace KTibiaX.Analyzer.Events {
    public class CreatureEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatureEventArgs"/> class.
        /// </summary>
        /// <param name="creature">The creature.</param>
        public CreatureEventArgs(Creature creature) {
            Creature = creature;
        }

        /// <summary>
        /// Gets or sets the creature.
        /// </summary>
        /// <value>The creature.</value>
        public Creature Creature { get; set; }
    }
}
