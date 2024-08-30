using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.Collections;


public class GroundPlanePlacement : MonoBehaviour
{
    public Button placeButton;
    public GameObject groundPlaneStage;
    public GameObject planeFinder;
    public ShotLaser laserBeam;
    public MoveControl movecontrol;

    private bool planePlaced = false;
    private PlaneFinderBehaviour planeFinderBehaviour;
    private AnchorInputListenerBehaviour anchorInputListener;

    public GameObject gamePlate;
    public Button rotateLeftButton;
    public Button rotateRightButton;

    void Start()
    {
        placeButton.onClick.AddListener(PlaceGroundPlane);

        rotateLeftButton.onClick.AddListener(RotateLeft);
        rotateRightButton.onClick.AddListener(RotateRight);
        rotateLeftButton.gameObject.SetActive(false); // Hide rotation buttons initially
        rotateRightButton.gameObject.SetActive(false); // Hide rotation buttons initially

        placeButton.interactable = false; // Disable the button initially
        planeFinderBehaviour = planeFinder.GetComponent<PlaneFinderBehaviour>();
        anchorInputListener = planeFinder.GetComponent<AnchorInputListenerBehaviour>();
        laserBeam = FindObjectOfType<ShotLaser>();
        movecontrol = FindObjectOfType<MoveControl>();

        // Subscribe to the PlaneFinderBehaviour's OnInteractiveHitTest event
        planeFinderBehaviour.OnInteractiveHitTest.AddListener(OnPlaneDetected);
    }

    private void OnPlaneDetected(HitTestResult result)
    {
        if (!planePlaced)
        {
            placeButton.interactable = true;
        }

        movecontrol.selectaudio.Play();
    }

    void PlaceGroundPlane()
    {
        if (!planePlaced)
        {
            // Enable the ground plane stage
            groundPlaneStage.SetActive(true);

            // Disable the Vuforia Plane Finder to prevent further plane detection
            planeFinderBehaviour.enabled = false;

            // Disable the Anchor Input Listener to prevent further plane placement
            anchorInputListener.enabled = false;

            // Set the flag to indicate that the plane is placed
            planePlaced = true;

            // Disable the button to prevent further placement
            placeButton.gameObject.SetActive(false);

            rotateLeftButton.gameObject.SetActive(true);
            rotateRightButton.gameObject.SetActive(true);

            if (movecontrol.tutorial)
            {
                movecontrol.tutorialText.text = "Next, you need to redirect the LIGHT from Runestone to the Portal.\nYou can do it by moving and rotating the mirrors.\nTry to tap on any tile under the mirrors.";
            }

            movecontrol.GameState = true;
            laserBeam.beam.laserObj.SetActive(true);
        }
    }

    void RotateLeft()
    {
        gamePlate.transform.Rotate(0, 90, 0);
    }

    void RotateRight()
    {
        gamePlate.transform.Rotate(0, -90, 0);
    }
}
