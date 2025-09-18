using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;

public class KinectPostBuildProcessor : IPostprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    public void OnPostprocessBuild(BuildReport report)
    {
        if (report.summary.platform == BuildTarget.StandaloneWindows64)
        {
            CopyKinectFiles(report.summary.outputPath);
        }
    }

    private void CopyKinectFiles(string buildPath)
    {
        string buildDirectory = Path.GetDirectoryName(buildPath);

        // Azure Kinect SDK DLL 路徑
        string[] kinectPaths = {
            @"C:\Program Files\Azure Kinect Body Tracking SDK\tools",
            @"C:\Program Files\Azure Kinect Body Tracking SDK\sdk\netstandard2.0\release",
            @"C:\Program Files\Azure Kinect SDK v1.4.1\sdk\netstandard2.0\release"
        };

        // 需要複製的檔案列表
        string[] requiredFiles = {
           "onnxruntime.dll", "directml.dll","cudart64_110.dll",
            "cublas64_11.dll", "cublasLt64_11.dll", "curand64_10.dll",
            "cudnn64_8.dll", "dnn_model_2_0_op11.onnx", "dnn_model_2_0_lite_op11.onnx",

            "onnxruntime_providers_cuda.dll","onnxruntime_providers_shared.dll",
            "onnxruntime_providers_tensorrt.dll","nvrtc-builtins64_114.dll",
            "nvrtc64_112_0.dll","nvinfer_plugin.dll","nvinfer.dll",
            "directml.dll","cudnn_cnn_infer64_8.dll","cudnn_ops_infer64_8.dll",
            "cufft64_10.dll"
        };
        
        foreach (string kinectPath in kinectPaths)
        {
            if (Directory.Exists(kinectPath))
            {
                foreach (string fileName in requiredFiles)
                {
                    string sourcePath = Path.Combine(kinectPath, fileName);
                    if (File.Exists(sourcePath))
                    {
                        string destPath = Path.Combine(buildDirectory, fileName);
                        try
                        {
                            File.Copy(sourcePath, destPath, true);
                            Debug.Log($"已複製 Kinect 檔案: {fileName}");
                        }
                        catch (System.Exception e)
                        {
                            Debug.LogError($"複製檔案 {fileName} 失敗: {e.Message}");
                        }
                    }
                }
            }
        }
    }
}
