using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingletonBehavior<GameManager>
{
    private int _currentScore = 0;

    public UnityEvent<int> OnScoreChanged = new UnityEvent<int>();
    public int CurrentScore
    {
        get
        {
            return _currentScore;
        }
        set
        {
            _currentScore = value;
            OnScoreChanged.Invoke(_currentScore);
        }
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            _currentScore = 0;
        }
    }

    public void AddCurrentScore()
    {
        CurrentScore += 1;
    }
}
