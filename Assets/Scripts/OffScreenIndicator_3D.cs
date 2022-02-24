using PixelPlay.OffScreenIndicator;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Attach the script to the off screen indicator panel.
/// </summary>
[DefaultExecutionOrder(-1)]
public class OffScreenIndicator_3D : OffScreenIndicator
{
    [SerializeField] private Camera targetCamera;

    protected override void LateUpdate()
    {
#if UNITY_EDITOR
        screenBounds = CalcScreenBound(screenBoundOffset);
#endif
        base.LateUpdate();
    }

    /// <summary>
    /// Draw the indicators on the screen and set thier position and rotation and other properties.
    /// </summary>
    protected override void DrawIndicators()
    {
        foreach(Target target in targets)
        {
            Vector3 screenPosition = OffScreenIndicatorCore.GetScreenPosition(targetCamera, target.transform.position);
            bool isTargetVisible = OffScreenIndicatorCore.IsTargetVisible(screenPosition);
            float distanceFromCamera = target.NeedDistanceText ? target.GetDistanceFromCamera(targetCamera.transform.position) : float.MinValue;// Gets the target distance from the camera.
            Indicator indicator = null;

            if(target.NeedBoxIndicator && isTargetVisible)
            {
                screenPosition.z = 0;
                indicator = GetIndicator(ref target.indicator, IndicatorType.BOX); // Gets the box indicator from the pool.
            }
            else if(target.NeedArrowIndicator && !isTargetVisible)
            {
                float angle = float.MinValue;
                OffScreenIndicatorCore.GetArrowIndicatorPositionAndAngle(ref screenPosition, ref angle, screenCentre, screenBounds);
                indicator = GetIndicator(ref target.indicator, IndicatorType.ARROW); // Gets the arrow indicator from the pool.
                indicator.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg); // Sets the rotation for the arrow indicator.
            }
            else
            {
                target.indicator?.Activate(false);
                target.indicator = null;
            }
            if(indicator)
            {
                indicator.SetImageColor(target.TargetColor);// Sets the image color of the indicator.
                indicator.SetDistanceText(distanceFromCamera); //Set the distance text for the indicator.
                indicator.transform.position = screenPosition; //Sets the position of the indicator on the screen.
                indicator.SetTextRotation(Quaternion.identity); // Sets the rotation of the distance text of the indicator.
            }
        }
    }


    private Vector3 CalcScreenBound(float screenBoundOffset)
    {
        var screenCentre = new Vector3(Screen.width, Screen.height, 0) / 2;
        var screenBounds = screenCentre * screenBoundOffset;
        return screenBounds;
    }
}
