using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLaser : MonoBehaviour
{
    public Material material;
    public Color color;
    public LaserBeam beam;

    private void Start()
    {
        beam = new LaserBeam(transform.position, transform.right, material, color);
    }

    private void Update()
    {
        beam.laser.positionCount = 0;
        beam.laserIndices.Clear();
        beam.CastRay(transform.position, transform.right, beam.laser);
    }
}
