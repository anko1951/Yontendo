using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField]
    private float moveDefault = 5f; // 移動速度
    [SerializeField]
    private float jumpForce = 5f; // ジャンプの力
    [SerializeField]
    private float rotationSpeed = 700f; // 回転速度
    [SerializeField]
    private float minMass = 10f; // 最小重量

    private PlayerData playerData; // カスタムスクリプト(PlayerData)
    private Rigidbody rb;

    /* ここから playerData 参照ステータス */
    private float addSpeed = 0f;
    private float dashSpeed = 0f;
    private float addJump = 0f;
    private float antiGravity = 0f;

    private float moveSpeed = 0f;
    private bool isDash = false;
    //private bool isJump = false;

    void Awake()
    {
        GameObject playerDataObj = GameObject.FindWithTag("PlayerData"); // PlayerDataのタグを持つオブジェクトを取得
        if (playerDataObj != null)
        {
            playerData = playerDataObj.GetComponent<PlayerData>(); // PlayerDataスクリプトを取得
        }
    }

    void Start()
    {
        // Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
		
        if (playerData != null)
        {
            addSpeed = playerData.GetAddSpeed();
            dashSpeed = playerData.GetDashSpeed();
            addJump = playerData.GetAddJump();
            antiGravity = playerData.GetAntiGravity();

            if (antiGravity > 0)
            {
                float nowMass = rb.mass;
                float tempMass = nowMass - antiGravity;
                rb.mass = Mathf.Max(tempMass, minMass);
            }
        }
		
    }

    void Update()
    {
        if(transform.position.y < 0.6){
            //isJump = false ;
        }
        // LShiftで加速
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = moveDefault + dashSpeed;
            isDash = true;
        }
        else
        {
            moveSpeed = moveDefault + addSpeed;
            isDash = false;
        }

        // 上下キーで前後に移動
        float moveInput = Input.GetAxis("Vertical"); // W/S または ↑/↓
        if (moveInput != 0)
        {
            Debug.Log(moveInput);
            if (isDash)
            {
                if (playerData != null)
                {
                    playerData.useDush();
                }
            }
            else
            {
                if (playerData != null)
                {
                    playerData.useWalk();
                }
            }
        }

        Vector3 moveDirection = transform.forward * moveInput * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + moveDirection);

        // 左右キーで方向調整
        float rotateInput = Input.GetAxis("Horizontal"); // A/D または ←/→
        float rotation = rotateInput * rotationSpeed * Time.deltaTime;

        // オブジェクトの回転
        transform.Rotate(Vector3.up * rotation);

        // 減速ロジック
        rotation *= 0.01f;

        // 回転値が小さい場合に停止
        if (Mathf.Abs(rotation) <= 0.51f){
            rotation = 0;
        }


        // スペースキーでジャンプ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerData != null)
            {
                playerData.useJump();
            }
            //if(isJump){ return ;}
            Jump();
        }

        /*ここから食べる系*/
        // Zを押すと"Apple"を食べる
        if (Input.GetKeyDown(KeyCode.Z)){
            Food food = playerData.EatFood("Apple");
            if (food != null){
                food.Consume();
            }
        }

    }

    void Jump()
    {
        //if(isJump){ return ;}
        // ジャンプするための力を加える
        //isJump = true ;
        rb.AddForce(Vector3.up * (jumpForce + addJump), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 衝突した相手のタグが "Enemy" か確認
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player hit an enemy!"); // デバッグログで確認
            gameObject.SetActive(false); // プレイヤーを非アクティブ化
            playerData.SendMessage("Died"); // "died" メソッドを呼び出し
        }
	}

    // 食べ物だったら削除してplayerDataに入れる
    private void OnTriggerEnter(Collider other)
    {
        // 食べ物オブジェクトか確認
        Food food = other.GetComponent<Food>();
        if (food != null)
        {
            // PlayerDataに追加
            playerData.AddFood(food);

            // ゲームワールドから見えなくする
            other.gameObject.SetActive(false);

            Debug.Log($"Picked up a {food.Type}!");
        }
    }
}
