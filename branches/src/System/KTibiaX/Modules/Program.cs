using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using KTibiaX.UI.Components;
using KTibiaX.Windows;
using Keyrox.Infra.Repository;
using Keyrox.Shared.Controls;

namespace KTibiaX {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.UserSkins.OfficeSkins.Register();

            string conn = DevExpress.Xpo.DB.SQLiteConnectionProvider.GetConnectionString(Path.Combine(Application.StartupPath, "DataBase\\ktibiax.s3db"));
            Keyrox.Infra.Domain.InitializeDbContext(conn, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            Keyrox.Infra.Domain.RegisterComponents(Assembly.GetAssembly(typeof(KTibiaX.Core.Model.FeatureData)), typeof(IRepositoryBase));

            Application.Run(new frm_RibbonMenu());
        }

        /// <summary>
        /// Handles the ThreadException event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Threading.ThreadExceptionEventArgs"/> instance containing the event data.</param>
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
            Output.Add(e.Exception);
        }

    }
}
