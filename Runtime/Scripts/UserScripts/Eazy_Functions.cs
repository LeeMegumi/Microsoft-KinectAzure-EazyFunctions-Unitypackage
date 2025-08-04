using System.Collections.Generic;
using UnityEngine;
using Microsoft.Azure.Kinect.BodyTracking;

public class Easy_Functions : MonoBehaviour
{
    [Header("References")]
    public TrackerHandler_multi trackerHandler;
    public DepthImageDisplay depthImageDisplay;

    [Header("Control Keys")]
    [Tooltip("�������f��ܪ�����")]
    public KeyCode toggleSkeletonKey = KeyCode.S;

    [Tooltip("�����`�׼v����ܪ�����")]
    public KeyCode toggleDepthImageKey = KeyCode.D;

    [Tooltip("�C�L������T������")]
    public KeyCode printInfoKey = KeyCode.I;

    [Tooltip("�����v��½�઺����")]
    public KeyCode toggleImageFlipKey = KeyCode.F;

    [Tooltip("�վ�v���Y�񪺫���]�t�X�Ʀr��1-3�^")]
    public KeyCode scaleImageKey = KeyCode.LeftShift;

    [Header("Display Settings")]
    [Tooltip("�O�_��ܧY�ɸ�T�b�e���W")]
    public bool showRealTimeInfo = true;

    [Tooltip("��T��ܪ���m")]
    public Vector2 infoDisplayPosition = new Vector2(10, 10);

    // �p���ܼ�
    private bool lastSkeletonState = true;
    private bool lastDepthImageState = true;
    private float[] scaleValues = { 0.5f, 1.0f, 1.5f }; // �w�]�Y���
    private int currentScaleIndex = 1; // �w�]�ϥ�1.0���Y��

    void Start()
    {
        InitializeReferences();
        PrintWelcomeMessage();
    }

    void Update()
    {
        HandleInputs();
    }

    /// <summary>
    /// ��l�Ʋե�Ѧ�
    /// </summary>
    private void InitializeReferences()
    {
        // �ˬd���n�ե�
        if (trackerHandler == null)
        {
            Debug.LogError("Easy_Functions: TrackerHandler_multi �����I�Цb Inspector �����w�C");
        }

        if (depthImageDisplay == null)
        {
            Debug.LogWarning("Easy_Functions: DepthImageDisplay �����I�`�׼v���\��N�L�k�ϥΡC");
        }
    }

    /// <summary>
    /// �B�z�Ҧ������J
    /// </summary>
    private void HandleInputs()
    {
        // �������f���
        if (Input.GetKeyDown(toggleSkeletonKey))
        {
            ToggleSkeleton();
        }

        // �����`�׼v�����
        if (Input.GetKeyDown(toggleDepthImageKey))
        {
            ToggleDepthImage();
        }

        // �C�L��e��T
        if (Input.GetKeyDown(printInfoKey))
        {
            PrintCurrentInfo();
        }

        // �����v��½��
        if (Input.GetKeyDown(toggleImageFlipKey))
        {
            ToggleImageFlip();
        }

        // �վ�v���Y��]Shift + 1/2/3�^
        if (Input.GetKey(scaleImageKey))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SetImageScale(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                SetImageScale(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                SetImageScale(2);
        }

        // ESC �䭫�m�Ҧ��]�w
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetAllSettings();
        }
    }

    /// <summary>
    /// ����w��T���M�ާ@����
    /// </summary>
    private void PrintWelcomeMessage()
    {
        Debug.Log("=== Easy_Functions �w�Ұ� ===");
        Debug.Log("�ާ@�����G");
        Debug.Log($"[{toggleSkeletonKey}] - �������f���");
        Debug.Log($"[{toggleDepthImageKey}] - �����`�׼v��");
        Debug.Log($"[{printInfoKey}] - �C�L������T");
        Debug.Log($"[{toggleImageFlipKey}] - �����v��½��");
        Debug.Log($"[{scaleImageKey} + 1/2/3] - �վ�v���Y�� (0.5x/1.0x/1.5x)");
        Debug.Log("[ESC] - ���m�Ҧ��]�w");
        Debug.Log("========================");
    }

