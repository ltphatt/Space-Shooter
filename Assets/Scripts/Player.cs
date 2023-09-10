using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;  // Tốc độ di chuyển của người chơi
    Vector2 rawInput;  // Đầu vào nguyên thủy từ bàn phím/điều khiển

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Vector2 minBounds;  // Giới hạn di chuyển tối thiểu
    Vector2 maxBounds;  // Giới hạn di chuyển tối đa

    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();  // Khởi tạo giới hạn di chuyển của người chơi
    }

    void Update()
    {
        Move();  // Xử lý di chuyển của người chơi
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;  // Tính toán sự thay đổi vị trí dựa trên đầu vào
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);  // Giới hạn vị trí theo trục x
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);  // Giới hạn vị trí theo trục y
        transform.position = newPos;  // Cập nhật vị trí mới của người chơi
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();  // Lấy dữ liệu đầu vào từ sự kiện di chuyển
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}