using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.CloudSave.Runtime
{
    public class CloudSaveModuleDescription : ApplicationModuleDescription
    {
        public IReadOnlyDictionary<GlobalId, CloudSaveSlotDescription> Slots { get; }

        public CloudSaveModuleDescription(Type registerType, IReadOnlyDictionary<GlobalId, CloudSaveSlotDescription> slots)
        {
            RegisterType = registerType ?? throw new ArgumentNullException(nameof(registerType));
            Slots = slots ?? throw new ArgumentNullException(nameof(slots));
        }
    }
}
