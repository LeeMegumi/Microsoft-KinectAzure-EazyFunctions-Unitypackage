using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �`�׼v����ܲե�
/// �Ω�bUnity UI�W��ܨӦ�SkeletalTrackingProvider���`�׼v�����
/// </summary>
public class DepthImageDisplay : MonoBehaviour
{
    [Header("UI �ե�")]
    [Tooltip("�Ω���ܲ`�׼v����RawImage�ե�")]
    public RawImage depthImageUI;

    [Header("��ܳ]�w")]
    [Tooltip("�O�_�ҥβ`�׼v�����")]
    public bool enableDisplay = true;

    [Tooltip("�`�׼v������s���j�]�V�ơ^�A�Ω󱱨��s�W�v")]
    [Range(5, 10)]
    public int updateInterval = 5;

    [Header("�v���B�z")]
    [Tooltip("�O�_½��v���]����½��^")]
    public bool flipHorizontal = false;

    [Tooltip("�O�_½��v���]����½��^")]
    public bool flipVertical = false;

    [Tooltip("�v���Y����")]
    [Range(0.1f, 2.0f)]
    public float imageScale = 1.0f;

    // �����ܼ�
    private Texture2D depthTexture;
    private BackgroundData frameData = new BackgroundData();
    private SkeletalTrackingProvider trackingProvider;
    private int frameCounter = 0;

    // �v���ؤo�֨�
    private int lastImageWidth = 0;
    private int lastImageHeight = 0;

    void Start()
    {
        InitializeDepthDisplay();
    }

    /// <summary>
    /// ��l�Ʋ`�׼v�����
    /// </summary>
    void InitializeDepthDisplay()
    {
        // �ˬdUI�ե�O�_�w���w
        if (depthImageUI == null)
        {
            Debug.LogError("DepthImageDisplay: depthImageUI is not assigned!");
            return;
        }

        // �M��SkeletalTrackingProvider�]���]�b�P�@��GameObject�Τ�����W�^
        trackingProvider = main.Instance.m_skeletalTrackingProvider;

        if (trackingProvider == null)
        {
            Debug.LogError("DepthImageDisplay: Could not find SkeletalTrackingProvider!");
            return;
        }

        Debug.Log("DepthImageDisplay initialized successfully.");
    }

    void Update()
    {
        // �ˬd�O�_�ҥ����
        if (!enableDisplay || trackingProvider == null || depthImageUI == null)
            return;

        // �����s�W�v
        frameCounter++;
        if (frameCounter % updateInterval != 0)
            return;

        // ��������̷s���V���
        bool hasNewData = trackingProvider.GetCurrentFrameData(ref frameData);

        if (hasNewData && frameData.DepthImageSize > 0)
        {
            UpdateDepthTexture();
        }       
    }

