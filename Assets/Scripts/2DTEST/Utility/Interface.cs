using UnityEngine;
using FSM;

public interface IHurt
{
    void Hurt(Transform target, RoleData targetdata);
}

public interface IAnimaEvent
{
    void StateEvent(string statename, bool state);
}

public interface IMapitem
{
    Map.PlaceData mapdata { get; set; }
}
