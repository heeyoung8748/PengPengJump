using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingletonBehavior<GameManager>
{
    public UnityEvent<int> OnScoreChanged = new UnityEvent<int>();
    public UnityEvent<int> OnHighScoreChanged = new UnityEvent<int>();
    public UnityEvent OnGameEnd = new UnityEvent();
    public UnityEvent GamePause = new UnityEvent();
    public UnityEvent<int> IsCombo = new UnityEvent<int>();

    public bool WasItComboBefore;
    public bool IsOver;
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

    private bool _pause = false;
    private int _currentScore = 0;

    private void Start()
    {

        Time.timeScale = 6;
        WasItComboBefore = false;
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
        ComboCounts = 0;
        ComboScore = 0;
        IsCombo.Invoke(ComboCounts);
        CurrentScore += 1;
    }

    public int ComboCounts;
    public int ComboScore;
    public void ComboIncrease()
    {

        if(WasItComboBefore == false)
        {
            ComboCounts = 0;
            ComboScore = 0;
        }
        ComboScore += 2;
        ComboCounts++;
        CurrentScore += ComboScore;
        WasItComboBefore = true;
        Debug.Log($"{ComboCounts}�޺�");
        IsCombo.Invoke(ComboCounts);
    }


    public GameObject GameOverUI;
    public void End()
    {
        OnGameEnd.Invoke();
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        int highScore = Mathf.Max(_currentScore, savedHighScore);
        PlayerPrefs.SetInt("HighScore", highScore);
        OnScoreChanged.Invoke(_currentScore);
        OnHighScoreChanged.Invoke(highScore);
        IsOver = true;
    }
}
