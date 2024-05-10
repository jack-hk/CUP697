using TMPro;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int health;
    public TextMeshProUGUI text;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            health -= 1;
            text.text = health + "";
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
