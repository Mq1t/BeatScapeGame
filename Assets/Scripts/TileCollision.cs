using UnityEngine;

public class TileCollision : MonoBehaviour
{
    // Non-trigger collision (isTrigger = false)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            int damage = 20; // Default damage for tile collision
            player.TakeDamage(damage);
            Debug.Log($"{name} hit {player.gameObject.name} for {damage} damage (Collision).");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{name} hit {gameObject.name}. (Collision)");
            Destroy(gameObject);
        }
    }

    // Trigger collision (isTrigger = true)
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            int damage = 20; // Default damage for tile collision
            player.TakeDamage(damage);
            Debug.Log($"{name} hit {player.gameObject.name} for {damage} damage (Trigger).");
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{name} hit {gameObject.name}. (Collision)");
            Destroy(gameObject);
        }
    }
}
