using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

public class Manager : MonoBehaviour
{
    Camera mCam;
    Transform Ground;

    GameObject obstacle;

    List<GameObject> obstacles;

    float spawnStopWatch = 0;
    float spawnTimer = 4;

    void Start()
    {
        obstacles = new List<GameObject>();

        Time.timeScale = 1;
        mCam = Camera.main;
        Ground = mCam.transform.Find("Ground");

        obstacle = Resources.Load<GameObject>("Prefabs/Enemy");
    }

    void Update()
    {
        spawnStopWatch += Time.deltaTime;
        if(spawnStopWatch > spawnTimer)
        {
            spawnStopWatch = 0;
            createObstacle();
        }

        Vector3 dePosition = new Vector3(-10f * Time.deltaTime, 0, 0);

        Ground.position += dePosition;
        if(obstacles.Count > 0)
        {
            foreach (GameObject obstacle in obstacles)
            {
                obstacle.transform.position += dePosition;
            }
        }

        if(Ground.position.x < -4.8f)
        {
            Ground.position += new Vector3((4.8f * 2), 0, 0);
        }
    }

    public void resetScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void createObstacle()
    {
        obstacles.Add(
            Instantiate(obstacle, new Vector3(35, -9.65f, 0), Quaternion.identity, null));

        spawnTimer += ((Random.Range(0, 10) / 10f) - 0.5f);
    }
}