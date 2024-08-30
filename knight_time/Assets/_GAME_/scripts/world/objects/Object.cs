using UnityEngine;

public class Object : MonoBehaviour
{
    public GameObject player; // Reference to the player object

    private SpriteRenderer objectRenderer;
    private float boxColliderSizeX;
    private float boxColliderSizeY;


    void Start()
    {
        player = MainHero.instance.gameObject;
        objectRenderer = GetComponent<SpriteRenderer>();

        Vector2 spriteSize = objectRenderer.bounds.size;
        Vector2 spritePlayerSize = player.GetComponent<SpriteRenderer>().bounds.size;
        boxColliderSizeX = spriteSize.x / 2 + spritePlayerSize.x / 2;
        boxColliderSizeY = spriteSize.y / 2 + +spritePlayerSize.y / 2;
    }

    void Update()
    {
        if (player.gameObject.transform.position.y > this.gameObject.transform.position.y &&
            player.gameObject.transform.position.y < this.gameObject.transform.position.y + boxColliderSizeY &&
           player.gameObject.transform.position.x > this.gameObject.transform.position.x - boxColliderSizeX &&
           player.gameObject.transform.position.x < this.gameObject.transform.position.x + boxColliderSizeX)
        {
            SetTransparency(true);
        }
        else
        {
            SetTransparency(false);
        }
    }

    /*
    void OnCollisionStay2D(Collision2D other)
    {
        MainHero character = other.gameObject.GetComponent<MainHero>();

        if (character != null)
        {
            Debug.Log(other.gameObject.transform.position.y + " " + this.transform.position.y);
            if (other.gameObject.transform.position.y >= this.transform.position.y)
            {
                //boxCollider.isTrigger = true;
                boxCollider.enabled = false;
                SetTransparency(true);
            }
            else
            {
                //boxCollider.isTrigger = false;
                boxCollider.enabled = true;
                SetTransparency(false);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        SetTransparency(false);
        boxCollider.enabled = true;
    }
    */

    void SetTransparency(bool transparent)
    {
        if (transparent)
        {
            objectRenderer.color = new Color(objectRenderer.color.r, objectRenderer.color.g, objectRenderer.color.b, 0.3f);
        }
        else
        {
            objectRenderer.color = new Color(objectRenderer.color.r, objectRenderer.color.g, objectRenderer.color.b, 1f);
        }
    }
}
