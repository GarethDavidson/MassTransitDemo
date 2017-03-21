using System.Threading.Tasks;

namespace TestSubscriber
{
    public interface ISubmitMessage<T>
    {
        Task Submit(T message);
    }
}
