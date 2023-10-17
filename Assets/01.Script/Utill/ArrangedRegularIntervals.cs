using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangedRegularIntervals : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private Transform gameObject1;
    [SerializeField]
    private Transform gameObject2;

    [SerializeField]
    private int count;

    [ContextMenu("간격맞춰서 놓기")]
    public void SetPos()
    {
        Vector3 _pos = gameObject2.position - gameObject1.position;

        for (int i = 0; i < count; i++)
        {
            GameObject _gameObject = Instantiate(prefab);
            _gameObject.transform.position = gameObject2.position + _pos * (i + 1);
        }
    }
}
