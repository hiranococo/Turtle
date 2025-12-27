using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yao : MonoBehaviour
{
    public Sprite[] YaoSprites;

    public void InitBenGua(int yaoType)
    {
        GetComponent<Image>().sprite = YaoSprites[yaoType];
    }

    public void InitBianGua(int yaoType)
    {
        if (yaoType == 0) // 阴
        {
            yaoType = 2;
        }
        GetComponent<Image>().sprite = YaoSprites[yaoType];
    }
}
