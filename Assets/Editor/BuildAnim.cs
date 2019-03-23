using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Linq;

public class BuildAnim : EditorWindow
{
    static BuildAnim myWindow;
    [MenuItem("Tools/CreateAnim")]
    static void Create()
    {
        myWindow = (BuildAnim)EditorWindow.GetWindow(typeof(BuildAnim), false, "BuildAnim", true);//创建窗口
        myWindow.Show();//展示
    }

    AnimatorController anim;
    Object folderObj;

    Object sel;
    string syspath = "Assets/Prefabs/Animator";
    private void OnGUI()
    {
        anim = EditorGUILayout.ObjectField("镜像原始Animator", anim,
            typeof(AnimatorController), true) as AnimatorController;
        folderObj = EditorGUILayout.ObjectField("保存Animator文件夹", folderObj,
            typeof(Object), true);
        syspath = EditorGUILayout.TextField("默认保存Animator文件夹", syspath);

        sel = EditorGUILayout.ObjectField(Selection.activeObject, typeof(Object), true);
        if (GUILayout.Button("开始生成"))
        {
            if (!sel)
            {
                Debug.Log("没有选中文件夹");
                return;
            }
            if (folderObj)
            {
                CreateAnim(anim, AssetDatabase.GetAssetPath(folderObj));
            }
            else
            {
                CreateAnim(anim, syspath);
            }
        }
    }

    public void CreateAnim(AnimatorController anim, string path)
    {
        var obj = Selection.activeObject;
        string selpath = AssetDatabase.GetAssetPath(obj);

        List<Sprite> sps;//= Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets).ToList();
        sps = new List<Sprite>();
        string spPath;
        for (int i = 1; i < 15; i++)
        {
            if (i < 10)
            {
                spPath = "/0" + i.ToString() + ".png";
            }
            else
            {
                spPath = "/" + i.ToString() + ".png";
            }
            sps.Add(AssetDatabase.LoadAssetAtPath<Sprite>(selpath + spPath));
        }

        if (sps.Count != 14)
        {
            Debug.Log("选中文件夹不是14张图");
            return;
        }

        sps.Sort((x, y) => string.Compare(x.name, y.name));

        EditorCurveBinding bing = EditorCurveBinding.PPtrCurve(null, typeof(SpriteRenderer), "m_Sprite");

        //0 1 2 3
        AnimationClip clip = new AnimationClip();
        AnimationClipSettings animationClipSettings = new AnimationClipSettings();
        animationClipSettings.additiveReferencePoseClip = clip;
        animationClipSettings.loopTime = true;
        AnimationUtility.SetAnimationClipSettings(clip, animationClipSettings);

        ObjectReferenceKeyframe[] idel = new ObjectReferenceKeyframe[5];
        for (int i = 0; i < 5; i++)
        {
            idel[i] = new ObjectReferenceKeyframe();
            idel[i].time = i * 0.333f;
            if (i == 4)
            {
                idel[i].value = sps[0];
            }
            else
            {
                idel[i].value = sps[i];
            }
        }

        AnimationUtility.SetObjectReferenceCurve(clip, bing, idel);

        AssetDatabase.CreateAsset(clip, AssetDatabase.GetAssetPath(Selection.activeObject) + "/" + "idle.anim");
        AssetDatabase.SaveAssets();

        //4
        AnimationClip clip2 = new AnimationClip();
        animationClipSettings.additiveReferencePoseClip = clip2;
        animationClipSettings.loopTime = false;
        AnimationUtility.SetAnimationClipSettings(clip2, animationClipSettings);
        ObjectReferenceKeyframe[] hit = new ObjectReferenceKeyframe[2];
        for (int i = 0; i < 2; i++)
        {
            hit[i] = new ObjectReferenceKeyframe();
            hit[i].time = i * 0.5f;
            hit[i].value = sps[4];
        }
        AnimationUtility.SetObjectReferenceCurve(clip2, bing, hit);
        AssetDatabase.CreateAsset(clip2, AssetDatabase.GetAssetPath(Selection.activeObject) + "/" + "hit.anim");
        AssetDatabase.SaveAssets();

        // 5 6 7 8
        AnimationClip clip3 = new AnimationClip();
        animationClipSettings.additiveReferencePoseClip = clip3;
        animationClipSettings.loopTime = true;
        AnimationUtility.SetAnimationClipSettings(clip3, animationClipSettings);
        ObjectReferenceKeyframe[] walk = new ObjectReferenceKeyframe[5];
        for (int i = 0; i < 5; i++)
        {
            walk[i] = new ObjectReferenceKeyframe();
            walk[i].time = i * 0.16667f;
            if (i == 4)
            {
                walk[i].value = sps[5];
            }
            else
            {
                walk[i].value = sps[i + 5];
            }
        }
        AnimationUtility.SetObjectReferenceCurve(clip3, bing, walk);
        AssetDatabase.CreateAsset(clip3, AssetDatabase.GetAssetPath(Selection.activeObject) + "/" + "walk.anim");
        AssetDatabase.SaveAssets();

        // 9  10  11  12  13
        AnimationClip clip4 = new AnimationClip();
        animationClipSettings.additiveReferencePoseClip = clip4;
        animationClipSettings.loopTime = false;
        AnimationUtility.SetAnimationClipSettings(clip4, animationClipSettings);
        ObjectReferenceKeyframe[] fight = new ObjectReferenceKeyframe[5];
        for (int i = 0; i < 5; i++)
        {
            fight[i] = new ObjectReferenceKeyframe();
            fight[i].time = i * 0.083333f;
            fight[i].value = sps[i + 9];
        }
        AnimationUtility.SetObjectReferenceCurve(clip4, bing, fight);
        AssetDatabase.CreateAsset(clip4, AssetDatabase.GetAssetPath(Selection.activeObject) + "/" + "fight.anim");
        AssetDatabase.SaveAssets();

        AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController(anim);
        animatorOverrideController["fight"] = clip4;
        animatorOverrideController["walk"] = clip3;
        animatorOverrideController["hit"] = clip2;
        animatorOverrideController["idle"] = clip;

        if (path != null)
        {
            AssetDatabase.CreateAsset(animatorOverrideController, path + "/" + Selection.activeObject.name + ".overrideController");
        }
        else
        {
            AssetDatabase.CreateAsset(animatorOverrideController, path + "/" + Selection.activeObject.name + "hero.overrideController");
        }
        AssetDatabase.SaveAssets();

    }
}
