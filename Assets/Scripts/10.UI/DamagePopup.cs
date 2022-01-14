using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float disappearTimer;
    public Color textColor;

    public static DamagePopup DamageFloat(Vector3 position, float damageAmount, bool isCriticalHit)
    {
        Vector3 yOffset = new Vector3(0, 3, 0);
        Transform damagePopupTransform = Instantiate(GameAssets.Instance.pfDamagePopup, position+yOffset, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);

        return damagePopup;
    }

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
    }

    public void Setup(float damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {
            //normalhit
            textMesh.fontSize = 0.3f;
        }
        else
        {
            textMesh.fontSize = 1f;
        }
        //textMesh.color = textColor;
        textMesh.geometrySortingOrder++;
        disappearTimer = 1f;
    }
    private void Update()
    {
        float moveYSpeed = 0.5f;
        transform.position += new Vector3(0,moveYSpeed*Time.deltaTime);

        disappearTimer -= Time.deltaTime;

        if (disappearTimer < 0)
        {
            //Start disappearing
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
