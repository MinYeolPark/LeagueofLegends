using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class AbilityCoolDown : MonoBehaviour
{
    public KeyCode keycode;
    public Image coolMask;
    public TextMeshProUGUI coolDownText;

    [SerializeField] private LeagueAbilityData ability;
    [SerializeField] private GameObject localChamp;

    private Image skillButtonImage;
    private AudioSource skillSource;
    private float coolDownDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;

    void Start()
    {
        Initialize(ability, localChamp);        
    }

    public void Initialize(LeagueAbilityData selectedAbility, GameObject selectedChamp)
    {
        ability = selectedAbility;
        skillButtonImage = GetComponent<Image>();
        //skillSource = GetComponent<AudioSource>();
        skillButtonImage.sprite = ability.icon;
        coolMask.sprite = ability.icon; 
        coolDownDuration = ability.coolDownTime;

        StartCoroutine(ability.Initialize(selectedChamp));
        AbilityReady();
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
        ability.SetReady();
        coolDownText.gameObject.SetActive(false);
        coolMask.gameObject.SetActive(false);
    }

    private void CoolDown()
    {
        ability.SetCoolDown();
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

    private void ButtonTriggered()
    {
        ability.SetActivate();
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;

        coolMask.gameObject.SetActive(true);
        coolDownText.gameObject.SetActive(true);

        //skillSource.clip = ability.skillSounds;
        //skillSource.Play();
        StartCoroutine(ability.TriggerAbility(localChamp));
    }
}
