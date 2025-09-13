# \# Kinect Azure Eazy Functions �]�h�H�鰩�[�l�ܡ^



> ��� Microsoft �x�� \*\*Azure Kinect Unity Body-Tracking Sample\*\* ��˦Ӧ��A  

> ���Ʊ�u�ֳt���o���[��ơv���}�o�̯���}�c�Y�ΡC

##### ---

#### \## �S��²��

�@��ҰʡG���� `Kinect Launcher`�A�}�����Y�}�l�^���`�׻P���[�C  

Easy\_Functions API�G���Ѧh���`�Ψ禡�A���{���Y�iŪ���H�ơB���`�y�СB���� UI ��ܵ��C  

����d�ҡG`Samples/BasicExample/KinectAzure_EazyUse` �����ܽd�Y�ɰ����B�`�׼v����ܻP�ֱ���ާ@�C

##### ---

#### \## �w�˨B�J

1\. �U���ø��� \*\*Microsoft-Kinect-Azure-EazyFunctions\*\* �M�סC  

&nbsp;  ��ĳ�H \*\*Unity 6000.0.53f1\*\* �άۮe�����}�ҡC  

2\. �w�� \*\*Microsoft Azure Kinect SDK\*\* �P \*\*Body Tracking SDK\*\*  

&nbsp;  �]�ݥ]�t \*Kinect Azure Viewer\* �P \*Body Tracking\* ����^�C  

