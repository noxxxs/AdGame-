using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargerCamera : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;

    [SerializeField] private float _rotationalSpeed = 30f;
    [SerializeField] private float _topClamp = 70f;
    [SerializeField] private float _bottomClamp = -40f;

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    [SerializeField] private float _smoothTime = 0.1f; // Додаємо плавність
    private float _yawSmoothVelocity;
    private float _pitchSmoothVelocity;

    private void LateUpdate()
    {
        CameraLogic();
    }

    private void CameraLogic()
    {
        float mouseX = GetMouseInput("Mouse X");
        float mouseY = GetMouseInput("Mouse Y");

        _cinemachineTargetPitch = UpdateRotation(_cinemachineTargetPitch, mouseY, _bottomClamp, _topClamp, true);
        _cinemachineTargetYaw = UpdateRotation(_cinemachineTargetYaw, mouseX, float.MinValue, float.MaxValue, false);

        ApplyRotations(_cinemachineTargetPitch, _cinemachineTargetYaw);
    }

    private void ApplyRotations(float pitch, float yaw)
    {
        float smoothPitch = Mathf.SmoothDampAngle(_followTarget.eulerAngles.x, pitch, ref _pitchSmoothVelocity, _smoothTime);
        float smoothYaw = Mathf.SmoothDampAngle(_followTarget.eulerAngles.y, yaw, ref _yawSmoothVelocity, _smoothTime);

        _followTarget.rotation = Quaternion.Euler(pitch, yaw, _followTarget.eulerAngles.z);
    }

    private float UpdateRotation(float currentRotation, float input, float min, float max, bool isAxis)
    {
        currentRotation += isAxis ? -input : input;
        return Mathf.Clamp(currentRotation, min, max);
    }

    private float GetMouseInput(string axis)
    {
        return Input.GetAxis(axis) * _rotationalSpeed;
    }














}
