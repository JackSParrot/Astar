using UnityEngine;
using System.Collections;
using JackSParrot.Navigation.Pathfinding;
using System.Collections.Generic;

public class PathfinderTest : MonoBehaviour
{
    public GameObject debugPrefab;
    Pathfinder pathfinder ;

    // Use this for initialization
    void Start()
    {
        pathfinder = new Pathfinder(100, 100, true, debugPrefab);
        Invoke("FindPath",1f);
    }

    public void Restart()
    {
        StopAllCoroutines();
        FindPath();
    }

    void FindPath()
    {
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        double max = double.MinValue;
        double min = double.MaxValue;
        double avg = 0;
        for (int i = 0; i < 100; ++i)
        {
            for (int j = 0; j < 100; ++j)
            {
                pathfinder.SetObstacle(i, j, Random.Range(0, 100) > 70);
            }
        }
        pathfinder.SetObstacle(0, 0, false);
        pathfinder.SetObstacle(99, 99, false);
        for (int i = 0; i < 100; ++i)
        {
            pathfinder.Reset();
            sw.Start();
            pathfinder.FindPath(0, 0, 99, 99);
            sw.Stop();
            double elapsed = sw.Elapsed.TotalSeconds;
            sw.Reset();
            if (elapsed < min)
                min = elapsed;
            if (elapsed > max)
                max = elapsed;
            avg += elapsed;
        }
        avg /= 100.0;
        string message = "Max: " + (max * 1000.0).ToString("000.000") + "ms Min: " + (min * 1000.0).ToString("000.000") + "ms Avg: " + (avg * 1000.0).ToString("000.000") + "ms";
        Debug.Log(message);
        _logs.Add(message);
        StartCoroutine(PathfinderCoroutine());
    }

    IEnumerator PathfinderCoroutine()
    {
        pathfinder.Reset();
        pathfinder.ResetObstacles();
        for (int i = 0; i < 100; ++i)
        {
            for (int j = 0; j < 100; ++j)
            {
                pathfinder.SetObstacle(i, j, Random.Range(0, 100) > 70);
            }
        }
        pathfinder.SetObstacle(99, 99, false);
        pathfinder.SetObstacle(0, 0, false);
        pathfinder.Init(0, 0, 99, 99);
        while(pathfinder.Result.Count < 1)
        {
            pathfinder.Tick();
            yield return null;
        }
    }

    List<string> _logs = new List<string>();
    private void OnGUI()
    {
        float width = Screen.width * 0.8f;
        float boxHeight = Screen.height * 0.33f;
        float itemHeight = boxHeight * 0.1f;
        var skin = GUI.skin;
        skin.label.fontSize = 24;
        GUI.skin = skin;
        GUI.Box(new Rect(10f, 10f, width, boxHeight),"");
        for (int i = 0; i < 10 && i < _logs.Count; ++i)
        {
            string currentLog = _logs[_logs.Count - 1 - i];
            float pos = 10f + (itemHeight * (9f - i));
            GUI.Label(new Rect(15f, pos, width - 10f, itemHeight - 5f), currentLog);
        }
    }
}
