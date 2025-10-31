using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour {

    [SerializeField]
    private float ballSpeed = 10;
    [SerializeField]
    private float speedIncrement = 1.03f;

    private Rigidbody2D rb2D;
    private Vector2 currSpeed;

    private float horSpeed;
    private float verSpeed;

    private void Start() {
        this.rb2D = this.GetComponent<Rigidbody2D>();
        // Start moving by 30 degree rotation
        this.horSpeed = this.ballSpeed / 2;
        this.verSpeed = -(this.ballSpeed * Globals.Sqrt3By2);
        this.currSpeed = new(this.horSpeed, this.verSpeed);
    }

    private void FixedUpdate() {
        this.currSpeed = new(this.horSpeed, this.verSpeed);
        this.rb2D.MovePosition(this.rb2D.position + (this.currSpeed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        // Vector2 normal = collision.contacts[0].normal;
        if (collider.gameObject.name is Globals.PlayerString) {
            this.HitPlayer();
        }
        if (collider.gameObject.name is Globals.CeilingString) {
            this.verSpeed *= -this.speedIncrement;
        }
        if (collider.gameObject.name is Globals.LeftWallString or Globals.RightWallString) {
            this.horSpeed *= -this.speedIncrement;
        }
    }

    private void HitPlayer() {
        this.verSpeed *= -this.speedIncrement;
        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput == 0) {
            return;
        }
        if (moveInput < 0) {
            if (this.horSpeed < 0) {
                this.horSpeed *= this.speedIncrement;
            } else {
                this.verSpeed *= this.speedIncrement;
            }
        } else {
            if (this.horSpeed > 0) {
                this.horSpeed *= this.speedIncrement;
            } else {
                this.verSpeed *= this.speedIncrement;
            }
        }
    }

}
