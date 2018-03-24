using UnityEngine;

namespace Assets.Scripts
{
    public class Brick : MonoBehaviour
    {
        //publics
        public Sprite[] HitSprites;

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
            
        }
    
        void Update()
        {
           
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            _hitCount++;
            if (_isBreakable)
            {
                AudioSource.PlayClipAtPoint(CrackSound, this.transform.localPosition);
                if (_hitCount < HitSprites.Length)
                {
                    this.GetComponent<SpriteRenderer>().sprite = HitSprites[_hitCount];
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

        void Kill()
        {
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
