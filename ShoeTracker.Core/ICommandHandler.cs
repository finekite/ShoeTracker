
namespace ShoeTracker.Core
{
    public interface ICommandHandler<in TParameter> where TParameter: ICommand
    {
        int HandleCommand(TParameter parameter);
    }
}
