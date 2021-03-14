using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using System;

[Serializable]
public class AssetReferenceMaterial : AssetReferenceT<Material>
{
    public AssetReferenceMaterial(string guid) : base(guid) { }
}

public class ResourceManager : Singleton<ResourceManager>
{
    [Header("Asset References")]
    public AssetReferenceSprite[] devLevelSpriteRefs;
    public AssetReferenceMaterial[] TileMaterialRefs;

    [Header("Assets")]
    public Sprite[] devLevelSprites;
    public Material[] TileMaterials;


    private void Awake()
    {
        devLevelSprites = new Sprite[devLevelSpriteRefs.Length];
        TileMaterials = new Material[TileMaterialRefs.Length];
    }

    public async UniTask LoadAll()
    {
        for (int i = 0; i < devLevelSpriteRefs.Length; i++)
        {
            devLevelSprites[i] = await devLevelSpriteRefs[i].LoadAssetAsync<Sprite>();
        }
        for (int i = 0; i < TileMaterialRefs.Length; i++)
        {
            TileMaterials[i] = await TileMaterialRefs[i].LoadAssetAsync<Material>();
        }
    }

    private void UnloadAll()
    {
        for (int i = 0; i < TileMaterialRefs.Length; i++)
        {
            TileMaterialRefs[i].ReleaseAsset();
        }
    }

    private void OnDestroy()
    {
        UnloadAll();
    }
}
