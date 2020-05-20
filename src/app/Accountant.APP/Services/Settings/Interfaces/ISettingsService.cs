namespace Accountant.APP.Services.Settings.Interfaces
{
    public interface ISettingsService
    {
        string AuthToken { get; set; }
        int? UserId { get; set; }
        int? GroupId { get; set; }
    }
}
