using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float hp = 1;
    public float Speed = 5;
    public float speedY;
    float force = 230;
    private bool Is_jump = false;
    private bool Is_DoubleJump = false;
    public bool k = false;//防止跳起来的时候有走动的动画
    private Rigidbody2D kk;//拿到刚体的组件
    private Animator am;

    //创建instance方法，使其他脚本可以使用Player.Instance.XXX来访问这个脚本的变量和函数（要public的变量或函数）
    public static Player _instance;
    public static Player Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
        kk = GetComponent<Rigidbody2D>();
      
    }// Use this for initialization
    void Start () {
        am = GetComponent<Animator>();
    }
	// Update is called once per frame
	void FixedUpdate () {
        MoveJump();
        speedY = kk.velocity.y;


    }
    void OnCollisionEnter2D(Collision2D collision)//判断机制，当物体碰到标签诶K的物体的时候就直接关掉
    {
        if (collision.collider.tag == "Plane")
        {
            k = false;
            Is_jump = false;
            Is_DoubleJump = false;
        }
        if(collision.collider.tag=="Key")
        {
            Key.Instance.die();
        }
    }
    void MoveJump()
    {
        if (!(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
        {
            am.SetTrigger("Stand");//在不做移动的时候，就让他播放站立的动画：：但是做不到好像，先保留这一行吧
        }
        float h = Input.GetAxisRaw("Horizontal");   
        if (h > 0)
        {
            transform.Translate(Vector3.right * h * Speed * Time.fixedDeltaTime, Space.World);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            if (!k)
            {
                am.SetTrigger("Walk");//当他在不在跳跃并且按下AD时候，播放走路的动画
            }
        }
        else if (h < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            transform.Translate(Vector3.right * -h * Speed * Time.fixedDeltaTime);
            if (!k)
            {
                am.SetTrigger("Walk");
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
            {
           
            if (!Is_jump)
                {
                am.SetTrigger("Jump");//调用跳跃的动画
                k = true;//当我们按下跳跃的时候，就不能让他同时播放走路的动画
                kk.AddForce(Vector3.up * force);
                    Is_jump = true;//第一次跳的的时候的布尔值，下面会有一个重置，知道它碰到地设定的东西都不会改变               
                }
                else if (Is_DoubleJump)
                { 
                    return;
                }
                else
                {
                am.SetTrigger("Jump");
                k = true;
                kk.AddForce(Vector3.up * force * 1.2f);
                Is_DoubleJump = true;//二段跳的判值，他之前是判断是不是已经使用过二段跳了，如果没使用的话就可以进入，进入之后就关掉，没碰到设定的东西不会改变
                }
            }
        }
    public void MinPlayer()//改变物体的大小，在使用镜子的收调此函数
    {
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);//缩小的大小
    }
    public void TakeDamage(float loss)//受伤时
    {
        hp -= loss;
        if (hp <= 0)
        {
            Debug.Log("Died");
        }
    }
}

