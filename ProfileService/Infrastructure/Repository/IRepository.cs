using ProfileService.Models;

namespace ProfileService.Infrastructure.Repository;

public interface IRepository<T1,T2 >
{
    Task<T2> GetProjects(int id);

    Task<T2> CreateProject(T1 data);
    Task UpdateProject(T1 data, int id);
    Task DeleteProject(int id);
}
