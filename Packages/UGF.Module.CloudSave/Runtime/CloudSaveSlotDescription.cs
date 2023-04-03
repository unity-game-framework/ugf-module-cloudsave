using System;
using UGF.Description.Runtime;

namespace UGF.Module.CloudSave.Runtime
{
    public class CloudSaveSlotDescription : DescriptionBase
    {
        public string Name { get; }

        public CloudSaveSlotDescription(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            Name = name;
        }
    }
}
