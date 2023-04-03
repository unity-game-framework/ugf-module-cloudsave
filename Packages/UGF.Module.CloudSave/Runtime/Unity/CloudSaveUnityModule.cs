using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Module.Serialize.Runtime;
using UGF.Serialize.Runtime;
using Unity.Services.CloudSave;

namespace UGF.Module.CloudSave.Runtime.Unity
{
    public class CloudSaveUnityModule : CloudSaveModule<CloudSaveUnityModuleDescription>
    {
        public ICloudSaveService Service { get { return CloudSaveService.Instance; } }

        protected ISerializeModule SerializeModule { get; }

        public CloudSaveUnityModule(CloudSaveUnityModuleDescription description, IApplication application) : base(description, application)
        {
            SerializeModule = Application.GetModule<ISerializeModule>();
        }

        protected override async Task<IReadOnlyList<string>> OnGetSlotNamesAsync()
        {
            return await Service.Data.RetrieveAllKeysAsync();
        }

        protected override async Task<object> OnReadAsync(string name, Type dataType)
        {
            var serializer = SerializeModule.Provider.Get<ISerializerAsync<string>>(Description.SerializerId);

            Dictionary<string, string> value = await Service.Data.LoadAsync(new HashSet<string> { name });
            object data = await serializer.DeserializeAsync(dataType, value[name], SerializeModule.Context);

            return data;
        }

        protected override async Task OnWriteAsync(string name, object data)
        {
            var serializer = SerializeModule.Provider.Get<ISerializerAsync<string>>(Description.SerializerId);

            string value = await serializer.SerializeAsync(data, SerializeModule.Context);

            await Service.Data.ForceSaveAsync(new Dictionary<string, object>
            {
                { name, value }
            });
        }

        protected override Task OnDeleteAsync(string name)
        {
            return Service.Data.ForceDeleteAsync(name);
        }
    }
}
