using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour {

    [SerializeField]
    private float ballSpeed = 10;
    [SerializeField]
    private float speedIncrement = 1.03f;
    [SerializeField]
    private GameObject player;

    private Rigidbody2D rb2D;
    private Vector2 currSpeed;

    private void Start() {
        this.rb2D = this.GetComponent<Rigidbody2D>();
        // Start moving by 30 degree rotation
        this.currSpeed = new(this.ballSpeed / 2, -(this.ballSpeed * Globals.Sqrt3By2));
    }

    private void FixedUpdate() {
        this.rb2D.MovePosition(this.rb2D.position + (this.currSpeed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.name is Globals.PlayerString) {
            this.HitPlayer(collision);
        } else {
            this.BounceOffNormal(collision.contacts[0].normal);
        }
    }

    private void BounceOffNormal(Vector2 normal) {
        float dot = (this.currSpeed.x * normal.x) + (this.currSpeed.y * normal.y);
        this.currSpeed.x -= 2 * dot * normal.x;
        this.currSpeed.y -= 2 * dot * normal.y;
    }

    private void HitPlayer(Collision2D collision) {
        // (h = sqrt(x^2 + y^2)) * increment
        float speed = Mathf.Sqrt(Mathf.Pow(this.currSpeed.x, 2) + Mathf.Pow(this.currSpeed.y, 2)) * this.speedIncrement;
        if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == 1) {
            float playerHalfWidth = this.player.GetComponent<Collider2D>().bounds.size.x / 2;
            float playerPosition = this.player.GetComponent<Collider2D>().bounds.center.x;
            float diffPercent = (playerPosition - collision.contacts[0].point.x) / playerHalfWidth;
            float angle = Globals.Pi8Over18 * diffPercent;
            this.currSpeed.x = Mathf.Sin(angle) * speed * -1;
            this.currSpeed.y = Mathf.Cos(angle) * speed * -1;
            this.BounceOffNormal(collision.contacts[0].normal);
        } else {
            float angle = Mathf.Atan(this.currSpeed.x / this.currSpeed.y);
            this.currSpeed.x = Mathf.Sin(angle) * speed * -1;
            this.currSpeed.y = Mathf.Cos(angle) * speed;
            this.BounceOffNormal(collision.contacts[0].normal);
        }
    }

}
