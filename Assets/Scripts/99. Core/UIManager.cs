using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<GameObject> abilities;
    [SerializeField] private List<GameObject> spells;
    [SerializeField] private List<Transform> itemSlots;

    public void Ability1CoolDown()
    {
        Image ability1Image = abilities[1].GetComponent<Image>();
        ability1Image.fillAmount = 1;

        Debug.Log(ability1Image.fillAmount);
    }
}
