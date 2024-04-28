public interface ICharacterAction
{
    public abstract void Action();

    public abstract void ConnectWithObject(MemoryObject connectedObject);

    public abstract void SelfDelete();
}
