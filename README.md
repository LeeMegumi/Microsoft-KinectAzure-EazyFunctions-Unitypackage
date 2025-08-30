# Kinect Azure Multi-Body Tracker

A comprehensive Unity package for Microsoft Azure Kinect multi-body tracking.

�@�ӥ�����Unity Microsoft Azure Kinect�h�H��l�ܮM��C

## Features | �\��S��

- Multi-person skeleton tracking | �h�H���[�l��
- Real-time depth image display | �Y�ɲ`�׼v�����
- Easy-to-use API functions | ����ϥΪ�API�禡
- Configurable tracking parameters | �i�t�m�l�ܰѼ�
- Keyboard controls for quick testing | ��L����ֳt����

## Requirements | �t�λݨD

### Unity Version | Unity����
- **Recommended:** Unity 2022.3.53f1 or Unity 6000.0.53f1 (or later) | **��ĳ�G** Unity 2022.3.53f1��Unity 6000.0.53f1�]�Χ�s�����^
- These versions have been tested during development and will not produce errors | �o�Ǫ����w�b�}�o�L�{�����աA���|�X�{���~

### Required SDKs | ���nSDK
**Important:** You must install both SDKs for the Kinect Azure device to function properly.

**���n�����G** �z�����w�˳o���SDK�AKinect Azure�]�Ƥ~�ॿ�`�B��C

- Microsoft Azure Kinect SDK  
  [https://www.microsoft.com/en-us/download/details.aspx?id=101454](https://www.microsoft.com/en-us/download/details.aspx?id=101454)
- Azure Kinect Body Tracking SDK  
  [https://www.microsoft.com/en-us/download/details.aspx?id=104221](https://www.microsoft.com/en-us/download/details.aspx?id=104221)

### Development Environment | �}�o����
- Visual Studio with NuGet Package Manager | ���NuGet�M��޲z����Visual Studio

## Installation | �w�˻���

### Step 1: Install Required SDKs | �B�J1�G�w�˥��n��SDK

**Before proceeding, ensure you have installed both Azure Kinect SDKs from the links above.**

**�b�~�򤧫e�A�нT�O�z�w�q�W�z�s���w�ˤF���Azure Kinect SDK�C**

### Step 2: Install Unity Package | �B�J2�G�w��Unity�M��

1. Open Unity Package Manager | �}��Unity�M��޲z��
2. Click "Add package from git URL" | �I���u�qGit URL�K�[�M��v
3. Enter: `https://github.com/LeeMegumi/Microsoft-KinectAzure-EazyFunctions-Unitypackage.git`

### Step 3: Install Required NuGet Packages | �B�J3�G�w�˥��n��NuGet�M��

**Important:** You must install the following Microsoft NuGet packages in Visual Studio. It is recommended to choose the same versions as your installed SDKs to avoid compatibility issues.

**���n�����G** �z�����bVisual Studio���w�˥H�UMicrosoft NuGet�M��C��ĳ��ܻP�z�w�˪�SDK�ۦP�����A�H�קK�ۮe�ʰ��D�C

Open your Unity project in Visual Studio and install these packages via NuGet Package Manager:

�bVisual Studio���}�ұz��Unity�M�סA�óz�LNuGet�M��޲z���w�˥H�U�M��G

- `Microsoft.Azure.Kinect.Sensor` - Version 1.4.1
- `Microsoft.Azure.Kinect.BodyTracking` - Version 1.1.2  
- `Microsoft.Azure.Kinect.BodyTracking.ONNXRuntime` - Version 1.10.0

**How to install NuGet packages in Visual Studio:**
1. Right-click on your project in Solution Explorer
2. Select "Manage NuGet Packages..."
3. Go to "Browse" tab
4. Search for each package name and install the specified version

**�p��bVisual Studio���w��NuGet�M��G**
1. �b�ѨM����`�ޤ��k���I���z���M��
2. ��ܡu�޲zNuGet�M��...�v
3. �e���u�s���v����
4. �j�M�C�ӮM��W�٨æw�˫��w����

## Quick Start | �ֳt�}�l

1. Add `TrackerHandler_multi` to your scene | �N`TrackerHandler_multi`�K�[�������
2. Add `DepthImageDisplay` component for depth visualization | �K�[`DepthImageDisplay`����i��`�׵�ı��
3. Add `Easy_Functions` for convenient controls | �K�[`Easy_Functions`�H�K�󱱨�
4. Configure your tracking parameters in the Inspector | �b�ˬd�����t�m�l�ܰѼ�

## API Reference | API�Ѧ�

### Getting Detection Info | ���o������T
- `GetDetectedBodyCount()` - Get current number of detected people | ���o��e�����쪺�H��
- `GetHeadPosition(bodyId)` - Get head position coordinates | ���o�Y����m�y��
- `GetAllHeadPositions()` - Get all detected head positions | ���o�Ҧ������쪺�Y����m

### Control Functions | ����\��
- `ToggleSkeleton()` - Toggle skeleton display | �������[���
- `ToggleDepthImage()` - Toggle depth image display | �����`�׼v�����
- `PrintCurrentInfo()` - Print detailed detection information | �C�L�ԲӰ�����T

## Controls | �������

- **[S]** - Toggle skeleton display | �������[���
- **[D]** - Toggle depth image | �����`�׼v��
- **[I]** - Print detection info | �C�L������T
- **[F]** - Toggle image flip | �����v��½��
- **[Shift + 1/2/3]** - Scale image | �Y��v��

## Troubleshooting | �����Ƹ�

If you encounter issues with Kinect Azure device not working:
1. Ensure both Azure Kinect SDKs are properly installed
2. Verify that all NuGet packages are installed with versions matching your SDKs
3. Check that your device drivers are up to date
4. Make sure you're using the recommended Unity versions (2022.3.53f1 or 6000.0.53f1)

�p�G�J��Kinect Azure�]�ƵL�k�u�@�����D�G
1. �T�O���Azure Kinect SDK���w���T�w��
2. ���ҩҦ�NuGet�M�󳣤w�w�˥B�����P�z��SDK�ǰt
3. �ˬd�z���]���X�ʵ{���O�_���̷s����
4. �T�O�z�ϥΪ��O��ĳ��Unity�����]2022.3.53f1��6000.0.53f1�^

## License | ���v����

[Your License Here]
