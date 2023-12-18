using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ty.ProjectSubak.Lobby
{
    public class UIAnimation : MonoBehaviour
    {
        #region ENUM
        enum Type
        {
            ChangeWH,
            MovePos
        }
        #endregion

        #region Struct
        [Serializable]
        struct Effect
        {
            [Header("Basic inform")]
            public Type type;
            public float duration;

            [Header("Pos")]
            public RectTransform startPos;
            public RectTransform endPos;

            [Header("Width/Height")]
            public float startWidth;           
            public float startHeight;
            public float endWidth;
            public float endHeight;
        }
        #endregion

        #region SerializeField
        [Header("Rect Transform")]
        [SerializeField] private RectTransform rt;
        [Header("Effects")]
        [SerializeField] private List<Effect> effects;
        #endregion

        #region PublicField
        public bool isWorking = false;
        #endregion

        #region PublicMethod
        public void CallEffect(int n)
        {
            if (n >= effects.Count) return;
            Effect e = effects[n];
            isWorking = true;

            switch (e.type)
            {
                case Type.ChangeWH:                    
                    StartCoroutine(ChangeWH(e));
                    break;
                case Type.MovePos:
                    StartCoroutine(MovePos(e));
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Unity
        private void Start()
        {
            isWorking = false;
            if (rt == null) rt = GetComponent<RectTransform>();
            if (effects[0].startPos != null) rt.position = effects[0].startPos.position;
            rt.sizeDelta = new Vector2 (effects[0].startWidth, effects[0].startHeight);
        }
        #endregion

        #region IEnumerator
        private IEnumerator MovePos(Effect e, int fps = 60)
        {
            float totalFrame = e.duration * fps;
            float frameTime = 1.0f / fps;

            rt.position = e.startPos.position;
            Vector3 v = (e.endPos.position - e.startPos.position) / totalFrame;

            for (float i = 0; i < totalFrame; i++)
            {
                transform.position += v;
                yield return new WaitForSeconds(frameTime);
            }

            transform.position = e.endPos.position;
            isWorking = false;
            yield break;
        }
        private IEnumerator ChangeWH(Effect e, int fps = 60)
        {
            float totalFrame = e.duration * fps;
            float frameTime = 1.0f / fps;

            rt.sizeDelta = new Vector2(e.startWidth, e.startHeight);
            Vector2 v = new Vector2(e.endWidth - e.startWidth, e.endHeight - e.startHeight) / totalFrame;

            for (float i = 0; i < totalFrame; i++)
            {
                rt.sizeDelta += v;
                yield return new WaitForSeconds(frameTime);
            }

            rt.sizeDelta = new Vector2(e.endWidth, e.endHeight);
            isWorking = false;
            yield break;
        }
        #endregion
    }
}

