using UnityEditor;
using UnityEngine;
using System.IO;

public class GameAssetsEditorWindow : EditorWindow
{
    private Sprite XSymbol;
    private Sprite OSymbol;
    private Sprite Background;

    private string _assetBundleName = "tic_tac_toe_assets";

    [MenuItem("Tic-Tac-Toe/Game Assets Editor")]
    public static void ShowWindow()
    {
        GetWindow<GameAssetsEditorWindow>("Game Assets Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Tic-Tac-Toe Assets Configuration", EditorStyles.boldLabel);

        XSymbol = (Sprite)EditorGUILayout.ObjectField("XSymbol", XSymbol, typeof(Sprite), false);
        OSymbol = (Sprite)EditorGUILayout.ObjectField("OSymbol", OSymbol, typeof(Sprite), false);
        Background = (Sprite)EditorGUILayout.ObjectField("Background", Background, typeof(Sprite), false);

        _assetBundleName = EditorGUILayout.TextField("Asset Bundle Name", _assetBundleName);

        if (GUILayout.Button("Build Asset Bundle"))
        {
            if (XSymbol != null && OSymbol != null && Background != null)
            {
                BuildAssetBundle();
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Please assign a GameAssetsConfig asset.", "OK");
            }
        }
    }

    private void BuildAssetBundle()
    {
        string assetBundleDirectory = "Assets/StreamingAssets";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        string[] assetPaths = new string[]
        {
            AssetDatabase.GetAssetPath(XSymbol),
            AssetDatabase.GetAssetPath(OSymbol),
            AssetDatabase.GetAssetPath(Background)

        };

        AssetBundleBuild build = new AssetBundleBuild
        {
            assetBundleName = _assetBundleName,
            assetNames = assetPaths
        };

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, new[] { build }, BuildAssetBundleOptions.None, BuildTarget.Android);
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Success", "Asset Bundle has been built!", "OK");
    }
}
