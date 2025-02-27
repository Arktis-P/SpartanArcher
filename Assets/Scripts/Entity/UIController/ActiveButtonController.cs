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

    protected virtual void Update()
    {
        
    }
}
