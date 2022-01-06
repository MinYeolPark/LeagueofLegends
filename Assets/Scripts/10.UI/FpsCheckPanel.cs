using UnityEngine;
using TMPro;

public class FpsCheckPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI msText;
    [SerializeField] TextMeshProUGUI fpsText;

    float deltaTime = 0.0f;

    private void Start()
    {
        InvokeRepeating("UpdateFPS", 1f,1f);
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
    
    void UpdateFPS()
    {
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;

        msText.text = string.Format("{0:0.0} ms", msec);
        fpsText.text = string.Format("FPS: {0:0.0}", fps);
    }
}
