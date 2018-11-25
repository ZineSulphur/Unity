using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    private SpriteRenderer sr;
    public GameObject Position1;
    public GameObject Position2;
    public GameObject CreatMirror;
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
    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPosition();
        GetMirror();
    }
    void GetPosition()//获取鼠标点击地方的坐标
    {
        ScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        MousePositionOnScreen = Input.mousePosition;
        MousePositionInWorld = Camera.main.ScreenToWorldPoint(MousePositionOnScreen);
    }
    void GetMirror()
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
    }
}
