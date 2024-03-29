using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(T)) as T;

                if (_instance == null)
                    _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
            }

            return _instance;
        }
    }

    protected void Awake()
    {
        _instance = this.transform.GetComponent<T>();
    }

    public virtual void Init()
    {
    }

    public virtual void Destroy()
    {
        Destroy(this);
    }
}