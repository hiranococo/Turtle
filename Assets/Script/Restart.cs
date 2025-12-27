using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Restart : MonoBehaviour
{
    public Transform benGuaGroup;
    public Transform bianGuaGroup;
    public CoinController coinController;
    public DetectGua detectGua;
    public API api;

    public void ResetGame()
    {
        GameObject[] texts = GameObject.FindGameObjectsWithTag("Text");
        foreach (GameObject t in texts)
        {
            t.GetComponent<TextMeshProUGUI>().text = "";
        }
        // destroy all yaos in bengua and biangua
        
        for (int i = benGuaGroup.childCount - 1; i >= 0; i--)
        {
            Destroy(benGuaGroup.GetChild(i).gameObject);
        }

        for (int i = bianGuaGroup.childCount - 1; i >= 0; i--)
        {
            Destroy(bianGuaGroup.GetChild(i).gameObject);
        }
        detectGua.benGua = "";
        detectGua.bianGua = "";
        coinController.dongYao.Clear();
        coinController.yaoIdx = 0;
        coinController.ableToToss = true;
        api.aiContent = "";
    }
}
