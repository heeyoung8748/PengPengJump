using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour
{
    public Platform PlatformPrefab;
    public int MaxPlatformCount = 10;

    private Platform[] _platforms;
    private int _nextSpawnPlatformIndex = 0; 

    private readonly Vector3 _poolPosition = new Vector3(0, -20, 0);
    private Vector3 _currentPoolPosition;


    void Start()
    {
        _platforms = new Platform[MaxPlatformCount];
        for (int i = 0; i < MaxPlatformCount; ++i)
        {
            _platforms[i] = Instantiate(PlatformPrefab, _poolPosition, Quaternion.identity);
            _platforms[i].gameObject.SetActive(false);
        }
        SpawnPlatform();
    }

    void SpawnPlatform()
    {
        int randomXpos = Random.Range(-5, 4);
        int randomZpos = Random.Range(4, 7);
        Vector3 spawnPosition = new Vector3(_currentPoolPosition.x + randomXpos,_currentPoolPosition.y, _currentPoolPosition.z + randomZpos);
        _currentPoolPosition = spawnPosition;
        Platform currentPlatform = _platforms[_nextSpawnPlatformIndex];
        currentPlatform.transform.position = spawnPosition;
        currentPlatform.gameObject.SetActive(true);
        currentPlatform.OnEnable();
        currentPlatform.GetComponent<Platform>().IsClamped.AddListener(SpawnPlatform);
        _nextSpawnPlatformIndex = (_nextSpawnPlatformIndex + 1) % MaxPlatformCount;
        Debug.Log($"실행?{_nextSpawnPlatformIndex}");
    }
}