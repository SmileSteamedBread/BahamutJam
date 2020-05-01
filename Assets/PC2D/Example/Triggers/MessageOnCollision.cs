using UnityEngine;

public class MessageOnCollision : MonoBehaviour
{
    public Color triggerColor;

    private Color _originalColor;

    void Start()
    {
        _originalColor = GetComponent<SpriteRenderer>().color;
    }

    /// <summary>
    /// 觸發條件： o 對象或此物件本身之中一個擁有 rigidbody (剛體)
    /// 偵測到 o 對象已經觸發此物件的 box collider 範圍，並將觸發狀態從 enter 改為 stay 。
    /// </summary>
    /// <param name="o"> 踩進 trigger 的物件 </param>
    void OnTriggerEnter2D(Collider2D o)
    {
        GetComponent<SpriteRenderer>().color = triggerColor;
        Debug.Log(gameObject.name + " OnTriggerEnter with " + o.name);
    }

    /// <summary>
    /// 觸發條件： o 對象或此物件本身之中一個擁有 rigidbody (剛體)
    /// o 對象觸發仍停在此物件上，持續發生的事件，要注意使用。
    /// </summary>
    /// <param name="o">  踩進 trigger 後並停留的物件 </param>
    void OnTriggerStay2D(Collider2D o)
    {
        GetComponent<SpriteRenderer>().color = triggerColor;
        Debug.Log(gameObject.name + " OnTriggerStay with " + o.name);
    }

    /// <summary>
    /// 觸發條件： o 對象或此物件本身之中一個擁有 rigidbody (剛體)
    /// o 對象離開了此物件的 box collider 範圍了，會發一次這個事件；狀態為 stay 變成 exit。
    /// </summary>
    /// <param name="o"> 踩進 trigger 後離開的物件 </param>
    void OnTriggerExit2D(Collider2D o)
    {
        GetComponent<SpriteRenderer>().color = _originalColor;
        Debug.Log(gameObject.name + " OnTriggerExit with " + o.name);
    }

    /// <summary>
    /// 若此物件有剛體( rigidbody )受到了 o 物件的碰撞、或此物件受到了掛有剛體( rigidbody ) o 物件的碰撞，發此事件。
    /// </summary>
    /// <param name="o"> 前來碰撞的物件 </param>
    void OnCollisionEnter2D(Collision2D o)
    {
        Debug.Log(gameObject.name + " OnCollisionEnter with " + o.collider.name);
    }

    /// <summary>
    /// 若此物件有剛體( rigidbody )受到了 o 物件的碰撞、或此物件受到了掛有剛體( rigidbody ) o 物件的碰撞之後仍未離開碰撞體；
    /// 正常來說，物件在發生碰撞後會有一些物理運算，但在某些狀況下會發生連續碰撞的現象如 方塊落在地板上；
    /// 但碰撞發生後一旦作用力運動結束，物體與碰撞體會處於靜止狀態，因為沒有繼續碰撞所以事件也不會連送，但沒離開也不會送離開事件。
    /// </summary>
    /// <param name="o"> 持續碰撞的物件 </param>
    void OnCollisionStay2D(Collision2D o)
    {
        Debug.Log(gameObject.name + " OnCollisionStay with " + o.collider.name);
    }

    /// <summary>
    /// 若此物件有剛體( rigidbody )受到了 o 物件的碰撞、或此物件受到了掛有剛體( rigidbody ) o 物件的碰撞之後離開碰撞體發此事件。
    /// </summary>
    /// <param name="o"> 發生碰撞後離開的物件 </param>
    void OnCollisionExit2D(Collision2D o)
    {
        Debug.Log(gameObject.name + " OnCollisionExit with " + o.collider.name);
    }
}
