using System;
using System.Collections;
using DarkenDinosaur.Data;
using UnityEngine;
using UnityEngine.Events;

namespace DarkenDinosaur.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private int addScorePerIteration;
        [SerializeField] private float iterationDelta;
        [SerializeField] private float minIterationDelta;
        [SerializeField] private float maxIterationDelta;
        [SerializeField] private float lessDeltaPerSecond;
        [SerializeField] private float nonLessDeltaTime;
        [Space] 
        [SerializeField] private int distance;
        [SerializeField] private int score;
        [SerializeField] private int highScoreCount;
        [SerializeField] private int balance;

        private int prevAwardedDistance = 0; 

        [SerializeField] private UnityEvent<int> scoreChanged;
        [SerializeField] private UnityEvent<int> distanceChanged;
        [SerializeField] private UnityEvent<int> highScoreChanged;
        [SerializeField] private UnityEvent<int> balanceChanged;

        private IEnumerator ScoreCounter()
        {
            while (true)
            {
                this.distance += this.addScorePerIteration;
                if (this.distance - this.prevAwardedDistance >= 50)
                {
                    this.prevAwardedDistance += 50;
                    this.score++;
                    this.scoreChanged?.Invoke(this.score);
                }
                this.distanceChanged?.Invoke(this.distance);

                if (this.score > this.highScoreCount)
                {
                    this.highScoreCount = this.score;
                    this.highScoreChanged?.Invoke(this.highScoreCount);
                }

                yield return new WaitForSeconds(this.iterationDelta);
            }
        }

        public void AddScore(int score)
        {
            this.score += score;
            this.scoreChanged?.Invoke(this.score);
        }

        private IEnumerator IterationDeltaCounter()
        {
            yield return new WaitForSeconds(this.nonLessDeltaTime);
            while (true)
            {
                this.iterationDelta -= this.lessDeltaPerSecond / 10;
                this.iterationDelta = Mathf.Clamp(this.iterationDelta, this.minIterationDelta, this.maxIterationDelta);

                yield return new WaitForSeconds(0.1f);
            }
        }
        
        /// <summary>
        /// Game start event handler.
        /// </summary>
        public void OnGameStart()
        {
            StartCoroutine(ScoreCounter());
            StartCoroutine(IterationDeltaCounter());
        }
        
        /// <summary>
        /// Player dead event handler,
        /// </summary>
        public void OnPlayerDead()
        {
            StopAllCoroutines();
        }
        
        /// <summary>
        /// Data loaded event handler.
        /// </summary>
        /// <param name="data">Game data</param>
        public void OnDataLoaded(GameData data)
        {
            this.highScoreCount = data.highScoreCount;
            this.balance = data.balance;
        }

        public int GetBalance()
        {
            return this.balance;
        }

        public void ChangeBalance(int value)
        { 
            Debug.Log($"Change balance: {value}");
            this.balance -= value;
            this.balanceChanged?.Invoke(this.balance);
        }
    }
}
