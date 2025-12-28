using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } private set { } }
    public int money = 0;
    private void Awake() //初始化employeeType/ inventory
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 销毁新创建的重复实例
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    void Update()
    {
        GainMoney();
    }
    
    private float timer = 0f;
    private void GainMoney()
    {
        timer += Time.deltaTime;
        if (timer >= 60f)
        {
            money ++;
            timer = 0f;
        }
    }


}
