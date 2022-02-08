using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CammeraSettings : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();

        if(GameManager.Instance.localChamp!=null)
        {
            cam.Follow = GameManager.Instance.localChamp.transform;
            cam.LookAt = GameManager.Instance.localChamp.transform;
        }        
    }
}
