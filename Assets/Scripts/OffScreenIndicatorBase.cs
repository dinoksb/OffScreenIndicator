using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public abstract class OffScreenIndicatorBase : MonoBehaviour
{
    public static Action<Target, bool> TargetStateChanged;

    public bool DisplayHelper { get => _displayHelper; set => _displayHelper = value; }
    public float ScreenBoundOffset { get => _screenBoundOffset; set => _screenBoundOffset = value; }
    public Camera TargetCamera { get => _targetCamera; set => _targetCamera = value; }

    [Range(0.5f, 0.9f)]
    [Tooltip("Distance offset of the indicators from the centre of the screen")]
    [SerializeField] protected float _screenBoundOffset = 0.9f;
    [SerializeField] protected Camera _targetCamera;

    protected List<Target> targets = new List<Target>();

    protected Vector3 _screenCentre;
    protected Vector3 _screenBounds;

    private bool _displayHelper;
    private Color _originGizmoColor;
    private Color _helperColor;

    private void Awake()
    {
        _screenCentre = new Vector3(Screen.width, Screen.height, 0) / 2;
        _screenBounds = _screenCentre * _screenBoundOffset;
        TargetStateChanged += handleTargetStateChanged;
        _originGizmoColor = Gizmos.color;
        _helperColor = new Color(1, 1, 1, 0.3f);
    }

    protected virtual void LateUpdate()
    {
        _screenBounds = _screenCentre * _screenBoundOffset;
        drawIndicators();
    }

    private void OnDestroy()
    {
        TargetStateChanged -= handleTargetStateChanged;
    }

    /// <summary>
    /// Draw the indicators on the screen and set thier position and rotation and other properties.
    /// </summary>
    protected abstract void drawIndicators();

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
    protected virtual Indicator getIndicator(ref Indicator indicator, IndicatorType type)
    {
        if (indicator != null)
        {
            if (indicator.Type != type)
            {
                indicator.Activate(false);
                indicator = type == IndicatorType.BOX ? BoxObjectPool.Current.GetPooledObject() : ArrowObjectPool.Current.GetPooledObject();
                indicator.Activate(true); // Sets the indicator as active.
            }
        }
        else
        {
            indicator = type == IndicatorType.BOX ? BoxObjectPool.Current.GetPooledObject() : ArrowObjectPool.Current.GetPooledObject();
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
    protected virtual void handleTargetStateChanged(Target target, bool active)
    {
        if (active)
        {
            targets.Add(target);
        }
        else
        {
            target.Indicator?.Activate(false);
            target.Indicator = null;
            targets.Remove(target);
        }
    }

    private void OnDrawGizmos()
    {
        if(_displayHelper)
        {
            Gizmos.color = _helperColor;
            Gizmos.DrawCube(_screenCentre, _screenBounds * 2);
            Gizmos.color = _originGizmoColor;
        }
    }
}
