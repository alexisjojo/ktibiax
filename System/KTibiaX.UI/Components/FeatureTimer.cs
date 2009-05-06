using System;
using System.ComponentModel;
using KTibiaX.UI.Util;
using System.Threading;

namespace KTibiaX.UI.Components {
    public class FeatureTimer : Component {

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureTimer"/> class.
        /// </summary>
        public FeatureTimer() {
        }

        #region "[rgn] Properties "
        private Timer Timer { get; set; }
        public Callback TimerCallBack { get; set; }
        public State State { get; private set; }

        public int Interval { get; set; }
        public bool IsActionRunning { get; set; }
        #endregion

        #region "[rgn] Events     "
        public event EventHandler OnStart;
        public event EventHandler OnStop;
        public event EventHandler OnActionComplete;
        #endregion

        /// <summary>
        /// Starts the specified timer call back.
        /// </summary>
        /// <param name="timerCallBack">The timer call back.</param>
        public void Start(Callback timerCallBack) {
            TimerCallBack = timerCallBack;
            Start();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start() {
            if (State == State.Stoped) {
                if (Interval == 0) { Interval = 500; }
                State = State.Started;
                Timer = new Timer(Timer_Tick, Timer, 1000, Interval);
                if (OnStart != null) { OnStart(this, EventArgs.Empty); }
            }
        }
        
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop() {
            if (State == State.Started) {
                State = State.Stoped;
                Timer.Change(Timeout.Infinite, Timeout.Infinite);
                if (OnStop != null) { OnStop(this, EventArgs.Empty); }
            }
        }

        /// <summary>
        /// Timer_s the tick.
        /// </summary>
        /// <param name="args">The args.</param>
        private void Timer_Tick(object args) {
            if (!IsActionRunning) {
                if (TimerCallBack != null) {
                    IsActionRunning = true;
                    TimerCallBack.BeginInvoke(Timer_ActionComplete, Timer);
                }
            }
        }

        /// <summary>
        /// Timer_s the action complete.
        /// </summary>
        /// <param name="result">The result.</param>
        private void Timer_ActionComplete(IAsyncResult result) {
            IsActionRunning = false;
            if (OnActionComplete != null) { OnActionComplete(this, EventArgs.Empty); }
        }
    }
}
