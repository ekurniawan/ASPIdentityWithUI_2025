using MyIdentityWeb.Models;

namespace MyIdentityWeb.Repositories
{
    public interface IConferenceRepository
    {
        Task<int> Add(ConferenceModel model);
        Task<IEnumerable<ConferenceModel>> GetAll();
        Task<ConferenceModel> GetById(int id);
    }
}
