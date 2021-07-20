namespace VRTK.Examples
{
    using UnityEngine;

    public class Screw : MonoBehaviour
    {
        public VRTK_InteractableObject linkedObject;
        public Transform head;

        public float spinSpeed = 360f;

        protected bool spinning;

        public GameObject bit;
        public GameObject target;

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
                head.transform.Rotate(new Vector3(spinSpeed * Time.deltaTime,0f,0f));

                if(bit.GetComponent<SphereCollider>().bounds.Contains(target.transform.position))
                {
                    Debug.Log("SCREWING");
                }
                else
                {
                    Debug.Log(" NOT SCREWING");
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