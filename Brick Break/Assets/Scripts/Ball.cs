using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour {

    [SerializeField]
    private float playerForce = 200;
    [SerializeField]
    private float startingForce = -500;
    [SerializeField]
    private GameObject player;

    private Rigidbody2D rb2D;

    private void Start() {
        this.rb2D = this.GetComponent<Rigidbody2D>();
        this.rb2D.AddForce(new Vector2(100, this.startingForce));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject == this.player) {
            float movement = Input.GetAxis("Horizontal");
            this.rb2D.AddForce(new Vector2(movement * this.playerForce, 0));
        }
    }

}
