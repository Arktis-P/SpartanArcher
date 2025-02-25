using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleTon<UIManager>
{
    public HealthbarController playerHealthBar;
    // public HealthbarController bossHealthBar;

    // Start is called before the first frame update
    public void Init()
    {
        playerHealthBar.Init();
    }
}
