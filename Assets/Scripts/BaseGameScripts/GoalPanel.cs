using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoalPanel : MonoBehaviour
{
    public Image thisImage;
    public Sprite thisSprite;
    public TextMeshProUGUI thisText;
    public string thisString;

    void Start()
    {
        SetUp();
    }

    void SetUp()
    {
        thisImage.sprite = thisSprite;
        thisText.text = thisString;
    }
}
