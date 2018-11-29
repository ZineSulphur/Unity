using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CamerFollow : MonoBehaviour
{

    public Material LOSMaskMaterial;
    public GameObject player;
    public GameObject minPos; //摄像机能在最左端位置放一个空物体来获取物质
    public GameObject maxPos;
    public GameObject lowPos;
    public GameObject highPos;
    public float speed = 1;
    public float speedY;
    public float direction = 1;

    private float minPosx;
    private float maxPosx;
    private float lowPosy;
    private float highPosy;

    // Use this for initialization
    void Start()
    {
        minPosx = minPos.transform.position.x;
        maxPosx = maxPos.transform.position.x;
        lowPosy = lowPos.transform.position.y;
        highPosy = highPos.transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FixCamaraPos();
        speedY = Mathf.Abs(Player.Instance.speedY);
    }

    void FixCamaraPos()
    {
        float pPosX = player.transform.position.x;
        float cPosX = transform.position.x;
        float pPoxY = player.transform.position.y;
        float cPoxY = transform.position.y;
        if (pPosX - cPosX > direction)
        {
            transform.position = new Vector3(cPosX + Time.deltaTime * speed, transform.position.y, transform.position.z);
        }
        if (pPosX - cPosX < -direction)
        {
            transform.position = new Vector3(cPosX - Time.deltaTime * speed, transform.position.y, transform.position.z);
        }
        if (pPoxY - cPoxY > direction)
        {
            transform.position = new Vector3(transform.position.x, cPoxY + Time.deltaTime * speedY, transform.position.z);
        }
        if (pPoxY - cPoxY < -direction)
        {
            transform.position = new Vector3(transform.position.x, cPoxY - Time.deltaTime * speedY, transform.position.z);
        }
        float realPosX = Mathf.Clamp(transform.position.x, minPosx, maxPosx);
        float realPoxY = Mathf.Clamp(transform.position.y, lowPosy, highPosy);
        transform.position = new Vector3(realPosX, realPoxY, transform.position.z);
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, LOSMaskMaterial);
    }
}
