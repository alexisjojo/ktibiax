using System;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.JScript;

namespace Keyrox.Shared.Parser {
    public sealed class Evaluator {

        /// <summary>
        /// Initializes the <see cref="Evaluator"/> class.
        /// </summary>
        static Evaluator() {
            var compiler = new JScriptCodeProvider().CreateCompiler();
            var parameters = new CompilerParameters() { GenerateInMemory = true };
            var results = compiler.CompileAssemblyFromSource(parameters, jscriptSource);

            Assembly assembly = results.CompiledAssembly;
            evaluatorType = assembly.GetType("Evaluator.Evaluator");
            evaluator = Activator.CreateInstance(evaluatorType);
        }

        #region "[rgn] Private Variables "
        private static object evaluator = null;
        private static Type evaluatorType = null;
        private static readonly string jscriptSource =
        @"package Evaluator
        {
           class Evaluator
           {
              public function Eval(expr : String) : String 
              { 
                 return eval(expr); 
              }
           }
        }";
        #endregion

        /// <summary>
        /// Evals to integer.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns></returns>
        public static int EvalToInteger(string statement) {
            try {
                var str = EvalToString(statement);
                int res = 0;
                int.TryParse(str, out res);
                return res;
            }
            catch { return default(int); }
        }

        /// <summary>
        /// Evals to double.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns></returns>
        public static double EvalToDouble(string statement) {
            try {
                var str = EvalToString(statement);
                double res = 0;
                double.TryParse(str, out res);
                return res;
            }
            catch { return default(double); }
        }

        /// <summary>
        /// Evals to boolean.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns></returns>
        public static bool EvalToBoolean(string statement) {
            try {
                var str = EvalToString(statement);
                bool res = false;
                bool.TryParse(str, out res);
                return res;
            }
            catch { return default(bool); }
        }

        /// <summary>
        /// Evals to string.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns></returns>
        public static string EvalToString(string statement) {
            return EvalToObject(statement).ToString();            
        }

        /// <summary>
        /// Evals to object.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns></returns>
        public static object EvalToObject(string statement) {
            return evaluatorType.InvokeMember(
                "Eval",
                BindingFlags.InvokeMethod,
                null,
                evaluator,
                new object[] { statement }
             );
        }
    }
}
