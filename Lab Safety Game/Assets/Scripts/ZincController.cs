using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZincController : MonoBehaviour {
    public int total;
    public Text zincCounter;
    public int currentZinc;

    void Start() 
    {
        currentZinc = total;
        zincCounter.text = currentZinc.ToString();
    }

    void OnMouseDown()
    {
        if (currentZinc > 0) 
        {
            currentZinc -= 1;
            zincCounter.text = currentZinc.ToString();

            GameObject piece = (GameObject) Instantiate(Resources.Load("zincPiece"));
        }
    }
}