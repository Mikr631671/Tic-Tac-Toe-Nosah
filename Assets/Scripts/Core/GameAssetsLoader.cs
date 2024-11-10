using UnityEngine;
using UnityEngine.UI;

public class GameAssetsLoader : MonoBehaviour
{
    [SerializeField] private AssetBundleManager _assetBundleManager;

    private Sprite xSymbolSprite; 
    private Sprite oSymbolSprite;
    private Sprite backgroundSprite;

    public Sprite XSymbolSprite => xSymbolSprite;
    public Sprite OSymbolSprite => oSymbolSprite;
    public Sprite BackgroundSprite => backgroundSprite;

    private void Start()
    {
        _assetBundleManager.LoadAssetBundle();

        xSymbolSprite = _assetBundleManager.GetSprite("assets/textures/cross.png");
        oSymbolSprite = _assetBundleManager.GetSprite("assets/textures/nought.png");
        backgroundSprite = _assetBundleManager.GetSprite("assets/textures/emptybg.png");
    }

    private void OnDestroy()
    {
        _assetBundleManager.UnloadAssetBundle();
    }
}
