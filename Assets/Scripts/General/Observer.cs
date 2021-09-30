namespace General
{
    public abstract class Observer
    {
        public abstract void OnNotify(object value, NotificationType notificationType);
    }
}