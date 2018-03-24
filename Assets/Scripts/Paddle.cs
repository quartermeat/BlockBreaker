using UnityEngine;

namespace Assets.Scripts
{
    public class Paddle : MonoBehaviour
    {
        public bool AutoPlay;

        private const int BlockNumWidth = 16;
        private Ball _ball;
        
        // Use this for initialization
        void Start ()
        {
            _ball = FindObjectOfType<Ball>();
        }
	
        // Update is called once per frame
        void Update () {

            if (AutoPlay)
            {
                AutoMove();
            }
            else
            {
                MoveWithMouse();
            }
        }

        private void MoveWithMouse()
        {
            Vector3 paddlePosVect = new Vector3(0.5f, this.transform.position.y, 5f);
            float mousePosGameBlocks = Input.mousePosition.x / Screen.width * BlockNumWidth;
            paddlePosVect.x = Mathf.Clamp(mousePosGameBlocks, 0f, 15f);
            this.transform.position = paddlePosVect;
        }

        private void AutoMove()
        {
            Vector3 paddlePosVect = new Vector3(0.5f, this.transform.position.y, 5f);
            Vector3 ballPosVect = _ball.transform.position;

            paddlePosVect.x = Mathf.Clamp(ballPosVect.x - 0.5f, 0f, 15f);
            this.transform.position = paddlePosVect;
        }
    }
}
