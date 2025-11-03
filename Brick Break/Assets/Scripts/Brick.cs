using UnityEngine;

public class Brick : MonoBehaviour {

    [SerializeField]
    private float totalBrickHealth = 5;

    private float currBrickHealth;

    private void Start() {
        this.currBrickHealth = this.totalBrickHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        this.currBrickHealth--;
        if (this.currBrickHealth <= 0) {
            Destroy(this.gameObject);
        } else {
            float gbValue = (this.currBrickHealth - 1) / this.totalBrickHealth;
            Transform visual = this.transform.Find(Globals.VisualString);
            if (visual != null) {
                Debug.Log("Found it!");
                visual.GetComponent<SpriteRenderer>().color = new Color(1f, gbValue, gbValue);
            }
        }
    }

}