    /// <summary>
    /// �b�e���W��ܧY�ɸ�T
    /// </summary>
    void OnGUI()
    {
        if (!showRealTimeInfo) return;

        GUIStyle style = new GUIStyle();
        style.fontSize = 14;
        style.normal.textColor = Color.white;
        style.fontStyle = FontStyle.Bold;

        // �إ߸�T��r
        string infoText = BuildRealTimeInfoText();

        // �p���r�ϰ�j�p
        Vector2 textSize = style.CalcSize(new GUIContent(infoText));
        Rect backgroundRect = new Rect(infoDisplayPosition.x - 5, infoDisplayPosition.y - 5,
                                     textSize.x + 10, textSize.y + 10);

        // ø�s�b�z���I��
        GUI.color = new Color(0, 0, 0, 0.7f);
        GUI.DrawTexture(backgroundRect, Texture2D.whiteTexture);
        GUI.color = Color.white;

        // ��ܤ�r
        GUI.Label(new Rect(infoDisplayPosition.x, infoDisplayPosition.y, textSize.x, textSize.y),
                 infoText, style);
    }

    /// <summary>
    /// �إߧY�ɸ�T��r
    /// </summary>
    private string BuildRealTimeInfoText()
    {
        string info = "=== Kinect Azure ���A ===\n";

        // �����H��
        int bodyCount = GetDetectedBodyCount();
        info += $"�����H��: {bodyCount}\n";

        // ���f��ܪ��A
        bool skeletonVisible = trackerHandler?.drawSkeletons ?? false;
        info += $"���f���: {(skeletonVisible ? "�}��" : "����")}\n";

        // �`�׼v�����A
        bool depthVisible = depthImageDisplay?.enableDisplay ?? false;
        info += $"�`�׼v��: {(depthVisible ? "���" : "����")}\n";

        // �`�׼v����T
        if (depthImageDisplay != null && depthVisible)
        {
            string depthInfo = depthImageDisplay.GetDepthImageInfo();
            info += $"{depthInfo}\n";
        }

        // ����Y����m�]�p�G���H�^
        if (bodyCount > 0)
        {
            Vector3 headPos = GetHeadPosition();
            if (headPos != Vector3.zero)
            {
                info += $"�Y����m: ({headPos.x:F2}, {headPos.y:F2}, {headPos.z:F2})\n";
                info += $"�Z��: {headPos.magnitude:F2}m\n";
            }
        }

        info += "\n=== �ާ@���� ===\n";
        info += $"[{toggleSkeletonKey}] ���f  [{toggleDepthImageKey}] �`��  [{printInfoKey}] ��T\n";
        info += $"[{toggleImageFlipKey}] ½��  [Shift+1/2/3] �Y��";

