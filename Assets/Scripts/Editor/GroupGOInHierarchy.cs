using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UnityTransformSort : System.Collections.Generic.IComparer<GameObject>
{
    public int Compare(GameObject lhs, GameObject rhs)
    {
        if (lhs == rhs) return 0;
        if (lhs == null) return -1;
        if (rhs == null) return 1;
        return (lhs.transform.GetSiblingIndex() > rhs.transform.GetSiblingIndex()) ? 1 : -1;
    }
}

public class GroupGOInHierarchy : ScriptableWizard
{
    [MenuItem("Tools/Group selected GameObjects %g")]
    // Start is called before the first frame update
    static void GroupSelectedGO()
    {
        GameObject[] gos = Selection.gameObjects;
        System.Array.Sort(gos, new UnityTransformSort());

        GameObject empty = new GameObject("empty");
        empty.transform.SetParent(gos[0].transform.parent);
        empty.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        empty.transform.SetSiblingIndex(gos[0].transform.GetSiblingIndex());
        for (int i = 0; i < gos.Length; i++)
        {
            gos[i].transform.SetParent(empty.transform);
        }
    }
}
