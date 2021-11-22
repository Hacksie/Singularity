using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HackedDesign
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private List<Effector> effectors = new List<Effector>();
        [SerializeField] private float effectorRadius;
        [SerializeField] private Color nearestColor = Color.magenta;
        [SerializeField] private Color clearColor = Color.white;

        private Effector nearest;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdateNearest();
        }

        public void OnFire(InputValue value)
        {
            if(value.isPressed)
            {
                nearest?.On();   
            }
            else
            {
                nearest?.Off();
            }
        }


        private void UpdateNearest()
        {
            //Debug.Log("Update nearest");
            nearest = null;
            float currentDistance = float.MaxValue;
            foreach (var effector in effectors)
            {
                //effector.UpdateColor(clearColor);
                if(Game.Instance.CurrentBall != null && (nearest == null || (effector.transform.position - Game.Instance.CurrentBall.transform.position).sqrMagnitude < currentDistance))
                {
                    currentDistance = (effector.transform.position - Game.Instance.CurrentBall.transform.position).sqrMagnitude;
                    nearest = effector;
                }
            }

            //nearest?.UpdateColor(nearestColor);
        }
    }
}
