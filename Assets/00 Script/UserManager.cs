using System;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    // Lưu instance của Singleton
    private static UserManager _instance;

    // Thuộc tính Instance cho phép truy cập duy nhất tới đối tượng Singleton
    public static UserManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Nếu instance chưa được khởi tạo, tạo mới và giữ nguyên giữa các scene
                _instance = FindObjectOfType<UserManager>();

                // Nếu không tìm thấy đối tượng Singleton trong scene, tạo mới nó
                if (_instance == null)
                {
                    GameObject obj = new GameObject("UserManager");
                    _instance = obj.AddComponent<UserManager>();
                }

                // Đảm bảo rằng đối tượng Singleton không bị hủy khi chuyển scene
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    // Các thuộc tính và phương thức của UserManager
    public string UserId { get; private set; }

    private UserManager()
    {
        UserId = Guid.NewGuid().ToString();  // Tạo ID người dùng duy nhất
    }

    void Awake()
    {
        // Đảm bảo chỉ có một instance duy nhất tồn tại
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);  // Hủy bỏ đối tượng nếu đã có instance khác
        }
    }
}