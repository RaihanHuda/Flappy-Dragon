using UnityEngine;

/// <summary>
/// (Opsional) Membuat tanah/latar bergerak ke kiri lalu meloncat balik
/// ke kanan supaya terlihat berjalan tanpa henti (efek looping).
/// Butuh 2 tile yang sama, berjejer. Pasang script ini di masing-masing tile.
/// </summary>
public class GroundLoop : MonoBehaviour
{
    public float speed = 3f;    // samakan dengan kecepatan pipa
    public float tileWidth = 20f; // lebar 1 tile
    public float leftLimit = -20f; // saat posisi X < ini, tile dipindah ke kanan

    void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.State.GameOver) return;

        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= leftLimit)
            transform.position += new Vector3(tileWidth * 2f, 0f, 0f);
    }
}
