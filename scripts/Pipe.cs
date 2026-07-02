using UnityEngine;

/// <summary>
/// Menggerakkan pipa ke kiri dan menghapusnya saat keluar layar.
/// Pasang di GameObject induk "Pipe" (yang membungkus pipa atas + bawah + titik skor).
/// </summary>
public class Pipe : MonoBehaviour
{
    public float speed = 3f;       // kecepatan geser ke kiri
    public float destroyX = -12f;  // batas kiri, sesuaikan dengan lebar kamera

    void Update()
    {
        // Pipa hanya bergerak saat game sedang dimainkan
        if (GameManager.Instance.CurrentState != GameManager.State.Playing) return;

        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < destroyX)
            Destroy(gameObject);
    }
}
