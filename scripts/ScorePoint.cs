using UnityEngine;

/// <summary>
/// Trigger tak terlihat di celah antara pipa.
/// Saat naga melewatinya, skor bertambah 1.
/// Pasang di child "ScorePoint" yang punya BoxCollider2D (Is Trigger = ON).
/// </summary>
public class ScorePoint : MonoBehaviour
{
    private bool counted;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (counted) return;

        if (other.CompareTag("Player"))
        {
            counted = true;
            GameManager.Instance.AddScore();
        }
    }
}
