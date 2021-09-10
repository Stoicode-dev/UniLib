using System;
using System.Diagnostics;

namespace Stoicode.UniLib.Utilities
{
    /// <summary>
    /// Executes an action x times per second
    /// </summary>
    public class TickExecutor
    {
        private readonly float fixedDelta;
        private readonly Stopwatch stopwatch;
        private readonly Action action;
        
        private double accumulator;
        private long previousTime;


        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="framesPerSecond">Executions per second</param>
        /// <param name="target">Execution target</param>
        public TickExecutor(float framesPerSecond, Action target)
        {
            fixedDelta = 1f / framesPerSecond;

            stopwatch = new Stopwatch();
            action = target;
        }

        /// <summary>
        /// Start executor
        /// </summary>
        public void Start()
        {
            accumulator = 0.0;
            previousTime = 0;
            stopwatch.Restart();
        }

        /// <summary>
        /// Stop executor
        /// </summary>
        public void Stop()
        {
            stopwatch.Stop();
        }

        /// <summary>
        /// Update executor
        /// (call in a monobehaviour update)
        /// </summary>
        public void Update()
        {
            var elapsedTicks = stopwatch.ElapsedTicks;
            accumulator += (double) (elapsedTicks - previousTime) / Stopwatch.Frequency;
            previousTime = elapsedTicks;

            if (!(accumulator >= fixedDelta)) 
                return;
            
            action();
            accumulator -= fixedDelta;
        }
    }
}