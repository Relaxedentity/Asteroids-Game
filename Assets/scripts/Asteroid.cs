using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// An asteroid
/// </summary>
public class Asteroid : MonoBehaviour
{
    [SerializeField]
    Sprite asteroidSprite0;
    [SerializeField]
    Sprite asteroidSprite1;
    [SerializeField]
    Sprite asteroidSprite2;
            const float MinImpulseForce = 1f;
        const float MaxImpulseForce = 3f;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        // set random sprite for asteroid
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        int spriteNumber = Random.Range(0, 3);
        if (spriteNumber < 1)
        {
            spriteRenderer.sprite = asteroidSprite0;
        }
        else if (spriteNumber < 2)
        {
            spriteRenderer.sprite = asteroidSprite1;
        }
        else
        {
            spriteRenderer.sprite = asteroidSprite2;
        }
	}

    /// <summary>
    /// Starts the asteroid moving in the given direction
    /// </summary>
    /// <param name="direction">direction for the asteroid to move</param>
    /// <param name="position">position for the asteroid</param>
    public void Initialize(Direction direction, Vector3 position)
    {
        // set asteroid position
        transform.position = position;

        // set random angle based on direction
        float angle;
        float randomAngle = Random.value * 30f * Mathf.Deg2Rad;
        if (direction == Direction.Up)
        {
            angle = 75 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Left)
        {
            angle = 165 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Down)
        {
            angle = 255 * Mathf.Deg2Rad + randomAngle;
        }
        else
        {
            angle = -15 * Mathf.Deg2Rad + randomAngle;
        }

        StartMoving(angle);

        // apply impulse force to get asteroid moving
        
    }

    /// <summary>
    /// Destroys the asteroid on collision with a bullet
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            AudioManager.Play(AudioClipName.AsteroidHit);
            Destroy(coll.gameObject);
            if (transform.localScale.x > 0.5)
            {
                Vector3 originalScale = transform.localScale;
                Vector3 adaptedScale = new Vector3(originalScale.x / 2, originalScale.y / 2);
                transform.localScale = adaptedScale;
                float originalRadius = gameObject.GetComponent<CircleCollider2D>().radius;
                float newRadius = originalRadius / 2;
                gameObject.GetComponent<CircleCollider2D>().radius = newRadius;
                GameObject smallAsteroid1 = Instantiate(gameObject);
                smallAsteroid1.GetComponent<Rigidbody2D>()
                .AddForce(transform.up * Random.Range(-50.0f, 150.0f));
                GetComponent<Rigidbody2D>()
                .angularVelocity = Random.Range(-0.0f, 90.0f);
                GameObject smallAsteroid2 = Instantiate(gameObject);
                smallAsteroid2.GetComponent<Rigidbody2D>()
                 .AddForce(transform.up * Random.Range(-50.0f, 150.0f));
                GetComponent<Rigidbody2D>()
                .angularVelocity = Random.Range(-0.0f, 90.0f);
                Destroy(gameObject);
            }
            else Destroy(gameObject);



            



        }
    }
    public void StartMoving(float angle)
    {
        const float MinImpulseForce = 1f;
        const float MaxImpulseForce = 3f;
        Vector2 moveDirection = new Vector2(
            Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(
            moveDirection * magnitude,
            ForceMode2D.Impulse);
    }
}
