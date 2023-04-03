using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Logs.Runtime;

namespace UGF.Module.CloudSave.Runtime
{
    public abstract class CloudSaveModule<TDescription> : ApplicationModule<TDescription>, ICloudSaveModule where TDescription : CloudSaveModuleDescription
    {
        protected CloudSaveModule(TDescription description, IApplication application) : base(description, application)
        {
        }

        public async Task<IReadOnlyList<GlobalId>> GetSlotIdsAsync()
        {
            IReadOnlyList<string> names = await GetSlotNamesAsync();
            var ids = new List<GlobalId>(names.Count);

            for (int i = 0; i < names.Count; i++)
            {
                string name = names[i];

                if (TryGetSlotByName(name, out GlobalId id, out _))
                {
                    ids.Add(id);
                }
            }

            return ids;
        }

        public Task<IReadOnlyList<string>> GetSlotNamesAsync()
        {
            return OnGetSlotNamesAsync();
        }

        public Task<object> ReadAsync(GlobalId slotId, Type dataType)
        {
            CloudSaveSlotDescription description = GetSlot(slotId);

            return ReadAsync(description.Name, dataType);
        }

        public Task<object> ReadAsync(string slotName, Type dataType)
        {
            Log.Debug("Cloud Save reading", new
            {
                slotName
            });

            return OnReadAsync(slotName, dataType);
        }

        public Task WriteAsync(GlobalId slotId, object data)
        {
            CloudSaveSlotDescription description = GetSlot(slotId);

            return WriteAsync(description.Name, data);
        }

        public Task WriteAsync(string slotName, object data)
        {
            Log.Debug("Cloud Save writing", new
            {
                slotName
            });

            return OnWriteAsync(slotName, data);
        }

        public Task DeleteAsync(GlobalId slotId)
        {
            CloudSaveSlotDescription description = GetSlot(slotId);

            return DeleteAsync(description.Name);
        }

        public Task DeleteAsync(string slotName)
        {
            if (string.IsNullOrEmpty(slotName)) throw new ArgumentException("Value cannot be null or empty.", nameof(slotName));

            Log.Debug("Cloud Save deleting", new
            {
                slotName
            });

            return OnDeleteAsync(slotName);
        }

        public CloudSaveSlotDescription GetSlot(GlobalId slotId)
        {
            return TryGetSlot(slotId, out CloudSaveSlotDescription description) ? description : throw new ArgumentException($"Cloud Save slot not found by the specified id: '{slotId}'.");
        }

        public bool TryGetSlot(GlobalId slotId, out CloudSaveSlotDescription description)
        {
            if (!slotId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(slotId));

            return Description.Slots.TryGetValue(slotId, out description);
        }

        public bool TryGetSlotByName(string name, out GlobalId slotId, out CloudSaveSlotDescription description)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            foreach ((GlobalId id, CloudSaveSlotDescription slot) in Description.Slots)
            {
                if (slot.Name == name)
                {
                    slotId = id;
                    description = slot;
                    return true;
                }
            }

            slotId = default;
            description = default;
            return false;
        }

        protected abstract Task<IReadOnlyList<string>> OnGetSlotNamesAsync();
        protected abstract Task<object> OnReadAsync(string name, Type dataType);
        protected abstract Task OnWriteAsync(string name, object data);
        protected abstract Task OnDeleteAsync(string name);
    }
}
