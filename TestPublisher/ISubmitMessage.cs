using System.Threading.Tasks;

namespace TestPublisher
{
    public interface ISubmitMessage<T>
    {
        Task Submit(T message);
    }
}
