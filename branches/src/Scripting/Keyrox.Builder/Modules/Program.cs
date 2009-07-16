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
        /// Extracts the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="path">The path.</param>
        /// <param name="ignoreIfExists">if set to <c>true</c> [ignore if exists].</param>
        /// <returns>The File Name.</returns>
        public static string ExtractResource(string resource, string path, bool ignoreIfExists) {
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path);
            var assembly = Assembly.GetExecutingAssembly();
            var IocNamespace = resource;

            if (File.Exists(fileName)) { if (!ignoreIfExists) { File.Delete(fileName); } else { return fileName; } }
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

        /// <summary>
        /// Extracts the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="path">The path.</param>
        /// <returns>The File Name.</returns>
        public static string ExtractResource(string resource, string path) {
            return ExtractResource(resource, path, false);
        }
    }
}
