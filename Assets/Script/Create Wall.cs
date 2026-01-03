using UnityEngine;

public class ScreenEdgeColliders : MonoBehaviour
{
    public PhysicMaterial bounceMaterial;

    void Start()
    {
        CreateWalls();
    }

    void CreateWalls()
    {
        // 获取主摄像机
        Camera cam = Camera.main;
        // 计算屏幕左下角和右上角的世界坐标
        float zPos = 0f;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 10));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 10));
        // 创建父物体
        GameObject wallsParent = new GameObject("ScreenWalls");
        // 创建四个厚度为1f的墙
        CreateWall("TopWall", wallsParent, new Vector3((bottomLeft.x + topRight.x) / 2, topRight.y, zPos), new Vector3(topRight.x - bottomLeft.x, 1f, 1f));
        CreateWall("BottomWall", wallsParent, new Vector3((bottomLeft.x + topRight.x) / 2, bottomLeft.y, zPos), new Vector3(topRight.x - bottomLeft.x, 1f, 1f));
        CreateWall("LeftWall", wallsParent, new Vector3(bottomLeft.x, (bottomLeft.y + topRight.y) / 2, zPos), new Vector3(1f, topRight.y - bottomLeft.y, 1f));
        CreateWall("RightWall", wallsParent, new Vector3(topRight.x, (bottomLeft.y + topRight.y) / 2, zPos), new Vector3(1f, topRight.y - bottomLeft.y, 1f));
    }

    void CreateWall(string name, GameObject parent, Vector3 pos, Vector3 scale)
    {
        GameObject wall = new GameObject(name);   // create wall object
        wall.transform.parent = parent.transform; // set parent
        wall.transform.position = pos;            // set position
        wall.tag = name;
        

        // 把墙稍微往外移一点点，避免卡住
        if (name.Contains("Top")) wall.transform.position += Vector3.up * 0.5f;
        if (name.Contains("Bottom")) wall.transform.position -= Vector3.up * 0.5f;
        if (name.Contains("Left")) wall.transform.position -= Vector3.right * 0.5f;
        if (name.Contains("Right")) wall.transform.position += Vector3.right * 0.5f;
        BoxCollider col = wall.AddComponent<BoxCollider>();
        col.size = scale;
    }

    public void BounceWall()
    {
        foreach (Collider col in GameObject.Find("ScreenWalls").GetComponentsInChildren<Collider>())
        {
            col.material = bounceMaterial;
        }
    }

    public void ResetWallPhysics()
    {
        foreach (Collider col in GameObject.Find("ScreenWalls").GetComponentsInChildren<Collider>())
        {
            col.material = null;
        }
    }
}