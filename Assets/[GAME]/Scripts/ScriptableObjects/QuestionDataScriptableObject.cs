using _GAME_.Scripts.Enums;
using _GAME_.Scripts.Models;
using UnityEngine;

namespace _GAME_.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Question Data", menuName = "Orange Bear / Question Data", order = 1)]
    public class QuestionDataScriptableObject : ScriptableObject
    {
        public Difficulty difficulty;
        public QuestionData[] questions;
    }
}