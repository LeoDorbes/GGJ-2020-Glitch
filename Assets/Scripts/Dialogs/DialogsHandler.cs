using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dialogs
{
    public class DialogsHandler : MonoBehaviour
    {
        [Serializable]
        public class DialogState
        {
            public string Text;
            public Sprite Background;
        }
        
        [SerializeField] private List<DialogState> _dialogs;
        [SerializeField] private GameObject _dialogBox;
        [SerializeField] private string _nextSceneName;

        private Image _dialogBoxBackground;
        private Text _dialogBoxText;

        private int _currentDialogIndex;
        private bool _isFading;

        private void Awake()
        {
            _dialogBoxBackground = _dialogBox.GetComponentInChildren<Image>();
            _dialogBoxText = _dialogBox.GetComponentInChildren<Text>();
            
            _dialogBoxBackground.sprite = _dialogs[_currentDialogIndex].Background;
            _dialogBoxText.text = _dialogs[_currentDialogIndex++].Text;
        }

        private void Update()
        {
            if (Input.GetButton("Jump") && !_isFading)
            {
                if (_currentDialogIndex < _dialogs.Count)
                {
                    StartCoroutine(NextDialog());
                }
                else
                {
                    SceneManager.LoadScene(_nextSceneName);
                }
            }
        }

        private IEnumerator NextDialog()
        {
            _isFading = true;
            _dialogBoxBackground.DOFade(0, 0.5f);
            _dialogBoxText.DOFade(0, 0.5f);
            yield return new WaitForSeconds(0.5f);

            _dialogBoxBackground.sprite = _dialogs[_currentDialogIndex].Background;
            _dialogBoxText.text = _dialogs[_currentDialogIndex++].Text;
            
            _dialogBoxBackground.DOFade(1, 0.5f);
            _dialogBoxText.DOFade(1, 0.5f);
            yield return new WaitForSeconds(0.5f);
            _isFading = false;
        }
    }
}