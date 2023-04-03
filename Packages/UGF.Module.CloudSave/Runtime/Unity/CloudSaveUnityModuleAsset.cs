using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UGF.Serialize.Runtime;
using UnityEngine;

namespace UGF.Module.CloudSave.Runtime.Unity
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Cloud Save/Cloud Save Unity Module", order = 2000)]
    public class CloudSaveUnityModuleAsset : ApplicationModuleAsset<CloudSaveUnityModule, CloudSaveUnityModuleDescription>
    {
        [AssetId(typeof(SerializerAsset))]
        [SerializeField] private GlobalId m_serializer;
        [SerializeField] private List<AssetIdReference<CloudSaveSlotDescriptionAsset>> m_slots = new List<AssetIdReference<CloudSaveSlotDescriptionAsset>>();

        public GlobalId Serializer { get { return m_serializer; } set { m_serializer = value; } }
        public List<AssetIdReference<CloudSaveSlotDescriptionAsset>> Slots { get { return m_slots; } }

        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var slots = new Dictionary<GlobalId, CloudSaveSlotDescription>();

            for (int i = 0; i < m_slots.Count; i++)
            {
                AssetIdReference<CloudSaveSlotDescriptionAsset> reference = m_slots[i];

                slots.Add(reference.Guid, reference.Asset.Build());
            }

            return new CloudSaveUnityModuleDescription(
                typeof(ICloudSaveModule),
                slots,
                m_serializer
            );
        }

        protected override CloudSaveUnityModule OnBuild(CloudSaveUnityModuleDescription description, IApplication application)
        {
            return new CloudSaveUnityModule(description, application);
        }
    }
}
