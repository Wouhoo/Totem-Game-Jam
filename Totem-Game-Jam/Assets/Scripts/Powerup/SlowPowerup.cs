using UnityEngine;

public class SlowPowerup : DragPowerup
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isBuilderMode())
        {
            Vector2 addedForce = player.GetComponent<Rigidbody2D>().linearVelocity;
            addedForce.Scale(new Vector2(0.3f, 0.3f));
            player.GetComponent<Rigidbody2D>().linearVelocity = addedForce;
            this.pickup();
            transform.position = new Vector3(transform.position.x, transform.position.y, 100);
        }
    }

}
