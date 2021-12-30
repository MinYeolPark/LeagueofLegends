using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider3D;
    [SerializeField] private BaseStats statsScripts;
    private Camera cameraToLookAt;

    //private bool isHidden = true;

    private void Start()
    {
        cameraToLookAt = Camera.main;

        statsScripts = GetComponentInParent<BaseStats>();
        healthSlider3D.maxValue = statsScripts.maxHealth;
        statsScripts.health = statsScripts.maxHealth;
    }

    private void Update()
    {
        healthSlider3D.value = statsScripts.health;

        Vector3 v = cameraToLookAt.transform.position - transform.position;
        v.x = v.z = 0;
        transform.LookAt(cameraToLookAt.transform.position - v);
    }
}
