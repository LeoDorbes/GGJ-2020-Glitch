using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class AnimationComposer : MonoBehaviour
    {
        [Header("COLOR PARAMS")]
        [SerializeField] private bool _animateColor;
        [SerializeField] private List<Image> _colorImages = new List<Image>();
        [SerializeField] private Color _endColor;
        [SerializeField] private float _colorAnimationDuration;
        [SerializeField] private bool _loopColorAnimation;
        private readonly List<Image> _initialColorImages = new List<Image>();
        
        [Header("OPACITY PARAMS")]
        [SerializeField] private bool _animateOpacity;
        [SerializeField] private List<Image> _opacityImages = new List<Image>();
        [SerializeField] private float _endOpacity;
        [SerializeField] private float _opacityAnimationDuration;
        [SerializeField] private bool _loopOpacityAnimation;
        private readonly List<Image> _initialOpacityImages = new List<Image>();

        [Header("SCALE PARAMS")]
        [SerializeField] private bool _animateScale;
        [SerializeField] private List<Transform> _scaleTransforms = new List<Transform>();
        [SerializeField] private float _endScale;
        [SerializeField] private float _scaleAnimationDuration;
        [SerializeField] private bool _loopScaleAnimation;
        private readonly List<Transform> _initialScaleTransforms = new List<Transform>();
        
        [Header("ROTATION PARAMS")]
        [SerializeField] private bool _animateRotation;
        [SerializeField] private List<Transform> _rotationTransforms = new List<Transform>();
        [SerializeField] private float _endRotation;
        [SerializeField] private float _rotationAnimationDuration;
        [SerializeField] private bool _loopRotationAnimation;
        private readonly List<Transform> _initialRotateTransforms = new List<Transform>();



        private void OnEnable()
        {
            if (_animateColor)
            {
                _initialColorImages.Clear();
                _colorImages.ForEach(i => _initialColorImages.Add(i));
                if (_loopColorAnimation)
                {
                    _colorImages.ForEach(image => image.DOColor(_endColor, _colorAnimationDuration).SetLoops(-1,  LoopType.Yoyo));
                }
                else
                {
                    _colorImages.ForEach(image => image.DOColor(_endColor, _colorAnimationDuration));
                }
            }

            if (_animateOpacity)
            {
                _initialOpacityImages.Clear();
                _opacityImages.ForEach(i => _initialOpacityImages.Add(i));
                if (_loopOpacityAnimation)
                {
                    _opacityImages.ForEach(image => image.DOColor(new Color(image.color.r, image.color.g, image.color.b, _endOpacity), _opacityAnimationDuration).SetLoops(-1, LoopType.Yoyo));
                }
                else
                {
                    _opacityImages.ForEach(image => image.DOColor(new Color(image.color.r, image.color.g, image.color.b, _endOpacity), _opacityAnimationDuration));
                }
            }
            
            if (_animateScale)
            {
                _initialScaleTransforms.Clear();
                _scaleTransforms.ForEach(t => _initialScaleTransforms.Add(t));
                if (_loopScaleAnimation)
                {
                    _scaleTransforms.ForEach(t => t.DOScale(_endScale, _scaleAnimationDuration).SetLoops(-1, LoopType.Yoyo));
                }
                else
                {
                    _scaleTransforms.ForEach(t => t.DOScale(_endScale, _scaleAnimationDuration));
                }
            }

            if (_animateRotation)
            {
                _initialRotateTransforms.Clear();
                _rotationTransforms.ForEach(t => _initialRotateTransforms.Add(t));
                var endRotation = new Vector3(transform.localRotation.x, transform.localRotation.y,
                    transform.localRotation.z + _endRotation);
                if (_loopRotationAnimation)
                {
                    _rotationTransforms.ForEach(t => t.DOLocalRotate(endRotation, _rotationAnimationDuration).SetLoops(-1, LoopType.Incremental));
                }
                else
                {
                    _rotationTransforms.ForEach(t => t.DOLocalRotate(endRotation, _rotationAnimationDuration));
                }
            }
        }

        private void OnDisable()
        {
            for (var i = 0; i < _colorImages.Count; i++)
            {
                _colorImages[i] = _initialColorImages[i];
                _colorImages[i].DOKill();
            }
            
            for (var i = 0; i < _opacityImages.Count; i++)
            {
                _opacityImages[i] = _initialOpacityImages[i];
                _opacityImages[i].DOKill();
            }
            
            for (var i = 0; i < _scaleTransforms.Count; i++)
            {
                _scaleTransforms[i] = _initialScaleTransforms[i];
                _scaleTransforms[i].DOKill();
            }
            
            for (var i = 0; i < _rotationTransforms.Count; i++)
            {
                _rotationTransforms[i] = _initialRotateTransforms[i];
                _rotationTransforms[i].DOKill();
            }
        }
    }
}
