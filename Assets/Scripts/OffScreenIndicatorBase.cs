using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public abstract class OffScreenIndicatorBase : MonoBehaviour
{
    public static Action<Target, bool> TargetStateChanged;

    public bool DisplayHelper { get => displayHelper; set => displayHelper = value; }
    public float ScreenBoundOffset { get => screenBoundOffset; set => screenBoundOffset = value; }
    public Camera TargetCamera { get => targetCamera; set => targetCamera = value; }

    [Range(0.5f, 0.9f)]
    [Tooltip("Distance offset of the indicators from the centre of the screen")]
    [SerializeField] protected float screenBoundOffset = 0.9f;
    [SerializeField] protected Camera targetCamera;

    protected List<Target> targets = new List<Target>();

    protected Vector3 screenCentre;
    protected Vector3 screenBounds;

    private bool displayHelper;
    private Color originGizmoColor;
    private Color helperColor;

    private void Awake()
    {
        screenCentre = new Vector3(Screen.width, Screen.height, 0) / 2;
        screenBounds = screenCentre * screenBoundOffset;
        TargetStateChanged += HandleTargetStateChanged;
        originGizmoColor = Gizmos.color;
        helperColor = new Color(1, 1, 1, 0.3f);
    }

    protected virtual void LateUpdate()
    {
        screenBounds = screenCentre * screenBoundOffset;
        DrawIndicators();
    }

    private void OnDestroy()
    {
        TargetStateChanged -= HandleTargetStateChanged;
    }

    /// <summary>
    /// Draw the indicators on the screen and set thier position and rotation and other properties.
    /// </summary>
    protected abstract void DrawIndicators();

    /// <summary>
    /// Get the indicator for the target.
    /// 1. If its not null and of the same required <paramref name="type"/> 
    ///     then return the same indicator;
    /// 2. If its not null but is of different type from <paramref name="type"/> 
    ///     then deactivate the old reference so that it returns to the pool 
    ///     and request one of another type from pool.
    /// 3. If its null then request one from the pool of <paramref name="type"/>.
    /// </summary>
    /// <param name="indicator"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    protected virtual Indicator GetIndicator(ref Indicator indicator, IndicatorType type)
    {
        if (indicator != null)
        {
            if (indicator.Type != type)
            {
                indicator.Activate(false);
                indicator = type == IndicatorType.BOX ? BoxObjectPool.current.GetPooledObject() : ArrowObjectPool.current.GetPooledObject();
                indicator.Activate(true); // Sets the indicator as active.
            }
        }
        else
        {
            indicator = type == IndicatorType.BOX ? BoxObjectPool.current.GetPooledObject() : ArrowObjectPool.current.GetPooledObject();
            indicator.Activate(true); // Sets the indicator as active.
        }
        return indicator;
    }

    /// <summary>
    /// 1. Add the target to targets list if <paramref name="active"/> is true.
    /// 2. If <paramref name="active"/> is false deactivate the targets indicator, 
    ///     set its reference null and remove it from the targets list.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="active"></param>
    protected virtual void HandleTargetStateChanged(Target target, bool active)
    {
        if (active)
        {
            targets.Add(target);
        }
        else
        {
            target.indicator?.Activate(false);
            target.indicator = null;
            targets.Remove(target);
        }
    }

    private void OnDrawGizmos()
    {
        if(displayHelper)
        {
            Gizmos.color = helperColor;
            Gizmos.DrawCube(screenCentre, screenBounds * 2);
            Gizmos.color = originGizmoColor;
        }
    }
}
