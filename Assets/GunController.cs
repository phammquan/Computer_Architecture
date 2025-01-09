using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform muzzlePoint; // Điểm phát đạn (MuzzlePoint)
    public GameObject bulletPrefab; // Prefab đạn
    public float bulletSpeed = 20f; // Tốc độ đạn

    // Hàm bắn súng
    public void Shoot()
    {
        Debug.Log("Gun Fired!");
    }
}
