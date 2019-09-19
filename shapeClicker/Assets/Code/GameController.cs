using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum State {Game, Setup }
public class GameController : MonoBehaviour
{
    public State CurrentState = State.Setup;
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
    }

    public void DestroyAllSpawnedObjects()
    {
        foreach (var obj in spawnedObjects)
        {
            Destroy(obj.gameObject);
        }
    }

    public void StartCountDown()
    {
        StartCoroutine(CountDown());
    }
}
