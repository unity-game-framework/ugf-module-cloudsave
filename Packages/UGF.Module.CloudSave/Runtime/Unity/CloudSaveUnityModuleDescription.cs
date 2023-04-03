using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.CloudSave.Runtime.Unity
{
    public class CloudSaveUnityModuleDescription : CloudSaveModuleDescription
    {
        public GlobalId SerializerId { get; }

        public CloudSaveUnityModuleDescription(
            Type registerType,
            IReadOnlyDictionary<GlobalId, CloudSaveSlotDescription> slots,
            GlobalId serializerId) : base(registerType, slots)
        {
            if (!serializerId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(serializerId));

            SerializerId = serializerId;
        }
    }
}
