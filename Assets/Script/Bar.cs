using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public GameObject pointer;
    public GameObject turtle;
    public float minPointerSpeed = 2f; // 指针摆动的最小速度
    public float maxPointerSpeed = 5f; // 指针摆动的最大速度
    public float difficulty = 0f; // 难度系数，范围从0到1
    public float maxPointerAngle = 90f; // 指针摆动的最大角度
    // Start is called before the first frame update


    private float currentPhase = 0f;
    public float holdTimer =  0f;
    public float currentPointerAngle = 0f;

    public GameObject greenZone;
    private float randomAngle = 0f;
    public float greenMin, greenMax;

    void Start()
    {
        StartAiming();
        randomAngle = Random.Range(-144f, 0f);
        greenZone.transform.localRotation = Quaternion.Euler(0, 0, randomAngle);
        greenMax = randomAngle + 90f;
        greenMin = randomAngle - 36f + 90f;
    }

    // Update is called once per frame
    void Update()
    {
       ProcessAiming();
    }

    void StartAiming() // reset all
    {
        holdTimer = 0f;
        currentPhase = 0f;
    }

    void ProcessAiming() // in update
    {
        float difficulty = Mathf.Clamp01(holdTimer / 5f); // 5秒钟达到最大难度 difficulty range [0,1]
        float currentSpeed = Mathf.Lerp(minPointerSpeed, maxPointerSpeed, difficulty); // 随着按住时间 difficulty 变大，速度会变快
        currentPhase += Time.deltaTime * currentSpeed; //计算累计的总路程
        currentPointerAngle = Mathf.Sin(currentPhase) * maxPointerAngle; // 计算当前角度
        pointer.transform.localRotation = Quaternion.Euler(0, 0, -currentPointerAngle);
    }
}
