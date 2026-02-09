using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyStats stats;

    private void Awake()
    {
        stats = GetComponent<EnemyStats>();

    }
    // Non-trigger collision (isTrigger = false)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(stats != null ? stats.damage : 1);
            Debug.Log($"{name} hit {player.gameObject.name} for {(stats != null ? stats.damage : 1)} damage (Collision).");
        }
    }

    // Trigger collision (isTrigger = true)
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(stats != null ? stats.damage : 1);
            Debug.Log($"{name} hit {player.gameObject.name} for {(stats != null ? stats.damage : 1)} damage (Trigger).");
        }
    }

}
