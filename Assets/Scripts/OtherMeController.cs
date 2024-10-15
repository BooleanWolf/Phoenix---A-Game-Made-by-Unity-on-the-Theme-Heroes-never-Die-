using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherMeController : MonoBehaviour
{
    public GameObject hero;
    public float followRange = 5f;
    private BushController bushController;
    public GameObject tilemapBush;
    private float speed = 3f;
    // Start is called before the first frame update
    private AudioSFXManager audioSFXManager;

    void Start()
    {
        GameObject audioObject = GameObject.FindWithTag("Audio");
        if (audioObject != null)
        {
            audioSFXManager = audioObject.GetComponent<AudioSFXManager>();
        }
        else
        {
            Debug.LogWarning("Audio GameObject with tag 'audio' not found!");
        }
        hero = GameObject.Find("Hero");
        tilemapBush = GameObject.Find("Tilemap_Bushes");
    }

    // Update is called once per frame
    void Update()
    {
        if (tilemapBush != null)
        {
            bushController = tilemapBush.GetComponent<BushController>();
        }
        float distanceToHero = Vector3.Distance(transform.position, hero.transform.position);

        if (distanceToHero <= followRange && !bushController.isHiding)
        {
            // Move towards the hero
            transform.position = Vector3.MoveTowards(transform.position, hero.transform.position, speed * Time.deltaTime);

            // Face the hero
            Vector3 direction = (hero.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Set the rotation only along the Z-axis to face the hero
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I hit me!");

        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            audioSFXManager.PlayMusicHurt(); 
            GameStatController.Instance.decrease_health(20);
        }
    }
}
