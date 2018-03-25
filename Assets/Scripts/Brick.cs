using UnityEngine;

namespace Assets.Scripts
{
    public class Brick : MonoBehaviour
    {
        //publics
        public Sprite[] HitSprites;
        public GameObject Smoke;
        public AudioClip CrackSound;
        public AudioClip DingSound;

        //privates
        private int _hitCount;
        private bool _isBreakable;

        //Awake becomes before Start...
        //unity docs say not to use constructors
        void Awake()
        {
            _hitCount = 0;
            if (tag == "Breakable")
            {
                LevelManager.BreakableBrickCount++;
                _isBreakable = true;
            }
            else
            {
                _isBreakable = false;
            }
        }

        void Start()
        {
            SetupSmoke();
        }

        void Update()
        {
           
        }

        private void SetupSmoke()
        {
            Vector3 smokePosVec = new Vector3(gameObject.transform.position.x + 0.5f,
                gameObject.transform.position.y + 0.16f, 0.0f);
            Smoke = Instantiate(Smoke, smokePosVec, Quaternion.identity);
            ParticleSystem.MainModule smokeParticles = Smoke.GetComponent<ParticleSystem>().main;
            smokeParticles.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            _hitCount++;
            if (_isBreakable)
            {
                AudioSource.PlayClipAtPoint(CrackSound, this.transform.localPosition);
                if (_hitCount < HitSprites.Length)
                {
                    if (HitSprites[_hitCount] != null)
                    {
                        this.GetComponent<SpriteRenderer>().sprite = HitSprites[_hitCount];
                    }
                    else
                    {
                        Debug.LogError("HitSprites[" + _hitCount +"] is null");
                    }
                        
                }else if (_hitCount >= HitSprites.Length)
                {
                    Kill();
                }
            }
            else
            {
                AudioSource.PlayClipAtPoint(DingSound, this.transform.localPosition);
            }

        }

        private void Kill()
        {
            Smoke.GetComponent<ParticleSystem>().Play();
            LevelManager.BreakableBrickCount--;
            LevelManager.CheckWinCondition();
            //last thing that get's called, because nothing else is available after
            Destroy(gameObject);
        }

        //todo: remove this method once we can implement win conditions
        void SimulateWin()
        {
            LevelManager.LoadNextLevel();
        }
    }
}
