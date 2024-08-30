using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam: MonoBehaviour
{
    Vector3 pos, dir;

    public GameObject laserObj;
    public LineRenderer laser;
    public List<Vector3> laserIndices = new List<Vector3>();
    int laserLayerMask = LayerMask.GetMask("Reflection");
    public MoveControl movecontrol = FindObjectOfType<MoveControl>();

    public LaserBeam(Vector3 pos, Vector3 dir, Material material, Color color)
    {
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam";
        this.laserObj.SetActive(false);
        this.pos = pos;
        this.dir = dir;

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.02f;
        this.laser.endWidth = 0.02f;
        this.laser.material = material;
        this.laser.startColor = color;
        this.laser.endColor = color;

        CastRay(pos, dir, laser);
    }

    public void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 0.7f, laserLayerMask))
        {
            CheckHit(hit,dir,laser);
        }
        else
        {
            laserIndices.Add(ray.GetPoint(0.6f));
            UpdateLaser();
        }
    }

    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndices.Count;

        foreach(Vector3 idx in laserIndices)
        {
            laser.SetPosition(count, idx);
            count++;
        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser)
    {
        if(hitInfo.collider.gameObject.tag == "Mirror")
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);

            CastRay(pos, dir, laser);
        }
        else if (!movecontrol.rotateState && hitInfo.collider.gameObject.tag == "EndingPoint")
        {
            if (movecontrol.GameState)
            {
                movecontrol.gameClear();
            }
            movecontrol.GameState = false;
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        else
        {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
    }
}
