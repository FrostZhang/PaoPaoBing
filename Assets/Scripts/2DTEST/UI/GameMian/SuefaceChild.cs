using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

[DisallowMultipleComponent()]
public abstract class SurfaceChild : MonoBehaviour
{
    protected Transform tr;
    protected GameObject go;

    protected virtual void Awake()
    {
        go = gameObject;
        tr = go.transform;
    }

    public virtual void Open()
    {
        go.SetActive(true);
    }
    public virtual void Close(bool destroy)
    {
        if (destroy)
        {
            Destroy(go);
        }
        else
            go.SetActive(false);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            string str = AssetDatabase.GetAssetPath(gameObject);
            var p = Path.GetFileNameWithoutExtension(str);
            if (p == this.GetType().ToString())
            {
                return;
            }
            var e = Path.GetExtension(str);
            var f = Path.Combine(this.GetType().ToString() + e);
            string err = AssetDatabase.RenameAsset(str, f);
            Debugger.App.Log("UI_Prefab " + p + " 名称变更为 " + this.GetType().ToString());
        }
    }
#endif

}