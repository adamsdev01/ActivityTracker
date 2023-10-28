using BlazorActivityTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BlazorActivityTracker.Data.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        readonly ActivityTrackerContext _dbContext = new();

        public ActivityRepository(ActivityTrackerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Activity> AddActivityAsync(Activity activity)
        {
            await _dbContext.Activities.AddAsync(activity);
            _dbContext.SaveChanges();
            return activity;
        }

        public async Task DeleteActivityAsync(int Id)
        {
            var existingActivity = await _dbContext.Activities
                .FirstOrDefaultAsync(t => t.Id == Id);

            _dbContext.Activities.Remove(existingActivity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Activity> FindActivityByIdAsync(int Id)
        {
            return await _dbContext.Activities
                .FirstOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<IEnumerable<Activity>> GetAllActivitiesAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            IQueryable<Activity> query = _dbContext.Activities;

            if (startDate.HasValue)
            {
                // Filter Activities with a start date greater than or equal to the provided startDate.
                query = query.Where(activity => activity.ActivityDate >= startDate);
            }

            if (endDate.HasValue)
            {
                // Filter Activities with an end date less than or equal to the provided endDate.
                query = query.Where(activity => activity.ActivityDate <= endDate);
            }

            return await query.ToListAsync();
        }

        public async Task UpdateActivityAsync(Activity updatedActivity)
        {
            var existingActivity = await _dbContext.Activities
                .FirstOrDefaultAsync(a => a.Id == updatedActivity.Id);

            existingActivity.ActivityDate = updatedActivity.ActivityDate;
            existingActivity.TotalHours = updatedActivity.TotalHours;
            existingActivity.Description = updatedActivity.Description;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsActivityDateExists(DateTime activityDate, int Id)
        {
           
            var activityId = await _dbContext.Activities
            .FromSqlRaw("SELECT Id FROM Activity WHERE CONVERT(date, ActivityDate) = CONVERT(date, {0}) AND Id != {1}", activityDate, Id)
            .Select(a => a.Id)
            .FirstOrDefaultAsync();

            if(activityId != 0)
            {
                return activityId > 0;
            }
            else 
            {
                return false;
            }
        }
    }
}