        return info;
    }

    #region ���}������� - �����\��

    /// <summary>
    /// �����e�����쪺�H��
    /// </summary>
    public int GetDetectedBodyCount()
    {
        if (trackerHandler == null) return 0;
        return trackerHandler.GetActiveBodyCount();
    }

    /// <summary>
    /// ������w�H�����Y���y�Ц�m�]���a�y�С^
    /// </summary>
    public Vector3 GetHeadPosition(uint bodyId = 0)
    {
        if (trackerHandler == null) return Vector3.zero;
        return trackerHandler.GetJointPosition(JointId.Head, bodyId);
    }

    /// <summary>
    /// ������w�H�����Y���@�ɮy�Ц�m
    /// </summary>
    public Vector3 GetHeadWorldPosition(uint bodyId = 0)
    {
        if (trackerHandler == null) return Vector3.zero;
        return trackerHandler.GetJointWorldPosition(JointId.Head, bodyId);
    }

    /// <summary>
    /// ����Ҧ������쪺�H���Y����m
    /// </summary>
    public List<Vector3> GetAllHeadPositions()
    {
        List<Vector3> headPositions = new List<Vector3>();

        if (trackerHandler == null) return headPositions;

        List<uint> bodyIds = trackerHandler.GetActiveBodyIds();

        foreach (uint bodyId in bodyIds)
        {
            Vector3 headPos = GetHeadPosition(bodyId);
            if (headPos != Vector3.zero)
            {
                headPositions.Add(headPos);
            }
        }

        return headPositions;
    }

    /// <summary>
    /// ������w���骺���`��m
    /// </summary>
    public Vector3 GetJointPosition(JointId jointId, uint bodyId = 0)
    {
        if (trackerHandler == null) return Vector3.zero;
        return trackerHandler.GetJointPosition(jointId, bodyId);
    }

    /// <summary>
    /// ����Ҧ������쪺����ID
    /// </summary>
    public List<uint> GetAllBodyIds()
    {
        if (trackerHandler == null) return new List<uint>();
        return trackerHandler.GetActiveBodyIds();
    }

    /// <summary>
    /// ����̱�����v�����H��ID
    /// </summary>
    public uint GetClosestBodyId()
    {
        if (trackerHandler == null) return 0;

        List<uint> bodyIds = GetAllBodyIds();
        if (bodyIds.Count == 0) return 0;

        uint closestId = bodyIds[0];
        float closestDistance = float.MaxValue;

        foreach (uint bodyId in bodyIds)
        {
            Vector3 headPos = GetHeadPosition(bodyId);
            float distance = headPos.magnitude;

            if (distance > 0 && distance < closestDistance)
            {
                closestDistance = distance;
                closestId = bodyId;
            }
        }

        return closestId;
    }

    #endregion

    #region ���}������� - ����\��

    /// <summary>
    /// �������f���
    /// </summary>
    public void ToggleSkeleton()
    {
        if (trackerHandler != null)
        {
            trackerHandler.turnOnOffSkeletons();
            bool isVisible = trackerHandler.drawSkeletons;
            Debug.Log($"���f��ܤw{(isVisible ? "�}��" : "����")}");
        }
        else
        {
            Debug.LogWarning("TrackerHandler_multi �����A�L�k�������f���");
        }
    }

    /// <summary>
    /// �]�w���f��ܪ��A
    /// </summary>
    public void SetSkeletonVisibility(bool visible)
    {
        if (trackerHandler != null && trackerHandler.drawSkeletons != visible)
        {
            trackerHandler.turnOnOffSkeletons();
        }
    }

    /// <summary>
    /// �����`�׼v�����
    /// </summary>
    public void ToggleDepthImage()
    {
        if (depthImageDisplay != null)
        {
            depthImageDisplay.ToggleDepthDisplay();
            bool isVisible = depthImageDisplay.enableDisplay;
            Debug.Log($"�`�׼v���w{(isVisible ? "���" : "����")}");
        }
        else
        {
            Debug.LogWarning("DepthImageDisplay �����A�L�k�����`�׼v�����");
        }
    }

    /// <summary>
    /// �����v��½��
    /// </summary>
    public void ToggleImageFlip()
    {
        if (depthImageDisplay != null)
        {
            // �b�����M����½�ध���`��
            bool currentH = depthImageDisplay.flipHorizontal;
            bool currentV = depthImageDisplay.flipVertical;

            if (!currentH && !currentV)
            {
                depthImageDisplay.SetImageFlip(true, false); // ����½��
                Debug.Log("�v���w�]�w������½��");
            }
            else if (currentH && !currentV)
            {
                depthImageDisplay.SetImageFlip(false, true); // ����½��
                Debug.Log("�v���w�]�w������½��");
            }
            else if (!currentH && currentV)
            {
                depthImageDisplay.SetImageFlip(true, true); // ��ӳ�½��
                Debug.Log("�v���w�]�w������+����½��");
            }
            else
            {
                depthImageDisplay.SetImageFlip(false, false); // ����½��
                Debug.Log("�v��½��w����");
            }
        }
        else
        {
            Debug.LogWarning("DepthImageDisplay �����A�L�k�����v��½��");
        }
    }

    /// <summary>
    /// �]�w�v���Y��
    /// </summary>
    public void SetImageScale(int scaleIndex)
    {
        if (depthImageDisplay != null && scaleIndex >= 0 && scaleIndex < scaleValues.Length)
        {
            currentScaleIndex = scaleIndex;
            float scale = scaleValues[scaleIndex];
            depthImageDisplay.SetImageScale(scale);
            Debug.Log($"�v���Y��w�]�w�� {scale}x");
        }
    }

    /// <summary>
    /// ���m�Ҧ��]�w
    /// </summary>
    public void ResetAllSettings()
    {
        Debug.Log("���m�Ҧ��]�w...");

        // ���m���f��ܬ��}��
        if (trackerHandler != null && !trackerHandler.drawSkeletons)
        {
            trackerHandler.turnOnOffSkeletons();
        }

        // ���m�`�׼v�������
        if (depthImageDisplay != null && !depthImageDisplay.enableDisplay)
        {
            depthImageDisplay.ToggleDepthDisplay();
        }

        // ���m�v���]�w
        if (depthImageDisplay != null)
        {
            depthImageDisplay.SetImageFlip(false, false);
            depthImageDisplay.SetImageScale(1.0f);
            currentScaleIndex = 1;
        }

        Debug.Log("�Ҧ��]�w�w���m���w�]��");
    }

    #endregion

    #region ���}������� - ��T�\��

    /// <summary>
    /// �C�L��e������T
    /// </summary>
    public void PrintCurrentInfo()
    {
        Debug.Log("=== ��e������T ===");

        if (trackerHandler == null)
        {
            Debug.Log("TrackerHandler_multi �����I");
            return;
        }

        int bodyCount = GetDetectedBodyCount();
        Debug.Log($"�����H��: {bodyCount}");

        if (bodyCount > 0)
        {
            List<uint> bodyIds = GetAllBodyIds();

            for (int i = 0; i < bodyIds.Count; i++)
            {
                uint bodyId = bodyIds[i];
                PrintBodyInfo(bodyId, i + 1);
            }
        }
        else
        {
            Debug.Log("�ثe�S�����������H��");
        }

        // ��ܨt�Ϊ��A
        PrintSystemStatus();

        Debug.Log("==================");
    }

    /// <summary>
    /// �C�L���w���骺�ԲӸ�T
    /// </summary>
    private void PrintBodyInfo(uint bodyId, int index)
    {
        Debug.Log($"--- �H�� {index} (ID: {bodyId}) ---");

        // �Y����m
        Vector3 headPos = GetHeadPosition(bodyId);
        if (headPos != Vector3.zero)
        {
            Debug.Log($"  �Y����m: ({headPos.x:F2}, {headPos.y:F2}, {headPos.z:F2})");
            Debug.Log($"  �Z����v��: {headPos.magnitude:F2}m");
        }

        // ���k���m
        Vector3 leftHand = GetJointPosition(JointId.HandLeft, bodyId);
        Vector3 rightHand = GetJointPosition(JointId.HandRight, bodyId);

        if (leftHand != Vector3.zero)
            Debug.Log($"  �����m: ({leftHand.x:F2}, {leftHand.y:F2}, {leftHand.z:F2})");

        if (rightHand != Vector3.zero)
            Debug.Log($"  �k���m: ({rightHand.x:F2}, {rightHand.y:F2}, {rightHand.z:F2})");

        // ���⨭��
        float height = EstimateBodyHeight(bodyId);
        if (height > 0)
            Debug.Log($"  ���⨭��: {height:F2}m");

        // �ˬd�O�_�|��
        bool isRaisingHands = IsRaisingBothHands(bodyId);
        Debug.Log($"  �O�_�|�_����: {(isRaisingHands ? "�O" : "�_")}");
    }

    /// <summary>
    /// �C�L�t�Ϊ��A
    /// </summary>
    private void PrintSystemStatus()
    {
        Debug.Log("--- �t�Ϊ��A ---");

        bool skeletonVisible = trackerHandler?.drawSkeletons ?? false;
        Debug.Log($"���f���: {(skeletonVisible ? "�}��" : "����")}");

        if (depthImageDisplay != null)
        {
            bool depthVisible = depthImageDisplay.enableDisplay;
            Debug.Log($"�`�׼v��: {(depthVisible ? "���" : "����")}");

            if (depthVisible)
            {
                string depthInfo = depthImageDisplay.GetDepthImageInfo();
                Debug.Log($"�`�׼v����T: {depthInfo}");
                Debug.Log($"�v���Y��: {scaleValues[currentScaleIndex]}x");
                Debug.Log($"����½��: {depthImageDisplay.flipHorizontal}");
                Debug.Log($"����½��: {depthImageDisplay.flipVertical}");
            }
        }
        else
        {
            Debug.Log("�`�׼v���ե�: �����");
        }
    }

    /// <summary>
    /// ������㪺�t�Ϊ��A�r��
    /// </summary>
    public string GetSystemStatusString()
    {
        string status = "=== Kinect Azure �t�Ϊ��A ===\n";
        status += $"�����H��: {GetDetectedBodyCount()}\n";
        status += $"���f���: {(trackerHandler?.drawSkeletons ?? false ? "�}��" : "����")}\n";

        if (depthImageDisplay != null)
        {
            status += $"�`�׼v��: {(depthImageDisplay.enableDisplay ? "���" : "����")}\n";
            if (depthImageDisplay.enableDisplay)
            {
                status += $"�`�׼v����T: {depthImageDisplay.GetDepthImageInfo()}\n";
            }
        }

        status += "========================";
        return status;
    }

    #endregion

    #region �i�����R�\��

    /// <summary>
    /// �p����w���骺�����]����^
    /// </summary>
    public float EstimateBodyHeight(uint bodyId = 0)
    {
        if (trackerHandler == null) return 0f;

        Vector3 headPos = GetJointPosition(JointId.Head, bodyId);
        Vector3 footLeftPos = GetJointPosition(JointId.FootLeft, bodyId);
        Vector3 footRightPos = GetJointPosition(JointId.FootRight, bodyId);

        if (headPos == Vector3.zero) return 0f;

        // �ϥθ��C���}�@�����
        float footY = Mathf.Min(footLeftPos.y, footRightPos.y);
        return Mathf.Abs(headPos.y - footY);
    }

    /// <summary>
    /// �ˬd���w����O�_�|�_����
    /// </summary>
    public bool IsRaisingBothHands(uint bodyId = 0)
    {
        Vector3 leftHand = GetJointPosition(JointId.HandLeft, bodyId);
        Vector3 rightHand = GetJointPosition(JointId.HandRight, bodyId);
        Vector3 head = GetJointPosition(JointId.Head, bodyId);

        if (leftHand == Vector3.zero || rightHand == Vector3.zero || head == Vector3.zero)
            return false;

        return leftHand.y > head.y && rightHand.y > head.y;
    }

    /// <summary>
    /// �ˬd���w����O�_�b���ۤ�ʧ@
    /// </summary>
    public bool IsWaving(uint bodyId = 0)
    {
        Vector3 rightHand = GetJointPosition(JointId.HandRight, bodyId);
        Vector3 rightShoulder = GetJointPosition(JointId.ShoulderRight, bodyId);

        if (rightHand == Vector3.zero || rightShoulder == Vector3.zero)
            return false;

        // �ˬd�k��O�_�b�ӻH���ץH�W
        return rightHand.y > rightShoulder.y;
    }

    #endregion
}
