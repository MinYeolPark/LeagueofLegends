using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class AbilityCoolDown : MonoBehaviour
{
    public LeagueAbilityData abilityData;
    private GameObject localChamp;    

    public KeyCode keycode;
    public Image coolMask;
    public TextMeshProUGUI coolDownText;

    public Image skillButtonImage;
    public AudioSource skillSource;
    public float coolDownDuration;

    private float nextReadyTime;
    private float coolDownTimeLeft;

    [SerializeField] private TextMeshProUGUI keyCodeText;
    [SerializeField] private TextMeshProUGUI costText;

    void Start()
    {
        if(localChamp==null)
        {
            if(Photon.Pun.PhotonNetwork.IsConnected)
            {
                localChamp = GameManager.Instance.localChamp.gameObject;
            }
        }
    }

    public void Initialize()
    {  
        StartCoroutine(abilityData.Initialize(localChamp));
        AbilityReady();

        keyCodeText.text = keycode.ToString();
        costText.text = abilityData.cost.ToString();
    }

    private void Update()
    {
        bool coolDownComplete = (Time.time > nextReadyTime);

        if(coolDownComplete)
        {
            AbilityReady();

            if (Input.GetKeyDown(keycode))
            {
                ButtonTriggered();
            }
        }
        else
        {
            CoolDown();
        }
    }

    private void AbilityReady()
    {
        if(abilityData!=null)
        {
            abilityData.SetReady();
            coolDownText.gameObject.SetActive(false);
            coolMask.gameObject.SetActive(false);
        }        
    }

    private void CoolDown()
    {
        abilityData.SetCoolDown();
        coolDownTimeLeft -= Time.deltaTime;

        float roundedCd = coolDownTimeLeft;

        if (roundedCd <= 1f)
        {
            coolDownText.text = roundedCd.ToString("F2");
        }
        else
        {
            roundedCd=Mathf.Round(coolDownTimeLeft);
            coolDownText.text = roundedCd.ToString();
        }
        coolMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
    }

    public void ButtonTriggered()
    {
        abilityData.SetActivate();
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;

        coolMask.gameObject.SetActive(true);
        coolDownText.gameObject.SetActive(true);

        //skillSource.clip = ability.skillSounds;
        //skillSource.Play();
        StartCoroutine(abilityData.TriggerAbility(localChamp));
    }
}
