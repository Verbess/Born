using System;
using System.Collections.Generic;
using UnityEngine;

namespace JackUtil {

    public static class RigidBody2DExtention {

        // TODO 定期GC，内存优化
        static Dictionary<Rigidbody2D, float> xVecDic = new Dictionary<Rigidbody2D, float>();
        static Dictionary<Rigidbody2D, float> yVecDic = new Dictionary<Rigidbody2D, float>();
        static Dictionary<Rigidbody2D, float> crushTimeDic = new Dictionary<Rigidbody2D, float>();
        const float colSensitivity = 0.01f;

        public static void MoveAndSlide(this Rigidbody2D rig, float xAxis, float yAxis, float moveSpeed = 2.5f, float accelerate = 0.008f, float decelerate = 0.0032f) {

            float xVec;
            if (!xVecDic.ContainsKey(rig)) {
                xVecDic.Add(rig, 0);
                xVec = 0;
            } else {
                xVec = xVecDic.GetValue(rig);
            }

            float yVec;
            if (!yVecDic.ContainsKey(rig)) {
                yVecDic.Add(rig, 0);
                yVec = 0;
            } else {
                yVec = yVecDic.GetValue(rig);
            }

            // Vector2 _tempVec2 = Vector2.zero;

            if (xAxis != 0) {

                xAxis = Mathf.SmoothDamp(rig.velocity.x, moveSpeed * Time.fixedDeltaTime * 60 * xAxis.ToOne(), ref xVec, accelerate);

            } else {

                xAxis = Mathf.SmoothDamp(rig.velocity.x, 0, ref xVec, decelerate);

            }

            if (yAxis != 0) {

                yAxis = Mathf.SmoothDamp(rig.velocity.y, moveSpeed * Time.fixedDeltaTime * 60 * yAxis.ToOne(), ref yVec, accelerate);

            } else {

                yAxis = Mathf.SmoothDamp(rig.velocity.y, 0, ref yVec, decelerate);

            }

            rig.velocity = new Vector2(xAxis, yAxis);

        }

        public static void MoveInPlatform(this Rigidbody2D rig, float xAxis, float moveSpeed = 10.5f, float accelerate = 0.018f, float decelerate = 0.0032f, float tentToSpeed = 0) {

            float xVec;
            if (!xVecDic.ContainsKey(rig)) {
                xVecDic.Add(rig, 0);
                xVec = 0;
            } else {
                xVec = xVecDic.GetValue(rig);
            }

            if (xAxis != 0) {

                rig.velocity = new Vector2(Mathf.SmoothDamp(rig.velocity.x, moveSpeed * Time.fixedDeltaTime * 60 * xAxis.ToOne(), ref xVec, accelerate), rig.velocity.y);

            } else {

                rig.velocity = new Vector2(Mathf.SmoothDamp(rig.velocity.x, tentToSpeed * Time.fixedDeltaTime * 60, ref xVec, decelerate), rig.velocity.y);
 
            }

        }

        public static void MoveInAir(this Rigidbody2D rig, float xAxis, float moveSpeed = 10.5f, float accelerate = 0.018f, float decelerate = 0.0032f, float tentToSpeed = 0) {

            float xVec;
            if (!xVecDic.ContainsKey(rig)) {
                xVecDic.Add(rig, 0);
                xVec = 0;
            } else {
                xVec = xVecDic.GetValue(rig);
            }

            if (xAxis != 0) {

                rig.velocity = new Vector2(Mathf.SmoothDamp(rig.velocity.x, moveSpeed * Time.fixedDeltaTime * 60 * xAxis.ToOne(), ref xVec, accelerate), rig.velocity.y);

            }

        }

        public static void Jump(this Rigidbody2D rig, float jumpAxis, ref bool isJump, float jumpSpeed = 10f) {

            if (jumpAxis > 0 && !isJump) {
                rig.velocity = new Vector2(rig.velocity.x, jumpSpeed);
                isJump = true;
            }

        }

        public static void Falling(this Rigidbody2D rig, float jumpAxis, float maxFallingSpeed, float gravity = -16f, float fallMultiplier = 4.5f, float raiseJumpMultiplier = 2.5f) {

            // 下落
            if (rig.velocity.y <= 0) {

                rig.velocity += Vector2.up * gravity * fallMultiplier * Time.fixedDeltaTime;
            
            // 上升时 未托底
            } else if (rig.velocity.y > 0 && jumpAxis == 0) {

                rig.velocity += Vector2.up * gravity * fallMultiplier * Time.fixedDeltaTime;

            // 上升时 托底
            } else if (rig.velocity.y > 0 && jumpAxis != 0) {

                rig.velocity += Vector2.up * gravity * raiseJumpMultiplier * Time.fixedDeltaTime;

            } else {
                DebugHelper.LogError("其它情况?");
            }

            if (rig.velocity.y <= -maxFallingSpeed) {
                rig.velocity = new Vector2(rig.velocity.x, -maxFallingSpeed);
            }

        }

        public static void CircleBounce(this Rigidbody2D rig, Vector2 startPos, float force) {

            // 力的方向
            Vector2 dir = (Vector2)rig.position - startPos;

            // 设置弹力
            float xForce = (dir.normalized * force).x;
            if (rig.position.y >= startPos.y) {

                rig.velocity = new Vector2(xForce, force);

            } else {

                rig.velocity = new Vector2(xForce, rig.velocity.y - force * 0.5f);

            }

        }

        public static Collider2D CollideBox(this Rigidbody2D _rig, Vector2 _offSet, Vector2 _size, LayerMask _layer) {

            Collider2D _col = Physics2D.OverlapBox((Vector2)_rig.transform.position + _offSet, _size, 0, _layer);
            return _col; 

        }

