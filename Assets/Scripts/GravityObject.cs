using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour {

    const float G = 2.5f;

    public float mass;
    public float rotation_speed;
    float radius;
    Vector3 position;

    public static List<GravityObject> GravityObjects;

    void OnEnable() {

        if (GravityObjects == null) {
            GravityObjects = new List<GravityObject>();
        }

        GravityObjects.Add(this);
    }

    void OnDisable() {
        GravityObjects.Remove(this);
    }

    void Awake() {
        radius = this.transform.lossyScale.x;
    }

    void FixedUpdate() {
        position = transform.position;

        foreach (GravityObject g_obj in GravityObjects) {
            if(g_obj != this) {
                ApplyGravity(g_obj);
            }
        }

        transform.Rotate(0, rotation_speed * Time.deltaTime, 0, Space.World);
    }

    void ApplyGravity(GravityObject gObj) {
        Vector3 direction = position - gObj.position;
        float distance = direction.sqrMagnitude;
        float boundaries = (this.radius + gObj.radius) / 2;
        Vector3 force_vector = Vector3.zero;

        force_vector = direction *  ((this.mass + gObj.mass) / distance);

        if (distance > boundaries) {
            gObj.transform.Translate(force_vector * Time.deltaTime);
        }
    }
}
