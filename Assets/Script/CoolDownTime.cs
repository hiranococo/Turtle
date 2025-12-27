using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoolDownTime : MonoBehaviour
{

    public TextMeshProUGUI text;
    public float cooldownTime = 600f; // 600 seconds = 10 minutes
    private float currentTimer = 0f;
    public bool isCooling = false;
    public GameObject upLayer;



    // Start is called before the first frame update
    void Start()
    {
        currentTimer = 0f;
        isCooling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooling)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0f)
            {
                isCooling = false;
                upLayer.SetActive(false);
            }
            else // update text
            {
                int minutes = Mathf.FloorToInt(currentTimer / 60f);
                int seconds = Mathf.FloorToInt(currentTimer % 60f);
                text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
        
    }

    public void ResetCooldown()
    {
        currentTimer = cooldownTime;
        isCooling = true;
        upLayer.SetActive(true);
    }
}
