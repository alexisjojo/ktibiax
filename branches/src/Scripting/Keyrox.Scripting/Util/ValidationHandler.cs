using System;
using System.Linq;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Reflection;
using Keyrox.Scripting.Attributes;

namespace Keyrox.Scripting.Util {
    public class ActionArgumentValidadtionHandler : ICallHandler {

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        public int Length { get; set; }
        
        /// <summary>
        /// Invokes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="getNext">The get next.</param>
        /// <returns></returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext) {
            Exception ex = null;
            if (input.Inputs.Count != 1) { ex = new ArgumentOutOfRangeException("Action methods can only contains one argument!"); }
            if (input.Inputs[0].GetType() != typeof(string[])) { ex = new ArgumentException("The action method argument must be of type string[]!"); }

            if (Length > -1) {
                var arg = (string[])input.Arguments[0];
                if (arg.Length != Length) { ex = new ArgumentOutOfRangeException("Invalid number of arguments!"); }
            }
            var types = input.MethodBase.GetCustomAttributes(typeof(ArgumentTypeAttribute), true);
            if (types != null && types.Count() > 0) {
                foreach (var tp in types.Cast<ArgumentTypeAttribute>()) {
                    try { Convert.ChangeType(input.Arguments[tp.Index], tp.Type); }
                    catch (InvalidCastException) { ex = new InvalidCastException(string.Format("The argument index: {0} must be of type: {1}!", tp.Index, tp.Type.ToString())); }
                }
            }
            return ex != null ? input.CreateExceptionMethodReturn(ex) : getNext()(input, getNext);
        }
    }

}
