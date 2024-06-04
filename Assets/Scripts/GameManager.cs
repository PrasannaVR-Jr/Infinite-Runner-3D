using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ObstaclePool[] PoolingData;
    public GameObject[] Obstacles;
    public static GameManager Instance;
    delegate void OnGameOver();
    event OnGameOver ongameover;
    public ObjectLooper ob;

    public bool canStartRunning
    {
        get;
        private set;
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


        ongameover += () => { StartCoroutine(LoadScene()); };
    }

    Vector3[] Positions = { new Vector3(-0.050048828125f, 0, 0), new Vector3(-0.8f, 0, 0), new Vector3(0.8f, 0, 0) };
    float timer=0;

    
    private void Update()
    {


        if (canStartRunning)
        {
            timer += Time.deltaTime;
            if (timer > 4)
            {
                DequeGameObject(PoolingData[Random.Range(0, 3)].Tag, Positions[Random.Range(0, 3)]);
                timer = 0;
            }
            ob.enabled = true;
        }
        else
        {
            ob.enabled = false;
        }
    }
    

    int GetIndexfromTag(string Tag)
    {
        switch (Tag)
        {
            case "Rocks": return 0;
            case "FlowerBox": return 1;
            case "TrashCan": return 2;
            default: return 0;
        }
    }

    public void EnqueGameObject(string Tag, GameObject gameObject)
    {
        PoolingData[GetIndexfromTag(Tag)].obstacleGameObjects.Enqueue(gameObject);
    }

    public void DequeGameObject(string Tag, Vector3 Pos)
    {
        int index = GetIndexfromTag(Tag);

        if (PoolingData[index].obstacleGameObjects.Count == 0)
        {
            
            Instantiate(Obstacles[index], Pos, Quaternion.identity);
        }
        else
        {
            GameObject gameObject;

            gameObject = PoolingData[index].obstacleGameObjects.Dequeue();
            gameObject.SetActive(true);
            gameObject.transform.position = Pos;
        }
    }

    public void StartGame()
    {
        canStartRunning = true;
    }

    public void GameOver()
    {
        canStartRunning = false;

        ongameover?.Invoke();
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
