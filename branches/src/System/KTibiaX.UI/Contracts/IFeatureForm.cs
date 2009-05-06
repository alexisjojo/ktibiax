using System;
using Tibia.Client;
using Tibia.Features.Model;
using System.Windows.Forms;

namespace KTibiaX.UI.Contracts {
    public interface IFeatureForm {

        /// <summary>
        /// Gets or sets the tibia client.
        /// </summary>
        /// <value>The tibia client.</value>
        TibiaClient TibiaClient { get; set; }

        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <value>The player.</value>
        Player Player { get; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Shows this instance.
        /// </summary>
        void Show();

        /// <summary>
        /// Hides this instance.
        /// </summary>
        void Hide();

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();

        /// <summary>
        /// Gets the action interval.
        /// </summary>
        /// <value>The action interval.</value>
        int ActionInterval { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is started.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is started; otherwise, <c>false</c>.
        /// </value>
        bool IsStarted { get; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Occurs when [form closed].
        /// </summary>
        event FormClosedEventHandler FormClosed;

        /// <summary>
        /// Occurs when [form closing].
        /// </summary>
        event FormClosingEventHandler FormClosing;
    }
}
