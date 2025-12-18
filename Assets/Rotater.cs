using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [Header("旋转设置")]
    [SerializeField] private Vector3 rotationAxis = Vector3.up; // 旋转轴
    [SerializeField] private float speed = 90f;                 // 度/秒
    [SerializeField] private Space space = Space.Self;          // 本地或世界空间

    void Update()
    {
        // 绕指定轴旋转
        transform.Rotate(rotationAxis, speed * Time.deltaTime, space);
    }
}
