using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keyrox.Scripting.Attributes;
using Keyrox.Scripting.Parser;

namespace Keyrox.Scripting.Actions {
    [ActionClass("Script Management")]
    public class SystemActions : ITibiaAction {

        #region ITibiaAction Members
        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>The client.</value>
        public Tibia.Client.TibiaClient Client { get; set; }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        /// <value>The script.</value>
        public Keyrox.Scripting.Parser.ScriptInfo Script { get; set; }
        #endregion

        [ActionTitle("Log", "Send a message to tibia client as a system message.")]
        [ActionConfig(ImageIndex = 4, InputText = "log(\"\")", CarretPosition = -2)]
        [ActionParameter("Message", "The message that will be sent.", 0, NeedQuotes = true)]
        [ActionExamples("log(\"Starting the hunt\")", "log(\"Depositing countitem(3031) gold coins.\")")]
        public ScriptActionResult Log(string[] args) {
            #region "[rgn] Argument Validation "
            if (args.Count() != 1) { throw new ArgumentException("Invalid number of arguments!"); }
            #endregion

            Client.Actions.Message.System.Send(args[0], Tibia.Features.SystemMsgColor.Green);
            return new ScriptActionResult() { Success = true };
        }

        [ActionTitle("Pause", "Pause the script until the Resume function is called.\nAll ktibiax features will be stoped, exept the Auto Healer and Player Alerts.")]
        [ActionConfig(ImageIndex = 14, InputText = "pause()")]
        [ActionExamples("pause()")]
        public ScriptActionResult Pause(string[] args) {
            return new ScriptActionResult() { Success = true, MustPause = true };
        }

        [ActionTitle("Resume", "Resume the current script.")]
        [ActionConfig(ImageIndex = 12, InputText = "resume()")]
        [ActionExamples("resume()")]
        public ScriptActionResult Resume(string[] args) {
            return new ScriptActionResult() { Success = true, MustResume = true };
        }

        [ActionTitle("Wait", "Pause the script ultil the end of the delay.\nBut it will continue attacking if a monster appears.")]
        [ActionConfig(ImageIndex = 14, InputText = "wait()", CarretPosition = -1)]
        [ActionParameter("Delay", "The delay in seconds to resume the script.", 0)]
        [ActionExamples("wait(5)")]
        public void Wait(string[] args) {
        }

        [ActionTitle("Error", "Stop the script and log the error.")]
        [ActionConfig(ImageIndex = 3, InputText = "error(\"\")", CarretPosition = -2)]
        [ActionParameter("Message", "The message that will be logged as reason to stop the script.", 0, NeedQuotes = true)]
        [ActionExamples("error(\"You have no balance to buy spears!\")")]
        public void Error(string[] args) {
        }

        [ActionTitle("Go to Line", "Move the execution to defined script line.")]
        [ActionConfig(ImageIndex = 5, InputText = "gotoline()", CarretPosition = -1)]
        [ActionParameter("Line Number", "The number of the line to jump for.", 0)]
        [ActionExamples("gotoline(35)")]
        public void GoToLine(string[] args) {
        }

        [ActionTitle("Go to Section", "Move the execution to defined script section.")]
        [ActionConfig(ImageIndex = 5, InputText = "gotosection()", CarretPosition = -1, AutoList = AutoListType.Sections)]
        [ActionParameter("Section Name", "The name of the section to jump for.", 0)]
        [ActionExamples("gotosection(HuntToDepot)")]
        public void GoToSection(string[] args) {
        }
    }
}
