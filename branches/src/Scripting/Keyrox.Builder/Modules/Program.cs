using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Keyrox.Scripting.Keywords;
using System.IO;
using System.Reflection;
using Keyrox.Builder.Features;

namespace Keyrox.Builder {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.UserSkins.OfficeSkins.Register();

            ItemKeywordCollection.Current.Load();
            Application.Run(new frm_Agent(args.Length > 0 ? args[0] : string.Empty));
        }

        /// <summary>
        /// Extracts the syntax file.
        /// </summary>
        /// <returns></returns>
        public static string ExtractSyntaxFile() {
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ScriptConfig\Script.syn");
            var assembly = Assembly.GetExecutingAssembly();
            var IocNamespace = "Keyrox.Builder.ScriptConfig.Script.syn";

            if (File.Exists(fileName)) { File.Delete(fileName); }
            using (var resourceStream = assembly.GetManifestResourceStream(IocNamespace)) {
                if (resourceStream == null) { throw new Exception(IocNamespace + " Not Found!"); }

                using (var reader = new StreamReader(resourceStream)) {
                    var contents = reader.ReadToEnd();
                    using (var writer = new StreamWriter(fileName, false)) {
                        writer.WriteLine(contents);
                    }
                }
            }
            return fileName;
        }
    }
}
