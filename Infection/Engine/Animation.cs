using System;
using OpenTK;
using Aiv.Fast2D;
using Infection;

namespace AIV_Engine
{
    class Animation
    {
        protected int numFrames;
        protected float frameDuration;
        protected bool isPlaying;
        protected int currentFrame;
        protected float elapsedTime;

        protected GameObject actor;

        public float SpeedModifier;


        public bool Loop;

        public bool CanBeInterrupted { get; protected set; }
        public bool IsPlaying { get => isPlaying; }
        public int CurrentFrame {  get => currentFrame; set { currentFrame = value; } }
        public Animation(GameObject actor, float fps, int frames, bool loop = false, bool canBeInterrupted = true)
        {
            this.numFrames = frames;

            this.frameDuration = 1 / fps;

            this.Loop = loop;
            this.actor = actor;
            
            this.CanBeInterrupted = canBeInterrupted;

            this.SpeedModifier = 1.0f;

            currentFrame = 0;
        }
        public virtual bool Update()
        {
            if(isPlaying)
            {
                elapsedTime += Game.DeltaTime;

                if(elapsedTime >= frameDuration / SpeedModifier)
                {
                    currentFrame++;
                    elapsedTime = 0.0f;

                    if(currentFrame >= numFrames)
                    {
                        if(Loop)
                        {
                            currentFrame = 0;
                            return false;
                        }
                        else
                        {
                            OnAnimationEnd();
                            return true;
                        }
                    }
                    actor.Offset = new Vector2(actor.FrameWidth * currentFrame, 0);
                }
            }
            return false;
        }

        protected virtual void OnAnimationEnd()
        {
            isPlaying = false;
        }

        public virtual void Play()
        {
            isPlaying = true;
        }

        protected virtual void Pause()
        {
            isPlaying = false;
        }

        public virtual void Stop()
        {
            isPlaying = false;
            currentFrame = 0;
            elapsedTime = 0.0f;
            actor.Offset = Vector2.Zero;
        }
    }
}
