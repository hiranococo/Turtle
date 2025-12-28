
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skin : MonoBehaviour
{
    public Texture2D skinTexture;
    public Sprite icon;
    public int price;
    public GameObject turtle;
    private Renderer rd;
    private TextMeshProUGUI money;
    private Image image;
    private bool isUnlocked = false;
    public GameObject upLayer;
    

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponentInChildren<Image>();
        image.sprite = icon;
        
        money = GetComponentInChildren<TextMeshProUGUI>();
        money.text = "$" + price.ToString();

        rd = turtle.GetComponent<Renderer>();
        if (price == 0)
        {
            isUnlocked = true;
            upLayer.SetActive(false);
        }
    }

    public void ApplySkin()
    {
        if (!isUnlocked)
        {
            if(GameManager.Instance.money >= price)
            {
                Debug.Log("1");
                GameManager.Instance.money -= price;
                isUnlocked = true;
                upLayer.SetActive(false);
            }
            else{return;}
        }
        else
        {

            Debug.Log("2");
            rd.material.mainTexture = skinTexture;
        }
        
    }


    public void Large()
    {
        gameObject.transform.localScale *=1.05f;
    }
    public void Small()
    {
        gameObject.transform.localScale = new Vector3(1,1,1);
    }
}
