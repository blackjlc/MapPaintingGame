using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Country country;

    private new Renderer renderer;

    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    public void SetMaterial(Material mat)
    {
        renderer.material = mat;
    }
}
