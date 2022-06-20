﻿// Copyright © 2015-2021 Pico Technology Co., Ltd. All Rights Reserved.

using System;
using System.IO;
using UnityEditor;

public static class PXR_PathHelper
{
    public static string MakeRelativePath(string fromPath, string toPath)
    {
        var fromUri = new Uri(Path.GetFullPath(fromPath));
        var toUri = new Uri(Path.GetFullPath(toPath));

        if (fromUri.Scheme != toUri.Scheme)
        {
            return toPath;
        }

        var relativeUri = fromUri.MakeRelativeUri(toUri);
        var relativePath = Uri.UnescapeDataString(relativeUri.ToString());

        if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
        {
            relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        return relativePath;
    }

    public static string GetPXRPluginPath()
    {
        return Path.GetFullPath("Packages/com.unity.xr.picoxr/");
    }

    public static string GetPlayerActivityName()
    {
        return "\"" + PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android) + "/com.unity3d.player.UnityPlayerActivity\"";
    }
}
