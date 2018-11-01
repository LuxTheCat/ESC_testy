using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

class CubeLogicManager: ComponentSystem
{
    struct Cubes
    {
        public Rotate rotator;
        public Transform transform;
        public Rigidbody rigidbody;
    }

    protected override void OnUpdate()
    {
        foreach (var entities in GetEntities<Cubes>())
        {
            entities.transform.Rotate(0f, entities.rotator.speed * Time.deltaTime, 0f);


            foreach (Collider collider in Physics.OverlapSphere(entities.transform.position, entities.rotator.pullRadius))
            {
                Vector3 forceDirection = entities.transform.position - collider.transform.position;
                collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * entities.rotator.pullForce * Time.deltaTime);
            }
        }
    }
}
