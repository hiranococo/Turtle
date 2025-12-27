using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TurtleTool : MonoBehaviour
{

public GameObject toolCanvas;
private Vector3 relativePos;
private bool isHovering = false;

    void Start()
    {
        toolCanvas.SetActive(false);
        relativePos = transform.position - toolCanvas.transform.position;
    }

    void OnMouseEnter()
    {
        isHovering = true;
    }
    void OnMouseExit()
    {
        isHovering = false;
    }

    void Update()
    {
        toolCanvas.transform.position = transform.position - relativePos;
        if (!toolCanvas.activeSelf && isHovering && Input.GetMouseButtonDown(1))
        {
            toolCanvas.SetActive(true);
        }
        else if (toolCanvas.activeSelf && Input.anyKeyDown)
        {
            Invoke("OnDisable", 0.1f);
        }
    }

    void OnDisable()
    {
        toolCanvas.SetActive(false);
    }




}
