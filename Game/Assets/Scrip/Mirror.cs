using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
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
    public  Player PlayerCompoment;

    // Use this for initialization
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
       // MirrorType=Input.GetButtonDown(KeyCode.Alpha1)
        GetPosition();
        GetMirror4();
        //先测试的是哪个产生镜像自己的镜子

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
                mirrorplayer = Instantiate(MirrorPlayer, (M1positon + M2positon) / 2, Quaternion.Euler(transform.eulerAngles + Mrotation));//产生镜子自己的地方
  
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
    }//第二种镜子：创造一个镜中的自己
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
               PlayerCompoment.MinPlayer();//在这个地方调用Player的变小功能
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

        PlayerCompoment.transform.position = (M1positon + M2positon) / 2 + AddPosition;
    }//传送，因为要延时调用所以直接新建函数使用Invoke比较方便
    void GetMirror5()
    {
        if(Input.GetMouseButtonDown(0))
        {
            light = Instantiate(Light, PlayerCompoment.transform.position, Light.transform.rotation);
        }
    }//洞穴光照探测功能,先用不上之后用得上了再调用出来，主要是和情景有关，不然不好设置黑得区域
}
