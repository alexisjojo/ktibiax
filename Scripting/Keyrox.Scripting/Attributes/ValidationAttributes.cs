using Keyrox.Scripting.Util;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Collections.Generic;

namespace Keyrox.Scripting.Attributes {
    public class ActionArgumentValidtionAttribute : HandlerAttribute {

        /// <summary>
        /// Gets or sets the length of the arguments.
        /// </summary>
        /// <value>The length of the argument.</value>
        public int Length { get; set; }

        /// <summary>
        /// Creates the handler.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        public override ICallHandler CreateHandler(IUnityContainer container) {
            return new ActionArgumentValidadtionHandler() { Length = this.Length };
        }
    }
}
