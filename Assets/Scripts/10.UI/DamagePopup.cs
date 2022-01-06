using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private Transform pfDamagePopup;

    private void Start()
    {
        Instantiate(pfDamagePopup, Vector3.zero, Quaternion.identity);
    }
}
