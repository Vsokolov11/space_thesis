using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : Environment {
    public float mass;
    public float rotation_speed;

    public Vector3 sideways_velocity;
    float radius;
    Vector3 position = Vector3.zero;

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
        Time.timeScale = time_multiplier;
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
        Vector3 direction = Vector3.Normalize(position - gObj.position);
        float distance =  Vector3.Distance(this.transform.position, gObj.transform.position);
        float boundaries = (this.radius + gObj.radius) / 2;
        Vector3 force_vector = Vector3.zero;


        force_vector = direction * (GR * ((this.mass * gObj.mass) / Mathf.Pow(distance, 2)));
        force_vector += gObj.sideways_velocity;
        force_vector /= gObj.mass;

        if (distance > boundaries) {
            gObj.transform.Translate(force_vector * Time.deltaTime, Space.Self);
        }

    }
}
