using UnityEngine;

namespace PC2D
{
    public class SimpleRotate : MonoBehaviour
    {
        public float xAngle;
        public float yAngle;
        public float zAngle; //想要實現平面旋轉效果，改Z軸就可以了。
        public float ratio = 1; //預設1倍

        private MovingPlatformMotor2D _mpMotor;

        // Use this for initialization
        void Start()
        {
            _mpMotor = GetComponent<MovingPlatformMotor2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // 使用 time.deltaTime 讓旋轉起來的效果更流暢
            transform.Rotate(xAngle * Time.deltaTime * ratio, yAngle * Time.deltaTime * ratio, zAngle * Time.deltaTime * ratio);
        }
    }
}
