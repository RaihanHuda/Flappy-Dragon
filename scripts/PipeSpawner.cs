using UnityEngine;

/// <summary>
/// Memunculkan prefab pipa secara berkala di sisi kanan layar,
/// dengan ketinggian celah acak.
/// Pasang di GameObject kosong bernama "PipeSpawner".
/// </summary>
public class PipeSpawner : MonoBehaviour
{
    [Header("Pengaturan")]
    public GameObject pipePrefab;      // prefab pipa
    public float spawnInterval = 1.8f; // jeda antar pipa (detik)
    public float spawnX = 10f;         // posisi X spawn (di luar layar kanan)
    public float minY = -2f;           // batas bawah posisi celah
    public float maxY = 2f;            // batas atas posisi celah

    private float timer;

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.State.Playing) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnPipe();
        }
    }

    void SpawnPipe()
    {
        float y = Random.Range(minY, maxY);
        Instantiate(pipePrefab, new Vector3(spawnX, y, 0f), Quaternion.identity);
    }
}
