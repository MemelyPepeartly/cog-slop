namespace CogSlop.Api.Models.Entities;

public class CogRuntimeSetting
{
    public int CogRuntimeSettingId { get; set; }

    public int WarningIntervalMinutes { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    public int? UpdatedByUserAccountId { get; set; }

    public UserAccount? UpdatedByUserAccount { get; set; }
}
