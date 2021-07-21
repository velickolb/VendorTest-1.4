namespace VRTK.Examples
{
    using System;
    using UnityEngine;
    using UnityEngine.Animations;

    public class Screw : MonoBehaviour
    {
        public VRTK_InteractableObject linkedObject;
        public Transform head;

        public float spinSpeed = 180f;

        protected bool spinning;

        public GameObject bit;
        public GameObject target;

        public bool IsConnected;

        

        protected virtual void OnEnable()
        {
            linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed += InteractableObjectUsed;
                linkedObject.InteractableObjectUnused += InteractableObjectUnused;
            }

        }



        protected virtual void OnDisable()
        {
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
                linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
            }

        }

        protected virtual void Update()
        {
            if (spinning)
            {
                head.transform.Rotate(new Vector3(-1 * spinSpeed * Time.deltaTime,0f,0f));

                if(bit.GetComponent<SphereCollider>().bounds.Contains(target.transform.position) && target.transform.position.y > 0.5935f)
                {
                    target.transform.Rotate(new Vector3(0f,spinSpeed * Time.deltaTime, 0f));
                    target.transform.Translate(new Vector3(0f,Time.deltaTime*-0.01f, 0f));
                }
                else
                {
                    Debug.Log("NOT SCREWING");
                }

            }
        }

        protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {
            StartScrew();
        }

        protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
        {
            StopScrew();
        }

        protected virtual void StartScrew()
        {   
            spinning = true;
        }

        protected virtual void StopScrew()
        {
            spinning = false;
        }
    }
}