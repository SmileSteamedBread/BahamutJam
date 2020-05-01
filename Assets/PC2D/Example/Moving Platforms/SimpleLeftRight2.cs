using UnityEngine;

namespace PC2D
{
    public class SimpleLeftRight2 : MonoBehaviour
    {
        public float leftRightAmount; //位移量
        public float time = 1; //位移的時間

        private MovingPlatformMotor2D _mpMotor;
        private float _startingX;
        private float _timer = 0;

        // Use this for initialization
        void Start()
        {
            _mpMotor = GetComponent<MovingPlatformMotor2D>();
            _startingX = transform.position.x;

            //防呆，不然會在0秒內快速切換
            if (time <= 1)
            {
                time = 1;
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            _timer += Time.deltaTime;

            if (_timer >= time)
            {
                Vector3 pos = transform.position;

                // 當前方塊位置還沒位移過，則位移 leftRightAmount
                if (transform.position.x < _startingX + leftRightAmount)
                {
                    pos.x = transform.position.x + leftRightAmount;
                }
                else if (transform.position.x > _startingX) // 若已經跑超過原本所在的位置，則回到原本位置
                {
                    pos.x = _startingX;
                }
                transform.position = pos;
                _timer = 0;
            }
        }
    }
}
