using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    public Coin[] coin;
    public Sprite[] CoinSprites; // 0 = 阴， 1 = 阳
    private int result; // 0 = 老阴， 1 = 少阳， 2 = 少阴， 3 = 老阳
    public GameObject yaoPrefab;
    public Transform benGuaGroup;
    public Transform bianGuaGroup;
    [HideInInspector] public int [] benGua = new int[6]; //记录本卦, 0 = 阴， 1 = 阳
    [HideInInspector] public int[] bianGua = new int[6]; //记录变卦， 0 = 阴， 1 = 阳
    [HideInInspector] public bool showBianGua = false;
    [HideInInspector] public int yaoIdx = 0;
    public bool ableToToss = true;
    public DetectGua detectGua;
    public List<int> dongYao = new List<int>();
    public GameObject selectPanel;

    public void Toss()
    {
        if (!ableToToss) return;
        if (GameManager.Instance.money < 3) return;
        GameManager.Instance.money -= 3;
        result = 0; // reset result
        foreach (Coin c in coin)
        {
            c.type = Random.Range(0, 2);
            c.GetComponent<Image>().sprite = CoinSprites[c.type];
            result += c.type;
        }
        GenerateYao(result);
        Record(result);
        yaoIdx ++; // move to next yao
    }

    void Update()
    {

        if(yaoIdx >= 6) // 扔完六次硬币
        {
            ableToToss = false;
            selectPanel.SetActive(false);
            if (showBianGua)
            {
                GenerateBianGua();
            }
            yaoIdx = 0; // reset yao index
            detectGua.DisplayGua();
            showBianGua = false; // reset bool
        }
    }

    public void GenerateBianGua()
    {
        for(int i = 0; i < 6; i++)
        {
            GameObject clone = Instantiate(yaoPrefab, bianGuaGroup);
            clone.GetComponent<Yao>().InitBianGua(bianGua[i]);
        }
    }

    public void GenerateYao(int result)
    {
        GameObject clone = Instantiate(yaoPrefab, benGuaGroup);
        clone.GetComponent<Yao>().InitBenGua(result);
    }

    public void Record(int result) // 只记录阴阳 0,1
    {
        if (result == 0 || result ==3) // 老阴或老阳
        {
            dongYao.Add(yaoIdx + 1);
        }
        benGua[yaoIdx] = result; //记录本卦
        if (result == 0) // 老阴
        {
            benGua[yaoIdx] = 0;
            bianGua[yaoIdx] = 1;
            showBianGua = true;
        }
        else if (result == 1) // 少阳
        {
            benGua[yaoIdx] = 1;
            bianGua[yaoIdx] = 1;
        }
        else if (result == 2) // 少阴
        {
            benGua[yaoIdx] = 0;
            bianGua[yaoIdx] = 0;
        }
        else if (result == 3) // 老阳
        {
            benGua[yaoIdx] = 1;
            bianGua[yaoIdx] = 0;
            showBianGua = true;
        }
    }
}
