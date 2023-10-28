using BlazorActivityTracker.Data.Models;

namespace BlazorActivityTracker.Data.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> AddActivityAsync(Activity activity);
        Task DeleteActivityAsync(int Id);
        Task<Activity> FindActivityByIdAsync(int Id);
        Task<IEnumerable<Activity>> GetAllActivitiesAsync(DateTime? startDate = null, DateTime? endDate = null);
        Task UpdateActivityAsync(Activity activity);
        Task<bool> IsActivityDateExists(DateTime activityDate, int Id);
    }
}
