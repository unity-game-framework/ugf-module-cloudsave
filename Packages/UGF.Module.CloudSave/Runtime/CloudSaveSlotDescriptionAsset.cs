using UGF.Description.Runtime;
using UnityEngine;

namespace UGF.Module.CloudSave.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Cloud Save/Cloud Save Slot Description", order = 2000)]
    public class CloudSaveSlotDescriptionAsset : DescriptionBuilderAsset<CloudSaveSlotDescription>
    {
        [SerializeField] private string m_name;

        public string Name { get { return m_name; } set { m_name = value; } }

        protected override CloudSaveSlotDescription OnBuild()
        {
            return new CloudSaveSlotDescription(m_name);
        }
    }
}
