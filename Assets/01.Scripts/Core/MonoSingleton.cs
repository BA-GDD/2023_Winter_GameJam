using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T>: MonoBehaviour where T : Component
{
    public static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<T>();

                if(instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(T).Name;   
                    instance = go.AddComponent<T>();
                }
            }

            return instance;
        }
    }
}
