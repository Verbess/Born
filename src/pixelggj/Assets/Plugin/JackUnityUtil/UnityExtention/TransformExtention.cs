using System;
using System.Collections.Generic;
using UnityEngine;

namespace JackUtil {

    public static class TransformExtention {

        public static T GunShoot<T>(this Transform _trans, T _bulletPrefab, Vector2 _targetPos, float _shootSpeed) where T: MonoBehaviour {

            Vector2 _pos = _trans.position; // 发射的起点坐标

            Vector2 _dir = _targetPos - _pos; // 发射方向

            T _go = GameObject.Instantiate(_bulletPrefab, _trans.parent); // 生成子弹

            _go.transform.position = _pos; // 子弹起始坐标

            _go.transform.rotation = _dir.To2DFaceRotation(); // 子弹面向

            Rigidbody2D _rig = _go.gameObject.GetComponent<Rigidbody2D>();

            if (_rig == null) {

                DebugHelper.LogError("子弹必须包含刚体");

                return null;

            }

            _rig.velocity = _dir.normalized * _shootSpeed; // 飞行

            // 忽略碰撞
            Collider2D _gunCol = _trans.gameObject.GetComponent<Collider2D>();

            if (_gunCol != null) {

                Collider2D _bulletCol = _go.GetComponent<Collider2D>();

                if (_bulletCol != null) {

                    Physics2D.IgnoreCollision(_gunCol, _bulletCol);

                }

            }

            return _go;

        }

    }
}