using System;
using Keyrox.Scripting.Actions;
using Keyrox.Scripting.Actions.Contracts;
using Keyrox.Scripting.Parser;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Tibia.Client;

namespace Keyrox.Scripting.Util {
    public sealed class Context {

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>The container.</value>
        private static IUnityContainer Container { get; set; }

        /// <summary>
        /// Initializes the container.
        /// </summary>
        private static void InitializeContainer() {
            Container = new UnityContainer();
            Container.AddNewExtension<Interception>();

            Container.RegisterType<IEvalActions, EvalActions>().Configure<Interception>().SetInterceptorFor<IEvalActions>(new InterfaceInterceptor());
            Container.RegisterType<IHuntActions, HuntActions>().Configure<Interception>().SetInterceptorFor<IHuntActions>(new InterfaceInterceptor());
            Container.RegisterType<IKillActions, KillActions>().Configure<Interception>().SetInterceptorFor<IKillActions>(new InterfaceInterceptor());
            Container.RegisterType<IParamActions, ParamActions>().Configure<Interception>().SetInterceptorFor<IParamActions>(new InterfaceInterceptor());
            Container.RegisterType<IPlayerActions, PlayerActions>().Configure<Interception>().SetInterceptorFor<IPlayerActions>(new InterfaceInterceptor());
            Container.RegisterType<ISystemActions, SystemActions>().Configure<Interception>().SetInterceptorFor<ISystemActions>(new InterfaceInterceptor());
        }

        /// <summary>
        /// Gets the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ITibiaAction GetAction(Type type, TibiaClient client, ScriptInfo script) {
            if (Container == null) { InitializeContainer(); }
            var res = (ITibiaAction)Container.Resolve(type);
            res.Client = client;
            res.Script = script;
            return res;
        }
    }
}
