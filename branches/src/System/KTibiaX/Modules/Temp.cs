using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Reflection;

namespace KTibiaX.Modules {
    class Temp {

        private static void TestInjection() {
            IUnityContainer container = new UnityContainer();
            container.AddNewExtension<Interception>();
            container.RegisterType<ISubscriberRepository, SubscriberRepository>();

            container.Configure<Interception>().SetDefaultInterceptorFor<ISubscriberRepository>(
                new TransparentProxyInterceptor());

            ISubscriberRepository repository = container.Resolve<ISubscriberRepository>();
            repository.Add(null);
        }

        public interface ISubscriberRepository {
            [NotNull]
            void Add(Subscriber subscriber);
        }

        public class SubscriberRepository : ISubscriberRepository {
            public void Add(Subscriber subscriber) {
                // Do Something
            }
        }

        public class Subscriber {
            public string EmailAddress { get; set; }
        }

        public class NotNullAttribute : HandlerAttribute {
            public override ICallHandler CreateHandler(IUnityContainer container) {
                return new NotNullHandler();
            }
        }

        public class NotNullHandler : Microsoft.Practices.Unity.InterceptionExtension.ICallHandler {
            public int Order { get; set; }

            public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext) {

                for (int i = 0; i < input.Inputs.Count; i++) {
                    object target = input.Inputs[i];
                    if (target == null) {
                        ParameterInfo parameterInfo = input.Inputs.GetParameterInfo(i);
                        ArgumentNullException ex = new ArgumentNullException(parameterInfo.Name);
                        return input.CreateExceptionMethodReturn(ex);
                    }
                }
                return getNext()(input, getNext);
            }
        }

    }
}
