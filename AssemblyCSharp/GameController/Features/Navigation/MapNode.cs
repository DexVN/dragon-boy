using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum MoveType { Left, Right, Waypoint, Capsule, SpaceShip }

public class MapExit
{
    public int DestMapID;

    public MoveType Type;

    public int TargetX; 

    public int TargetY;

    public short NpcTemplateId;

    public sbyte MenuSelectedItem;

    public MapExit(int destId, MoveType type, int targetX = -1, int targetY = -1)
    {
        DestMapID = destId;
        Type = type;
        TargetX = targetX;
        TargetY = targetY;
    }

    public MapExit(int destId, MoveType type, short npcTemplateId, sbyte menuSelectedItem)
    {
        DestMapID = destId;
        Type = type;
        UnityEngine.Debug.Log("[MapExit] NPC Template ID: " + npcTemplateId + ", Menu Selected Item: " + menuSelectedItem);
        NpcTemplateId = npcTemplateId;
        MenuSelectedItem = menuSelectedItem;
    }
}

public class MapNode
{
    public int MapID;

    public List<MapExit> Exits = new List<MapExit>();

    public MapNode(int id) { MapID = id; }
}