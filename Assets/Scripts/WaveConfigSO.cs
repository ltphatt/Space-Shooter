using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObject để lưu trữ thông tin về cấu hình của một loạt Wave
[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] Transform pathPrefab;  // Prefab đường đi cho các kẻ địch
    [SerializeField] float moveSpeed = 5f;  // Tốc độ di chuyển của kẻ địch
    [SerializeField] List<GameObject> enemyPrefabs;  // Danh sách các Prefab của kẻ địch
    [SerializeField] float timeBetweenEnemySpawns = 1f;  // Thời gian giữa các lần spawn kẻ địch
    [SerializeField] float spawnTimeVariance = 0f;  // Biến thể thời gian spawn
    [SerializeField] float minimumSpawnTime = 0.2f;  // Thời gian spawn tối thiểu

    // Trả về số lượng kẻ địch trong danh sách enemyPrefabs
    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    // Trả về prefab của kẻ địch tại chỉ mục đã cho
    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    // Trả về điểm bắt đầu của đường đi
    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    // Trả về danh sách các điểm trên đường đi
    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    // Trả về tốc độ di chuyển
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    // Trả về thời gian spawn ngẫu nhiên trong khoảng cho phép
    public float GetRandomSpawnTime()
    {
        float SpawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, timeBetweenEnemySpawns + spawnTimeVariance);
        return Mathf.Clamp(SpawnTime, minimumSpawnTime, float.MaxValue);
    }
}
