using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject popupPrefab; // 팝업용 프리팹을 할당할 변수

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (popupPrefab.activeSelf)
            {
                ClosePopup();
            }
        }
    }

    public void ShowPopup()
    {
        // 팝업을 활성화
        popupPrefab.SetActive(true);
    }

    public void ClosePopup()
    {
        // 팝업을 비활성화하거나 제거
        popupPrefab.SetActive(false);
    }
}
