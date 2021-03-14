using UnityEngine;
using Cysharp.Threading.Tasks;

public class MapManager : Singleton<MapManager>
{
    public Vector2Int size = new Vector2Int(10, 10);
    [SerializeField]
    public TileController[] tiles;

    [Header("Reference")]
    public Transform MapParent;

    // Start is called before the first frame update
    void Start()
    {
        InitializeMap().Forget();
    }

    private async UniTaskVoid InitializeMap()
    {
        await ResourceManager.Instance.LoadAll();

        // Load tiles
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].Initialize();
            tiles[i].AddTroops(1, tiles[i].country);
            // print(i + ", " + j);
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].country == Country.Name.None)
                continue;
            Color color = ColorHelper.Colors[(int)tiles[i].country];
            color.a = 0.5f;
            Gizmos.color = color;
            Gizmos.DrawCube(tiles[i].transform.position, Vector3.one);
        }
    }
}
