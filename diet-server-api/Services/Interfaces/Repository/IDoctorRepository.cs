using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Admin;
using diet_server_api.DTO.Responses.Admin;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface IDoctorRepository
    {
        public Task<DoctorCreatorResponse> CreateDoctor(DoctorCreatorRequest request);
    }
}