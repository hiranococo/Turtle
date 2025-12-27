using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    public CoinController coinController;

    public void ManuallySelect(int type)
    {
        if (!coinController.ableToToss) return;
        coinController.GenerateYao(type);
        coinController.Record(type);
        coinController.yaoIdx++; // move to next yao
    }

}
