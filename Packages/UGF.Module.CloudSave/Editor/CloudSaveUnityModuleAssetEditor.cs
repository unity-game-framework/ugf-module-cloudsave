using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.CloudSave.Runtime.Unity;
using UnityEditor;

namespace UGF.Module.CloudSave.Editor
{
    [CustomEditor(typeof(CloudSaveUnityModuleAsset))]
    internal class CloudSaveUnityModuleAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertySerializer;
        private AssetIdReferenceListDrawer m_listSlots;
        private ReorderableListSelectionDrawerByPath m_listSlotsSelection;

        private void OnEnable()
        {
            m_propertySerializer = serializedObject.FindProperty("m_serializer");
            m_listSlots = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_slots"));

            m_listSlotsSelection = new ReorderableListSelectionDrawerByPath(m_listSlots, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listSlots.Enable();
            m_listSlotsSelection.Enable();
        }

        private void OnDisable()
        {
            m_listSlots.Disable();
            m_listSlotsSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertySerializer);

                m_listSlots.DrawGUILayout();
                m_listSlotsSelection.DrawGUILayout();
            }
        }
    }
}
