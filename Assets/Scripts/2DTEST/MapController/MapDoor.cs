using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using System.Linq;

public class MapDoor : MonoBehaviour
{
    Transform tr;
    Transform body;
    public MapChild data { get; private set; }
    private Renderer[] mts;
    private Collider cd;
    float offset;
    public delegate void MapDoorHandle(bool inOut);
    public MapDoorHandle OnPlayerPass;

    void Awake()
    {
        tr = transform;
        body = tr.GetChild(0);
        offset = body.localPosition.y;
        body.localPosition = new Vector3(0, offset + tr.position.y + tr.position.z * 0.5f, 0);

        mts = tr.GetComponentsInChildren<Renderer>(true);
        cd = tr.GetComponent<Collider>();
    }

    private void OnDisable()
    {
        OnPlayerPass = null;
    }

    private void Update()
    {
        body.localPosition = new Vector3(0, offset + tr.position.y + tr.position.z * 0.5f, 0);
    }

    public void CloseDoor()
    {
        cd.enabled = false;
        foreach (var item in mts)
        {
            item.enabled = false;
        }
    }

    public void OpenDoor()
    {
        cd.enabled = true;
        foreach (var item in mts)
        {
            item.enabled = true;
        }
    }

    public void SetDoorData(MapChild chid)
    {
        data = chid;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameApp.Instance.mapController.SkipMap(data.mapid);
            OnPlayerPass?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OnPlayerPass?.Invoke(false);
    }
}
