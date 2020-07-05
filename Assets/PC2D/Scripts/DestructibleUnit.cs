using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可破壞的物件，綁上這個腳本它會被 Tag : Shuriken 給破壞。
/// </summary>
public class DestructibleUnit : MonoBehaviour
{
    public int m_Hp = 0;

    // Start is called before the first frame update
    void Start()
    {
        CheckHp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 檢查是否已經被破壞，是的話刪除物件。
    /// </summary>
    private void CheckHp()
    {
        if (m_Hp == 0)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 觸發相關檢查，記得觸發的物件要勾 IsTrigger
    /// </summary>
    /// <param name="o"></param>
    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.gameObject.tag.Equals("Shuriken")) //被手裏劍打到
        {
            //手裏劍擊中後銷毀
            Destroy(o.gameObject);
            m_Hp--;
            CheckHp();
        }
    }
}
