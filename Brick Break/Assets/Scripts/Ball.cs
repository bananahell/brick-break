using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour {

    [SerializeField]
    private float ballSpeed = 10;

    private Rigidbody2D rb2D;

    private float horSpeed;
    private float verSpeed;

    private void Start() {
        this.rb2D = this.GetComponent<Rigidbody2D>();
        // Start moving by 30 degree rotation
        float sqrt3 = 1.73205080756887729f;
        this.horSpeed = this.ballSpeed / 2;
        this.verSpeed = -(this.ballSpeed * sqrt3 / 2);
    }

    private void FixedUpdate() {
        Vector2 speed = new(this.horSpeed, this.verSpeed);
        this.rb2D.MovePosition(this.rb2D.position + (speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log(collider.gameObject.name);
        if (collider.gameObject.name is Globals.PlayerString or Globals.CeilingString) {
            this.verSpeed *= -1;
        }
        if (collider.gameObject.name is Globals.LeftWallString or Globals.RightWallString) {
            this.horSpeed *= -1;
        }
    }

}
