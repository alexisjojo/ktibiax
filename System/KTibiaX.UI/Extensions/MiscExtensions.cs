using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Keyrox.Shared.Objects;
using Tibia.Features.Structures;

namespace KTibiaX.UI.Extensions {
    public static class MiscExtensions {

        public static StringCollection GetCollection(this IList<Location> list) {
            var result = new StringCollection();
            if (list != null) {
                foreach (var item in list) result.Add(item.ToString());
            }
            return result;
        }

        public static IList<Location> GetLocationList(this StringCollection list) {
            var result = new List<Location>();
            if (list != null) {
                foreach (var item in list) {

                    string[] pos = item.Split(new char[] { " ".ToChar() }, StringSplitOptions.RemoveEmptyEntries);
                    var X = pos[0].Replace("X:", "").Trim();
                    var Y = pos[1].Replace("Y:", "").Trim();
                    var Z = pos[2].Replace("Z:", "").Trim();
                    var D = pos[3].Replace("D:", "").Trim();
                    result.Add(new Location(X.ToUInt32(), Y.ToUInt32(), Z.ToUInt32(), D.ToUInt32()));
                }
            }
            return result;
        }

        public static StringCollection GetCollection(this IList<string> list) {
            var result = new StringCollection();
            if (list != null) {
                foreach (var item in list) result.Add(item.ToString());
            }
            return result;
        }

        public static IList<string> GetNameList(this StringCollection list) {
            var result = new List<string>();
            if (list != null) {
                foreach (var item in list) result.Add(item);
            }
            return result;
        }
    }
}
