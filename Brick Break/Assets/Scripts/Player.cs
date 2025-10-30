using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

    [SerializeField]
    private float playerSpeed = 10;

    private Rigidbody2D rb2D;

    private bool isCollidingLeft = false;
    private bool isCollidingRight = false;

    private void Start() {
        this.rb2D = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 movement;
        if ((moveInput < 0 && !this.isCollidingLeft) || (moveInput > 0 && !this.isCollidingRight)) {
            movement = new(moveInput * this.playerSpeed * Time.fixedDeltaTime, 0);
            this.rb2D.MovePosition(this.rb2D.position + movement);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == Globals.LeftWallString) {
            this.isCollidingLeft = true;
        } else if (collider.gameObject.name == Globals.RightWallString) {
            this.isCollidingRight = true;
        } else {
            Debug.Log("Collided with ball?");
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.name == Globals.LeftWallString) {
            this.isCollidingLeft = false;
        } else if (collider.gameObject.name == Globals.RightWallString) {
            this.isCollidingRight = false;
        } else {
            Debug.Log("Uncollided with ball?");
        }
    }

}
