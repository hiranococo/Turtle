using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetectGua : MonoBehaviour
{
    public CoinController coinController;
    public TextMeshProUGUI benGuaText;
    public TextMeshProUGUI bianGuaText;
    public string benGua, bianGua;

    public Dictionary<Vector3, int> guaDict = new Dictionary<Vector3, int>()
    {
        // 乾一兑二离三震四巽五坎六艮七坤八
        {new Vector3(0,0,0), 8},
        {new Vector3(0,0,1), 7},
        {new Vector3(0,1,0), 6},
        {new Vector3(0,1,1), 5},
        {new Vector3(1,0,0), 4},
        {new Vector3(1,0,1), 3},
        {new Vector3(1,1,0), 2},
        {new Vector3(1,1,1), 1}
    };

    public List<List<string>> guaInfo = new List<List<string>>()
    {
        new List<string>(){"乾为天","天泽履","天火同人","天雷无妄","天风姤","天水讼","天山遁","天地否"},
        new List<string>(){"泽天夬","兑为泽","泽火革","泽雷随","泽风大过","泽水困","泽山咸","泽地萃"},
        new List<string>(){"火天大有","火泽睽","离为火","火雷噬嗑","火风鼎","火水未济","火山旅","火地晋"},
        new List<string>(){"雷天大壮","雷泽归妹","雷火丰","震为雷","雷风恒","雷水解","雷山小过","雷地豫"},
        new List<string>(){"风天小畜","风泽中孚","风火家人","风雷益","巽为风","风水涣","风山渐","风地观"},
        new List<string>(){"水天需","水泽节","水火既济","水雷屯","水风井","坎为水","水山蹇","水地比"},
        new List<string>(){"山天大畜","山泽损","山火贲","山雷颐","山风蛊","山水蒙","艮为山","山地剥"},
        new List<string>(){"地天泰","地泽临","地火明夷","地雷复","地风升","地水师","地山谦","坤为地"}
    };

    public void DisplayGua()
    {
        int down1 = guaDict[new Vector3 (coinController.benGua[0], coinController.benGua[1], coinController.benGua[2])] - 1; //下卦
        int up1 = guaDict[new Vector3 (coinController.benGua[3], coinController.benGua[4], coinController.benGua[5])] - 1; //上卦
        benGua = guaInfo[up1][down1];
        benGuaText.text = benGua;
        if (coinController.showBianGua)
        {
            int down2 = guaDict[new Vector3 (coinController.bianGua[0], coinController.bianGua[1], coinController.bianGua[2])] - 1; //下卦
            int up2 = guaDict[new Vector3 (coinController.bianGua[3], coinController.bianGua[4], coinController.bianGua[5])] - 1; //上卦
            bianGua = guaInfo[up2][down2];
            bianGuaText.text = bianGua;
        }
    }
}
