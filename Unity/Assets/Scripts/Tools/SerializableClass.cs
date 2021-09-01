using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Vector3Serializer
{
    public float x;
    public float y;
    public float z;

    public Vector3Serializer() { x = y = z = 0.0f; }
    public Vector3Serializer(Vector3 v3) { Fill(v3); }

    public void Fill(Vector3 v3)
    {
        x = v3.x;
        y = v3.y;
        z = v3.z;
    }

    public Vector3 V3 { get { return new Vector3(x, y, z); } }
}

[Serializable]
public class QuaternionSerializer
{
    public float x;
    public float y;
    public float z;
    public float w;

    public QuaternionSerializer() { x = y = z = w = 0.0f; }
    public QuaternionSerializer(Quaternion q) { Fill(q); }

    public void Fill(Quaternion q)
    {
        x = q.x;
        y = q.y;
        z = q.z;
        w = q.w;
    }

    public Quaternion Q { get { return new Quaternion(x, y, z, w); } }
}

[Serializable]
public class KeyFrameSerializer
{
    public float time;
    public float value;

    public KeyFrameSerializer(float t, float v)
    {
        time = t;
        value = v;
    }

    public void SetKeyframe(ref AnimationCurve _animCurve)
    {
        _animCurve.AddKey(time, value);
    }

    public void SetKeyframe(int _index, ref Keyframe[] _animCurve)
    {
        _animCurve[_index] = new Keyframe(time, value);
    }
}

[Serializable]
public class KeyFrameVector3Serializer
{
    public float time;
    public Vector3Serializer vector = new Vector3Serializer();

    public KeyFrameVector3Serializer(float t, Vector3 v3)
    {
        time = t;
        vector.Fill(v3);
    }

    public void SetKeyframe(ref AnimationCurve _animCurveX, ref AnimationCurve _animCurveY, ref AnimationCurve _animCurveZ)
    {
        _animCurveX.AddKey(time, vector.x);
        _animCurveY.AddKey(time, vector.y);
        _animCurveZ.AddKey(time, vector.z);
    }

    public void SetKeyframe(int _index, ref Keyframe[] _animCurveX, ref Keyframe[] _animCurveY, ref Keyframe[] _animCurveZ)
    {
        _animCurveX[_index] = new Keyframe(time, vector.x);
        _animCurveY[_index] = new Keyframe(time, vector.y);
        _animCurveZ[_index] = new Keyframe(time, vector.z);
    }
}

[Serializable]
public class KeyFrameQuaternionSerializer
{
    public float time;
    public QuaternionSerializer quaternion = new QuaternionSerializer();

    public KeyFrameQuaternionSerializer(float t, Quaternion q)
    {
        time = t;
        quaternion.Fill(q);
    }

    public void SetKeyframe(ref AnimationCurve _animCurveX, ref AnimationCurve _animCurveY, ref AnimationCurve _animCurveZ, ref AnimationCurve _animCurveW)
    {
        _animCurveX.AddKey(time, quaternion.x);
        _animCurveY.AddKey(time, quaternion.y);
        _animCurveZ.AddKey(time, quaternion.z);
        _animCurveW.AddKey(time, quaternion.w);
    }
}