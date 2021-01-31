using System;
using System.Text;
using UnityEngine;

[Serializable]
public struct Pos {

    public readonly static Pos zero;

    public float x { get; set; }
    public float y { get; set; }

    static Pos() {
        zero = new Pos(0, 0);
    }

    public Pos(Pos pos) {
        this.x = pos.x;
        this.y = pos.y;
    }

    public Pos(Vector2 v2) {
        this.x = v2.x;
        this.y = v2.y;
    }

    public Pos(float x, float y) {
        this.x = x;
        this.y = y;
    }

    public Pos(int x, int y) {
        this.x = x;
        this.y = y;
    }

    
    public void SetX(float x) {
        this.x = x;
    }
    public void SetY(float y) {
        this.y = y;
    }

    public float this[int index] {
        get {
            if (index == 0) {
                return x;
            } else if (index == 1) {
                return y;
            }
            throw new Exception("Index Must Be 0 or 1");
        }
    }

    public override string ToString() {
        string s = x + "," + y;
        return s;
    }

    public bool EqualPos(Pos targetPos) {
        if (x == targetPos.x && y == targetPos.y) {
            return true;
        } else {
            return false;
        }
    }

    public static bool EqualPos(Pos posA, Pos posB) {
        if (posA[0] == posB[0] && posA[1] == posB[1]) {
            return true;
        } else {
            return false;
        }
    }

    public static float GetDistance(Pos posA, Pos posB) {

        float _xoff = Math.Abs(posA[0] - posB[0]);
        float _yoff = Math.Abs(posA[1] - posB[1]);

        float _dis = _xoff > _yoff ? _xoff : _yoff;

        return _dis;
        
    }

    public static float GetOppositeRadius(Pos posA, Pos posB) {

        float xoff = Math.Abs(posA[0] - posB[0]);
        float yoff = Math.Abs(posA[1] - posB[1]);

        float radius = (float)Math.Sqrt(xoff * xoff + yoff * yoff);

        return radius;

    }

    public Vector3 ToV3() {
        return new Vector3(x, y, 0);
    }

}

public static class PosExtention {

    public static Pos SetPos(ref this Pos p, int x, int y) {
        p.SetX(x);
        p.SetY(y);
        return p;
    }

    public static Pos SetPos(ref this Pos p, Pos posTarget) {
        p.SetX(posTarget.x);
        p.SetY(posTarget.y);
        return p;
    }

    public static Pos SetAndReturn(this Pos p, int x, int y) {
        p.SetX(x);
        p.SetY(y);
        return p;
    }

    public static Pos SetAndReturn(this Pos p, Pos posTarget) {
        p.SetX(posTarget.x);
        p.SetY(posTarget.y);
        return p;
    }

}