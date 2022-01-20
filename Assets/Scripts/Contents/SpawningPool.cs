using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{

    [SerializeField]
    int _monsterCount = 0;

    [SerializeField]
    int _keepMonsterCount = 0;

    [SerializeField]
    Vector3 _spawnPos;

    [SerializeField]
    float _spawnRadius = 15.0f;

    [SerializeField]
    float _spawnTime = 5.0f;

    private void AddMonsterCount(int value) { _monsterCount += value; }

    private void KeepMonsterCount(int count) { _keepMonsterCount = count; }
    
    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    
    void Update()
    {
        while(_monsterCount < _keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }

    
    IEnumerator ReserveSpawn()
    {

        yield return new WaitForSeconds(Random.Range(0, _spawnTime));

        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");

        Vector3 randPos;

        Vector3 randDir = Random.insideUnitSphere * _spawnRadius;
        randDir.y = 0;

        yield return null;
    }
}
