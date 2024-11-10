using System.IO;
using UnityEngine;

public class PlatformSpecificAssetLoader : MonoBehaviour
{
    private AssetBundle _loadedBundle;

    public void LoadPlatformSpecificAssetBundle(string bundleName)
    {
        string platformFolder = GetPlatformFolder();
        string bundlePath = Path.Combine(Application.streamingAssetsPath, platformFolder, bundleName);

        _loadedBundle = AssetBundle.LoadFromFile(bundlePath);

        if (_loadedBundle == null)
        {
            Debug.LogError($"Failed to load Asset Bundle for platform: {Application.platform}");
        }
        else
        {
            Debug.Log($"Loaded Asset Bundle for platform: {Application.platform}");
        }
    }

    private string GetPlatformFolder()
    {
#if UNITY_STANDALONE_WIN
            return "StandaloneWindows";
#elif UNITY_STANDALONE_OSX
            return "StandaloneOSX";
#elif UNITY_ANDROID
        return "Android";
#elif UNITY_IOS
            return "iOS";
#else
            return "UnknownPlatform";
#endif
    }

    private void OnDestroy()
    {
        if (_loadedBundle != null)
        {
            _loadedBundle.Unload(false);
        }
    }
}
