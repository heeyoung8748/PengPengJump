using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingletonBehavior<GameManager>
{
    private int _currentScore = 0;
    private bool _pause = false;

    public UnityEvent<int> OnScoreChanged = new UnityEvent<int>();
    public UnityEvent GamePause = new UnityEvent();
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
    private void Start()
    {
        Time.timeScale = 6;
        GamePause.AddListener(IsPaused);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            _currentScore = 0;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _pause = !_pause;
            GamePause?.Invoke();
        }
    }

    private void IsPaused()
    {
        if(_pause) Time.timeScale = 0;
        else Time.timeScale = 2;
        return;
    }
    public void AddCurrentScore()
    {
        CurrentScore += 1;
    }
}
