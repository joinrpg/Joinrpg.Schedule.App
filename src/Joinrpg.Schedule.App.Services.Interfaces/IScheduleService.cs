namespace Joinrpg.Schedule.App.Services.Interfaces
{
    public interface IScheduleService
    {
        IProjectScheduleService GetProjectScheduleService(int projectId);
    }
}