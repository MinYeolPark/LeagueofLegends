using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float disappearTimer;
    private Color textColor;
    [SerializeField] private Transform pfDamagePopup;

    public static DamagePopup Create(Vector3 position, int damageAmount, bool isCriticalHit)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.Instance.pfDamagePopup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);

        return damagePopup;
    }

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());
        if(!isCriticalHit)
        {
            //normalhit
            textMesh.fontSize = 36f;
            //textColor=
        }
        else
        {
            textMesh.fontSize = 42f;
        }
        textMesh.color = textColor;
        textMesh.geometrySortingOrder++;
        disappearTimer = 1f;
    }
    private void Update()
    {
        float moveYSpeed = 20f;
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
