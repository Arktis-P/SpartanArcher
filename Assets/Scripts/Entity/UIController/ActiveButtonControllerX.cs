using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveButtonControllerX : ActiveButtonController
{
    protected override void Update()
    {
        if (activeSkill.isFeverReady)  // if dash ready
        {
            activeButtonImage.sprite = readySprite;
            activeButtonImage.rectTransform.sizeDelta = new Vector2(activeButtonImage.rectTransform.sizeDelta.x, 120.0f);
            activeButtonImage.color = Color.white;
            activeText.color = Color.white;
        }
        else
        {
            activeButtonImage.sprite = cooldownSprite;
            activeButtonImage.rectTransform.sizeDelta = new Vector2(activeButtonImage.rectTransform.sizeDelta.x, 112.5f);
            activeButtonImage.color = Color.grey;
            activeText.color = Color.grey;
        }
    }
}
