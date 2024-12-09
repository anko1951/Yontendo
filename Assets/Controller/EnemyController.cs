using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
  [SerializeField]
    GameObject Player; // ターゲットとなるプレイヤーオブジェクト
    [SerializeField]
    float farm = 8.0f; // 距離の閾値
    [SerializeField]
    float Speed = 1.4f; // 移動速度
    [SerializeField]
    float jump = 17.2f;
    [SerializeField]
    float wanderRadius = 3.0f; // うろうろする範囲の半径
    [SerializeField]
    float wanderInterval = 1.0f; // うろうろ時の移動頻度
    [SerializeField]
    float rotationTolerance = 1.0f; // 角度の許容範囲

    private float jumpForce;
    private Rigidbody rb; // Rigidbodyを使用するための変数
    private bool isTracking = false; // 追跡モードのオンオフフラグ
    private PlayerData playerDataScript; // PlayerDataスクリプトの参照
    private Vector3 wanderTarget; // うろうろ時の目標地点

    void Awake()
    {
        // PlayerDataタグを持つオブジェクトを探す
        GameObject playerDataObject = GameObject.FindWithTag("PlayerData");
        if (playerDataObject != null)
        {
            // PlayerDataスクリプトを取得
            playerDataScript = playerDataObject.GetComponent<PlayerData>();
        }
        else
        {
            FindOutPlayerData();
        }
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null){
            Player = playerObject;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpForce = jump * rb.mass;

        // コルーチンを開始
        StartCoroutine(ToggleTracking());
        StartCoroutine(UpdateWanderTarget());
    }

    void FixedUpdate()
    {
        if (Player == null) return;

        rb.useGravity = !rb.useGravity;

        if (isTracking)
        {
            // 追跡モードの動作
            float distance = Vector3.Distance(transform.position, Player.transform.position);

            if (distance <= farm)
            {
                Vector3 direction = (Player.transform.position - transform.position).normalized;
                rb.AddForce(Vector3.up * (jumpForce) * Time.deltaTime, ForceMode.Impulse);
                rb.MovePosition(transform.position + direction * Speed * Time.fixedDeltaTime);
                Vector3 angle = new Vector3(0,30,0);
                // 回転を進行方向に合わせる
                RotateTowards(direction);
                // PlayerDataのUseHungryメソッドを呼び出す
                if (playerDataScript != null)
                {
                    playerDataScript.UseHungry();
                }
            }
        }
        else
        {
           Vector3 direction = (wanderTarget - transform.position).normalized;
           rb.MovePosition(transform.position + direction * Speed * Time.fixedDeltaTime);
        }
    }

    private void RotateTowards(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);

        // 角度差が許容範囲を超えている場合のみ回転を更新
        if (angleDifference > rotationTolerance)
        {
            rb.MoveRotation(targetRotation);
        }
    }

    // 追跡モードを0.2秒ごとに切り替えるコルーチン
    private IEnumerator ToggleTracking()
    {
        while (true)
        {
            bool previousTracking = isTracking;
            isTracking = !isTracking; // フラグを切り替え
            yield return new WaitForSeconds(0.2f); // 0.2秒待機
        }
    }

    private IEnumerator UpdateWanderTarget()
    {
        while (true)
        {
            if (!isTracking) // うろうろモードのとき
            {
                // 次の目標地点を設定
                Vector2 randomPoint = Random.insideUnitCircle * wanderRadius;
                wanderTarget = new Vector3(transform.position.x + randomPoint.x, transform.position.y, transform.position.z + randomPoint.y);
                Vector3 direction = (wanderTarget - transform.position).normalized;
                RotateTowards(direction);
                rb.AddForce(Vector3.up * (jumpForce) * Time.deltaTime, ForceMode.Impulse);
            }
            yield return new WaitForSeconds(wanderInterval); // 目標地点の更新間隔
        }
    }

    //強制タイトル
    private void FindOutPlayerData(){
        Debug.LogWarning("PlayerDataが見つかりませんでした！Titleへ戻ります。");
        SceneManager.LoadScene("Title");
    }

}
