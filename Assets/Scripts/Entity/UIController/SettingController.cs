using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    private bool isWASD;

    public InputActionAsset inputActions;

    private InputActionMap player0;
    private InputActionMap player1;

    public GameObject wasdButton;
    public GameObject udlrButton;

    private void Awake()
    {
        UpdateBool();

        player0 = inputActions.FindActionMap("Player0");
        player1 = inputActions.FindActionMap("Player1");

        EnablePlayer0Input();
    }

    public void SwitchPlayerInput()
    {
        ConvertBool();
        if (isWASD) EnablePlayer0Input();
        else EnablePlayer1Input();
    }

    private void EnablePlayer0Input()
    {
        UpdateBool();

        player1.Disable();
        player0.Enable();

        wasdButton.GetComponent<Image>().color = isWASD ? Color.white : Color.grey;
        wasdButton.GetComponent<Button>().interactable = false;
        udlrButton.GetComponent<Image>().color = isWASD ? Color.grey : Color.white;
        udlrButton.GetComponent<Button>().interactable = true;
    }
    private void EnablePlayer1Input()
    {
        UpdateBool();

        player0.Disable();
        player1.Enable();

        wasdButton.GetComponent<Image>().color = isWASD ? Color.grey : Color.white;
        wasdButton.GetComponent<Button>().interactable = true;
        udlrButton.GetComponent<Image>().color = isWASD ? Color.white : Color.grey;
        udlrButton.GetComponent<Button>().interactable = false;
    }
    private void UpdateBool()
    {
        isWASD = GameManager.Instance.IsWASD;
    }
    private void ConvertBool()
    {
        GameManager.Instance.ConvertInput();
        UpdateBool();
    }
}
