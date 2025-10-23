public class PlayerControllerBase : IPlayerState
{
    public PlayerManager pMng;

    public PlayerControllerBase(PlayerManager pMng)
    {
        this.pMng = pMng;
    }

    public virtual void InitController()
    {
    }

    public virtual void Tick()
    {
        
    }
}