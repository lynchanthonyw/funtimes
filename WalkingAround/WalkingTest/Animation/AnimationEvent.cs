namespace WalkingTest.Animation
{
    public interface IAnimationEvent
    {
        void PreEvent();

        void EventStart();

        void EventEnd();

        void PostEvent();
    }
}