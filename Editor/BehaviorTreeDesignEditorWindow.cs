using BTDesigner;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviorTreeDesignEditorWindow : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset;

    private BehaviorTreeView treeView;
    private InspectorView inspectorView;

    [MenuItem("BTDesigner/Behavior Tree Designer")]
    public static void OpenWindow()
    {
        BehaviorTreeDesignEditorWindow wnd = GetWindow<BehaviorTreeDesignEditorWindow>();
        wnd.titleContent = new GUIContent("Behavior Tree Designer");
        wnd.minSize = new Vector2(800, 600);
    }

    public static void OpenWindow(BehaviorTreeDesign treeDesign)
    {
        BehaviorTreeDesignEditorWindow wnd = GetWindow<BehaviorTreeDesignEditorWindow>();
        wnd.titleContent = new GUIContent("Behavior Tree Designer");
        wnd.minSize = new Vector2(800, 600);

        wnd.SelectTree(treeDesign);
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        if (Selection.activeObject is BehaviorTreeDesign treeDesign)
        {
            OpenWindow(treeDesign);
            return true;
        }
        return false;
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        m_VisualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Packages/com.mastardy.btdesigner/Editor/Resources/UXML/BehaviorTreeDesignEditorWindow.uxml");
        m_VisualTreeAsset.CloneTree(root);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Packages/com.mastardy.btdesigner/Editor/Resources/Styles/BehaviorTreeDesignEditorWindow.uss");
        root.styleSheets.Add(styleSheet);

        treeView = root.Q<BehaviorTreeView>();
        inspectorView = root.Q<InspectorView>();

        treeView.OnNodeSelected = OnNodeSelectionChanged;
        
        OnSelectionChange();
    }

    private void OnSelectionChange()
    {
        BehaviorTreeDesign tree = Selection.activeObject as BehaviorTreeDesign;
        if (!tree || !AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID())) return;

        treeView.PopulateView(tree);
    }

    private void OnNodeSelectionChanged(NodeView nodeView)
    {
        inspectorView.UpdateSelection(nodeView);
    }
    
    private void SelectTree(BehaviorTreeDesign treeDesign)
    {
        if (!treeDesign) return;
        treeView.PopulateView(treeDesign);
    }
}
