using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    public float force, minTimeToExplode, maxTimeToExplode;
    public int minFireworks, maxFireworks;
    public GameObject fireworkPrefab;
    public int maxExplosions = 3;
    public float speed;
    public KeyCode botonIzquierdo;


    private Rigidbody2D _rb;
    private SpriteRenderer _rend;
    private int _count = 0;
    private Vector2 _dir = Vector2.up;
    private float currentTime, timeToExplode;
    private float randomTime;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rend = GetComponent<SpriteRenderer>();
        
    }


    void Update()
    {
        if (Input.GetKeyDown(botonIzquierdo))
        {
            GameManager.instance.SetFuegos(GameManager.instance.GetFuegos());
            Instantiate(fireworkPrefab, transform.position, Quaternion.identity);
            RandomExplode();
            if (_count < maxExplosions)
            {
                if (currentTime >= randomTime)
                {
                    int rnd = Random.Range(minFireworks, maxFireworks);
                    for (int i = 0; i < rnd; i++)
                    {
                        GameObject fuego1 = Instantiate(fireworkPrefab, transform.position, Quaternion.identity);
                        fuego1.GetComponent<Firework>().DireccionAleatoria();
                        _count++;
                    }
                }
            }

            if (currentTime >= timeToExplode)
            {
                Destroy(gameObject);
                currentTime = 0;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = _dir * speed;
    }
    
    void RandomExplode()
    {
        randomTime = Random.Range(minTimeToExplode, maxTimeToExplode);
        
    }
    public void DireccionAleatoria()
    {
        _dir.y = Random.Range(-1, 2); // [-1, 2)

        do
        {
            _dir.x = Random.Range(-1, 2);
        } while (_dir.x == 0);
    }
}
