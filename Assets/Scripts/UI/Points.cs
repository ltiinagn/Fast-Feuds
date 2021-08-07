using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //stackoverflow
    private Text pointText;

    private void Awake()
    {
        pointText = GetComponent<Text>();
        RegisterEvents();
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }

    private void RegisterEvents()
    {
        // Register the listener to the manager's event
        // PointManager.OnScoreChanged += HandleOnScoreChanged;
    }

    private void UnregisterEvents()
    {
        // Unregister the listener
        // ScoreManager.OnScoreChanged -= HandleOnScoreChanged;
    }

    private void HandleOnPointChanged(int newScore)
    {
        //scoreText.text = newScore.ToString();
    }
}
