# Kinect Azure Multi-Body Tracker

A comprehensive Unity package for Microsoft Azure Kinect multi-body tracking.

一個全面的Unity Microsoft Azure Kinect多人體追蹤套件。

## Features | 功能特色

- Multi-person skeleton tracking | 多人骨架追蹤
- Real-time depth image display | 即時深度影像顯示
- Easy-to-use API functions | 易於使用的API函式
- Configurable tracking parameters | 可配置追蹤參數
- Keyboard controls for quick testing | 鍵盤控制快速測試

## Requirements | 系統需求

### Unity Version | Unity版本
- **Recommended:** Unity 2022.3.53f1 or Unity 6000.0.53f1 (or later) | **建議：** Unity 2022.3.53f1或Unity 6000.0.53f1（或更新版本）
- These versions have been tested during development and will not produce errors | 這些版本已在開發過程中測試，不會出現錯誤

### Required SDKs | 必要SDK
**Important:** You must install both SDKs for the Kinect Azure device to function properly.

**重要提醒：** 您必須安裝這兩個SDK，Kinect Azure設備才能正常運行。

- Microsoft Azure Kinect SDK  
  [https://www.microsoft.com/en-us/download/details.aspx?id=101454](https://www.microsoft.com/en-us/download/details.aspx?id=101454)
- Azure Kinect Body Tracking SDK  
  [https://www.microsoft.com/en-us/download/details.aspx?id=104221](https://www.microsoft.com/en-us/download/details.aspx?id=104221)

### Development Environment | 開發環境
- Visual Studio with NuGet Package Manager | 具備NuGet套件管理器的Visual Studio

## Installation | 安裝說明

### Step 1: Install Required SDKs | 步驟1：安裝必要的SDK

**Before proceeding, ensure you have installed both Azure Kinect SDKs from the links above.**

**在繼續之前，請確保您已從上述連結安裝了兩個Azure Kinect SDK。**

### Step 2: Install Unity Package | 步驟2：安裝Unity套件

1. Open Unity Package Manager | 開啟Unity套件管理器
2. Click "Add package from git URL" | 點擊「從Git URL添加套件」
3. Enter: `https://github.com/LeeMegumi/Microsoft-KinectAzure-EazyFunctions-Unitypackage.git`

### Step 3: Install Required NuGet Packages | 步驟3：安裝必要的NuGet套件

**Important:** You must install the following Microsoft NuGet packages in Visual Studio. It is recommended to choose the same versions as your installed SDKs to avoid compatibility issues.

**重要提醒：** 您必須在Visual Studio中安裝以下Microsoft NuGet套件。建議選擇與您安裝的SDK相同版本，以避免相容性問題。

Open your Unity project in Visual Studio and install these packages via NuGet Package Manager:

在Visual Studio中開啟您的Unity專案，並透過NuGet套件管理器安裝以下套件：

- `Microsoft.Azure.Kinect.Sensor` - Version 1.4.1
- `Microsoft.Azure.Kinect.BodyTracking` - Version 1.1.2  
- `Microsoft.Azure.Kinect.BodyTracking.ONNXRuntime` - Version 1.10.0

**How to install NuGet packages in Visual Studio:**
1. Right-click on your project in Solution Explorer
2. Select "Manage NuGet Packages..."
3. Go to "Browse" tab
4. Search for each package name and install the specified version

**如何在Visual Studio中安裝NuGet套件：**
1. 在解決方案總管中右鍵點擊您的專案
2. 選擇「管理NuGet套件...」
3. 前往「瀏覽」分頁
4. 搜尋每個套件名稱並安裝指定版本

## Quick Start | 快速開始

1. Add `TrackerHandler_multi` to your scene | 將`TrackerHandler_multi`添加到場景中
2. Add `DepthImageDisplay` component for depth visualization | 添加`DepthImageDisplay`元件進行深度視覺化
3. Add `Easy_Functions` for convenient controls | 添加`Easy_Functions`以便於控制
4. Configure your tracking parameters in the Inspector | 在檢查器中配置追蹤參數

## API Reference | API參考

### Getting Detection Info | 取得偵測資訊
- `GetDetectedBodyCount()` - Get current number of detected people | 取得當前偵測到的人數
- `GetHeadPosition(bodyId)` - Get head position coordinates | 取得頭部位置座標
- `GetAllHeadPositions()` - Get all detected head positions | 取得所有偵測到的頭部位置

### Control Functions | 控制功能
- `ToggleSkeleton()` - Toggle skeleton display | 切換骨架顯示
- `ToggleDepthImage()` - Toggle depth image display | 切換深度影像顯示
- `PrintCurrentInfo()` - Print detailed detection information | 列印詳細偵測資訊

## Controls | 控制按鍵

- **[S]** - Toggle skeleton display | 切換骨架顯示
- **[D]** - Toggle depth image | 切換深度影像
- **[I]** - Print detection info | 列印偵測資訊
- **[F]** - Toggle image flip | 切換影像翻轉
- **[Shift + 1/2/3]** - Scale image | 縮放影像

## Troubleshooting | 疑難排解

If you encounter issues with Kinect Azure device not working:
1. Ensure both Azure Kinect SDKs are properly installed
2. Verify that all NuGet packages are installed with versions matching your SDKs
3. Check that your device drivers are up to date
4. Make sure you're using the recommended Unity versions (2022.3.53f1 or 6000.0.53f1)

如果遇到Kinect Azure設備無法工作的問題：
1. 確保兩個Azure Kinect SDK都已正確安裝
2. 驗證所有NuGet套件都已安裝且版本與您的SDK匹配
3. 檢查您的設備驅動程式是否為最新版本
4. 確保您使用的是建議的Unity版本（2022.3.53f1或6000.0.53f1）

## License | 授權條款

[Your License Here]
