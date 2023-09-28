using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class ACMovementComponent : MonoBehaviour
{
    Rigidbody _rb;
    ACInputHandler _input;
    [SerializeField] float _moveSpeed;
    private void Start()
    {
        //カーソルロック
        Cursor.lockState = CursorLockMode.Locked;
        //コンポーネント取得
        _rb = this.GetComponent<Rigidbody>();
        //入力ハンドラ取得
        _input = GameObject.FindAnyObjectByType<ACInputHandler>();
    }
    private void FixedUpdate()
    {
        //移動処理
        _rb.AddForce(this.transform.forward * _moveSpeed * _input.MoveInput.y);
        _rb.AddForce(this.transform.right * _moveSpeed * _input.MoveInput.x);
    }
}
