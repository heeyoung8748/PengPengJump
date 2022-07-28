using UnityEngine;

// ������ �����ϰ� �ֱ������� ���ġ�ϴ� ��ũ��Ʈ
public class PlatformSpawner : MonoBehaviour
{
    public Platform PlatformPrefab;
    public int MaxPlatformCount = 30;

    private Platform[] _platforms;
    private int _nextSpawnPlatformIndex = 0; 

    private readonly Vector3 _poolPosition = new Vector3(20, 20, 20);
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
        int randomXpos = Random.Range(-2, 4);
        int randomZpos = Random.Range(4, 7);
        float randomScale = Random.Range(0.3f, 1f);
        Vector3 spawnPosition = new Vector3(_currentPoolPosition.x + randomXpos, 0f, _currentPoolPosition.z + randomZpos);
        Platform currentPlatform = _platforms[_nextSpawnPlatformIndex];
        currentPlatform.transform.position = spawnPosition;
        currentPlatform.transform.localScale = new Vector3(randomScale, randomScale * 1.4f, randomScale);

        // ������Ʈ �ս� �������� ȿ���� ���뿡 �ֱ�

        _currentPoolPosition = spawnPosition;
        currentPlatform.gameObject.SetActive(true);
        currentPlatform.OnEnable();
        currentPlatform.GetComponent<Platform>().IsClamped.AddListener(SpawnPlatform);
        _nextSpawnPlatformIndex = (_nextSpawnPlatformIndex + 1) % MaxPlatformCount;
        Debug.Log($"����?{_nextSpawnPlatformIndex}");
    }
}