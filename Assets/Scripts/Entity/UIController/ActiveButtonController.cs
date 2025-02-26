using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveButtonController : MonoBehaviour
{
    // only shows timer of active skills
    public Image activeButtonImage;
    public ActiveSkill activeSkill;
    public TextMeshProUGUI activeText;

    public Sprite readySprite;
    public Sprite cooldownSprite;

    private void Start()
    {
        // process errors
        if (activeButtonImage == null || activeSkill == null || readySprite == null || cooldownSprite == null)
        { Debug.LogError("Active Button Controller Error!"); return; }
    }

    private void Update()
    {
        if (activeSkill.isDashReady)  // if dash ready
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
