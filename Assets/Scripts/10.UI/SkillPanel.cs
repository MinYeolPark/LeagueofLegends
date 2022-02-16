using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillPanel : MonoBehaviour
{
    public BaseChampController localChamp;
    public LeagueObjectData localChampData;

    public List<AbilityCoolDown> abilityCoolDown;
    public List<AbilityCoolDown> spellCoolDown;

    private void Start()
    {
        if (Photon.Pun.PhotonNetwork.IsConnected)
        {
            if (localChamp == null)
            {
                localChamp = GameManager.Instance.localChamp;
                if(localChamp.gameObject.TryGetComponent<BaseStats>(out BaseStats stats))
                {
                    localChampData = stats.localData;
                }
            }
        }

        //Initialize
        for (int i = 0; i < abilityCoolDown.Count; i++)
        {
            abilityCoolDown[i].abilityData = localChampData.localChampionAbilities[i];
            abilityCoolDown[i].skillButtonImage = abilityCoolDown[i].GetComponent<Image>();
            abilityCoolDown[i].skillButtonImage.sprite = abilityCoolDown[i].abilityData.icon;
            abilityCoolDown[i].coolMask.sprite = abilityCoolDown[i].abilityData.icon;
            abilityCoolDown[i].coolDownDuration = abilityCoolDown[i].abilityData.coolDownTime;

            abilityCoolDown[i].Initialize();
        }
    }

    public void Initialize()
    {
        //for (int i = 0; i < abilityCoolDown.Count; i++)
        //{
        //    abilityCoolDown[i].abilityData = localChampData.localChampionAbilities[i];
        //    abilityCoolDown[i].skillButtonImage = abilityCoolDown[i].GetComponent<Image>();
        //    abilityCoolDown[i].skillButtonImage.sprite = abilityCoolDown[i].abilityData.icon;
        //    abilityCoolDown[i].coolMask.sprite = abilityCoolDown[i].abilityData.icon;
        //    abilityCoolDown[i].coolDownDuration = abilityCoolDown[i].abilityData.coolDownTime;
        //}
    }
}
