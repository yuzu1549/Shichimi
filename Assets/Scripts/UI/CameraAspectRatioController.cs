using UnityEngine;

/// <summary>
/// カメラの描画領域を指定したアスペクト比に保つ。
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraAspectRatioController : MonoBehaviour
{
    private const float MinAspectValue = 0.01f;

    [Header("基準アスペクト比")]
    [SerializeField] private float targetAspectWidth = 16f;
    [SerializeField] private float targetAspectHeight = 9f;

    private Camera targetCamera;
    private int lastScreenWidth;
    private int lastScreenHeight;

    private void Awake()
    {
        targetCamera = GetComponent<Camera>();
        ApplyAspectRatio();
    }

    private void Update()
    {
        if (lastScreenWidth == Screen.width &&
            lastScreenHeight == Screen.height)
        {
            return;
        }

        ApplyAspectRatio();
    }

    /// <summary>
    /// 現在の画面サイズに合わせてカメラの描画領域を調整する。
    /// </summary>
    private void ApplyAspectRatio()
    {
        if (Screen.height <= 0 ||
            targetAspectWidth <= MinAspectValue ||
            targetAspectHeight <= MinAspectValue)
        {
            return;
        }

        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;

        float targetAspect = targetAspectWidth / targetAspectHeight;
        float currentAspect = (float)Screen.width / Screen.height;

        if (currentAspect > targetAspect)
        {
            float viewportWidth = targetAspect / currentAspect;
            float viewportX = (1f - viewportWidth) * 0.5f;

            targetCamera.rect = new Rect(viewportX, 0f, viewportWidth, 1f);
            return;
        }

        float viewportHeight = currentAspect / targetAspect;
        float viewportY = (1f - viewportHeight) * 0.5f;

        targetCamera.rect = new Rect(0f, viewportY, 1f, viewportHeight);
    }
}