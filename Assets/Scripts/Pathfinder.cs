using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    WaveConfigSO waveConfig;  // Thông tin cấu hình của loạt Wave
    List<Transform> waypoints;  // Danh sách các điểm trên đường đi
    int waypointIndex = 0;  // Chỉ mục của điểm trên đường đi
    EnemySpawner enemySpawner;  // Tham chiếu đến EnemySpawner

    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();  // Tìm và lấy tham chiếu đến EnemySpawner trong scene
    }

    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();  // Lấy thông tin cấu hình loạt Wave hiện tại từ EnemySpawner
        waypoints = waveConfig.GetWaypoints();  // Lấy danh sách các điểm trên đường đi từ cấu hình loạt Wave
        transform.position = waypoints[waypointIndex].position;  // Đặt vị trí ban đầu tại điểm xuất phát
    }

    void Update()
    {
        FollowPath();  // Theo dõi đường đi
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)  // Nếu còn điểm trên đường đi
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;  // Vị trí đích của điểm tiếp theo
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;  // Tính toán khoảng cách di chuyển trong khung thời gian
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, delta);  // Di chuyển gần đến vị trí đích
            if (transform.position == targetPosition)  // Nếu đạt được vị trí đích
            {
                waypointIndex++;  // Chuyển sang điểm tiếp theo trên đường đi
            }
        }
        else
        {
            Destroy(gameObject);  // Nếu đã đi hết đường đi, hủy GameObject hiện tại
        }
    }
}
