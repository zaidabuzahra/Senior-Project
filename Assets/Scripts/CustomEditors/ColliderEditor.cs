using UnityEngine;
using UnityEditor;

namespace EditorTool
{
    [CustomEditor(typeof(CollisionTool))]
    public class ColliderEditor : Editor
    {
        public bool isTrigger, snapSpawn;
        public CollisionType chosenCollision;
        GameObject collisionObject;
        public override void OnInspectorGUI()
        {
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Collider Shape: ");
                chosenCollision = (CollisionType)EditorGUILayout.EnumPopup(chosenCollision);
                isTrigger = GUILayout.Toggle(isTrigger, " Is Trigger");
                snapSpawn = GUILayout.Toggle(snapSpawn, " Snap To Selection");
            }

            if (GUILayout.Button("Create"))
            {
                collisionObject = new GameObject();
                if (snapSpawn) collisionObject.transform.position = Selection.activeGameObject.transform.position;

                switch (chosenCollision)
                {
                    case CollisionType.Cube:
                        collisionObject.AddComponent<BoxCollider>();
                        break;
                    case CollisionType.Sphere:
                        collisionObject.AddComponent<SphereCollider>();
                        break;
                    default:
                        break;
                }
                collisionObject.GetComponent<Collider>().isTrigger = isTrigger;
            }
        }
    }

    public enum CollisionType
    {
        Cube,
        Sphere
    }
}
