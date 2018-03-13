﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeManager : MonoBehaviour {

    public GameObject player;
    public float biomeSize = 20;
    public int biomeNum = 0;
    public int biomeBuffer = 4;
    public List<GameObject> biomes = new List<GameObject>();
    public GameObject treesPrefab;
    public List<GameObject> biomeTypes;
    public GameObject startPrefab;
    public GameObject basePrefab;
    public Transform parent;

	// Update is called once per frame
	void Update () {
        biomeNum = Mathf.RoundToInt( Mathf.Abs( player.transform.position.x) / biomeSize) + biomeBuffer;
        for (int i = biomes.Count; i <= biomeNum ; i++)
        {
            GenerateBiome(i);
            //Despawn unused biome
            if (biomeNum - biomeBuffer - 2 >= 0)
            {
                biomes[biomeNum - biomeBuffer - 2].GetComponent<Biome>().DeSpawn();
            }
            if (i > 10)
            {
                return;
            }
        } 

	}

    public void GenerateBiome(int biomeNum)
    {
        //TODO: randomly choose biome type

        GameObject prefabToSpawn = treesPrefab;
        if( biomeNum == 0)
        {
            prefabToSpawn = startPrefab;
        }else if (biomeNum == 1)
        {
            prefabToSpawn = basePrefab;
        }
        else
        {
            //Choose a random biome from biometypes
            int r = Random.Range(0,biomeTypes.Count);

            prefabToSpawn = biomeTypes[r];
        }

        Vector3 pos = new Vector3(-1f*biomeNum*biomeSize - biomeSize/2,0,0);

        GameObject go = Instantiate(prefabToSpawn, pos, Quaternion.identity, parent).gameObject;

        biomes.Add(go);
    }
}
