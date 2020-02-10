using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScnceManger : MonoBehaviour
{
    public GameObject Menubar;
    public GameObject Toru;
    public GameObject Setting;
    public GameObject Pause;
    public GameObject End;
    public GameObject End2;
    public Player player;
    public UImanager uimanager;


    public void LoadScene(string names)
    {
        SceneManager.LoadScene(names);
    }
    //pop up menu
    public void OpenShop()
    {
        if (Menubar != null)
        {
            bool isActive = Menubar.activeSelf;
            Menubar.SetActive(!isActive);
        }

    }
    public void TTU()
    {
        if (Toru != null)
        {
            bool isActive = Toru.activeSelf;
            Toru.SetActive(!isActive);
        }

    }
    public void setting()
    {
        if (Setting != null)
        {
            bool isActive = Setting.activeSelf;
            Setting.SetActive(!isActive);
        }

    }
    public void pause()
    {
        if (Pause != null)
        {
            player.Onpause();
            bool isActive = Pause.activeSelf;
            Pause.SetActive(!isActive);

        }
    }
    public void unpause()
    {
        StartCoroutine(player.Unpause1());
        Pause.SetActive(false);
    }
    public void OnDie()
    {
        End.SetActive(true);
    }
    public void OnDie2()
    {
        End.SetActive(false);
        End2.SetActive(true);
    }
    public void ExitApp()
    {
        Application.Quit();
    }
}
