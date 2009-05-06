using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Castle.Core;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Keyrox.Infra.Logger;
using Keyrox.Infra.Session;
using Rhino.Commons;

namespace Keyrox.Infra {
    public static class Domain {

        /// <summary>
        /// Initializes the db context.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public static void InitializeDbContext(string connectionString) {
            InitializeDbContext(connectionString, AutoCreateOption.None);
        }

        /// <summary>
        /// Initializes the db context.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="option">The option.</param>
        public static void InitializeDbContext(string connectionString, AutoCreateOption option) {
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, option);
            IsDBInitialized = true;
        }

        /// <summary>
        /// Registers the components.
        /// </summary>
        /// <param name="asm">The asm.</param>
        public static void RegisterComponents(Assembly asm) {
            RegisterComponents(asm, typeof(Keyrox.Infra.Repository.IRepositoryBase), typeof(Keyrox.Infra.Service.IServiceBase));
        }

        /// <summary>
        /// Registers the components.
        /// </summary>
        /// <param name="asm">The assembly.</param>
        /// <param name="contracts">The contracts.</param>
        public static void RegisterComponents(Assembly asm, params Type[] contracts) {

            var inContracts = contracts.ToList();
            var types = asm.GetTypes();
            var windsorContainer = new Castle.Windsor.WindsorContainer();

            foreach (Type type in types) {
                if (!type.IsInterface && !type.IsAbstract) {
                    var interfaces = type.GetInterfaces();
                    if (interfaces.Count() > 2) {

                        foreach (Type contract in inContracts) {
                            if (interfaces.Contains(contract)) {

                                if (!windsorContainer.Kernel.HasComponent(interfaces[2].FullName)) {
                                    windsorContainer.AddComponentWithLifestyle(interfaces[2].FullName, interfaces[2], type, LifestyleType.Singleton);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            windsorContainer.AddComponentWithLifestyle(typeof(SessionProvider).FullName, typeof(ISessionProvider), typeof(SessionProvider), LifestyleType.Singleton);
            if (ConfigurationManager.AppSettings.AllKeys.Contains("EnableLog")) {
                bool useLog = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableLog"].ToString());
                if (useLog) windsorContainer.AddComponentWithLifestyle(typeof(Log).FullName, typeof(ILog), typeof(Log), LifestyleType.Singleton);
            }
            IoC.Initialize(windsorContainer);
            IsIoCInitialized = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether IoC instance initialized.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this IoC instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public static bool IsIoCInitialized { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether DB instance is initialized.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if DB instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public static bool IsDBInitialized { get; private set; }

        /// <summary>
        /// Throws the not initialized context exception.
        /// </summary>
        public static void ThrowNotInitializedContext() {
            if (!IsIoCInitialized)
                throw new Exception(Properties.Resources.DomainNotInitializedMessage);
        }

        /// <summary>
        /// Gets the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetComponent<T>() {
            return IoC.Resolve<T>();
        }

        /// <summary>
        /// Verify if defined component exists in current IoC Container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool ComponentExists<T>() {
            return IoC.Container.Kernel.HasComponent(typeof(T).FullName);
        }

        /// <summary>
        /// Components the exists.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static bool ComponentExists(Type type) {
            return IoC.Container.Kernel.HasComponent(type.FullName);
        }

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component">The component.</param>
        public static void AddComponent<T>() {
            IoC.Container.AddComponentWithLifestyle(typeof(T).FullName, typeof(T), typeof(T), LifestyleType.Singleton);
        }

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="type">The type.</param>
        public static void AddComponent(Type type) {
            IoC.Container.AddComponentWithLifestyle(type.FullName, type, type, LifestyleType.Singleton);
        }

    }
}
