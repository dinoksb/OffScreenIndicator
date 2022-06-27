using PixelPlay.OffScreenIndicator;
using UnityEngine;

/// <summary>
/// Attach the script to the off screen indicator panel.
/// </summary>
[DefaultExecutionOrder(-1)]
public class OffScreenIndicator : OffScreenIndicatorBase
{
    protected override void LateUpdate()
    {
#if UNITY_EDITOR
        _screenBounds = calcScreenBound(_screenBoundOffset);
#endif
        base.LateUpdate();
    }

    /// <summary>
    /// Draw the indicators on the screen and set thier position and rotation and other properties.
    /// </summary>
    protected override void drawIndicators()
    {
        foreach(Target target in targets)
        {
            Vector3 screenPosition = OffScreenIndicatorCore.GetScreenPosition(_targetCamera, target.transform.position);

            if(_targetCamera.orthographic)
                screenPosition = new Vector3(screenPosition.x, screenPosition.y, 0.01f);

            bool isTargetVisible = OffScreenIndicatorCore.IsTargetVisible(screenPosition);
            float distanceFromCamera = target.NeedDistanceText ? target.GetDistanceFromCamera(_targetCamera.transform.position) : float.MinValue;// Gets the target distance from the camera.
            Indicator indicator = null;

            if(target.NeedBoxIndicator && isTargetVisible)
            {
                screenPosition.z = 0;
                indicator = getIndicator(ref target.Indicator, IndicatorType.BOX); // Gets the box indicator from the pool.
            }
            else if(target.NeedArrowIndicator && !isTargetVisible)
            {
                float angle = float.MinValue;
                OffScreenIndicatorCore.GetArrowIndicatorPositionAndAngle(ref screenPosition, ref angle, _screenCentre, _screenBounds);
                indicator = getIndicator(ref target.Indicator, IndicatorType.ARROW); // Gets the arrow indicator from the pool.
                if(indicator != null)
                    indicator.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg); // Sets the rotation for the arrow indicator.
            }
            else
            {
                target.Indicator?.Activate(false);
                target.Indicator = null;
            }
            if(indicator)
            {
                indicator.SetImageColor(target.TargetColor);// Sets the image color of the indicator.
                indicator.SetDistanceText(distanceFromCamera); //Set the distance text for the indicator.
                indicator.SetTextRotation(Quaternion.identity); // Sets the rotation of the distance text of the indicator.
                indicator.transform.position = screenPosition; //Sets the position of the indicator on the screen.
            }
        }
    }


    private Vector3 calcScreenBound(float screenBoundOffset)
    {
        var screenCentre = new Vector3(Screen.width, Screen.height, 0) / 2;
        var screenBounds = screenCentre * screenBoundOffset;
        return screenBounds;
    }
}
