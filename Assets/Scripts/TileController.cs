using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class TileController : MonoBehaviour
{
    public Country.Name country;
    public int DevLevel
    {
        get => devLevel;
        set
        {
            Assert.IsTrue(value > (-1) && value < 4, "Dev level should be in the range of 0 and 3");
            devLevel = value;
        }
    }
    [SerializeField]
    private int devLevel;
    private Troop[] troops;

    private TextMeshPro text;
    private SpriteRenderer devUI;
    private new Renderer renderer;


    // Start is called before the first frame update
    void Awake()
    {
        troops = new Troop[Country.Count];
        for (int i = 0; i < troops.Length; i++)
        {
            troops[i] = new Troop((Country.Name)i, 0);
        }
        renderer = GetComponent<Renderer>();
        text = transform.GetChild(0).GetComponent<TextMeshPro>();
        devUI = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    public void Initialize()
    {
        renderer.material = ResourceManager.Instance.TileMaterials[(int)country];
        if (devLevel > 0)
            devUI.sprite = ResourceManager.Instance.devLevelSprites[devLevel - 1];
    }

    public void AddTroops(int count, Country.Name country)
    {
        int countryIdx = (int)country;
        troops[countryIdx].count += count;
        text.color = ColorHelper.Colors[countryIdx];
        text.text = $"{troops[countryIdx].count:0.#}k";
    }

    public void RemoveTroops(int count, Country.Name country)
    {
        int countryIdx = (int)country;
        troops[countryIdx].count -= count;
        Assert.IsTrue(troops[countryIdx].count > 0, "troop count shouldnt be less than zero!");

        if (count == 0)
        {
            text.text = "";
        }
    }

    public void SetBattle(int count1, int count2, string hexColor1, string hexColor2)
    {
        text.color = Color.white;
        text.text = $"<color={hexColor1}>{count1} VS <color={hexColor2}>{count2}";
    }

    public void SetCountry(Country.Name country, Material mat)
    {
        this.country = country;
        renderer.material = mat;
    }
}
