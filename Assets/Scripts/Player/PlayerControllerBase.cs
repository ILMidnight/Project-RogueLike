public class PlayerControllerBase : IPlayerState
{
    protected PlayerManager pMng;

    public PlayerControllerBase(PlayerManager pMng)
    {
        this.pMng = pMng;
    }

    public virtual void Tick()
    {
        
    }
}