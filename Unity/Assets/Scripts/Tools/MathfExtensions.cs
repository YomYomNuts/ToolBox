using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
    public static bool IsZero(this Vector3 v)
    {
        return Mathf.Approximately(v.x, 0.0f) && Mathf.Approximately(v.y, 0.0f) && Mathf.Approximately(v.z, 0.0f);
    }
    
    public static float Mult(this Vector3 v, Vector3 w)
    {
        return v.x * w.x + v.y * w.y + v.z * w.z;
    }
    
    public static float Div(this Vector3 v, Vector3 w)
    {
        return v.x / w.x + v.y / w.y + v.z / w.z;
    }

    public static int IsProjectedPointOnLineSegment(this Vector2 p, Vector2 v1, Vector2 v2)
    {
        Vector2 e1 = new Vector2(v2.x - v1.x, v2.y - v1.y);
        float recArea = Vector2.Dot(e1, e1);
        Vector2 e2 = new Vector2(p.x - v1.x, p.y - v1.y);
        float val = Vector2.Dot(e1, e2);
        return (val < 0 ? -1 : (val > recArea ? 1 : 0));
    }

    public static Vector2 GetProjectedPointOnLineFast(this Vector2 p, Vector2 v1, Vector2 v2)
    {
        Vector2 e1 = new Vector2(v2.x - v1.x, v2.y - v1.y);
        Vector2 e2 = new Vector2(p.x - v1.x, p.y - v1.y);
        float val = Vector2.Dot(e1, e2);
        float len2 = e1.x * e1.x + e1.y * e1.y;
        return new Vector2(v1.x + (val * e1.x) / len2, v1.y + (val * e1.y) / len2);
    }
}

public static class MathfExtensions
{
    public static bool LineSegementsIntersect(Vector3 p, Vector3 p2, Vector3 q, Vector3 q2, out Vector3 intersection)
    {
        intersection = new Vector3();

        Vector3 r = p2 - p;
        Vector3 s = q2 - q;
        var rxs = Vector3.Cross(r, s);
        var qp = q - p;
        var pq = p - q;
        var qpxr = Vector3.Cross(qp, r);

        // If r x s = 0 and qp x r = 0, then the two lines are collinear.
        if (rxs.IsZero() && qpxr.IsZero())
        {
            // 1. If either  0 <= qp * r <= r * r or 0 <= pq * s <= * s
            // then the two lines are overlapping,
            if ((0 <= qp.Mult(r) && qp.Mult(r) <= r.Mult(r)) || (0 <= pq.Mult(s) && pq.Mult(s) <= s.Mult(s)))
                return true;

            // 2. If neither 0 <= qp * r = r * r nor 0 <= pq * s <= s * s
            // then the two lines are collinear but disjoint.
            // No need to implement this expression, as it follows from the expression above.
            return false;
        }

        // 3. If r x s = 0 and qp x r != 0, then the two lines are parallel and non-intersecting.
        if (rxs.IsZero() && !qpxr.IsZero())
            return false;

        // t = qp x s / (r x s)
        var t = Vector3.Cross(qp, s).Div(rxs);

        // u = qp x r / (r x s)

        var u = Vector3.Cross(qp, r).Div(rxs);

        // 4. If r x s != 0 and 0 <= t <= 1 and 0 <= u <= 1
        // the two line segments meet at the point p + t r = q + u s.
        if (!rxs.IsZero() && (0 <= t && t <= 1) && (0 <= u && u <= 1))
        {
            // We can calculate the intersection point using either t or u.
            intersection = p + r * t;

            // An intersection was found.
            return true;
        }

        // 5. Otherwise, the two line segments are not parallel but do not intersect.
        return false;
    }
}
