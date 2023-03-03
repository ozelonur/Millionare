using OrangeBear.Core;
using UnityEngine;

namespace _GAME_.Scripts.Managers
{
    public class SoundManager : Manager<SoundManager>
    {
        #region Serialized Fields

        [Header("Sounds")] [SerializeField] private AudioSource gameOpenMusic;
        [SerializeField] private AudioSource correctAnswerSound;
        [SerializeField] private AudioSource wrongAnswerSound;
        [SerializeField] private AudioSource questionSound;

        #endregion

        #region MonoBehavior Methods

        private void Start()
        {
            gameOpenMusic.Play();
        }

        #endregion

        #region Public Methods

        public void PlayGameOpenMusic()
        {
            StopAllMusic();
            gameOpenMusic.Play();
        }

        public void PlayCorrectAnswerSound()
        {
            StopAllMusic();
            correctAnswerSound.Play();
        }

        public void PlayWrongAnswerSound()
        {
            StopAllMusic();
            wrongAnswerSound.Play();
        }

        public void PlayQuestionSound()
        {
            StopAllMusic();
            questionSound.Play();
        }

        #endregion

        #region Private Methods

        private void StopAllMusic()
        {
            correctAnswerSound.Stop();
            wrongAnswerSound.Stop();
            questionSound.Stop();
            gameOpenMusic.Stop();
        }

        #endregion
    }
}