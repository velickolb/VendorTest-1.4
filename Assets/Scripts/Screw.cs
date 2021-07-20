namespace VRTK.Examples
{
    using UnityEngine;

    public class Screw : MonoBehaviour
    {
        public VRTK_InteractableObject linkedObject;

        protected virtual void OnEnable()
        {
            linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            }
        }

        protected virtual void OnDisable()
        {
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
            }
        }

        protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {
            Screwing();
        }

        protected virtual void Screwing()
        {
            print("SCREWING");
        }
    }
}