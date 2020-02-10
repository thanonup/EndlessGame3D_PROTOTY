using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public Text Coin;
    public Text CoinEnd;
    public Text Score;
    public Text ScoreEnd;
    public Text Unpausecount;
    public GameObject GUnpause;

    float currentTime = 3f;
    float starttime = 3f;

    private float score1;

    [HideInInspector]
    public bool die = false;
    [HideInInspector]
    public bool unpause;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Updatescore();
        Unpuasecount1();
    }
    //coin count
    public void Updatecoin(int Coins)
    {
        Coin.text = Coins.ToString("0000");
    }
    public void Updatescore()
    {
        if (die)
            return;
        score1 += Time.deltaTime + 0.25f;
        Score.text = ((int)score1).ToString("0000000");
        Endscore(score1);
    }
    public void Unpuasecount1()
    {
        if (unpause)
        {
            GUnpause.SetActive(true);
            

            currentTime -= 1 * Time.deltaTime;
            Unpausecount.text = currentTime.ToString("0");

            if (currentTime <= 1)
            {
                currentTime = 0;
                GUnpause.SetActive(false);
                unpause = false;
                currentTime = starttime;
            }
        }

    }
    public void Endscore(float scoreend)
    {
        ScoreEnd.text = ((int)scoreend).ToString("0000000");
    }
    public void coinend(int Coins)
    {
        CoinEnd.text = Coins.ToString("0000");
    }
}
