using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR; // Needed for input checks

public class ForcePlayerPose : MonoBehaviour
{
    public enum PoseType { Sit, LieDown }

    [Header("Pose Settings")]
    public PoseType poseType = PoseType.Sit;
    public Transform targetTransform; // Where the player should be moved
    public bool disableLocomotion = true;
    public bool disableLocomotionForever = false;

    [Header("Trigger Settings")]
    public bool useTrigger = false; // Set true if using OnTriggerEnter
    public string playerTag = "Player"; // Tag for the XR Rig

    [SerializeField] GameObject LocomotionMove;
    [SerializeField] GameObject LocomotionTurn;

    private bool isPosed = false;

    public void ForcePose(GameObject player)
    {
        if (targetTransform == null) return;

        XROrigin rig = player.GetComponent<XROrigin>();
        if (rig == null)
        {
            Debug.LogWarning("No XRRig found on player.");
            return;
        }

        // Disable movement
        if (disableLocomotion)
        {
            if (LocomotionMove != null) LocomotionMove.SetActive(false);
            if (LocomotionTurn != null) LocomotionTurn.SetActive(false);
        }

        // Offset camera to align with target
        Transform cameraTransform = rig.Camera.transform;
        Vector3 offset = player.transform.position - cameraTransform.position;

        // Move XR Rig
        player.transform.position = targetTransform.position + offset;

        // Align rotation
        Vector3 targetEuler = targetTransform.rotation.eulerAngles;
        player.transform.rotation = Quaternion.Euler(0f, targetEuler.y, 0f);

        isPosed = true; // Player is now posed
    }

    private void Update()
    {
        if (!isPosed) return;

        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        if (rightHand.isValid)
        {
            if (rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool aButton) && aButton && !disableLocomotionForever)
            {
                StandUp();
            }
        }
    }

    private void StandUp()
    {
        if (disableLocomotion)
        {
            if (LocomotionMove != null) LocomotionMove.SetActive(true);
            if (LocomotionTurn != null) LocomotionTurn.SetActive(true);
        }

        isPosed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!useTrigger) return;
        if (other.CompareTag(playerTag))
        {
            ForcePose(other.gameObject);
        }
    }
}   