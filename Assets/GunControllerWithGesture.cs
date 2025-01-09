using Oculus.Interaction;
using UnityEngine;

public class GunControllerWithGesture : MonoBehaviour
{
    public Transform muzzlePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public OVRHand hand; // Bàn tay đang theo dõi

    private void Update()
    {
        // Kiểm tra cử chỉ Pinch (bóp ngón tay trỏ và cái)
        if (hand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.Log("Gun Fired with Gesture!");
        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = muzzlePoint.forward * bulletSpeed;
        Destroy(bullet, 5f);
    }
}