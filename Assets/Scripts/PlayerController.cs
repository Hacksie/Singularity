using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

namespace HackedDesign
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private List<Effector> effectors = new List<Effector>();
        [SerializeField] private float effectorRadius;
        [SerializeField] private Spawner spawner;

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
                if(!Game.Instance.State.Playing)
                {
                    Game.Instance.SetPlaying();
                    return;
                }
                //Debug.Log("Fire");
                nearest?.On();
            }
            // else
            // {
            //     nearest?.Off();
            // }
        }


        private void UpdateNearest()
        {
            nearest = effectors.FirstOrDefault(e => e.isOver);
        }
    }
}
