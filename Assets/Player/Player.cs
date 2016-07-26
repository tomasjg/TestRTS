﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RTS;

public class Player : MonoBehaviour {

    public string username;
    public bool human;
    public HUD hud;
    public WorldObject SelectedObject { get; set; }

    public int startMoney, startMoneyLimit, startPower, startPowerLimit;
    private Dictionary<ResourceType, int> resources, resourceLimits;

    // Use this for initialization
    void Start () {
        hud = GetComponentInChildren<HUD>();
        AddStartResourceLimits();
        AddStartResources();
    }
	
	// Update is called once per frame
	void Update () {
        if (human)
        {
            hud.SetResourceValues(resources, resourceLimits);
        }
    }

    ///////////////////// Parte 9 /////////////////////
    void Awake()
    {
        resources = InitResourceList();
        resourceLimits = InitResourceList();
    }

    // Inicialización
    private Dictionary<ResourceType, int> InitResourceList()
    {
        Dictionary<ResourceType, int> list = new Dictionary<ResourceType, int>();
        list.Add(ResourceType.Money, 0);
        list.Add(ResourceType.Power, 0);
        return list;
    }

    // add their start Resources(and limits).
    private void AddStartResourceLimits()
    {
        IncrementResourceLimit(ResourceType.Money, startMoneyLimit);
        IncrementResourceLimit(ResourceType.Power, startPowerLimit);
    }
    private void AddStartResources()
    {
        AddResource(ResourceType.Money, startMoney);
        AddResource(ResourceType.Power, startPower);
    }

    // helper methods to add values for each resource type
    public void AddResource(ResourceType type, int amount)
    {
        resources[type] += amount;
    }
    public void IncrementResourceLimit(ResourceType type, int amount)
    {
        resourceLimits[type] += amount;
    }

    ////////////////////// PARTE 10 //////////////////////
    public void AddUnit(string unitName, Vector3 spawnPoint, Vector3 rallyPoint, Quaternion rotation, Building creator)
    {
        Units units = GetComponentInChildren<Units>();
        GameObject newUnit = (GameObject)Instantiate(ResourceManager.GetUnit(unitName), spawnPoint, rotation);
        newUnit.transform.parent = units.transform;
        Unit unitObject = newUnit.GetComponent<Unit>();
        Debug.Log("add " + unitName + " to player");
        if (unitObject)
        {
            unitObject.Init(creator);
            if (spawnPoint != rallyPoint) unitObject.StartMove(rallyPoint);
        }
    }
}
