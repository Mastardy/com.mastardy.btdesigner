using UnityEngine.UIElements;
using UnityEditor;

namespace BTDesigner
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits> { }

        private Editor editor;
        
        public void UpdateSelection(NodeView nodeView)
        {
            Clear();
            
            UnityEngine.Object.DestroyImmediate(editor);

            editor = Editor.CreateEditor(nodeView.node);
            
            IMGUIContainer container = new IMGUIContainer(() =>
            {
                if(editor.target) editor.OnInspectorGUI();
            });
            
            Add(container);
        }
    }
}