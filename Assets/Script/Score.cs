using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{

    // Update is called once per frame
    public void Enlarge()
    {
        transform.localScale *= 1.05f;
    }

    public void Reset()
    {
        transform.localScale = Vector3.one;
        gameObject.SetActive(false);
    }
}