    /// <summary>
    /// ��s�`�ׯ��z����ܦbUI�W
    /// </summary>
    void UpdateDepthTexture()
    {
        try
        {
            int imageWidth = frameData.DepthImageWidth;
            int imageHeight = frameData.DepthImageHeight;

            // �ˬd�v���ؤo�O�_����
            if (imageWidth <= 0 || imageHeight <= 0)
            {
                Debug.LogWarning("DepthImageDisplay: Invalid image dimensions.");
                return;
            }

            // �p�G�v���ؤo���ܡA���s�Ыد��z
            if (depthTexture == null || lastImageWidth != imageWidth || lastImageHeight != imageHeight)
            {
                CreateNewTexture(imageWidth, imageHeight);
            }

            // ��s���z���
            UpdateTextureData(imageWidth, imageHeight);

            // �����ܧ�쯾�z
            depthTexture.Apply();

            // �]�w��UI�ե�
            depthImageUI.texture = depthTexture;

            // �����Y��
            ApplyImageTransform();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"DepthImageDisplay: Error updating depth texture: {e.Message}");
        }
    }

    /// <summary>
    /// �Ыطs�����z
    /// </summary>
    void CreateNewTexture(int width, int height)
    {
        // �����ª����z
        if (depthTexture != null)
        {
            DestroyImmediate(depthTexture);
        }

        // �Ыطs���z�]RGB24�榡�^
        depthTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
        depthTexture.filterMode = FilterMode.Point; // �ϥ��I�L�o�H�O���U�Q
        depthTexture.wrapMode = TextureWrapMode.Clamp;

        // ��s�ؤo�֨�
        lastImageWidth = width;
        lastImageHeight = height;

        Debug.Log($"DepthImageDisplay: Created new texture {width}x{height}");
    }

    /// <summary>
    /// ��s���z�������
    /// </summary>
    void UpdateTextureData(int width, int height)
    {
        // �T�O��Ƥj�p���T
        int expectedSize = width * height * 3; // RGB�榡�A�C�ӹ���3�Ӧ줸��
        if (frameData.DepthImageSize != expectedSize)
        {
            Debug.LogWarning($"DepthImageDisplay: Data size mismatch. Expected: {expectedSize}, Got: {frameData.DepthImageSize}");
            return;
        }

        // �Ы��C��}�C
        Color32[] pixels = new Color32[width * height];

        // �ഫ�`�׸�Ƭ��C����
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // �p��b�`�׸�Ƥ�������
                int depthIndex = GetDepthDataIndex(x, y, width, height);

                // �p��b�����}�C��������
                int pixelIndex = GetPixelIndex(x, y, width, height);

                // �T�O���ަb���Ľd��
                if (depthIndex >= 0 && depthIndex + 2 < frameData.DepthImage.Length &&
                    pixelIndex >= 0 && pixelIndex < pixels.Length)
                {
                    byte r = frameData.DepthImage[depthIndex];
                    byte g = frameData.DepthImage[depthIndex + 1];
                    byte b = frameData.DepthImage[depthIndex + 2];

                    pixels[pixelIndex] = new Color32(r, g, b, 255);
                }
            }
        }

        // �]�w�����쯾�z
        depthTexture.SetPixels32(pixels);
    }

    /// <summary>
    /// ����`�׸�Ư��ޡ]�Ҽ{½��^
    /// </summary>
    int GetDepthDataIndex(int x, int y, int width, int height)
    {
        // ����½��
        if (flipHorizontal)
            x = width - 1 - x;
        if (flipVertical)
            y = height - 1 - y;

        return ((y * width) + x) * 3; // RGB�榡�A�C�ӹ���3�Ӧ줸��
    }

    /// <summary>
    /// ��������}�C����
    /// </summary>
    int GetPixelIndex(int x, int y, int width, int height)
    {
        // Unity�����z�y�Шt�ΡG(0,0)�b���U��
        return ((height - 1 - y) * width) + x;
    }

    /// <summary>
    /// ���μv���ܴ��]�Y��B½�൥�^
    /// </summary>
    void ApplyImageTransform()
    {
        if (depthImageUI != null)
        {
            // �����Y��
            Vector3 scale = Vector3.one * imageScale;
            depthImageUI.transform.localScale = scale;
        }
    }

    /// <summary>
    /// �����`�׼v����ܪ��}��
    /// </summary>
    public void ToggleDepthDisplay()
    {
        enableDisplay = !enableDisplay;

        if (!enableDisplay && depthImageUI != null)
        {
            depthImageUI.texture = null;
            depthImageUI.color = new Color32(255,255,255,0);
        }
        else
        {
            depthImageUI.color = Color.white;
        }

            Debug.Log($"DepthImageDisplay: Display {(enableDisplay ? "enabled" : "disabled")}");
    }

    /// <summary>
    /// �]�w�v��½��
    /// </summary>
    public void SetImageFlip(bool horizontal, bool vertical)
    {
        flipHorizontal = horizontal;
        flipVertical = vertical;
    }

    /// <summary>
    /// �]�w�v���Y��
    /// </summary>
    public void SetImageScale(float scale)
    {
        imageScale = Mathf.Clamp(scale, 0.1f, 2.0f);
    }

    /// <summary>
    /// �����e�`�׼v����T
    /// </summary>
    public string GetDepthImageInfo()
    {
        if (frameData.DepthImageWidth > 0 && frameData.DepthImageHeight > 0)
        {
            return $"�`�׼v��: {frameData.DepthImageWidth}x{frameData.DepthImageHeight}, " +
                   $"��Ƥj�p: {frameData.DepthImageSize} bytes, " +
                   $"�ɶ��W: {frameData.TimestampInMs:F1}ms";
        }
        return "�L�`�׼v�����";
    }

    void OnDestroy()
    {
        // �M�z���z�귽
        if (depthTexture != null)
        {
            DestroyImmediate(depthTexture);
            depthTexture = null;
        }
    }

    void OnDisable()
    {
        // ��ե�Q�T�ήɲM�����
        if (depthImageUI != null)
        {
            depthImageUI.texture = null;
        }
    }
}