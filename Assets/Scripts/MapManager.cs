using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.Assertions;

public enum Country { None, Red, Green, Blue };

public class MapManager : Singleton<MapManager>
{
    public Vector2Int size = new Vector2Int(10, 10);
    private TileController[,] tiles;

    [Header("Reference")]
    [AssetReferenceUILabelRestriction("TileMats")]
    public Transform MapParent;
    public AssetReference[] TileMaterials;

    // Start is called before the first frame update
    void Start()
    {
        InitializeMap().Forget();
    }

    private async UniTaskVoid InitializeMap()
    {
        // Load tile materials
        Material[] mats = new Material[TileMaterials.Length];
        for (int i = 0; i < TileMaterials.Length; i++)
        {
            mats[i] = await LoadAsset<Material>(TileMaterials[i]);
        }

        // Load tiles
        int tileCount = 0;
        int childCount = MapParent.childCount;
        tiles = new TileController[size.x, size.y];
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                tiles[i, j] = MapParent.GetChild(tileCount).GetComponent<TileController>();
                switch (tiles[i, j].country)
                {
                    case Country.Red:
                        tiles[i, j].SetMaterial(mats[6]);
                        break;
                    case Country.Green:
                        tiles[i, j].SetMaterial(mats[3]);
                        break;
                    case Country.Blue:
                        tiles[i, j].SetMaterial(mats[0]);
                        break;
                    default:
                        tiles[i, j].SetMaterial(mats[2]);
                        break;
                }

                tileCount++;
                // print(i + ", " + j);
                Assert.IsFalse(tileCount > childCount, "There shouldn't be more tile!");
            }
        }
    }

    private async UniTask<T> LoadAsset<T>(AssetReference assetRef)
    {
        var handle = assetRef.LoadAssetAsync<T>();
        await UniTask.WaitUntil(() => { return handle.IsDone; });
        return handle.Result;
    }

    private async UniTask<T> InstantiateComponent<T>(AssetReference assetRef)
    {
        var handle = assetRef.InstantiateAsync();
        await UniTask.WaitUntil(() => { return handle.IsDone; });
        return handle.Result.GetComponent<T>();
    }

    private void UnloadMaterials()
    {
        for (int i = 0; i < TileMaterials.Length; i++)
        {
            TileMaterials[i].ReleaseAsset();
        }
    }
}
