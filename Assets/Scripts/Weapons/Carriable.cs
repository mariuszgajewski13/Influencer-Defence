using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Carriable : MonoBehaviour
{
    private GameObject displayTextUI;
    private GameObject objectIconUI;
    private GameObject secondaryIconUI;

    [SerializeField] protected int Amount = 1;

    [SerializeField] protected Sprite objectIcon;
    [SerializeField] protected Sprite secondaryIcon;
    protected string displayText;

    protected void FindUI()
    {
        objectIconUI = GameObject.Find("UI/AmmoCounter/ActiveWeapon/WeaponsIcons");
        secondaryIconUI = GameObject.Find("UI/AmmoCounter/ActiveWeapon/AmmoIcons");
        displayTextUI = GameObject.Find("UI/AmmoCounter/ActiveWeapon/AmmoAmount");
    }

    protected void UpdateUI()
    {
        if (objectIcon != null) objectIconUI.GetComponent<Image>().sprite = objectIcon;
        if (secondaryIcon != null) secondaryIconUI.GetComponent<Image>().sprite = secondaryIcon;
        if (displayText != null) displayTextUI.GetComponent<TextMeshProUGUI>().text = displayText;
    }
}
