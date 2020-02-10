using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashHP : MonoBehaviour
{
    public int Trash;
    public int numOfTrash;

    public Image[] trash;
    public Sprite fullTrash;
    public Sprite emptyTrash;

    void Start()
    {
        
    }


    void Update()
    {
        if(Trash > numOfTrash)
        {
            Trash = numOfTrash;
        }
        for(int i = 0; i < trash.Length; i++)
        {
            if(i< Trash)
            {
                trash[i].sprite = fullTrash;
            }
            else
            {
                trash[i].sprite = emptyTrash;
            }
            if(i < numOfTrash)
            {
                trash[i].enabled = true;
            }
            else
            {
                trash[i].enabled = false;
            }
        }
    }
}
