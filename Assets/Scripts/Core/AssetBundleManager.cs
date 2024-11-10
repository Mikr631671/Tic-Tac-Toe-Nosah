using System.IO;
using UnityEngine;

public class AssetBundleManager : MonoBehaviour
{
    private AssetBundle _loadedBundle;
    private const string BundleName = "tic_tac_toe_assets";

    public void LoadAssetBundle()
    {
        string bundlePath = Path.Combine(Application.streamingAssetsPath, BundleName);
        _loadedBundle = AssetBundle.LoadFromFile(bundlePath);

        if (_loadedBundle == null)
        {
            Debug.LogError("Failed to load Asset Bundle!");
        }
    }

    public Sprite GetSprite(string assetName)
    {
        if (_loadedBundle == null)
        {
            Debug.LogError("Asset Bundle is not loaded.");
            return null;
        }

        return _loadedBundle.LoadAsset<Sprite>(assetName);
    }

    public void UnloadAssetBundle()
    {
        if (_loadedBundle != null)
        {
            _loadedBundle.Unload(false);
            _loadedBundle = null;
        }
    }
}
