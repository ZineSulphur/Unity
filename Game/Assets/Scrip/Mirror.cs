using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mirror : MonoBehaviour
{
    public float MirrorRange=2;//镜子产生必须在这个距离之内
    public float Distance;//这是鼠标距离玩家的距离，用于判断是否在距离之内
    public bool kkk = false;
    public Vector3 MirrorPlayerPosition;
    public char MirrorType = 'o';
    public GameObject AttackMan;
    public GameObject Position1;
    public GameObject Position2;
    public GameObject CreatMirror;
    public GameObject MirrorPlayer;
    public GameObject Light;
    public Vector2 M1positon;
    public Vector2 M2positon;
    public Vector3 Mrotation;
    public Vector2 ScreenPosition;
    public Vector2 MousePositionOnScreen;
    public Vector2 MousePositionInWorld;
    private bool Othera = true;
    private bool c1 = false;//第一个点是否存在
    private bool isMirror = false;//镜子是否已经存在
    private GameObject point1;
    private GameObject point2;
    private GameObject mirror;
    private GameObject light;
    private GameObject mirrorplayer;
    private GameObject attackman;
    public static Mirror _instance;
    public static Mirror Instance
    {
        get
        {
            return _instance;
        }
    }
    public float X;
    public float Y;

    // Use this for initialization
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       // MirrorType=Input.GetButtonDown(KeyCode.Alpha1)
        GetPosition();//下面那行就是计算距离的，看着挺长其实就是距离
        Distance = Mathf.Sqrt((MousePositionInWorld.x - Player.Instance.transform.position.x)*(MousePositionInWorld.x - Player.Instance.transform.position.x) + (MousePositionInWorld.y - Player.Instance.transform.position.y)*(MousePositionInWorld.y - Player.Instance.transform.position.y));
     
        //先测试的是哪个产生镜像自己的镜子
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MirrorType = 'a';
        }else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            MirrorType = 'b';
        }else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            MirrorType = 'c';
        }else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            MirrorType = 'd';
        }
       
        if (MirrorType=='a')
        {
            if (Distance > MirrorRange)
            {
                return;
            }
            GetMirror200();
        }else if(MirrorType == 'b')
            {   
            if (Distance > MirrorRange)//产生镜子距离的判断，下同
            {
                return;
            }
            GetMirror3();
        }
        else if(MirrorType == 'c')
            {
            if (Distance > MirrorRange)
            {
                return;
            }
            GetMirror4();
        }else if(MirrorType == 'd')
        {
            if (Distance > MirrorRange)
            {
                return;
            }
            GetMirror5();
        }
    }
    void GetPosition()//获取鼠标点击地方的坐标
    {
        ScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        MousePositionOnScreen = Input.mousePosition;
        MousePositionInWorld = Camera.main.ScreenToWorldPoint(MousePositionOnScreen);
    }
    void GetMirror1()
    {
        if (!c1)//第一个点是否存在
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (isMirror == true)
                {
                    Destroy(mirror);
                    Destroy(point1);
                    Destroy(point2);
                    isMirror = false;
                }
                point1 = Instantiate(Position1, MousePositionInWorld, transform.rotation) as GameObject;//生成第一个物体
                M1positon = MousePositionInWorld;//并且获取第一个物体产生时候的坐标
                c1 = true;
                Othera = true;
            }
        }
        if (Input.GetMouseButtonUp(0) && c1 == true)
        {
           
            Othera = false;//当鼠标拿起来的时候重置一下
            point2 = Instantiate(Position2, MousePositionInWorld, transform.rotation) as GameObject;
            M2positon = MousePositionInWorld;//获取第二个物体产生的坐标
            Mrotation.z = Mathf.Atan((M1positon.y - M2positon.y) / (M1positon.x - M2positon.x)) * 60+180;//获取需要转的角度
            mirror = Instantiate(CreatMirror, (M1positon + M2positon) / 2, Quaternion.Euler(transform.eulerAngles + Mrotation));//产生镜子的时候该表角度是用Euler来算的
            isMirror = true;
        }
        if (!Othera && c1 == true)
        {
            M2positon = MousePositionInWorld;//获取第二个物体产生的坐标
            Mrotation.z = Mathf.Atan((M1positon.y - M2positon.y) / (M1positon.x - M2positon.x)) * 60;//获取需要转的角度
            point2.transform.position = M2positon;
            mirror.transform.position = (M1positon + M2positon) / 2;
            mirror.transform.rotation = Quaternion.Euler(transform.eulerAngles + Mrotation);
            if (Input.GetMouseButtonDown(0))
            {
                Othera = true;
                c1 = false;
            }
        }
        if (Input.GetMouseButtonDown(1) && isMirror == true)//右键删除创建中的镜子
        {
            Destroy(mirror);
            Destroy(point1);
            Destroy(point2);
            isMirror = false;
            Othera = true;
            c1 = false;
        }
    }//第一种镜子：直接创造一个镜子，用于二次跳跃
    void GetMirror2()
    {
        if (!c1)//第一个点是否存在
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isMirror == true)
                {
                    Destroy(mirror);
                    Destroy(point1);
                    Destroy(point2);
                    Destroy(mirrorplayer);
                    isMirror = false;
                }
                point1 = Instantiate(Position1, MousePositionInWorld, transform.rotation) as GameObject;//生成第一个物体
                M1positon = MousePositionInWorld;//并且获取第一个物体产生时候的坐标
                c1 = true;
                Othera = true;
            }
        }
        if (Input.GetMouseButtonUp(0) && c1 == true)
        {
            Othera = false;//当鼠标拿起来的时候重置一下
            point2 = Instantiate(Position2, MousePositionInWorld, transform.rotation) as GameObject;
            M2positon = MousePositionInWorld;//获取第二个物体产生的坐标
            Mrotation.z = Mathf.Atan((M1positon.y - M2positon.y) / (M1positon.x - M2positon.x)) * 60;//获取需要转的角度
            mirror = Instantiate(CreatMirror, (M1positon + M2positon) / 2, Quaternion.Euler(transform.eulerAngles + Mrotation));//产生镜子的时候该表角度是用Euler来算的         
            isMirror = true;
        }
        if (!Othera && c1 == true)
        {
            M2positon = MousePositionInWorld;//获取第二个物体产生的坐标
            Mrotation.z = Mathf.Atan((M1positon.y - M2positon.y) / (M1positon.x - M2positon.x)) * 60;//获取需要转的角度
            point2.transform.position = M2positon;
            mirror.transform.position = (M1positon + M2positon) / 2;
            mirror.transform.rotation = Quaternion.Euler(transform.eulerAngles + Mrotation);

            
            if (Input.GetMouseButtonDown(0))
            {
                Othera = true;
                c1 = false;
               
                MirrorPlayerPosition = (M1positon + M2positon) / 2;
                Vector3 Mirrorotation = Mrotation;
                Mirrorotation.z = Mrotation.z + 90;
                Vector3 MirrorPosition = new Vector3(2 * Mirror.Instance.MirrorPlayerPosition.x - Player.Instance.transform.position.x, 2 * Mirror.Instance.MirrorPlayerPosition.y - Player.Instance.transform.position.y,0);
                mirrorplayer = Instantiate(MirrorPlayer, MirrorPosition, Quaternion.Euler(transform.eulerAngles + Mirrorotation));//产生镜子自己的地方


            }
        }
        if (Input.GetMouseButtonDown(1) && isMirror == true)//右键删除创建中的镜子
        {
            Destroy(mirror);
            Destroy(point1);
            Destroy(point2);
            Destroy(mirrorplayer);
            isMirror = false;
            Othera = true;
            c1 = false;
        }
    }//第二种镜子：创造一个镜中的自己,这个是点对称的
    void GetMirror200()
    {
        M2Position();
        if (!c1)//第一个点是否存在
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isMirror == true)
                {
                    Destroy(mirror);
                    Destroy(point1);
                    Destroy(point2);
                    Destroy(mirrorplayer);
                    isMirror = false;
                }
                point1 = Instantiate(Position1, MousePositionInWorld, transform.rotation) as GameObject;//生成第一个物体
                M1positon = MousePositionInWorld;//并且获取第一个物体产生时候的坐标
                c1 = true;
                Othera = true;
            }
        }
        if (Input.GetMouseButtonUp(0) && c1 == true)
        {
            Othera = false;//当鼠标拿起来的时候重置一下
            point2 = Instantiate(Position2, MousePositionInWorld, transform.rotation) as GameObject;
            M2positon = MousePositionInWorld;//获取第二个物体产生的坐标
            Mrotation.z = Mathf.Atan((M1positon.y - M2positon.y) / (M1positon.x - M2positon.x)) * 60;//获取需要转的角度
            mirror = Instantiate(CreatMirror, (M1positon + M2positon) / 2, Quaternion.Euler(transform.eulerAngles + Mrotation));//产生镜子的时候该表角度是用Euler来算的         
            isMirror = true;
        }
        if (!Othera && c1 == true)
        {
            M2positon = MousePositionInWorld;//获取第二个物体产生的坐标
            Mrotation.z = Mathf.Atan((M1positon.y - M2positon.y) / (M1positon.x - M2positon.x)) * 60;//获取需要转的角度
            point2.transform.position = M2positon;
            mirror.transform.position = (M1positon + M2positon) / 2;
            mirror.transform.rotation = Quaternion.Euler(transform.eulerAngles + Mrotation);


            if (Input.GetMouseButtonDown(0))
            {
                Othera = true;
                c1 = false;

                MirrorPlayerPosition = (M1positon + M2positon) / 2;
                Vector3 Mirrorotation = Mrotation;
                Mirrorotation.z = Mrotation.z + 90;
                Vector3 MirrorPosition = new Vector3(X, Y, 0);
             
                mirrorplayer = Instantiate(MirrorPlayer, MirrorPosition, Quaternion.Euler(transform.eulerAngles + Mirrorotation));//产生镜子自己的地方


            }
        }
        if (Input.GetMouseButtonDown(1) && isMirror == true)//右键删除创建中的镜子
        {
            Destroy(mirror);
            Destroy(point1);
            Destroy(point2);
            Destroy(mirrorplayer);
            isMirror = false;
            Othera = true;
            c1 = false;
        }
    }//这个是以轴对称的

    void M2Position()
    {
        float A = (M2positon.y - M1positon.y)/ (M2positon.x - M1positon.x);
        float B = -1;
        float C = M1positon.y - A * M1positon.x;
        float tmp = (A * Player.Instance.transform.position.x + B * Player.Instance.transform.position.y + C) / (A * A + B * B);
        X = Player.Instance.transform.position.x - 2 * A * tmp;
        Y= Player.Instance.transform.position.y - 2 * B * tmp;
    }//计算轴对称的左边XY
    void GetMirror3()
    {
        if (!c1)//第一个点是否存在
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isMirror == true)
                {
                    Destroy(mirror);
                    Destroy(point1);
                    Destroy(point2);
                    isMirror = false;
                }
                point1 = Instantiate(Position1, MousePositionInWorld, transform.rotation) as GameObject;//生成第一个物体
                M1positon = MousePositionInWorld;//并且获取第一个物体产生时候的坐标
                c1 = true;
                Othera = true;
            }
        }
        if (Input.GetMouseButtonUp(0) && c1 == true)
        {
            Othera = false;//当鼠标拿起来的时候重置一下
            point2 = Instantiate(Position2, MousePositionInWorld, transform.rotation) as GameObject;
            M2positon = MousePositionInWorld;//获取第二个物体产生的坐标
            Mrotation.z = Mathf.Atan((M1positon.y - M2positon.y) / (M1positon.x - M2positon.x)) * 60;//获取需要转的角度
            mirror = Instantiate(CreatMirror, (M1positon + M2positon) / 2, Quaternion.Euler(transform.eulerAngles + Mrotation));//产生镜子的时候该表角度是用Euler来算的
            isMirror = true;
        }
        if (!Othera && c1 == true)
        {
            M2positon = MousePositionInWorld;//获取第二个物体产生的坐标
            Mrotation.z = Mathf.Atan((M1positon.y - M2positon.y) / (M1positon.x - M2positon.x)) * 60;//获取需要转的角度
            point2.transform.position = M2positon;
            mirror.transform.position = (M1positon + M2positon) / 2;
            mirror.transform.rotation = Quaternion.Euler(transform.eulerAngles + Mrotation);
            if (Input.GetMouseButtonDown(0))
            {
                Player.Instance.MinPlayer();
              // PlayerCompoment.MinPlayer();//在这个地方调用Player的变小功能
                Othera = true;
                c1 = false;
            }
        }
        if (Input.GetMouseButtonDown(1) && isMirror == true)//右键删除创建中的镜子
        {
            Destroy(mirror);
            Destroy(point1);
            Destroy(point2);
            isMirror = false;
            Othera = true;
            c1 = false;
        }
    }//第三种镜子：调用Player中的函数，吧自己变小
    void GetMirror4()//第四种镜子：传送镜子：暂时先定为任意地点
    {  
        if (!c1)//第一个点是否存在
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isMirror == true)
                {
                    Destroy(mirror);
                    Destroy(point1);
                    Destroy(point2);
                    isMirror = false;
                }
                point1 = Instantiate(Position1, MousePositionInWorld, transform.rotation) as GameObject;//生成第一个物体
                M1positon = MousePositionInWorld;//并且获取第一个物体产生时候的坐标
                c1 = true;
                Othera = true;
            }
        }
        if (Input.GetMouseButtonUp(0) && c1 == true)
        {
            Othera = false;//当鼠标拿起来的时候重置一下
            point2 = Instantiate(Position2, MousePositionInWorld, transform.rotation) as GameObject;
            M2positon = MousePositionInWorld;//获取第二个物体产生的坐标
            Mrotation.z = Mathf.Atan((M1positon.y - M2positon.y) / (M1positon.x - M2positon.x)) * 60;//获取需要转的角度
            mirror = Instantiate(CreatMirror, (M1positon + M2positon) / 2, Quaternion.Euler(transform.eulerAngles + Mrotation));//产生镜子的时候该表角度是用Euler来算的
            isMirror = true;
        }
        if (!Othera && c1 == true)
        {
            M2positon = MousePositionInWorld;//获取第二个物体产生的坐标
            Mrotation.z = Mathf.Atan((M1positon.y - M2positon.y) / (M1positon.x - M2positon.x)) * 60;//获取需要转的角度
            point2.transform.position = M2positon;
            mirror.transform.position = (M1positon + M2positon) / 2;
            mirror.transform.rotation = Quaternion.Euler(transform.eulerAngles + Mrotation);
            if (Input.GetMouseButtonDown(0))
            {
            
                Invoke("TP", 2);
                Othera = true;
                c1 = false;
            }
        }
        if (Input.GetMouseButtonDown(1) && isMirror == true)//右键删除创建中的镜子
        {
            Destroy(mirror);
            Destroy(point1);
            Destroy(point2);
            isMirror = false;
            Othera = true;
            c1 = false;
        }
    }
    void TP()
    {
        Vector2 AddPosition = new Vector2(1, 0);//传送的位置稍微往前一点传送一个位置，会从镜子中心传出

       Player.Instance .transform.position = (M1positon + M2positon) / 2 + AddPosition;
    }//传送，因为要延时调用所以直接新建函数使用Invoke比较方便
    void GetMirror9()
    {
        if(Input.GetMouseButtonDown(0))
        {
            light = Instantiate(Light, Player.Instance .transform.position, Light.transform.rotation);
        }
    }//洞穴光照探测功能,先用不上之后用得上了再调用出来，主要是和情景有关，不然不好设置黑得区域
    void GetMirror5()//攻击
    {
        if (!c1)//第一个点是否存在
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isMirror == true)
                {
                    Destroy(mirror);
                    Destroy(point1);
                    Destroy(point2);
                    Destroy(attackman);
                    isMirror = false;
                }
                point1 = Instantiate(Position1, MousePositionInWorld, transform.rotation) as GameObject;//生成第一个物体
                M1positon = MousePositionInWorld;//并且获取第一个物体产生时候的坐标
                c1 = true;
                Othera = true;
            }
        }
        if (Input.GetMouseButtonUp(0) && c1 == true)
        {
            Othera = false;//当鼠标拿起来的时候重置一下
            point2 = Instantiate(Position2, MousePositionInWorld, transform.rotation) as GameObject;
            M2positon = MousePositionInWorld;//获取第二个物体产生的坐标
            Mrotation.z = Mathf.Atan((M1positon.y - M2positon.y) / (M1positon.x - M2positon.x)) * 60;//获取需要转的角度
            mirror = Instantiate(CreatMirror, (M1positon + M2positon) / 2, Quaternion.Euler(transform.eulerAngles + Mrotation));//产生镜子的时候该表角度是用Euler来算的
            isMirror = true;
        }
        if (!Othera && c1 == true)
        {
            M2positon = MousePositionInWorld;//获取第二个物体产生的坐标
            Mrotation.z = Mathf.Atan((M1positon.y - M2positon.y) / (M1positon.x - M2positon.x)) * 60;//获取需要转的角度
            point2.transform.position = M2positon;
            mirror.transform.position = (M1positon + M2positon) / 2;
            mirror.transform.rotation = Quaternion.Euler(transform.eulerAngles + Mrotation);
            if (Input.GetMouseButtonDown(0))
            {
                MirrorPlayerPosition = MousePositionInWorld;//下面这些是对替身产生出来时候的一些位置，角度微调。
                Vector2 Move = (M1positon + M2positon) / 2;
                Vector2 AddMove = new Vector2(1, 0);
                Vector3 Mrotation1 = Mrotation;
                if(Player.Instance.transform.position.x-MirrorPlayerPosition.x<0)
                {
                    Move += AddMove;
                }else
                {
                    kkk = true;
                    Move -= AddMove;
                    Mrotation1.y += 180;
                }
                Mrotation1.z -= 90;
                attackman = Instantiate (AttackMan, Move, Quaternion.Euler(transform.eulerAngles + Mrotation1));
                Othera = true;
                c1 = false;
            }
        }
        if (Input.GetMouseButtonDown(1) && isMirror == true)//右键删除创建中的镜子
        {
            Destroy(mirror);
            Destroy(point1);
            Destroy(point2);
            Destroy(attackman);
            isMirror = false;
            Othera = true;
            c1 = false;
        }
    }
  
}
