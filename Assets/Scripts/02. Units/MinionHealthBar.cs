using UnityEngine;
using UnityEngine.UI;
public class MinionHealthBar : MonoBehaviour
{
    [SerializeField] private Slider minionHpBar;
    [SerializeField] private BaseStats minionStats;

    private void Start()
    {
        minionHpBar = GetComponentInChildren<Slider>();
        minionStats = GetComponentInParent<BaseStats>();

        minionHpBar.maxValue = minionStats.maxHealth;
    }

    private void FixedUpdate()
    {
        minionHpBar.value = minionStats.health;
        gameObject.transform.LookAt(Camera.main.transform);
    }

    public void OnValueChange()
    {
        Debug.Log("On Value CHange");
    }
}
