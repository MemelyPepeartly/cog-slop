namespace CogSlop.Api.Models.Dtos;

public record CogSessionDto(
    int CogSessionId,
    DateTime CogInAtUtc,
    DateTime? CogOutAtUtc,
    int? DurationMinutes,
    bool IsOpen,
    string? CogInNote,
    string? CogOutNote);
