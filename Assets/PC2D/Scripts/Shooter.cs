using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 發射飛行武器的腳本，掛在某個物件身上就可以使用。
/// </summary>
public class Shooter : MonoBehaviour
{
    public BulletUnit _bulletClone = null; //子彈的預製物
    public Transform _NinjaVisual = null; //忍者的面向判斷
    public int _flySpeed = 100;

    /// <summary>
    /// 要發射飛行武器的時候，呼叫這個方法。
    /// </summary>
    public void Shot(Vector2 direction)
    {
        BulletUnit bu = Instantiate(_bulletClone);
        bu.transform.position = _NinjaVisual.position;

        // 如果玩家沒有按任何方向鍵，預設往忍者前方射
        if (direction.x == 0 && direction.y == 0)
        {
            bu.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _NinjaVisual.localScale.x * _flySpeed);
        }
        else if (direction.x >= 0 && Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
        {
            bu.GetComponent<Rigidbody2D>().AddForce(Vector2.right * _flySpeed);
        }
        else if (direction.x < 0 && Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
        {
            bu.GetComponent<Rigidbody2D>().AddForce(Vector2.left * _flySpeed);
        }
        else if (direction.y >= 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            bu.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _flySpeed);
        }
        else if (direction.y < 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            bu.GetComponent<Rigidbody2D>().AddForce(Vector2.down * _flySpeed);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
