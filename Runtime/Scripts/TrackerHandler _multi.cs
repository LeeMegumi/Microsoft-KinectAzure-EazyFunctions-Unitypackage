using System.Collections.Generic;
using UnityEngine;
using Microsoft.Azure.Kinect.BodyTracking;

public class TrackerHandler_multi : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject pointBodyPrefab; // �bInspector�����wpointBody�w�s��

    [Header("Tracking Settings")]
    public float maxTrackingDistance = 5.0f; // �̤j�l�ܶZ��
    public bool drawSkeletons = true;

    // �즳���M�g�r��
    public Dictionary<JointId, JointId> parentJointMap;
    Dictionary<JointId, Quaternion> basisJointMap;

    // �޲z�h�Ө��骺�r�� - �ϥΨ���ID�@��key
    private Dictionary<uint, GameObject> activeBodyObjects = new Dictionary<uint, GameObject>();
    private Dictionary<uint, Quaternion[]> bodyJointRotations = new Dictionary<uint, Quaternion[]>();

    // �s�W�G�x�s���`��m��T���r��
    private Dictionary<uint, Vector3[]> bodyJointPositions = new Dictionary<uint, Vector3[]>();

    Quaternion Y_180_FLIP = new Quaternion(0.0f, 1.0f, 0.0f, 0.0f);

    void Awake()
    {
        InitializeJointMappings();
    }

    void InitializeJointMappings()
    {
        parentJointMap = new Dictionary<JointId, JointId>();

        // �]�w���l���Y�M�g
        parentJointMap[JointId.Pelvis] = JointId.Count;
        parentJointMap[JointId.SpineNavel] = JointId.Pelvis;
        parentJointMap[JointId.SpineChest] = JointId.SpineNavel;
        parentJointMap[JointId.Neck] = JointId.SpineChest;
        parentJointMap[JointId.ClavicleLeft] = JointId.SpineChest;
        parentJointMap[JointId.ShoulderLeft] = JointId.ClavicleLeft;
        parentJointMap[JointId.ElbowLeft] = JointId.ShoulderLeft;
        parentJointMap[JointId.WristLeft] = JointId.ElbowLeft;
        parentJointMap[JointId.HandLeft] = JointId.WristLeft;
        parentJointMap[JointId.HandTipLeft] = JointId.HandLeft;
        parentJointMap[JointId.ThumbLeft] = JointId.HandLeft;
        parentJointMap[JointId.ClavicleRight] = JointId.SpineChest;
        parentJointMap[JointId.ShoulderRight] = JointId.ClavicleRight;
        parentJointMap[JointId.ElbowRight] = JointId.ShoulderRight;
        parentJointMap[JointId.WristRight] = JointId.ElbowRight;
        parentJointMap[JointId.HandRight] = JointId.WristRight;
        parentJointMap[JointId.HandTipRight] = JointId.HandRight;
        parentJointMap[JointId.ThumbRight] = JointId.HandRight;
        parentJointMap[JointId.HipLeft] = JointId.SpineNavel;
        parentJointMap[JointId.KneeLeft] = JointId.HipLeft;
        parentJointMap[JointId.AnkleLeft] = JointId.KneeLeft;
        parentJointMap[JointId.FootLeft] = JointId.AnkleLeft;
        parentJointMap[JointId.HipRight] = JointId.SpineNavel;
        parentJointMap[JointId.KneeRight] = JointId.HipRight;
        parentJointMap[JointId.AnkleRight] = JointId.KneeRight;
        parentJointMap[JointId.FootRight] = JointId.AnkleRight;
        parentJointMap[JointId.Head] = JointId.Pelvis;
        parentJointMap[JointId.Nose] = JointId.Head;
        parentJointMap[JointId.EyeLeft] = JointId.Head;
        parentJointMap[JointId.EarLeft] = JointId.Head;
        parentJointMap[JointId.EyeRight] = JointId.Head;
        parentJointMap[JointId.EarRight] = JointId.Head;

        // �]�w��Ǳ���
        Vector3 zpositive = Vector3.forward;
        Vector3 xpositive = Vector3.right;
        Vector3 ypositive = Vector3.up;

        Quaternion leftHipBasis = Quaternion.LookRotation(xpositive, -zpositive);
        Quaternion spineHipBasis = Quaternion.LookRotation(xpositive, -zpositive);
        Quaternion rightHipBasis = Quaternion.LookRotation(xpositive, zpositive);
        Quaternion leftArmBasis = Quaternion.LookRotation(ypositive, -zpositive);
        Quaternion rightArmBasis = Quaternion.LookRotation(-ypositive, zpositive);
        Quaternion leftHandBasis = Quaternion.LookRotation(-zpositive, -ypositive);
        Quaternion rightHandBasis = Quaternion.identity;
        Quaternion leftFootBasis = Quaternion.LookRotation(xpositive, ypositive);
        Quaternion rightFootBasis = Quaternion.LookRotation(xpositive, -ypositive);

        basisJointMap = new Dictionary<JointId, Quaternion>();

        basisJointMap[JointId.Pelvis] = spineHipBasis;
        basisJointMap[JointId.SpineNavel] = spineHipBasis;
        basisJointMap[JointId.SpineChest] = spineHipBasis;
        basisJointMap[JointId.Neck] = spineHipBasis;
        basisJointMap[JointId.ClavicleLeft] = leftArmBasis;
        basisJointMap[JointId.ShoulderLeft] = leftArmBasis;
        basisJointMap[JointId.ElbowLeft] = leftArmBasis;
        basisJointMap[JointId.WristLeft] = leftHandBasis;
        basisJointMap[JointId.HandLeft] = leftHandBasis;
        basisJointMap[JointId.HandTipLeft] = leftHandBasis;
        basisJointMap[JointId.ThumbLeft] = leftArmBasis;
        basisJointMap[JointId.ClavicleRight] = rightArmBasis;
        basisJointMap[JointId.ShoulderRight] = rightArmBasis;
        basisJointMap[JointId.ElbowRight] = rightArmBasis;
        basisJointMap[JointId.WristRight] = rightHandBasis;
        basisJointMap[JointId.HandRight] = rightHandBasis;
        basisJointMap[JointId.HandTipRight] = rightHandBasis;
        basisJointMap[JointId.ThumbRight] = rightArmBasis;
        basisJointMap[JointId.HipLeft] = leftHipBasis;
        basisJointMap[JointId.KneeLeft] = leftHipBasis;
        basisJointMap[JointId.AnkleLeft] = leftHipBasis;
        basisJointMap[JointId.FootLeft] = leftFootBasis;
        basisJointMap[JointId.HipRight] = rightHipBasis;
        basisJointMap[JointId.KneeRight] = rightHipBasis;
        basisJointMap[JointId.AnkleRight] = rightHipBasis;
        basisJointMap[JointId.FootRight] = rightFootBasis;
        basisJointMap[JointId.Head] = spineHipBasis;
        basisJointMap[JointId.Nose] = spineHipBasis;
        basisJointMap[JointId.EyeLeft] = spineHipBasis;
        basisJointMap[JointId.EarLeft] = spineHipBasis;
        basisJointMap[JointId.EyeRight] = spineHipBasis;
        basisJointMap[JointId.EarRight] = spineHipBasis;
    }

    public void updateTracker(BackgroundData trackerFrameData)
    {
        // �����e�˴��쪺�Ҧ�����ID
        HashSet<uint> currentBodyIds = new HashSet<uint>();
        HashSet<uint> validBodyIds = new HashSet<uint>(); // ���Ľd�򤺪�����ID

        // �B�z�Ҧ��˴��쪺����
        for (int i = 0; i < (int)trackerFrameData.NumOfBodies; i++)
        {
            Body skeleton = trackerFrameData.Bodies[i];
            uint bodyId = skeleton.Id;

            // �N�Ҧ��˴��쪺����ID�[�JcurrentBodyIds
            currentBodyIds.Add(bodyId);

            // �ˬd����O�_�b�l�ܽd��
            if (IsBodyInTrackingRange(skeleton))
            {
                validBodyIds.Add(bodyId);

                // �p�G�O�s������A�Ыع�����pointBody
                if (!activeBodyObjects.ContainsKey(bodyId))
                {
                    CreateBodyObject(bodyId);
                }

                // ��s���鰩�[
                UpdateBodySkeleton(skeleton, bodyId);
            }
        }
        Debug.Log(GetActiveBodyCount());
        // �����W�X�d��Τ��A�Q�˴��쪺����
        RemoveInactiveBodies(validBodyIds);
    }

    private bool IsBodyInTrackingRange(Body skeleton)
    {
        // �ˬdpelvis joint�O�_����
        var pelvisPosition = skeleton.JointPositions3D[(int)JointId.Pelvis];

        // �ˬdjoint position�O�_���ġ]���Ǳ��p�U�i��|�^�ǵL�ĭȡ^
        if (float.IsNaN(pelvisPosition.X) || float.IsNaN(pelvisPosition.Y) || float.IsNaN(pelvisPosition.Z) ||
            float.IsInfinity(pelvisPosition.X) || float.IsInfinity(pelvisPosition.Y) || float.IsInfinity(pelvisPosition.Z))
        {
            return false;
        }

        Vector3 pelvisPos = new Vector3((float)pelvisPosition.X, (float)pelvisPosition.Y, (float)pelvisPosition.Z);

        // �ˬd�O�_���s�V�q�]�i���ܵL�Ħ�m�^
        if (pelvisPos.magnitude < 0.01f)
        {
            return false;
        }

        return pelvisPos.magnitude <= maxTrackingDistance;
    }

    private void CreateBodyObject(uint bodyId)
    {
        if (pointBodyPrefab == null)
        {
            Debug.LogError("pointBodyPrefab is not assigned!");
            return;
        }

        // ��Ҥ�pointBody�w�s��
        GameObject newBodyObject = Instantiate(pointBodyPrefab, transform);
        newBodyObject.name = $"Body_{bodyId}";

        // �[�J�޲z�r��
        activeBodyObjects[bodyId] = newBodyObject;
        bodyJointRotations[bodyId] = new Quaternion[(int)JointId.Count];
        bodyJointPositions[bodyId] = new Vector3[(int)JointId.Count]; // �s�W�G��l�Ʀ�m�}�C

        Debug.Log($"Created body object for ID: {bodyId}");
    }

    private void UpdateBodySkeleton(Body skeleton, uint bodyId)
    {
        if (!activeBodyObjects.ContainsKey(bodyId))
            return;

        GameObject bodyObject = activeBodyObjects[bodyId];
        Quaternion[] jointRotations = bodyJointRotations[bodyId];
        Vector3[] jointPositions = bodyJointPositions[bodyId]; // �s�W�G�����m�}�C

        // ��s�C�����`
        for (int jointNum = 0; jointNum < (int)JointId.Count; jointNum++)
        {
            // �p�����`��m�M����
            Vector3 jointPos = new Vector3(
                skeleton.JointPositions3D[jointNum].X,
                -skeleton.JointPositions3D[jointNum].Y,
                skeleton.JointPositions3D[jointNum].Z
            );

            Quaternion jointRot = Y_180_FLIP * new Quaternion(
                skeleton.JointRotations[jointNum].X,
                skeleton.JointRotations[jointNum].Y,
                skeleton.JointRotations[jointNum].Z,
                skeleton.JointRotations[jointNum].W
            ) * Quaternion.Inverse(basisJointMap[(JointId)jointNum]);

            // �x�s����M��m��T
            jointRotations[jointNum] = jointRot;
            jointPositions[jointNum] = jointPos; // �s�W�G�x�s��m��T

            // ��s���`Transform
            Transform jointTransform = bodyObject.transform.GetChild(jointNum);
            jointTransform.localPosition = jointPos;
            jointTransform.localRotation = jointRot;

            // ��s���f�s�u
            UpdateBoneConnection(skeleton, bodyObject, jointNum);
        }
    }

    private void UpdateBoneConnection(Body skeleton, GameObject bodyObject, int jointNum)
    {
        const int boneChildNum = 0;
        Transform jointTransform = bodyObject.transform.GetChild(jointNum);

        if (parentJointMap[(JointId)jointNum] != JointId.Head && parentJointMap[(JointId)jointNum] != JointId.Count)
        {
            Vector3 jointPos = new Vector3(
                skeleton.JointPositions3D[jointNum].X,
                -skeleton.JointPositions3D[jointNum].Y,
                skeleton.JointPositions3D[jointNum].Z
            );

            Vector3 parentTrackerSpacePosition = new Vector3(
                skeleton.JointPositions3D[(int)parentJointMap[(JointId)jointNum]].X,
                -skeleton.JointPositions3D[(int)parentJointMap[(JointId)jointNum]].Y,
                skeleton.JointPositions3D[(int)parentJointMap[(JointId)jointNum]].Z
            );

            Vector3 boneDirectionTrackerSpace = jointPos - parentTrackerSpacePosition;
            Vector3 boneDirectionWorldSpace = transform.rotation * boneDirectionTrackerSpace;
            Vector3 boneDirectionLocalSpace = Quaternion.Inverse(jointTransform.rotation) * Vector3.Normalize(boneDirectionWorldSpace);

            Transform boneTransform = jointTransform.GetChild(boneChildNum);
            boneTransform.localScale = new Vector3(1, 20.0f * 0.5f * boneDirectionWorldSpace.magnitude, 1);
            boneTransform.localRotation = Quaternion.FromToRotation(Vector3.up, boneDirectionLocalSpace);
            boneTransform.position = jointTransform.position - 0.5f * boneDirectionWorldSpace;

            if (!boneTransform.gameObject.activeInHierarchy)
                boneTransform.gameObject.SetActive(true);
        }
        else
        {
            jointTransform.GetChild(boneChildNum).gameObject.SetActive(false);
        }
    }

    private void RemoveInactiveBodies(HashSet<uint> validBodyIds)
    {
        List<uint> bodiesToRemove = new List<uint>();

        // ��X�ݭn����������]���b���Ľd�򤺪�����^
        foreach (uint bodyId in activeBodyObjects.Keys)
        {
            if (!validBodyIds.Contains(bodyId))
            {
                bodiesToRemove.Add(bodyId);
            }
        }

        // �����L�Ī�����
        foreach (uint bodyId in bodiesToRemove)
        {
            if (activeBodyObjects.ContainsKey(bodyId))
            {
                GameObject bodyObjectToDestroy = activeBodyObjects[bodyId];
                activeBodyObjects.Remove(bodyId);
                bodyJointRotations.Remove(bodyId);
                bodyJointPositions.Remove(bodyId); // �s�W�G�P�ɲ�����m��T

                // �ϥ�Destroy�Ӥ��ODestroyImmediate�A�קK�bUpdate���X�{���D
                if (bodyObjectToDestroy != null)
                {
                    Destroy(bodyObjectToDestroy);
                }

                Debug.Log($"Removed body object for ID: {bodyId}");
            }
        }
    }

    public void turnOnOffSkeletons()
    {
        drawSkeletons = !drawSkeletons;

        // ��Ҧ����D�������H�����i����
        foreach (GameObject bodyObject in activeBodyObjects.Values)
        {
            for (int jointNum = 0; jointNum < (int)JointId.Count; jointNum++)
            {
                Transform jointTransform = bodyObject.transform.GetChild(jointNum);
                MeshRenderer jointRenderer = jointTransform.gameObject.GetComponent<MeshRenderer>();
                MeshRenderer boneRenderer = jointTransform.GetChild(0).GetComponent<MeshRenderer>();

                if (jointRenderer != null)
                    jointRenderer.enabled = drawSkeletons;
                if (boneRenderer != null)
                    boneRenderer.enabled = drawSkeletons;
            }
        }
    }

    /// <summary>
    /// ������w����M���`���۹����
    /// </summary>
    /// <param name="jointId">���`ID</param>
    /// <param name="bodyId">����ID�A�w�]��0�]�۰ʿ�ܲĤ@�Ө���^</param>
    /// <returns>���`���۹����</returns>
    public Quaternion GetRelativeJointRotation(JointId jointId, uint bodyId = 0)
    {
        // �p�G�S�����wbodyId�A�ϥβĤ@�ӧ�쪺����
        if (bodyId == 0 && activeBodyObjects.Count > 0)
        {
            bodyId = new List<uint>(activeBodyObjects.Keys)[0];
        }

        if (!bodyJointRotations.ContainsKey(bodyId))
            return Quaternion.identity;

        Quaternion[] jointRotations = bodyJointRotations[bodyId];
        JointId parent = parentJointMap[jointId];
        Quaternion parentJointRotationBodySpace = Quaternion.identity;

        if (parent == JointId.Count)
        {
            parentJointRotationBodySpace = Y_180_FLIP;
        }
        else
        {
            parentJointRotationBodySpace = jointRotations[(int)parent];
        }

        Quaternion jointRotationBodySpace = jointRotations[(int)jointId];
        Quaternion relativeRotation = Quaternion.Inverse(parentJointRotationBodySpace) * jointRotationBodySpace;

        return relativeRotation;
    }

    /// <summary>
    /// ������w����M���`�����a��m�]�۹��l�ܾ��^
    /// </summary>
    /// <param name="jointId">���`ID</param>
    /// <param name="bodyId">����ID�A�w�]��0�]�۰ʿ�ܲĤ@�Ө���^</param>
    /// <returns>���`�����a��m�A�p�G�䤣��h��^Vector3.zero</returns>
    public Vector3 GetJointPosition(JointId jointId, uint bodyId = 0)
    {
        // �p�G�S�����wbodyId�A�ϥβĤ@�ӧ�쪺����
        if (bodyId == 0 && activeBodyObjects.Count > 0)
        {
            bodyId = new List<uint>(activeBodyObjects.Keys)[0];
        }

        // �ˬd����O�_�s�b
        if (!bodyJointPositions.ContainsKey(bodyId))
        {
            Debug.LogWarning($"Body ID {bodyId} not found in joint positions!");
            return Vector3.zero;
        }

        // �ˬd���`ID�O�_����
        int jointIndex = (int)jointId;
        if (jointIndex < 0 || jointIndex >= bodyJointPositions[bodyId].Length)
        {
            Debug.LogWarning($"Invalid joint ID: {jointId}");
            return Vector3.zero;
        }

        return bodyJointPositions[bodyId][jointIndex];
    }

    /// <summary>
    /// ������w����M���`���@�ɦ�m
    /// </summary>
    /// <param name="jointId">���`ID</param>
    /// <param name="bodyId">����ID�A�w�]��0�]�۰ʿ�ܲĤ@�Ө���^</param>
    /// <returns>���`���@�ɦ�m�A�p�G�䤣��h��^Vector3.zero</returns>
    public Vector3 GetJointWorldPosition(JointId jointId, uint bodyId = 0)
    {
        Vector3 localPosition = GetJointPosition(jointId, bodyId);

        if (localPosition == Vector3.zero)
            return Vector3.zero;

        // �ഫ���@�ɮy��
        Vector3 worldPosition = transform.TransformPoint(localPosition);
        return worldPosition;
    }

    /// <summary>
    /// ������w���骺�Ҧ����`��m
    /// </summary>
    /// <param name="bodyId">����ID�A�w�]��0�]�۰ʿ�ܲĤ@�Ө���^</param>
    /// <returns>�]�t�Ҧ����`��m���}�C�A�p�G�䤣��h��^null</returns>
    public Vector3[] GetAllJointPositions(uint bodyId = 0)
    {
        // �p�G�S�����wbodyId�A�ϥβĤ@�ӧ�쪺����
        if (bodyId == 0 && activeBodyObjects.Count > 0)
        {
            bodyId = new List<uint>(activeBodyObjects.Keys)[0];
        }

        if (!bodyJointPositions.ContainsKey(bodyId))
        {
            Debug.LogWarning($"Body ID {bodyId} not found!");
            return null;
        }

        // �^�ǰ}�C���ƥ��A�קK�~���ק�
        Vector3[] allPositions = new Vector3[bodyJointPositions[bodyId].Length];
        System.Array.Copy(bodyJointPositions[bodyId], allPositions, bodyJointPositions[bodyId].Length);

        return allPositions;
    }

    /// <summary>
    /// �p�������`�������Z��
    /// </summary>
    /// <param name="jointId1">�Ĥ@�����`ID</param>
    /// <param name="jointId2">�ĤG�����`ID</param>
    /// <param name="bodyId">����ID�A�w�]��0�]�۰ʿ�ܲĤ@�Ө���^</param>
    /// <returns>�����`�������Z���A�p�G�L�k�p��h��^-1</returns>
    public float GetJointDistance(JointId jointId1, JointId jointId2, uint bodyId = 0)
    {
        Vector3 pos1 = GetJointPosition(jointId1, bodyId);
        Vector3 pos2 = GetJointPosition(jointId2, bodyId);

        if (pos1 == Vector3.zero || pos2 == Vector3.zero)
        {
            Debug.LogWarning($"Cannot calculate distance between {jointId1} and {jointId2} for body {bodyId}");
            return -1f;
        }

        return Vector3.Distance(pos1, pos2);
    }

    /// <summary>
    /// �ˬd���w���`����m�O�_����
    /// </summary>
    /// <param name="jointId">���`ID</param>
    /// <param name="bodyId">����ID�A�w�]��0�]�۰ʿ�ܲĤ@�Ө���^</param>
    /// <returns>��m�O�_����</returns>
    public bool IsJointPositionValid(JointId jointId, uint bodyId = 0)
    {
        Vector3 position = GetJointPosition(jointId, bodyId);

        // �ˬd�O�_���s�V�q�Υ]�t�L�ĭ�
        return position != Vector3.zero &&
               !float.IsNaN(position.x) && !float.IsNaN(position.y) && !float.IsNaN(position.z) &&
               !float.IsInfinity(position.x) && !float.IsInfinity(position.y) && !float.IsInfinity(position.z);
    }

    // �����e�l�ܪ�����ƶq
    public int GetActiveBodyCount()
    {
        return activeBodyObjects.Count;
    }

    // ����Ҧ����D���骺ID�C��
    public List<uint> GetActiveBodyIds()
    {
        return new List<uint>(activeBodyObjects.Keys);
    }

    // �ھ�ID��������H
    public GameObject GetBodyObject(uint bodyId)
    {
        return activeBodyObjects.ContainsKey(bodyId) ? activeBodyObjects[bodyId] : null;
    }

    // �M�z�Ҧ������H�]�b�ݭn���m�ɨϥΡ^
    public void ClearAllBodies()
    {
        List<uint> allBodyIds = new List<uint>(activeBodyObjects.Keys);

        foreach (uint bodyId in allBodyIds)
        {
            GameObject bodyObjectToDestroy = activeBodyObjects[bodyId];
            activeBodyObjects.Remove(bodyId);
            bodyJointRotations.Remove(bodyId);
            bodyJointPositions.Remove(bodyId); // �s�W�G�M�z��m��T

            if (bodyObjectToDestroy != null)
            {
                Destroy(bodyObjectToDestroy);
            }
        }

        Debug.Log("Cleared all body objects");
    }

    // ����ˬd�òM�z�L�Ī������H�]�i�H�b�ݭn�ɽեΡ^
    public void ForceCleanupInvalidBodies()
    {
        List<uint> bodiesToRemove = new List<uint>();

        foreach (var kvp in activeBodyObjects)
        {
            if (kvp.Value == null)
            {
                bodiesToRemove.Add(kvp.Key);
            }
        }

        foreach (uint bodyId in bodiesToRemove)
        {
            activeBodyObjects.Remove(bodyId);
            bodyJointRotations.Remove(bodyId);
            bodyJointPositions.Remove(bodyId); // �s�W�G�M�z��m��T
            Debug.Log($"Force removed invalid body object for ID: {bodyId}");
        }
    }

    void OnDestroy()
    {
        ClearAllBodies();
    }
}