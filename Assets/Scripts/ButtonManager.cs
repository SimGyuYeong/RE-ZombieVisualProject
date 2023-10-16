using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject popupPrefab; // �˾��� �������� �Ҵ��� ����

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
        // �˾��� Ȱ��ȭ
        popupPrefab.SetActive(true);
    }

    public void ClosePopup()
    {
        // �˾��� ��Ȱ��ȭ�ϰų� ����
        popupPrefab.SetActive(false);
    }
}
