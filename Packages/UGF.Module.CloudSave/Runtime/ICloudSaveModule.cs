using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.CloudSave.Runtime
{
    public interface ICloudSaveModule : IApplicationModule
    {
        Task<IReadOnlyList<GlobalId>> GetSlotIdsAsync();
        Task<IReadOnlyList<string>> GetSlotNamesAsync();
        Task<object> ReadAsync(GlobalId slotId, Type dataType);
        Task<object> ReadAsync(string slotName, Type dataType);
        Task WriteAsync(GlobalId slotId, object data);
        Task WriteAsync(string slotName, object data);
        Task DeleteAsync(GlobalId slotId);
        Task DeleteAsync(string slotName);
        CloudSaveSlotDescription GetSlot(GlobalId slotId);
        bool TryGetSlot(GlobalId slotId, out CloudSaveSlotDescription description);
    }
}
