using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

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

        private bool _sceneAlreadyLoading;


        private void Awake()
        {
            _dialogBoxBackground = _dialogBox.GetComponentInChildren<Image>();
            _dialogBoxText = _dialogBox.GetComponentInChildren<Text>();
            
            _dialogBoxBackground.sprite = _dialogs[_currentDialogIndex].Background;
            _dialogBoxText.text = _dialogs[_currentDialogIndex++].Text;

        }

        private void Update()
        {
            if (Input.anyKeyDown && !_isFading)
            {
                if (_currentDialogIndex < _dialogs.Count)
                {
                    StartCoroutine(NextDialog());
                }
                else
                {
                    if (_sceneAlreadyLoading)
                        return;
                    
                    _dialogBoxBackground.DOFade(0, 0.5f);
                    SceneManager.LoadSceneAsync(_nextSceneName);
                    _sceneAlreadyLoading = true;
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