using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleTon<UIManager>
{
    public HealthbarController playerHealthBar;
    // public HealthbarController bossHealthBar;

    private bool isOnStartTitle = false;
    public GameObject startTitle;

    // Start is called before the first frame update
    public void Init()
    {
        playerHealthBar.Init();

        SwitchStartTitle();
    }

    // switch on/off start title
    public void SwitchStartTitle()
    {
        isOnStartTitle = !isOnStartTitle;
        startTitle.SetActive(isOnStartTitle);
    }
}
