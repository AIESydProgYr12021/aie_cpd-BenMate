using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;
using System.Threading;

public class Builder
{
    public static void Build()
    {
        string assetFolderPath = Application.dataPath;
        string pcFileName = assetFolderPath + "/../Builds/pc/DontTurn.exe";
        string webFileName = assetFolderPath + "/../Builds/web/";
	    string androidFilename = assetFolderPath + "/../Builds/android/DontTurn.apk";

        var scenes = EditorBuildSettings.scenes;
        var levels = scenes.Select(z => z.path).ToArray();

        Debug.Log("Starting WebGL Build");
        BuildPipeline.BuildPlayer(levels, webFileName, BuildTarget.WebGL, BuildOptions.None);
        Debug.Log("Finished WebGL Build");

        Debug.Log("Starting Windows Build");
        BuildPipeline.BuildPlayer(levels, pcFileName, BuildTarget.StandaloneWindows, BuildOptions.None);
        Debug.Log("Finished Windows Build");

        Debug.Log("Starting Android Build");
        BuildPipeline.BuildPlayer(levels, androidFilename, BuildTarget.Android, BuildOptions.None);
        Debug.Log("Finished Android Build");

        Thread.Sleep(2000);
    }
}
