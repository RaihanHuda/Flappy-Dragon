using UnityEngine;

/// <summary>
/// Kontrol naga (pemain): gravitasi, lompat/kepak saat tap,
/// rotasi mengikuti kecepatan, dan mati saat menabrak.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class DragonController : MonoBehaviour
{
    [Header("Gerakan")]
    public float flapForce = 6f;     // kekuatan lompatan tiap tap
    public float rotateSpeed = 8f;   // kehalusan rotasi
    public float maxUpAngle = 30f;   // sudut hidung naik maksimal
    public float maxDownAngle = -90f;// sudut hidung menukik maksimal

    private Rigidbody2D rb;
    private bool isDead;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Naga "menggantung" dulu sampai pemain menekan tap pertama
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
    }

    void Update()
    {
        if (isDead) return;

        if (GameManager.GetTapInput())
        {
            // Tap pertama = mulai game + langsung mengaktifkan gravitasi
            if (GameManager.Instance.CurrentState == GameManager.State.Ready)
            {
                GameManager.Instance.StartGame();
                rb.bodyType = RigidbodyType2D.Dynamic;
            }

            if (GameManager.Instance.CurrentState == GameManager.State.Playing)
                Flap();
        }

        RotateWithVelocity();
    }

    void Flap()
    {
        rb.velocity = Vector2.up * flapForce; // reset lalu dorong ke atas
    }

    void RotateWithVelocity()
    {
        if (GameManager.Instance.CurrentState != GameManager.State.Playing) return;

        // Naik -> hidung ke atas, jatuh -> hidung menukik
        float angle = Mathf.Clamp(rb.velocity.y * 5f, maxDownAngle, maxUpAngle);
        Quaternion target = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, target, rotateSpeed * Time.deltaTime);
    }

    // Menabrak pipa / tanah / langit-langit = mati
    void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;
        GameManager.Instance.GameOver();
        // Naga tetap dijatuhkan gravitasi setelah mati (efek nyungsep)
    }
}
