using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    public GameObject PlayerBulletPrefab;
    public GameObject bulletPos1;
    public GameObject bulletPos2;
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet1 = (GameObject)Instantiate(PlayerBulletPrefab);
            bullet1.transform.position = bulletPos1.transform.position;

            GameObject bullet2 = (GameObject)Instantiate(PlayerBulletPrefab);
            bullet2.transform.position = bulletPos2.transform.position;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
    }

    private void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);   
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);   
        
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();
            Destroy(gameObject);
        }
    }

    private void PlayExplosion()
    {
        GameObject exp = (GameObject)Instantiate(explosion);
        exp.transform.position = transform.position;
    }
}
