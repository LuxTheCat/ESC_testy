using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

class CubeLogicManager: ComponentSystem
{
    public float time { get; private set; }
    public float timeSwitch { get; private set; }
    public bool turned { get; private set; }

    struct Cubes
    {
        public Rotate rotator;
        public Transform transform;
        public Rigidbody rigidbody;
    }

    protected override void OnUpdate()
    {
        time += Time.deltaTime;

        foreach (var entities in GetEntities<Cubes>())
        {
            entities.transform.Rotate(0f, entities.rotator.speed * Time.deltaTime, 0f);


            foreach (Collider collider in Physics.OverlapSphere(entities.transform.position, entities.rotator.pullRadius))
            {
                Vector3 forceDirection = entities.transform.position - collider.transform.position;
                collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * entities.rotator.pullForce * Time.deltaTime);
            }
        }


        if (time > timeSwitch)
        {
            timeSwitch += 20f;

            foreach (var entities in GetEntities<Cubes>())
            {
                entities.rotator.pullForce *= -1f;
                entities.rotator.pullRadius*= 2f;
            }
        }
    }

    protected override void OnStartRunning()
    {
        turned = true;
        timeSwitch = 20f;
    }
}
