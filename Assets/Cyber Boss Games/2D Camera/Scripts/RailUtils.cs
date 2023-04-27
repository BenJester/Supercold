using System.Collections.Generic;
using UnityEngine;

public class RailUtils
{

    public static Vector3 ProjectPositionOnRail(Vector3 pos, List<RailAnchor2D> anchors) {
        int closestNodeIndex = GetClosestNode(pos, anchors);

        if (closestNodeIndex == 0) {
            return ProjectOnSegment(anchors[0].transform.position, 
                                    anchors[1].transform.position, 
                                    pos);
        } else if (closestNodeIndex == anchors.Count - 1) {
            return ProjectOnSegment(anchors[anchors.Count - 1].transform.position, 
                                    anchors[anchors.Count - 2].transform.position, 
                                    pos);
        } else {
            Vector3 leftSeg = ProjectOnSegment(anchors[closestNodeIndex-1].transform.position, 
                                                anchors[closestNodeIndex].transform.position, 
                                                pos);
            Vector3 rightSeg = ProjectOnSegment(anchors[closestNodeIndex+1].transform.position, 
                                                anchors[closestNodeIndex].transform.position, 
                                                pos);

            if ((pos - leftSeg).sqrMagnitude <= (pos - rightSeg).sqrMagnitude) {
                return leftSeg;
            } else {
                return rightSeg;
            }
        }
    }

    public static int GetClosestNode(Vector3 pos, List<RailAnchor2D> anchors) {
        int closestNodeIndex = 0;
        float shortestDistance = 0.0f;

        for (int i = 0; i < anchors.Count; i++) {
            float sqrDistance = (anchors[i].transform.position - pos).sqrMagnitude;
            if (shortestDistance == 0.0f || sqrDistance < shortestDistance) {
                shortestDistance = sqrDistance;
                closestNodeIndex = i;
            }
        }
        
        return closestNodeIndex;
    }

    private static Vector3 ProjectOnSegment(Vector3 v1, Vector3 v2, Vector3 pos) {
        Vector3 v1ToPos = pos - v1;
        Vector3 segDirection = (v2 - v1).normalized;

        float distanceFromV1 = Vector3.Dot(segDirection, v1ToPos);
        if (distanceFromV1 < 0.0f) {
            return v1;
        } else if (distanceFromV1 * distanceFromV1 > (v2-v1).sqrMagnitude) {
            return v2;
        } else {
            Vector3 fromV1 = segDirection * distanceFromV1;
            return v1 + fromV1;
        }
    }
}