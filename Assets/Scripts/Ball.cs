using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Ball : MonoBehaviour
    {
        private Paddle _paddle;

        private Vector3 _paddleToBallVector;
        private Boolean _ballLaunched = false;
        private AudioSource _boingSound;

        // Use this for initialization
        void Start()
        {
            _boingSound = GetComponent<AudioSource>();
            _paddle = GameObject.FindObjectOfType<Paddle>();
            _paddleToBallVector = gameObject.transform.position - _paddle.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_ballLaunched)
            {
                gameObject.transform.position = _paddle.transform.position + _paddleToBallVector;
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(2.0f, 10.0f);
                    _ballLaunched = true;
                }
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            
            if (_ballLaunched)
            {
                _boingSound.Play();
                if (collision.gameObject.tag != "Paddle")
                {
                    //do brick stuff
                }
                else
                {
                    Vector2 tweak =
                        new Vector2(
                            (gameObject.GetComponent<Rigidbody2D>().position.x - _paddle.GetComponent<Rigidbody2D>().position.x) * 5f,
                            0f);
                    //Vector2 tweak = new Vector2(Random.Range(0, 0.2f), Random.Range(0, 0.2f));
                    GetComponent<Rigidbody2D>().velocity += tweak;
                }
            }

        }
    }
}
