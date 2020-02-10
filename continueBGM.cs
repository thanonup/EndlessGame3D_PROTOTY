using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class continueBGM : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] BGMs = GameObject.FindGameObjectsWithTag("Music");
        if(BGMs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

}
