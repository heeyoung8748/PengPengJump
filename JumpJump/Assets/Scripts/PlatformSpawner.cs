using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour
{
    private Platform[] PlatformPrefab;
    private int _maxPlatformNum = 2;

    public int MaxPlatformCount = 30;

    private Platform[] _platforms;
    private int _nextSpawnPlatformIndex = 0; 

    private readonly Vector3 _poolPosition = new Vector3(20, 20, 20);
    private Vector3 _currentPoolPosition;


    void Start()
    {
        PlatformPrefab = new Platform[_maxPlatformNum];
        for(int i = 0; i < _maxPlatformNum; i++)
        {
            PlatformPrefab[i] = transform.GetChild(i).GetComponent<Platform>();
        }
        _platforms = new Platform[MaxPlatformCount];
        for (int i = 0; i < MaxPlatformCount; ++i)
        {
            int num = Random.Range(0, 2);
            _platforms[i] = Instantiate(PlatformPrefab[num], _poolPosition, Quaternion.identity);
            _platforms[i].gameObject.SetActive(false);
        }
        SpawnPlatform();
    }

    void SpawnPlatform()
    {
        int randomXpos = Random.Range(-2, 4) * 10;
        int randomZpos = Random.Range(4, 7) * 10;
        float randomScale = Random.Range(0.5f, 0.9f);
        Vector3 spawnPosition = new Vector3(_currentPoolPosition.x + randomXpos, -5f, _currentPoolPosition.z + randomZpos);
        Platform currentPlatform = _platforms[_nextSpawnPlatformIndex];
        currentPlatform.transform.position = spawnPosition;
        currentPlatform.transform.localScale = new Vector3(randomScale, randomScale * 1.4f, randomScale);

        currentPlatform.gameObject.SetActive(true);
        // 오브젝트 둥실 떠오르는 효과는 이쯤에 넣기
        

        _currentPoolPosition = spawnPosition;
        currentPlatform.OnEnable();
        currentPlatform.GetComponent<Platform>().IsClamped.AddListener(SpawnPlatform);
        _nextSpawnPlatformIndex = (_nextSpawnPlatformIndex + 1) % MaxPlatformCount;
        Debug.Log($"실행?{_nextSpawnPlatformIndex}");
    }
}