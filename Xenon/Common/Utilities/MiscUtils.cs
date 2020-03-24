using System.Collections.Generic;

namespace Xenon.Common.Utilities {
	public static class MiscUtils {
        public static T KeyByValue<T, W>(this Dictionary<T, W> dict, W val) {
            T key = default;
            foreach (KeyValuePair<T, W> pair in dict) {
                if (EqualityComparer<W>.Default.Equals(pair.Value, val)) {
                    key = pair.Key;
                    break;
                }
            }
            return key;
        }
    }
}
