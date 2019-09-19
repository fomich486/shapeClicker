using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum State {Game, Setup }
public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField]
    private Transform startTranform;
    [SerializeField]
    private Transform finalTransform;

    public State CurrentState = State.Setup;
    private Spawner spawner;
    [SerializeField]
    private float spawnDelta = 3f;
    private float nextSpawnTime;
    [SerializeField]
    private float deltaIncreseSpeed = 10f;
    private float nextSpeedIncreaseTime;
    private float speed = 0.1f;

    private float score;
    public float Score {
        get => score;
        set
        {
            score = value;
            HUD.Instance.ScoreText.text = score.ToString();
        }
    }
    public UnityEvent GameStartEvent;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        spawner = new Spawner(startTranform, finalTransform);

    }

    private void Update()
    {
        if (CurrentState == State.Game)
        {
             RunGame();
        }
    }

    IEnumerator CountDown()
    {
        HUD.Instance.CountDownText.gameObject.SetActive(true);
        for (int _counter = 3; _counter >= 0; _counter--)
        {
            if(_counter == 0)
                HUD.Instance.CountDown = "START";
            else
                HUD.Instance.CountDown = _counter.ToString();
            yield return new WaitForSeconds(0.75f);
        }
        HUD.Instance.CountDownText.gameObject.SetActive(false);
        HUD.Instance.ScoreText.gameObject.SetActive(true);
        GameStartEvent.Invoke();
        CurrentState = State.Game;
        nextSpawnTime = Time.time;
        nextSpeedIncreaseTime = Time.time + deltaIncreseSpeed;
    }

    public void DestroyObject(GameObject _obj)
    {
        print("Spawned objects befor " + spawnedObjects.Count);
        if (spawnedObjects.Remove(_obj))
        {
            print("Spawned objects after " + spawnedObjects.Count);
        }
    }

    public void DestroyAllSpawnedObjects()
    {
        CurrentState = State.Setup;
        foreach (var obj in spawnedObjects)
        {
            Destroy(obj.gameObject);
        }
    }

    public void StartCountDown()
    {
        StartCoroutine(CountDown());
    }

    private void RunGame()
    {
        if(Time.time>nextSpawnTime)
        {
            spawnedObjects.Add(spawner.GetGameObject(speed));
            nextSpawnTime = Time.time + spawnDelta;
        }

        if (Time.time > nextSpeedIncreaseTime)
        {
            foreach (var g in spawnedObjects)
            {
                speed *= 1.2f;
                g.GetComponent<PlayObject>().Speed = speed;
                nextSpeedIncreaseTime = Time.time + deltaIncreseSpeed;

            }
        }
    }
}
