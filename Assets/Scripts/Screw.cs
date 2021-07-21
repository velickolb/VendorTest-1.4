namespace VRTK.Examples
{
    using System;
    using UnityEngine;
    using UnityEngine.Animations;
    using UnityEngine.Events;

    public class Screw : MonoBehaviour
    {
        public VRTK_InteractableObject linkedObject;
        public VRTK_SnapDropZone dropZone;
        public Transform head;

        public float spinSpeed = 180f;
        /// <summary>
        /// Is drill spinning
        /// </summary>
        protected bool isSpinning;
        /// <summary>
        /// Is hose connected to drill
        /// </summary>
        protected bool IsConnected = false;

        public GameObject bit;
        public GameObject target;
        /// <summary>
        /// Sound to play on hose connect
        /// </summary>
        public AudioClip ConnectSound;
        /// <summary>
        /// Point for attaching the hose
        /// </summary>
        public Transform HoseAttackPoint;
        /// <summary>
        /// Fired on screw reaching limit
        /// </summary>
        UnityEvent OnScrewed;
        public GameObject trigger;


        /// <summary>
        /// Info canvases
        /// </summary>
        public GameObject FinishedBoard,StartingBoard,ExitBoard,ScrewBoard;

        /// <summary>
        /// Warning canvases
        /// </summary>
        public GameObject HoseNotConnected;

        protected virtual void OnEnable()
        {
            linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed += InteractableObjectUsed;
                linkedObject.InteractableObjectUnused += InteractableObjectUnused;

                dropZone.ObjectSnappedToDropZone += ConnectHose;
            }

            if (OnScrewed == null)
                OnScrewed = new UnityEvent();

            OnScrewed.AddListener(Screwed);

        }




        protected virtual void OnDisable()
        {
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
                linkedObject.InteractableObjectUnused -= InteractableObjectUnused;

                dropZone.ObjectSnappedToDropZone -= ConnectHose;
            }

        }

        protected virtual void Update()
        {
            if (isSpinning)
            {

                head.transform.Rotate(new Vector3(-1 * spinSpeed * Time.deltaTime,0f,0f));

                if (bit.GetComponent<SphereCollider>().bounds.Contains(target.transform.position) && target.transform.localPosition.y > 0.5924f)
                {
                    target.transform.Rotate(new Vector3(0f, spinSpeed * Time.deltaTime, 0f));
                    target.transform.Translate(new Vector3(0f, Time.deltaTime * -0.01f, 0f));
                }
                else if (target.transform.localPosition.y < 0.5924f)
                {
                    OnScrewed.Invoke();
                }
                else
                {
                    Debug.Log("Not screwing");
                }

            }
            
        }

        protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {   
            if (IsConnected)
                StartScrew();
            else
                HoseNotConnected.SetActive(true);

            trigger.transform.localPosition -= new Vector3(0, 0, 0.01f);
            

        }

        protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
        {
            StopScrew();
            trigger.transform.localPosition += new Vector3(0, 0, 0.01f);
        }

        protected virtual void StartScrew()
        {
            isSpinning = true;
        }

        protected virtual void StopScrew()
        {
            isSpinning = false;
        }

        protected virtual void ConnectHose(object sender, SnapDropZoneEventArgs e)
        {   
            HoseAttackPoint.SetParent(dropZone.transform.parent.transform);
            HoseAttackPoint.Rotate(new Vector3(-90,0,0));
            HoseAttackPoint.localScale = new Vector3(0.09f, 0.09f, 0.09f);
            IsConnected = true;
            HoseNotConnected.SetActive(false);
            ScrewBoard.SetActive(true);
            StartingBoard.SetActive(false);
            print(dropZone.transform.parent.transform.name);
        }

        private void Screwed()
        {
            FinishedBoard.SetActive(true);
            ScrewBoard.SetActive(false);
            ExitBoard.SetActive(true);
        }

    }
}