using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatusBoard : MonoBehaviour
{
    public GameObject statusBoard;

    public void OnBoardPopup()
    {
        if(statusBoard.activeSelf==false)
        {
            statusBoard.SetActive(true);
        }
        else
        {
            statusBoard.SetActive(false);
        }
    }
}
