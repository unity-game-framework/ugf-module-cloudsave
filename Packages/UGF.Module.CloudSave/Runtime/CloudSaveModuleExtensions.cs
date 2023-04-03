using System;
using System.Threading.Tasks;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.CloudSave.Runtime
{
    public static class CloudSaveModuleExtensions
    {
        public static async Task<T> ReadAsync<T>(this ICloudSaveModule module, GlobalId slotId) where T : class
        {
            if (module == null) throw new ArgumentNullException(nameof(module));
            if (!slotId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(slotId));

            return (T)await module.ReadAsync(slotId, typeof(T));
        }

        public static async Task<T> ReadAsync<T>(this ICloudSaveModule module, string slotName) where T : class
        {
            if (module == null) throw new ArgumentNullException(nameof(module));
            if (string.IsNullOrEmpty(slotName)) throw new ArgumentException("Value cannot be null or empty.", nameof(slotName));

            return (T)await module.ReadAsync(slotName, typeof(T));
        }
    }
}