        public static Collider2D CollideCircle(this Rigidbody2D _rig, Vector2 _offSet, float _radius, LayerMask _layer) {

            Collider2D _col = Physics2D.OverlapCircle((Vector2)_rig.transform.position + _offSet, _radius, _layer);
            return _col; 

        }

        public static Collider2D[] CollideAll(this Rigidbody2D _rig, Vector2 _offSet, Vector2 _size, LayerMask _layer) {

            Collider2D[] _cols = Physics2D.OverlapBoxAll((Vector2)_rig.transform.position + _offSet, _size, _layer);
            return _cols;

        }

        public static bool IsOnWall(this Rigidbody2D _rig, Vector2 _vec, LayerMask _layer) {

            Collider2D _collider = _rig.gameObject.GetComponent<Collider2D>();

            _vec = _vec * colSensitivity;

            Vector3 _size = _collider.bounds.size;
            _size.Set(_size.x + _vec.x, _size.y + _vec.y, 0);

            Collider2D _col = CollideBox(_rig, _collider.offset + _vec, _size, _layer);
            return _col != null;

        }

        public static bool IsOnFloor(this Rigidbody2D _rig, LayerMask _layer) {

            Collider2D _collider = _rig.gameObject.GetComponent<Collider2D>();

            // 方法一： 检测脚底大量点
            Vector2[] _footPos = new Vector2[10];
            float _xStart = _rig.transform.position.x - 0.1f;
            for (int i = 0; i < 10; i += 1) {

                _footPos[i] = new Vector2(0, _rig.transform.position.y + -_collider.bounds.size.y / 2f);

                RaycastHit2D _hit = Physics2D.Linecast(new Vector2(_xStart, _rig.transform.position.y), _footPos[i]);

                if (_hit.collider != null) {

                    if (_hit.collider.tag == "Wall") {

                        return true;

                    }

                }

            }

            return false;

            // // 方法二： 脚底Overlap
            // Collider2D[] _cols = Physics2D.OverlapBoxAll(_rig.transform.position, _collider.bounds.size, 0, _layer);

            // if (_cols.Length > 0) {
            //     return true;
            // } else {
            //     return false;
            // }

        }

        public static bool IsOnHorizontalWall(this Rigidbody2D _rig, Vector2 _horizontalOff, LayerMask _wallLayer, float _radius = 0.1f) {

            Collider2D _collider = _rig.gameObject.GetComponent<Collider2D>();
            _horizontalOff.Set(_horizontalOff.x * 0.4f, 0.2f);

            // Vector3 _size = new Vector3(0.1f, 0.01f, 0);

            Collider2D _col = CollideCircle(_rig, _horizontalOff, _radius, _wallLayer);
            return _col != null;

        }

        public static bool IsOnGround(this Rigidbody2D _rig, Vector2 _bottomOffset, LayerMask _groundLayer, float _radius = 0.1f) {

            Collider2D _collider = _rig.gameObject.GetComponent<Collider2D>();

            // Vector3 _size = new Vector3(_collider.bounds.size.x * 0.01f, 0.1f, 0);

            // 在玩家下面生成一个BOX判断
            // 位置 + 尺寸 + 角度 + 碰撞层
            Collider2D _col = CollideCircle(_rig, _bottomOffset, _radius, _groundLayer);
            return _col != null;

        }

        // public static bool IsOnGround(this Rigidbody2D _rig, LayerMask _groundLayer) {

        //     Collider2D _collider = _rig.gameObject.GetComponent<Collider2D>();
        //     Vector2 _vec = new Vector2(0, -0.5f + _collider.offset.y);

        //     // Vector3 _size = new Vector3(_collider.bounds.size.x * 0.01f, 0.1f, 0);

        //     // 在玩家下面生成一个BOX判断
        //     // 位置 + 尺寸 + 角度 + 碰撞层
        //     Collider2D _col = CollideCircle(_rig, _vec, 0.1f, _groundLayer);
        //     return _col != null;

        // }

        public static void ShootParabola(this Rigidbody2D _rig, Vector2 _targetPosition) {

            Vector2 _vec = FindInitialVelocity(_rig, _targetPosition);

            _rig.AddForce(_vec * _rig.mass, ForceMode2D.Impulse);

        }

        public static Vector2 FindInitialVelocity(this Rigidbody2D _rig, Vector2 _targetPosition) {

            // 速度向量
            Vector2 _vec = Vector2.zero;

            Vector2 _dir = _targetPosition - (Vector2)_rig.transform.position;

            float _range = _dir.magnitude;

            Vector2 _rigDir = _dir.normalized;

            float _maxYPos = _targetPosition.y;

            if (_maxYPos < _rig.transform.position.y) {

                _maxYPos = _rig.transform.position.y;

            }

            // Y方向的初始速度
            float _ft = -2f * Physics2D.gravity.y * (_maxYPos - _rig.transform.position.y);
            if (_ft < 0) {
                _ft = 0;
            }

            _vec.y = Mathf.Sqrt(_ft);

            // 最大时间
            _ft = -2f * (_maxYPos - _rig.transform.position.y) / Physics2D.gravity.y;
            if (_ft < 0) {
                _ft = 0;
            }
            float _timeToMax = Mathf.Sqrt(_ft);

            // 到达Y轴的时间
            _ft = -2f * (_maxYPos - _targetPosition.y) / Physics2D.gravity.y;
            if (_ft < 0) {
                _ft = 0;
            }

            float _timeToY = Mathf.Sqrt(_ft);

            float _totalTimeMax = _timeToMax + _timeToY;

            // 返回初始运动量
            float _horizontalMagnitude = _range / _totalTimeMax;

            _vec.x = _horizontalMagnitude * _rigDir.x;
            // _vec.y = _horizontalMagnitude * _rigDir.y;

            return _vec;

        }

    }
}