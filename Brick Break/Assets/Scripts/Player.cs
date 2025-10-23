using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float playerSpeed = 10;

    private void Update() {
        float moveInput = Input.GetAxis("Horizontal");
        Vector3 movement = new(moveInput * this.playerSpeed * Time.deltaTime, 0, 0);
        this.transform.position += movement;
    }

}
