using UnityEngine;

public class SlimeGenerator : MonoBehaviour
{   
    [SerializeField]
    GameObject[] Slimes; // SlimeのPrefabリスト
    [SerializeField]
    int pops = 1; // 初期値1
    [SerializeField]
    float minSpawnRadius = 70.0f; // 最小生成半径
    [SerializeField]
    float spawnRadius = 20.0f; // 生成半径
    [SerializeField]
    float spawnInterval = 10.0f; // 10秒ごと
    [SerializeField]
    float popsIncreaseInterval = 30.0f; // pops増加間隔

    void Start()
    {
        // 10秒ごとにGenerateSlimesを呼び出す
        InvokeRepeating(nameof(GenerateSlimes), 0f, spawnInterval);

        // popsを30秒ごとに増加させる
        StartCoroutine(IncreasePops());
    }

    void GenerateSlimes()
    {
        for (int i = 0; i < pops; i++)
        {
            // Slimes配列からランダムに選択
            GameObject slimePrefab = Slimes[Random.Range(0, Slimes.Length)];

            float xPosision = Random.Range(-spawnRadius, spawnRadius);
            xPosision = SetPosition(xPosision);

            float zPosision = Random.Range(-spawnRadius, spawnRadius);
            zPosision = SetPosition(zPosision);

            // ランダムな位置を生成
            Vector3 spawnPosition = transform.position + new Vector3(
                xPosision,
                0,
                zPosision
            );

            // Slimeを生成
            Instantiate(slimePrefab, spawnPosition, Quaternion.identity);
        }
    }

    System.Collections.IEnumerator IncreasePops()
    {
        while (true)
        {
            yield return new WaitForSeconds(popsIncreaseInterval);
            pops++;
        }
    }

    float SetPosition(float anyPosision){
        if(anyPosision < 0){
            anyPosision -= minSpawnRadius;
        }
        else
        {
            anyPosision += minSpawnRadius;
        }
        return anyPosision;
    }
}
