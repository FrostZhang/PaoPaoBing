using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class TextrueToT2 : MonoBehaviour
{
    [MenuItem("Assets/BTools/GuideTextureFormat(引导图片统一设置)")]
    static void GuideTextureFormat()
    {
        TextureFormat2Defult();
        TextureCut();
        TextureFormat();
    }
    [MenuItem("Assets/BTools/TextureCut(From 1334*750 To 1024 * 1024)")]
    static void TextureCut()
    {
        Object[] objects = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        if (objects.Length == 0) { return; }
        Texture2D textureOut = new Texture2D(1024, 1024, UnityEngine.TextureFormat.ARGB32, false);
        RenderTexture render = new RenderTexture(1334, 750, 24, RenderTextureFormat.ARGB32);
        for (int i = 0; i < objects.Length; i++)
        {
            string path = AssetDatabase.GetAssetPath(objects[i]);
            Texture2D texutre = objects[i] as Texture2D;
            if (texutre)
            {
                if (texutre.height != 750 || texutre.width != 1334) continue;
                Graphics.Blit(texutre, render); RenderTexture.active = render;
                //File.Move(path,path.Insert (path.LastIndexOf ("."), "_"));
                textureOut.ReadPixels(new Rect(0, 0, 1024, 750), 0, 274);
                textureOut.ReadPixels(new Rect(1024, 0, 310, 274), 0, 0);
                textureOut.ReadPixels(new Rect(1024, 274, 310, 274), 310, 0);
                textureOut.ReadPixels(new Rect(1024, 548, 310, 202), 620, 0);
                textureOut.Apply();
                /// 导出
                byte[] bytes = textureOut.EncodeToJPG();
                //System.IO.File.WriteAllBytes (path.Insert (path.LastIndexOf ("."), "_"), bytes);
                System.IO.File.WriteAllBytes(path, bytes);
                EditorUtility.DisplayProgressBar("TextureCut", path, (float)i / objects.Length);
            }
        }
        EditorUtility.ClearProgressBar();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
    }
    [MenuItem("Assets/BTools/TextureFormat(Format 1024 * 1024)")]
    static void TextureFormat()
    {
        Object[] objects = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        if (objects.Length == 0) { return; }
        for (int i = 0; i < objects.Length; i++)
        {
            Texture2D texutre = objects[i] as Texture2D;
            if (texutre)
            {
                if (texutre.height != 1024 || texutre.width != 1024) { continue; }
                string path = AssetDatabase.GetAssetPath(objects[i]);
                TextureImporter textureImporter = TextureImporter.GetAtPath(path) as TextureImporter;
                textureImporter.textureType = TextureImporterType.Default;
                textureImporter.npotScale = TextureImporterNPOTScale.None;
                textureImporter.isReadable = false;
                textureImporter.spriteImportMode = SpriteImportMode.None;
                textureImporter.mipmapEnabled = false;
                textureImporter.wrapMode = TextureWrapMode.Clamp; textureImporter.
                    filterMode = FilterMode.Point; textureImporter.maxTextureSize = 1024;
                //textureImporter.textureFormat = TextureImporterFormat.ETC_RGB4; 
                TextureImporterPlatformSettings Android = textureImporter.GetPlatformTextureSettings("Android");
                Android.format = TextureImporterFormat.ETC2_RGB4; Android.overridden = true;
                Android.maxTextureSize = 1024;
                TextureImporterPlatformSettings iPhone = textureImporter.GetPlatformTextureSettings("iPhone");
                iPhone.overridden = true;
                iPhone.format = TextureImporterFormat.ASTC_RGB_8x8; iPhone.maxTextureSize = 1024;
                textureImporter.SetPlatformTextureSettings(Android);
                textureImporter.SetPlatformTextureSettings(iPhone);
                //textureImporter.assetBundleName = path.Substring (0, path.LastIndexOf (".")).ToLower().Replace("resources-","resources"); 
                //textureImporter.assetBundleVariant = "ab"; 
                AssetDatabase.ImportAsset(path);
                EditorUtility.DisplayProgressBar("TextureFormat(Format 1024 * 1024)", path, (float)i / objects.Length);
            }
        }
        EditorUtility.ClearProgressBar();
    }
    [MenuItem("Assets/BTools/TextureFormat2Defult")]
    static void TextureFormat2Defult()
    {
        Object[] objects = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        if (objects.Length == 0) { return; }
        for (int i = 0; i < objects.Length; i++)
        {
            Texture2D texutre = objects[i] as Texture2D;
            if (texutre)
            {
                /* if (texutre.height != 1024 || texutre.width != 1024) { continue; } */
                string path = AssetDatabase.GetAssetPath(objects[i]);
                TextureImporter textureImporter = TextureImporter.GetAtPath(path) as TextureImporter;
                textureImporter.textureType = TextureImporterType.Default;
                textureImporter.npotScale = TextureImporterNPOTScale.None;
                textureImporter.isReadable = false; textureImporter.spriteImportMode = SpriteImportMode.None;
                textureImporter.mipmapEnabled = false;
                textureImporter.wrapMode = TextureWrapMode.Clamp;
                textureImporter.filterMode = FilterMode.Bilinear; textureImporter.maxTextureSize = 2048;
                //textureImporter.textureFormat = TextureImporterFormat.ETC_RGB4; 
                TextureImporterPlatformSettings Android = textureImporter.GetPlatformTextureSettings("Android");
                Android.format = TextureImporterFormat.ETC2_RGB4;
                Android.overridden = false;
                Android.maxTextureSize = 2048;
                TextureImporterPlatformSettings iPhone = textureImporter.GetPlatformTextureSettings("iPhone");
                iPhone.overridden = false; iPhone.format = TextureImporterFormat.ASTC_RGB_8x8;
                iPhone.maxTextureSize = 2048; textureImporter.SetPlatformTextureSettings(Android);
                textureImporter.SetPlatformTextureSettings(iPhone);
                textureImporter.assetBundleName = path.Substring(0, path.LastIndexOf(".")).ToLower().Replace("resources-", "resources");
                textureImporter.assetBundleVariant = "ab";
                AssetDatabase.ImportAsset(path);
                EditorUtility.DisplayProgressBar("TextureFormat2Defult", path, (float)i / objects.Length);
            }
        }
        EditorUtility.ClearProgressBar();
    }
}