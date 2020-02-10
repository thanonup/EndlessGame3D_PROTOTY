using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public GameObject[] obstacles;
    public Vector2 numberOfObstacles;

    public GameObject coin;
    public Vector2 numberOfCoins;

    public GameObject trash;
    public Vector2 numberOfTrash;

    public List<GameObject> newObstacles;
    public List<GameObject> newCoins;
    public List<GameObject> newTrash;
    // Start is called before the first frame update
    void Start()
    {
        int newNumberOfObstacles = (int)Random.Range(numberOfObstacles.x, numberOfObstacles.y);
        int newNumberOfCoins = (int)Random.Range(numberOfCoins.x, numberOfCoins.y);
        int newOfTrash = (int)Random.Range(numberOfTrash.x, numberOfTrash.y);

        for (int i = 0; i < newNumberOfObstacles; i++)
        {
            newObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform));
            newObstacles[i].SetActive(false);
            
        }
        for (int i = 0; i < newNumberOfCoins; i++)
        {
            newCoins.Add(Instantiate(coin, transform));
            newCoins[i].SetActive(false);
        }
        for (int i = 0; i < newOfTrash; i++)
        {
            newTrash.Add(Instantiate(trash, transform));
            newTrash[i].SetActive(false);
        }

        PositionateObstacles();
        PositionateTrash();
        PositionateCoins();


    }
    void PositionateObstacles()
    {
        for (int i = 0; i < newObstacles.Count; i++)
        {
            int posZMin = (250 / newObstacles.Count) + (250 / newObstacles.Count) * i;          
            int posZMax = (250 / newObstacles.Count) + (250 / newObstacles.Count) * i + 1;
            int rand = (int)Random.Range(posZMin, posZMax);            
            newObstacles[i].transform.localPosition = new Vector3(0, 0, rand);                       
            newObstacles[i].SetActive(true);
            if (newObstacles[i].GetComponent<ChageLane>() != null)
                newObstacles[i].GetComponent<ChageLane>().PositionLane();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = new Vector3(0, 0, transform.position.z + 250 * 2);
            PositionateObstacles();
            PositionateTrash();
            PositionateCoins();
            other.GetComponent<Player>().IncreaseSpeed();
        }
    }
    void PositionateCoins()
    {
        for (int i = 0; i < newCoins.Count; i++)
        {
            int posZMin = (250 / newCoins.Count) + (250 / newCoins.Count) * i;
            int posZMax = (250 / newCoins.Count) + (250 / newCoins.Count) * i + 1;

            int rand1 = (int)Random.Range(posZMin, posZMax);

            
            newCoins[i].transform.localPosition = new Vector3(transform.position.x, 0.208f, rand1);
            newCoins[i].SetActive(true);
            newCoins[i].GetComponent<ChageLane>().PositionLane();
        }
    }
    void PositionateTrash()
    {
        for (int i = 0; i < newTrash.Count; i++)
        {
            int rand = (int)Random.Range(0f, 250f);

            newTrash[i].transform.localPosition = new Vector3(transform.position.x, 0.095f, rand );
            newTrash[i].SetActive(true);
            newTrash[i].GetComponent<ChageLane>().PositionLane();
        }
    }

}
