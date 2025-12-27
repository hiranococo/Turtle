using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "$" + GameManager.Instance.money.ToString();
    }
}
