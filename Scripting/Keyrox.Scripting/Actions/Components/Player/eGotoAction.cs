using System;
using Tibia.Client;
using Tibia.Features.Structures;

namespace Keyrox.Scripting.Actions.Components.Player {
    public class eGotoAction : ActionBaseComponent {

        /// <summary>
        /// Initializes a new instance of the <see cref="Goto"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public eGotoAction(TibiaClient client)
            : base(client) {
        }

        /// <summary>
        /// The max number of seconds before check if the location is out of range.
        /// </summary>
        protected const int MaxSecondsIdle = 5;

        #region "[rgn] Internal Variables "
        protected DateTime StartTime { get; set; }
        protected Location Location { get; set; }
        #endregion

        /// <summary>
        /// Executes the specified location.
        /// </summary>
        /// <param name="location">The location.</param>
        public bool Execute(Location location) {
            //First execution.
            if (StartTime == DateTime.MinValue) { StartTime = DateTime.Now; }

            //Player is attacking.
            if (Player.Target != null) { return false; }

            if (Player.IsWalking == false) {
                //Elapsed time verification.
                var elapsed = DateTime.Now.Subtract(StartTime);
                if (elapsed.TotalSeconds >= MaxSecondsIdle) {
                    //if (IsDestinationOutOfRange()) { throw new Exception("The destination is unreachable!"); }
                    throw new Exception("The maximum idle time has been reached.");
                }
                //Send the player to requested location.
                else { Player.Go(location); }
            }
            else { StartTime = DateTime.Now; }

            //Requested location has arrived.
            if (Player.Location.Equals(location)) { return true; }

            //Restart the process.
            Location = location;
            Wait(1000);
            return Execute(Location);
        }

    }
}
