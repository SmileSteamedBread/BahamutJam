using UnityEngine;

/// <summary>
/// This class is a simple example of how to build a controller that interacts with PlatformerMotor2D.
/// </summary>
[RequireComponent(typeof(PlatformerMotor2D))]
public class PlayerController2D : MonoBehaviour
{
    public int _money = 0;
    public int _hp = 3;
    private PlatformerMotor2D _motor;
    private Shooter _shooter;
    private bool _restored = true;
    private bool _enableOneWayPlatforms;
    private bool _oneWayPlatformsAreWalls;

    private const int _hp_max = 3;

    // Use this for initialization
    void Start()
    {
        _motor = GetComponent<PlatformerMotor2D>();
        _shooter = GetComponent<Shooter>();
    }

    // before enter en freedom state for ladders
    void FreedomStateSave(PlatformerMotor2D motor)
    {
        if (!_restored) // do not enter twice
            return;

        _restored = false;
        _enableOneWayPlatforms = _motor.enableOneWayPlatforms;
        _oneWayPlatformsAreWalls = _motor.oneWayPlatformsAreWalls;
    }
    // after leave freedom state for ladders
    void FreedomStateRestore(PlatformerMotor2D motor)
    {
        if (_restored) // do not enter twice
            return;

        _restored = true;
        _motor.enableOneWayPlatforms = _enableOneWayPlatforms;
        _motor.oneWayPlatformsAreWalls = _oneWayPlatformsAreWalls;
    }

    // Update is called once per frame
    void Update()
    {
        // use last state to restore some ladder specific values
        if (_motor.motorState != PlatformerMotor2D.MotorState.FreedomState)
        {
            // try to restore, sometimes states are a bit messy because change too much in one frame
            FreedomStateRestore(_motor);
        }

        // Jump?
        // If you want to jump in ladders, leave it here, otherwise move it down
        if (Input.GetButtonDown(PC2D.Input.JUMP))
        {
            _motor.Jump();
            _motor.DisableRestrictedArea();
        }

        _motor.jumpingHeld = Input.GetButton(PC2D.Input.JUMP);

        // XY freedom movement
        if (_motor.motorState == PlatformerMotor2D.MotorState.FreedomState)
        {
            _motor.normalizedXMovement = Input.GetAxis(PC2D.Input.HORIZONTAL);
            _motor.normalizedYMovement = Input.GetAxis(PC2D.Input.VERTICAL);

            return; // do nothing more
        }

        // X axis movement
        if (Mathf.Abs(Input.GetAxis(PC2D.Input.HORIZONTAL)) > PC2D.Globals.INPUT_THRESHOLD)
        {
            _motor.normalizedXMovement = Input.GetAxis(PC2D.Input.HORIZONTAL);
        }
        else
        {
            _motor.normalizedXMovement = 0;
        }

        if (Input.GetAxis(PC2D.Input.VERTICAL) != 0)
        {
            bool up_pressed = Input.GetAxis(PC2D.Input.VERTICAL) > 0;
            if (_motor.IsOnLadder())
            {
                if (
                    (up_pressed && _motor.ladderZone == PlatformerMotor2D.LadderZone.Top)
                    ||
                    (!up_pressed && _motor.ladderZone == PlatformerMotor2D.LadderZone.Bottom)
                 )
                {
                    // do nothing!
                }
                // if player hit up, while on the top do not enter in freeMode or a nasty short jump occurs
                else
                {
                    // example ladder behaviour

                    _motor.FreedomStateEnter(); // enter freedomState to disable gravity
                    _motor.EnableRestrictedArea();  // movements is retricted to a specific sprite bounds

                    // now disable OWP completely in a "trasactional way"
                    FreedomStateSave(_motor);
                    _motor.enableOneWayPlatforms = false;
                    _motor.oneWayPlatformsAreWalls = false;

                    // start XY movement
                    _motor.normalizedXMovement = Input.GetAxis(PC2D.Input.HORIZONTAL);
                    _motor.normalizedYMovement = Input.GetAxis(PC2D.Input.VERTICAL);
                }
            }
        }
        else if (Input.GetAxis(PC2D.Input.VERTICAL) < -PC2D.Globals.FAST_FALL_THRESHOLD)
        {
            _motor.fallFast = false;
        }

        // 如果按下了 Shot 的 Input的話，
        if (Input.GetButtonDown(PC2D.Input.SHOT))
        {
            //把現在忍者的水平跟垂直按壓位移當作參數傳遞
            Vector2 pos = new Vector2(Input.GetAxis(PC2D.Input.HORIZONTAL), Input.GetAxis(PC2D.Input.VERTICAL));
            //發射手裏劍
            _shooter.Shot(pos);   
        }

        if (Input.GetButtonDown(PC2D.Input.DASH))
        {
            _motor.Dash();
        }
    }

    /// <summary>
    /// 觸發相關檢查，記得觸發的物件要勾 IsTrigger
    /// </summary>
    /// <param name="o"></param>
    void OnTriggerEnter2D(Collider2D o)
    {
        //Debug.Log(gameObject.name + " OnTriggerEnter with " + o.name);
        if (o.tag.Equals("Exit")) //離開這層迷宮，前往下一層
        {
            MiniSceneManager._instance.CheckNextScene();
        }
        else if (o.tag.Equals("Money")) //吃到錢
        {
            _money += 100;
        }
        else if (o.tag.Equals("RedWater")) //吃到補血道具
        {
            //如果回復血量1點沒有超過血量最大值
            if (_hp + 1 <= _hp_max)
            {
                _hp += 1;
            }
            else
            {
                _hp = _hp_max;
            }
        }
    }

    /// <summary>
    /// 不想用觸發，想用碰撞的就吃這邊的事件
    /// </summary>
    /// <param name="o"></param>
    void OnCollisionEnter2D(Collision2D o)
    {
        //Debug.Log(gameObject.name + " OnCollisionEnter with " + o.collider.name);
        if (o.gameObject.tag.Equals("Exit")) //離開這層迷宮，前往下一層
        {
            MiniSceneManager._instance.CheckNextScene();
        }
        else if (o.gameObject.tag.Equals("Money")) //吃到錢
        {
            _money += 100;
        }
        else if (o.gameObject.tag.Equals("RedWater")) //吃到補血道具
        {
            //如果回復血量1點沒有超過血量最大值
            if (_hp + 1 <= _hp_max)
            {
                _hp += 1;
            }
            else
            {
                _hp = _hp_max;
            }
        }
    }
}
