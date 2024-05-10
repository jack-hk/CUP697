using UnityEngine;

public class EnemyMouse : MonoBehaviour
{
    //follow mouse
    public GameObject target;
    public float trackSpeed;
    Rigidbody2D rigidBody;
    Vector2 position = new Vector2(0f, 0f);

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FollowMouse();
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(position);
    }

    private void FollowMouse()
    {
        position = Vector2.Lerp(transform.position, target.transform.position, trackSpeed / 100);
    }
}
