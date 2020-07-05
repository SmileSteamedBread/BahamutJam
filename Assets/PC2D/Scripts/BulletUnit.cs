using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 飛行武器的腳本
/// </summary>
public class BulletUnit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 讓手里劍旋轉，感覺比較酷。
        transform.Rotate(Vector3.forward * 100);
    }
}
