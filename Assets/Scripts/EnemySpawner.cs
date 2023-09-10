using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    WaveConfigSO currentWave;  // Wave hiện tại đang được spawn
    [SerializeField] List<WaveConfigSO> waveConfigs;  // Danh sách các cấu hình Wave
    [SerializeField] float timeBetweenWaves = 0f;  // Thời gian giữa các loạt Wave
    [SerializeField] bool isLooping;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());  // Bắt đầu gọi hàm sinh Wave
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;  // Thiết lập Wave hiện tại
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i),  // Tạo kẻ địch từ Prefab
                                currentWave.GetStartingWaypoint().position,  // Tại vị trí điểm bắt đầu
                                Quaternion.Euler(0, 0, 180),  // Với hướng mặt mặc định
                                transform);  // Tạo trong phạm vi của Spawner
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());  // Đợi một khoảng thời gian ngẫu nhiên trước khi spawn kẻ địch tiếp theo
                }

                yield return new WaitForSeconds(timeBetweenWaves);  // Đợi một khoảng thời gian giữa các loạt Wave
            }
        }
        while (isLooping);

    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;  // Trả về thông tin Wave hiện tại
    }
}
