using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool smooth;
        public float smoothTime = 5f;
        

        public Quaternion m_CharacterTargetRot;
        public Quaternion m_CameraTargetRot;

        void Start()
        {
        }
        public void Init(Transform character, Transform camera)
        {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
        }


        public void LookRotation(Transform character, Transform camera)
        {
            Cursor.lockState = CursorLockMode.Locked;

            float yRot = Input.GetAxis("Mouse X") * XSensitivity;
            float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

            m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

            if(clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

            if(smooth)
            {
                character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }
        }
        public void LookOveride(Transform character, Transform camera)
        {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;

            m_CharacterTargetRot.x = 0f;
            m_CharacterTargetRot.z = 0f;
            m_CameraTargetRot.z = 0f;
            m_CameraTargetRot.y = 0f;
        }
        public void CamGoBackAll(Transform character, Transform camera)
        {
            m_CameraTargetRot.x = 0f;

            m_CameraTargetRot.z = 0f;
            m_CameraTargetRot.y = 0f;
            camera.localRotation = m_CameraTargetRot;

        }
        public void CamGoBack(Transform character, Transform camera, float speed)
        {

            if (m_CameraTargetRot.x > 0)
            {
                m_CameraTargetRot.x -= 1f * Time.deltaTime * speed;

            }
            if (m_CameraTargetRot.x < 0)
            {
                m_CameraTargetRot.x += 1f * Time.deltaTime * speed;

            }

            if (m_CameraTargetRot.y > 0)
            {
                m_CameraTargetRot.y -= 1f * Time.deltaTime * speed;

            }
            if (m_CameraTargetRot.y < 0)
            {
                m_CameraTargetRot.y += 1f * Time.deltaTime * speed;

            }

            if (m_CameraTargetRot.z > 0)
            {
                m_CameraTargetRot.z -= 1f * Time.deltaTime * speed;

            }
            if (m_CameraTargetRot.z < 0)
            {
                m_CameraTargetRot.z += 1f * Time.deltaTime * speed;

            }





            camera.localRotation = m_CameraTargetRot;



        }


        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}
