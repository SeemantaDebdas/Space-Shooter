using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private void Awake()
    {
        int numberOfInstances = FindObjectsOfType<MusicManager>().Length;
        if (numberOfInstances > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
