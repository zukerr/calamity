using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        private PlayerShoot playerShoot;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if(!LevelController.Instance.IsGameFinished())
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (Cursor.lockState == CursorLockMode.None)
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                    else
                    {
                        playerShoot.Shoot();
                    }
                }
            }
        }
    }
}