&nbsp;  \*\*? ���ˡI\*\* �Y�����T�w�ˡA�`�׬۾��N�L�k�B�@�C  

3\. �� Unity �M�פ��}�ҽd�ҳ����G  

&nbsp;  `Assets/Packages/com.megumi.kinect-azure-tracker/Samples/BasicExample/KinectAzure_EazyUse.unity`

##### ---

#### \## �d�ҳ�������

###### | ���� | �@�� |

###### |------|------|

###### | \*\*Environment\*\* | �O���P�a���A�§G���A�i�����C |

###### | \*\*Kinect Launcher\*\* | ��l�Ʋ`�׬۾��P���[�l�ܬy�{�C |

###### | \*\*Main Camera\*\* | ���������A�۾������L�S��\��C |

###### | \*\*Canvas\*\* | UI �ڪ���A���t�Y�ɲ`�׼v���C |

###### | \*\*How to use\*\* | ���� `Easy\_Functions` �}���A�i�����I�s�� Public ��k�C |

##### ---

#### \## Easy\_Functions ���}��k�@��

###### | ��k | ���� |

###### |------|------|

###### | `GetDetectedBodyCount()` | ���o�ثe�����쪺�H�ơC |

###### | `GetHeadPosition(bodyId=0)` | ���o���w�H���Y�������a�y�СC |

###### | `GetHeadWorldPosition(bodyId=0)` | ���o�Y�����@�ɮy�СC |

###### | `GetAllHeadPositions()` | ���o�Ҧ��H���Y���y�вM��C |

###### | `GetJointPosition(jointId, bodyId=0)` | ���o���N���`���a�y�СC |

###### | `GetAllBodyIds()` | ���o�Ҧ������쪺 body ID�C |

###### | `GetClosestBodyId()` | ���o�Z���̪񪺤H�� ID�C |

###### | `ToggleSkeleton()` / `SetSkeletonVisibility(bool)` | �����γ]�w���[��ܡC |

###### | `ToggleDepthImage()` / `SetDepthImageVisibility(bool)` | �����γ]�w�`�׼v����ܡC |

###### | `ToggleImageFlip()` | �`�������v��½��Ҧ��C |

###### | `SetImageScale(int index)` | �]�w�`�׼v���Y��]���� 0.5��/1��/1.5�ѡ^�C |

###### | `ResetAllSettings()` | ���m���[�B�v����ܻP�v���]�w�C |

###### | `PrintCurrentInfo()` | �b Console �L�X�����Ա��C |

###### | `GetSystemStatusString()` | ���o�t�Ϊ��A�r��C |

###### | `EstimateBodyHeight(bodyId=0)` | �ʦ������C |

###### | `IsRaisingBothHands(bodyId=0)` | �O�_�|����C |

##### ---

###### \## ��L�ֱ���]�d�ҳ����^

###### | �� | �\�� |

###### |----|------|

###### | \*\*S\*\* | �} / �����[��� |

###### | \*\*D\*\* | �} / ���`�׼v�� |

###### | \*\*I\*\* | Console �C�L������T |

###### | \*\*F\*\* | �v��½��Ҧ����� |

###### | \*\*Shift + 1/2/3\*\* | �`�׼v���Y�� 0.5�� / 1�� / 1.5�� |

###### | \*\*Esc\*\* | ���m�Ҧ��]�w |

##### ---

#### \## ���v

#### ���M�׶ȨѾǳN��s�P�оǥܽd�ϥΡC�п�u Microsoft Azure Kinect SDK �� Body Tracking SDK �x����v���ڡC

# 

#### ---------------------------

# 

# \# Kinect Azure Eazy Functions (Multi-Body Tracker)

> A plug-and-play wrapper around the official \*\*Azure Kinect Unity Body-Tracking Sample\*\*.  

> Grab real-time skeleton data in just a few lines of code.



#### \## Features

\* \*\*One-click launch\*\* �V `Kinect Launcher` starts depth capture and body tracking automatically.  

\* \*\*Easy\_Functions API\*\* �V call ready-made methods to get body count, joint positions or toggle UI.  

\* \*\*Complete sample\*\* �V see `Samples/BasicExample` for live tracking, depth view and hot-keys.



#### \## Installation

1\. Download and unzip \*\*Microsoft-Kinect-Azure-EazyFunctions\*\*.  

&nbsp;  Open with \*\*Unity 6000.0.53f1\*\* (or a compatible LTS).  

2\. Install \*\*Azure Kinect SDK\*\* and \*\*Body Tracking SDK\*\* including \*Kinect Azure Viewer\* and \*Body Tracking\* modules.  

&nbsp;  \*\*Required �V\*\* the depth camera will not work without them.  

3\. Open the sample scene:  

&nbsp;  `Assets/Packages/com.megumi.kinect-azure-tracker/Samples/BasicExample/KinectAzure_EazyUse`



#### \## Sample Scene Objects

###### | Object | Purpose |

###### |--------|---------|

###### | \*\*Environment\*\* | Lights and floor only. |

###### | \*\*Kinect Launcher\*\* | Initializes depth camera and body tracking. |

###### | \*\*Main Camera\*\* | Scene view. |

###### | \*\*Canvas\*\* | UI root, contains the live depth texture. |

###### | \*\*How to use\*\* | Holds the `Easy\_Functions` script �V call its methods directly. |

###### 

#### \## Easy\_Functions Public API

###### | Method | Description |

###### |--------|-------------|

###### | `GetDetectedBodyCount()` | Current number of tracked bodies. |

###### | `GetHeadPosition(bodyId=0)` | Local coordinates of the head joint. |

###### | `GetHeadWorldPosition(bodyId=0)` | World coordinates of the head joint. |

###### | `GetAllHeadPositions()` | List of all head positions. |

###### | `GetJointPosition(jointId, bodyId=0)` | Local coords of any joint. |

###### | `GetAllBodyIds()` | List of tracked body IDs. |

###### | `GetClosestBodyId()` | Returns the ID of the closest body. |

###### | `ToggleSkeleton()` / `SetSkeletonVisibility(bool)` | Toggle or set skeleton lines. |

###### | `ToggleDepthImage()` / `SetDepthImageVisibility(bool)` | Toggle or set depth view. |

###### | `ToggleImageFlip()` | Cycle through flip modes. |

###### | `SetImageScale(int index)` | Set depth view scale (0.5�� / 1�� / 1.5��). |

###### | `ResetAllSettings()` | Restore default display settings. |

###### | `PrintCurrentInfo()` | Log detailed tracking info to Console. |

###### | `GetSystemStatusString()` | Returns a status string. |

###### | `EstimateBodyHeight(bodyId=0)` | Rough height estimation. |

###### | `IsRaisingBothHands(bodyId=0)` | Checks if both hands are raised. |

# 

#### \## Hot-keys (Sample Scene)

###### | Key | Action |

###### |-----|--------|

###### | \*\*S\*\* | Toggle skeleton |

###### | \*\*D\*\* | Toggle depth view |

###### | \*\*I\*\* | Print tracking info |

###### | \*\*F\*\* | Flip image |

###### | \*\*Shift + 1/2/3\*\* | Scale image 0.5�� / 1�� / 1.5�� |

###### | \*\*Esc\*\* | Reset all settings |



#### \## License

For academic and educational use only. Please comply with Microsoft Azure Kinect SDK and Body Tracking SDK license terms.



