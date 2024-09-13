using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayCollectible : MonoBehaviour
{
    public int gemNumber = 0;
    public Text textComponent;
    void Start()
    {
        UpdateText();
    }
    private void UpdateText(){
        textComponent.text = gemNumber.ToString();
    }
    public void GemCollect(){
        gemNumber++;
        UpdateText();
    }
}